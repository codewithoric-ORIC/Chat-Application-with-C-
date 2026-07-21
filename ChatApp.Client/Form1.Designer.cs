
namespace ChatApp.Client;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.splitContainer1 = new SplitContainer();
        this.lstOnlineUsers = new ListBox();
        this.lblUsers = new Label();
        this.panelSidebar = new Panel();
        this.flowChat = new FlowLayoutPanel();
        this.panelInput = new Panel();
        this.btnSend = new Button();
        this.txtMessage = new TextBox();
        this.lblChat = new Label();
        this.panelChatHeader = new Panel();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.panelSidebar.SuspendLayout();
        this.panelInput.SuspendLayout();
        this.panelChatHeader.SuspendLayout();
        this.SuspendLayout();
        // 
        // splitContainer1
        // 
        this.splitContainer1.Dock = DockStyle.Fill;
        this.splitContainer1.Location = new Point(0, 0);
        this.splitContainer1.Name = "splitContainer1";
        this.splitContainer1.Size = new Size(1100, 700);
        this.splitContainer1.SplitterDistance = 280;
        this.splitContainer1.TabIndex = 0;
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.panelSidebar);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.flowChat);
        this.splitContainer1.Panel2.Controls.Add(this.panelInput);
        this.splitContainer1.Panel2.Controls.Add(this.panelChatHeader);
        // 
        // lstOnlineUsers
        // 
        this.lstOnlineUsers.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
        this.lstOnlineUsers.BorderStyle = BorderStyle.None;
        this.lstOnlineUsers.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        this.lstOnlineUsers.DrawMode = DrawMode.OwnerDrawVariable;
        this.lstOnlineUsers.ItemHeight = 45;
        this.lstOnlineUsers.Location = new Point(0, 60);
        this.lstOnlineUsers.Name = "lstOnlineUsers";
        this.lstOnlineUsers.Size = new Size(280, 640);
        this.lstOnlineUsers.TabIndex = 1;
        this.lstOnlineUsers.DrawItem += new DrawItemEventHandler(this.LstOnlineUsers_DrawItem);
        this.lstOnlineUsers.MeasureItem += new MeasureItemEventHandler(this.LstOnlineUsers_MeasureItem);
        this.lstOnlineUsers.SelectedIndexChanged += new EventHandler(this.LstOnlineUsers_SelectedIndexChanged);
        // 
        // lblUsers
        // 
        this.lblUsers.AutoSize = true;
        this.lblUsers.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        this.lblUsers.ForeColor = Color.FromArgb(0, 136, 204);
        this.lblUsers.Location = new Point(20, 15);
        this.lblUsers.Name = "lblUsers";
        this.lblUsers.Size = new Size(125, 25);
        this.lblUsers.TabIndex = 0;
        this.lblUsers.Text = "Online Users";
        // 
        // panelSidebar
        // 
        this.panelSidebar.BackColor = Color.White;
        this.panelSidebar.Controls.Add(this.lblUsers);
        this.panelSidebar.Controls.Add(this.lstOnlineUsers);
        this.panelSidebar.Dock = DockStyle.Fill;
        this.panelSidebar.Location = new Point(0, 0);
        this.panelSidebar.Name = "panelSidebar";
        this.panelSidebar.Size = new Size(280, 700);
        this.panelSidebar.TabIndex = 2;
        // 
        // flowChat
        // 
        this.flowChat.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
        this.flowChat.AutoScroll = true;
        this.flowChat.BackColor = Color.FromArgb(240, 242, 245);
        this.flowChat.FlowDirection = FlowDirection.TopDown;
        this.flowChat.Location = new Point(0, 60);
        this.flowChat.Name = "flowChat";
        this.flowChat.Size = new Size(816, 560);
        this.flowChat.TabIndex = 2;
        this.flowChat.WrapContents = false;
        // 
        // panelInput
        // 
        this.panelInput.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left) 
            | AnchorStyles.Right)));
        this.panelInput.BackColor = Color.White;
        this.panelInput.Controls.Add(this.btnSend);
        this.panelInput.Controls.Add(this.txtMessage);
        this.panelInput.Location = new Point(0, 620);
        this.panelInput.Name = "panelInput";
        this.panelInput.Size = new Size(816, 80);
        this.panelInput.TabIndex = 1;
        // 
        // btnSend
        // 
        this.btnSend.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        this.btnSend.BackColor = Color.FromArgb(0, 136, 204);
        this.btnSend.FlatAppearance.BorderSize = 0;
        this.btnSend.FlatStyle = FlatStyle.Flat;
        this.btnSend.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnSend.ForeColor = Color.White;
        this.btnSend.Location = new Point(696, 15);
        this.btnSend.Name = "btnSend";
        this.btnSend.Size = new Size(100, 50);
        this.btnSend.TabIndex = 1;
        this.btnSend.Text = "Send";
        this.btnSend.UseVisualStyleBackColor = false;
        this.btnSend.Click += new EventHandler(this.BtnSend_Click);
        this.btnSend.MouseEnter += new EventHandler(this.BtnSend_MouseEnter);
        this.btnSend.MouseLeave += new EventHandler(this.BtnSend_MouseLeave);
        // 
        // txtMessage
        // 
        this.txtMessage.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
            | AnchorStyles.Right)));
        this.txtMessage.BorderStyle = BorderStyle.FixedSingle;
        this.txtMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtMessage.Location = new Point(20, 15);
        this.txtMessage.Multiline = true;
        this.txtMessage.Name = "txtMessage";
        this.txtMessage.Size = new Size(660, 50);
        this.txtMessage.TabIndex = 0;
        this.txtMessage.KeyDown += new KeyEventHandler(this.TxtMessage_KeyDown);
        // 
        // lblChat
        // 
        this.lblChat.AutoSize = true;
        this.lblChat.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        this.lblChat.ForeColor = Color.FromArgb(64, 64, 64);
        this.lblChat.Location = new Point(20, 15);
        this.lblChat.Name = "lblChat";
        this.lblChat.Size = new Size(120, 25);
        this.lblChat.TabIndex = 0;
        this.lblChat.Text = "Public Chat";
        // 
        // panelChatHeader
        // 
        this.panelChatHeader.BackColor = Color.White;
        this.panelChatHeader.Controls.Add(this.lblChat);
        this.panelChatHeader.Dock = DockStyle.Top;
        this.panelChatHeader.Location = new Point(0, 0);
        this.panelChatHeader.Name = "panelChatHeader";
        this.panelChatHeader.Size = new Size(816, 60);
        this.panelChatHeader.TabIndex = 3;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1100, 700);
        this.Controls.Add(this.splitContainer1);
        this.Name = "Form1";
        this.Text = "Chat Application";
        this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
        this.Load += new EventHandler(this.Form1_Load);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.ResumeLayout(false);
        this.panelSidebar.ResumeLayout(false);
        this.panelSidebar.PerformLayout();
        this.panelInput.ResumeLayout(false);
        this.panelInput.PerformLayout();
        this.panelChatHeader.ResumeLayout(false);
        this.panelChatHeader.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private SplitContainer splitContainer1;
    private ListBox lstOnlineUsers;
    private Label lblUsers;
    private FlowLayoutPanel flowChat;
    private Panel panelInput;
    private Button btnSend;
    private TextBox txtMessage;
    private Label lblChat;
    private Panel panelSidebar;
    private Panel panelChatHeader;
}

