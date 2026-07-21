
using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatApp.Shared.Models;
using ChatApp.Shared.Protocols;
using ChatApp.Shared.Data;
using BCrypt.Net;

namespace ChatApp.Server;

public class TcpChatServer
{
    private TcpListener _listener;
    private Dictionary<string, TcpClient> _onlineClients = new();
    private readonly object _lock = new();
    private ChatDbContext _dbContext = new();

    public TcpChatServer(string ipAddress, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
    }

    public async Task StartAsync()
    {
        _listener.Start();
        Console.WriteLine($"Server started on {_listener.LocalEndpoint}...");
        Console.WriteLine("Waiting for clients...");

        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        var stream = client.GetStream();
        var lengthBuffer = new byte[4];
        string? username = null;

        try
        {
            while (true)
            {
                // Read message length (4 bytes)
                int bytesRead = await stream.ReadAsync(lengthBuffer, 0, 4);
                if (bytesRead == 0) break; // Client disconnected

                // Convert length to int (little-endian)
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(lengthBuffer);
                }
                int messageLength = BitConverter.ToInt32(lengthBuffer, 0);

                // Read message content
                var messageBuffer = new byte[messageLength];
                int totalBytesRead = 0;
                while (totalBytesRead < messageLength)
                {
                    bytesRead = await stream.ReadAsync(messageBuffer, totalBytesRead, messageLength - totalBytesRead);
                    if (bytesRead == 0) break; // Client disconnected
                    totalBytesRead += bytesRead;
                }
                if (totalBytesRead < messageLength) break; // Incomplete message, client disconnected

                var message = MessageProtocol.Deserialize<Message>(messageBuffer);
                if (message == null) continue;

                Console.WriteLine($"Received: {message.Content} from {message.SenderUsername}");

                if (message.Content.StartsWith("REGISTER:"))
                {
                    var parts = message.Content.Split(':');
                    if (parts.Length >= 3)
                    {
                        var regUsername = parts[1];
                        var regPassword = parts[2];
                        var passwordHash = BCrypt.Net.BCrypt.HashPassword(regPassword);

                        var success = _dbContext.RegisterUser(regUsername, passwordHash);
                        var response = new Message
                        {
                            SenderUsername = "SERVER",
                            Content = success ? "REGISTER_SUCCESS" : "REGISTER_FAILED",
                            IsPrivate = true,
                            ReceiverUsername = message.SenderUsername
                        };
                        await SendToClientAsync(client, response);
                    }
                }
                else if (message.Content.StartsWith("LOGIN:"))
                {
                    var parts = message.Content.Split(':');
                    if (parts.Length >= 3)
                    {
                        var loginUsername = parts[1];
                        var loginPassword = parts[2];
                        var storedHash = _dbContext.GetUserPasswordHash(loginUsername);

                        bool loginSuccess = storedHash != null && BCrypt.Net.BCrypt.Verify(loginPassword, storedHash);
                        if (loginSuccess)
                        {
                            lock (_lock)
                            {
                                _onlineClients[loginUsername] = client;
                            }
                            username = loginUsername;

                            var response = new Message
                            {
                                SenderUsername = "SERVER",
                                Content = "LOGIN_SUCCESS",
                                IsPrivate = true,
                                ReceiverUsername = loginUsername
                            };
                            await SendToClientAsync(client, response);
                            
                            // Send current users list directly to the new client first
                            List<string> users;
                            lock (_lock)
                            {
                                users = _onlineClients.Keys.ToList();
                            }
                            var usersMsg = new Message
                            {
                                SenderUsername = "SERVER",
                                Content = "USERS:" + string.Join(",", users),
                                IsPrivate = true
                            };
                            await SendToClientAsync(client, usersMsg);
                            
                            // Now broadcast to everyone else
                            await BroadcastOnlineUsersAsync();

                            // Send public chat history
                            var history = _dbContext.GetMessageHistory(isPrivate: false);
                            foreach (var msg in history)
                            {
                                await SendToClientAsync(client, msg);
                            }
                        }
                        else
                        {
                            var response = new Message
                            {
                                SenderUsername = "SERVER",
                                Content = "LOGIN_FAILED",
                                IsPrivate = true,
                                ReceiverUsername = message.SenderUsername
                            };
                            await SendToClientAsync(client, response);
                        }
                    }
                }
                else
                {
                    _dbContext.SaveMessage(message);
                    if (message.IsPrivate && !string.IsNullOrEmpty(message.ReceiverUsername))
                    {
                        await SendPrivateMessageAsync(message);
                    }
                    else
                    {
                        await BroadcastMessageAsync(message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            if (username != null)
            {
                lock (_lock)
                {
                    _onlineClients.Remove(username);
                }
                await BroadcastOnlineUsersAsync();
            }
            client.Close();
        }
    }

    private async Task SendToClientAsync(TcpClient client, Message message)
    {
        var data = MessageProtocol.Serialize(message);
        await client.GetStream().WriteAsync(data);
    }

    private async Task BroadcastMessageAsync(Message message)
    {
        var data = MessageProtocol.Serialize(message);
        List<TcpClient> clients;
        lock (_lock)
        {
            clients = _onlineClients.Values.ToList();
        }
        foreach (var client in clients)
        {
            try
            {
                await client.GetStream().WriteAsync(data);
            }
            catch { }
        }
    }

    private async Task SendPrivateMessageAsync(Message message)
    {
        TcpClient? receiverClient = null;
        lock (_lock)
        {
            if (_onlineClients.TryGetValue(message.ReceiverUsername!, out var client))
            {
                receiverClient = client;
            }
        }
        if (receiverClient != null)
        {
            await SendToClientAsync(receiverClient, message);
        }
        // Also send back to sender
        TcpClient? senderClient = null;
        lock (_lock)
        {
            if (_onlineClients.TryGetValue(message.SenderUsername, out var sClient))
            {
                senderClient = sClient;
            }
        }
        if (senderClient != null && senderClient != receiverClient)
        {
            await SendToClientAsync(senderClient, message);
        }
    }

    private async Task BroadcastOnlineUsersAsync()
    {
        List<string> users;
        lock (_lock)
        {
            users = _onlineClients.Keys.ToList();
        }
        var message = new Message
        {
            SenderUsername = "SERVER",
            Content = "USERS:" + string.Join(",", users),
            IsPrivate = true
        };
        await BroadcastMessageAsync(message);
    }
}

