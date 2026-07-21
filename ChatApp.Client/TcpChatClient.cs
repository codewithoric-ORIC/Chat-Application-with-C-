
using System.Net.Sockets;
using ChatMessage = ChatApp.Shared.Models.Message;
using ChatApp.Shared.Protocols;

namespace ChatApp.Client;

public class TcpChatClient
{
    private TcpClient _client;
    private NetworkStream? _stream;
    private readonly string _serverIp;
    private readonly int _port;
    private bool _isRunning;
    private List<string> _lastOnlineUsers = new();

    private event Action<ChatMessage>? _onMessageReceived;
    private event Action<List<string>>? _onOnlineUsersUpdated;

    public event Action<ChatMessage>? OnMessageReceived
    {
        add
        {
            _onMessageReceived += value;
        }
        remove
        {
            _onMessageReceived -= value;
        }
    }
    public event Action<List<string>>? OnOnlineUsersUpdated
    {
        add
        {
            _onOnlineUsersUpdated += value;
            // When a new handler is added, send the last known users list immediately
            if (_lastOnlineUsers != null)
            {
                value?.Invoke(_lastOnlineUsers);
            }
        }
        remove
        {
            _onOnlineUsersUpdated -= value;
        }
    }
    public event Action? OnConnected;
    public event Action? OnDisconnected;

    public TcpChatClient(string serverIp, int port)
    {
        _serverIp = serverIp;
        _port = port;
        _client = new TcpClient();
    }

    public async Task ConnectAsync()
    {
        await _client.ConnectAsync(_serverIp, _port);
        _stream = _client.GetStream();
        _isRunning = true;
        OnConnected?.Invoke();
        _ = ReceiveMessagesAsync();
    }

    public async Task SendMessageAsync(ChatMessage message)
    {
        if (_stream == null) return;
        var data = MessageProtocol.Serialize(message);
        await _stream.WriteAsync(data);
    }

    private async Task ReceiveMessagesAsync()
    {
        var lengthBuffer = new byte[4];
        while (_isRunning)
        {
            try
            {
                // Read message length (4 bytes)
                int bytesRead = await _stream.ReadAsync(lengthBuffer, 0, 4);
                if (bytesRead == 0) break; // Server disconnected

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
                    bytesRead = await _stream.ReadAsync(messageBuffer, totalBytesRead, messageLength - totalBytesRead);
                    if (bytesRead == 0) break; // Server disconnected
                    totalBytesRead += bytesRead;
                }
                if (totalBytesRead < messageLength) break; // Incomplete message, server disconnected

                var message = MessageProtocol.Deserialize<ChatMessage>(messageBuffer);
                if (message == null) continue;

                if (message.Content.StartsWith("USERS:"))
                {
                    var users = message.Content.Substring(6).Split(',').ToList();
                    _lastOnlineUsers = users;
                    _onOnlineUsersUpdated?.Invoke(users);
                }
                else
                {
                    _onMessageReceived?.Invoke(message);
                }
            }
            catch
            {
                break;
            }
        }
        _isRunning = false;
        OnDisconnected?.Invoke();
    }

    public void Disconnect()
    {
        _isRunning = false;
        _client.Close();
    }
}

