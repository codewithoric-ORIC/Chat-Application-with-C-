
using ChatMessage = ChatApp.Shared.Models.Message;
using System.Drawing.Drawing2D;

namespace ChatApp.Client;

public partial class Form1 : Form
{
    private TcpChatClient? _client;
    private string? _currentUsername;
    private string? _selectedUser;
    private bool _isPublicChat = true;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        using var loginForm = new LoginForm();
        if (loginForm.ShowDialog() != DialogResult.OK)
        {
            Application.Exit();
            return;
        }

        _client = loginForm.Client;
        _currentUsername = loginForm.Username;
        this.Text = $"Chat Application - {_currentUsername}";

        _client.OnMessageReceived += HandleMessageReceived;
        _client.OnOnlineUsersUpdated += HandleOnlineUsersUpdated;
    }

    private void HandleMessageReceived(ChatMessage message)
    {
        this.Invoke((MethodInvoker)delegate
        {
            AddMessageToChat(message);
        });
    }

    private void HandleOnlineUsersUpdated(List<string> users)
    {
        this.Invoke((MethodInvoker)delegate
        {
            lstOnlineUsers.Items.Clear();
            lstOnlineUsers.Items.Add("Public Chat");
            foreach (var user in users.Where(u => u != _currentUsername))
            {
                lstOnlineUsers.Items.Add(user);
            }
        });
    }

    // Helper to draw rounded rectangle path
    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        var arcSize = radius * 2;
        path.AddArc(rect.X, rect.Y, arcSize, arcSize, 180, 90);
        path.AddArc(rect.X + rect.Width - arcSize, rect.Y, arcSize, arcSize, 270, 90);
        path.AddArc(rect.X + rect.Width - arcSize, rect.Y + rect.Height - arcSize, arcSize, arcSize, 0, 90);
        path.AddArc(rect.X, rect.Y + rect.Height - arcSize, arcSize, arcSize, 90, 90);
        path.CloseAllFigures();
        return path;
    }

    private void AddMessageToChat(ChatMessage message)
    {
        var isOwnMessage = message.SenderUsername == _currentUsername;
        var availableWidth = flowChat.ClientSize.Width - 40; // Account for margins/padding
        var maxBubbleWidth = Math.Min(500, availableWidth - 20);

        // Create content labels first to measure size
        var lblSender = new Label
        {
            Text = message.SenderUsername,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = isOwnMessage ? Color.White : Color.FromArgb(64, 64, 64),
            BackColor = Color.Transparent,
            Location = new Point(16, 10),
            AutoSize = true
        };

        var lblContent = new Label
        {
            Text = message.Content,
            Font = new Font("Segoe UI", 11F),
            ForeColor = isOwnMessage ? Color.White : Color.FromArgb(33, 33, 33),
            BackColor = Color.Transparent,
            Location = new Point(16, 32),
            MaximumSize = new Size(maxBubbleWidth - 32, 0),
            AutoSize = true
        };

        var lblTime = new Label
        {
            Text = message.Timestamp.ToString("HH:mm"),
            Font = new Font("Segoe UI", 8F),
            ForeColor = isOwnMessage ? Color.FromArgb(230, 230, 230) : Color.Gray,
            BackColor = Color.Transparent,
            Location = new Point(16, 32 + lblContent.Height + 6),
            AutoSize = true
        };

        // Calculate bubble size
        int contentWidth = Math.Max(lblContent.Width, lblSender.Width + lblTime.Width + 30) + 32;
        int bubbleWidth = Math.Min(maxBubbleWidth, contentWidth);
        int bubbleHeight = 32 + lblContent.Height + 6 + lblTime.Height + 12;

        // Create main holder panel
        var mainPanel = new Panel
        {
            Width = availableWidth,
            Height = bubbleHeight + 16,
            Margin = new Padding(8, 4, 8, 4),
            BackColor = Color.Transparent
        };

        var bubblePanel = new Panel
        {
            Width = bubbleWidth,
            Height = bubbleHeight,
            BackColor = Color.Transparent,
            Padding = new Padding(0),
            Location = isOwnMessage 
                ? new Point(mainPanel.Width - bubbleWidth - 8, 0) 
                : new Point(8, 0)
        };

        // Add custom paint event for rounded corners and shadow
        bubblePanel.Paint += (sender, e) =>
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            var rect = new Rectangle(0, 0, bubblePanel.Width - 1, bubblePanel.Height - 1);
            var path = GetRoundedPath(rect, 12);

            // Draw shadow for non-own messages
            if (!isOwnMessage)
            {
                using var shadowBrush = new SolidBrush(Color.FromArgb(20, Color.Gray));
                for (int i = 0; i < 3; i++)
                {
                    g.FillPath(shadowBrush, GetRoundedPath(new Rectangle(2 + i, 2 + i, rect.Width - i, rect.Height - i), 12));
                }
            }

            // Fill bubble color
            var bubbleColor = isOwnMessage ? Color.FromArgb(0, 150, 220) : Color.White;
            using var bubbleBrush = new SolidBrush(bubbleColor);
            g.FillPath(bubbleBrush, path);

            // Draw border for non-own messages
            if (!isOwnMessage)
            {
                using var borderPen = new Pen(Color.FromArgb(220, 220, 220), 1);
                g.DrawPath(borderPen, path);
            }
        };

        // Add controls to bubble
        bubblePanel.Controls.Add(lblSender);
        bubblePanel.Controls.Add(lblContent);
        bubblePanel.Controls.Add(lblTime);

        mainPanel.Controls.Add(bubblePanel);

        flowChat.Controls.Add(mainPanel);
        flowChat.ScrollControlIntoView(mainPanel);
    }

    private void BtnSend_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 100, 150);
        }
    }

    private void BtnSend_MouseLeave(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 136, 204);
        }
    }

    private async void BtnSend_Click(object? sender, EventArgs e)
    {
        if (_client == null || string.IsNullOrWhiteSpace(txtMessage.Text)) return;

        var message = new ChatMessage
        {
            SenderUsername = _currentUsername!,
            Content = txtMessage.Text.Trim(),
            Timestamp = DateTime.Now,
            IsPrivate = !_isPublicChat,
            ReceiverUsername = _isPublicChat ? null : _selectedUser
        };

        await _client.SendMessageAsync(message);
        txtMessage.Clear();
        txtMessage.Focus();
    }

    private void TxtMessage_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && !e.Shift)
        {
            e.SuppressKeyPress = true;
            BtnSend_Click(sender, e);
        }
    }

    private void LstOnlineUsers_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstOnlineUsers.SelectedItem == null) return;

        var selected = lstOnlineUsers.SelectedItem.ToString();
        if (selected == "Public Chat")
        {
            _isPublicChat = true;
            _selectedUser = null;
            lblChat.Text = "Public Chat";
        }
        else
        {
            _isPublicChat = false;
            _selectedUser = selected;
            lblChat.Text = $"Private Chat with {selected}";
        }
    }

    // Helper to get a consistent color for a username
    private Color GetAvatarColor(string username)
    {
        int hash = username.GetHashCode();
        var r = (byte)((hash & 0xFF0000) >> 16);
        var g = (byte)((hash & 0x00FF00) >> 8);
        var b = (byte)(hash & 0x0000FF);
        // Make sure the color is not too light
        r = (byte)(r % 180 + 40);
        g = (byte)(g % 180 + 40);
        b = (byte)(b % 180 + 40);
        return Color.FromArgb(r, g, b);
    }

    private void LstOnlineUsers_MeasureItem(object? sender, MeasureItemEventArgs e)
    {
        e.ItemHeight = 45;
    }

    private void LstOnlineUsers_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= lstOnlineUsers.Items.Count) return;

        var itemText = lstOnlineUsers.Items[e.Index].ToString() ?? string.Empty;
        e.DrawBackground();

        if (itemText == "Public Chat")
        {
            // Draw Public Chat item with icon
            var iconBounds = new Rectangle(e.Bounds.X + 10, e.Bounds.Y + 5, 35, 35);
            using var brush = new SolidBrush(Color.FromArgb(0, 136, 204));
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(brush, iconBounds);
            
            // Draw a simple public icon (two people)
            using var textBrush = new SolidBrush(Color.White);
            using var font = new Font("Segoe UI", 14, FontStyle.Bold);
            var textSize = e.Graphics.MeasureString("💬", font);
            e.Graphics.DrawString("💬", font, textBrush, 
                iconBounds.X + (iconBounds.Width - textSize.Width)/2f, 
                iconBounds.Y + (iconBounds.Height - textSize.Height)/2f);

            // Draw "Public Chat" text
            using var textBrush2 = new SolidBrush(Color.FromArgb(64, 64, 64));
            var textFont = new Font("Segoe UI", 11, FontStyle.Regular);
            var textPoint = new Point(e.Bounds.X + 55, e.Bounds.Y + 10);
            e.Graphics.DrawString(itemText, textFont, textBrush2, textPoint);
        }
        else
        {
            // Draw user item with avatar (first letter)
            var firstLetter = string.IsNullOrEmpty(itemText) ? "?" : itemText[0].ToString().ToUpper();
            var avatarColor = GetAvatarColor(itemText);
            var avatarBounds = new Rectangle(e.Bounds.X + 10, e.Bounds.Y + 5, 35, 35);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            using var brush = new SolidBrush(avatarColor);
            e.Graphics.FillEllipse(brush, avatarBounds);
            
            // Draw first letter in avatar
            using var textBrush = new SolidBrush(Color.White);
            var font = new Font("Segoe UI", 14, FontStyle.Bold);
            var letterSize = e.Graphics.MeasureString(firstLetter, font);
            e.Graphics.DrawString(firstLetter, font, textBrush,
                avatarBounds.X + (avatarBounds.Width - letterSize.Width)/2f,
                avatarBounds.Y + (avatarBounds.Height - letterSize.Height)/2f);
            
            // Draw username
            using var nameBrush = new SolidBrush(Color.FromArgb(64, 64, 64));
            var nameFont = new Font("Segoe UI", 11, FontStyle.Regular);
            var namePoint = new Point(e.Bounds.X + 55, e.Bounds.Y + 10);
            e.Graphics.DrawString(itemText, nameFont, nameBrush, namePoint);
        }

        e.DrawFocusRectangle();
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        _client?.Disconnect();
    }
}

