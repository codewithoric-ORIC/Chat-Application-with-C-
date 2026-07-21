using ChatApp.Server;

// Use "0.0.0.0" to listen on all network interfaces (so other devices can connect)
var server = new TcpChatServer("0.0.0.0", 8888);
await server.StartAsync();
