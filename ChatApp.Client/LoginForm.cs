
using ChatMessage = ChatApp.Shared.Models.Message;

namespace ChatApp.Client;

public partial class LoginForm : Form
{
    public string? Username { get; private set; }
    public TcpChatClient? Client { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
        // Allow dragging the form since we removed the border
        this.MouseDown += LoginForm_MouseDown;
        panelHeader.MouseDown += LoginForm_MouseDown;
    }

    // For dragging the borderless form
    private bool dragging = false;
    private Point dragCursorPoint;
    private Point dragFormPoint;

    private void LoginForm_MouseDown(object? sender, MouseEventArgs e)
    {
        dragging = true;
        dragCursorPoint = Cursor.Position;
        dragFormPoint = this.Location;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (dragging)
        {
            Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            this.Location = Point.Add(dragFormPoint, new Size(dif));
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        dragging = false;
    }

    // Button hover effects
    private void Btn_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 100, 150);
        }
    }

    private void Btn_MouseLeave(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 136, 204);
        }
    }

    private void BtnRegister_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 136, 204);
            btn.ForeColor = Color.White;
        }
    }

    private void BtnRegister_MouseLeave(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(0, 136, 204);
        }
    }

    private async void BtnLogin_Click(object? sender, EventArgs e)
    {
        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;
        var serverIp = txtServerIp.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(serverIp))
        {
            MessageBox.Show("Please fill in all fields");
            return;
        }

        try
        {
            Client = new TcpChatClient(serverIp, 8888);
            await Client.ConnectAsync();

            var loginMessage = new ChatMessage
            {
                SenderUsername = username,
                Content = $"LOGIN:{username}:{password}",
                IsPrivate = true
            };

            Client.OnMessageReceived += (msg) =>
            {
                if (msg.Content == "LOGIN_SUCCESS")
                {
                    Username = username;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (msg.Content == "LOGIN_FAILED")
                {
                    MessageBox.Show("Login failed! Check username/password");
                    Client.Disconnect();
                }
            };

            await Client.SendMessageAsync(loginMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error connecting: {ex.Message}");
        }
    }

    private async void BtnRegister_Click(object? sender, EventArgs e)
    {
        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;
        var serverIp = txtServerIp.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(serverIp))
        {
            MessageBox.Show("Please fill in all fields");
            return;
        }

        try
        {
            var tempClient = new TcpChatClient(serverIp, 8888);
            await tempClient.ConnectAsync();

            var registerMessage = new ChatMessage
            {
                SenderUsername = username,
                Content = $"REGISTER:{username}:{password}",
                IsPrivate = true
            };

            tempClient.OnMessageReceived += (msg) =>
            {
                if (msg.Content == "REGISTER_SUCCESS")
                {
                    MessageBox.Show("Registration successful! Now you can login.");
                }
                else if (msg.Content == "REGISTER_FAILED")
                {
                    MessageBox.Show("Registration failed! Username already exists.");
                }
                tempClient.Disconnect();
            };

            await tempClient.SendMessageAsync(registerMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error connecting: {ex.Message}");
        }
    }
}

