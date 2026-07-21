
namespace ChatApp.Shared.Models;

public class Message
{
    public string SenderUsername { get; set; } = string.Empty;
    public string? ReceiverUsername { get; set; } // Null = Public Chat
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public bool IsPrivate { get; set; }
}
