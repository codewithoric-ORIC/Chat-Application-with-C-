
namespace ChatApp.Client;

partial class LoginForm
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
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblUsername = new System.Windows.Forms.Label();
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.lblPassword = new System.Windows.Forms.Label();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.btnLogin = new System.Windows.Forms.Button();
        this.btnRegister = new System.Windows.Forms.Button();
        this.lblServerIp = new System.Windows.Forms.Label();
        this.txtServerIp = new System.Windows.Forms.TextBox();
        this.panelHeader = new System.Windows.Forms.Panel();
        this.panelContent = new System.Windows.Forms.Panel();
        this.panelHeader.SuspendLayout();
        this.panelContent.SuspendLayout();
        this.SuspendLayout();
        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblTitle.ForeColor = System.Drawing.Color.White;
        this.lblTitle.Location = new System.Drawing.Point(30, 25);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(243, 41);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Chat Application";
        // 
        // lblUsername
        // 
        this.lblUsername.AutoSize = true;
        this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.lblUsername.Location = new System.Drawing.Point(30, 90);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(78, 19);
        this.lblUsername.TabIndex = 1;
        this.lblUsername.Text = "Username";
        // 
        // txtUsername
        // 
        this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtUsername.Location = new System.Drawing.Point(30, 115);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(280, 27);
        this.txtUsername.TabIndex = 0;
        // 
        // lblPassword
        // 
        this.lblPassword.AutoSize = true;
        this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.lblPassword.Location = new System.Drawing.Point(30, 160);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(74, 19);
        this.lblPassword.TabIndex = 3;
        this.lblPassword.Text = "Password";
        // 
        // txtPassword
        // 
        this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtPassword.Location = new System.Drawing.Point(30, 185);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '●';
        this.txtPassword.Size = new System.Drawing.Size(280, 27);
        this.txtPassword.TabIndex = 1;
        // 
        // btnLogin
        // 
        this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(204)))));
        this.btnLogin.FlatAppearance.BorderSize = 0;
        this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.btnLogin.ForeColor = System.Drawing.Color.White;
        this.btnLogin.Location = new System.Drawing.Point(30, 280);
        this.btnLogin.Name = "btnLogin";
        this.btnLogin.Size = new System.Drawing.Size(130, 40);
        this.btnLogin.TabIndex = 3;
        this.btnLogin.Text = "Login";
        this.btnLogin.UseVisualStyleBackColor = false;
        this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
        this.btnLogin.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
        this.btnLogin.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
        // 
        // btnRegister
        // 
        this.btnRegister.BackColor = System.Drawing.Color.White;
        this.btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(204)))));
        this.btnRegister.FlatAppearance.BorderSize = 2;
        this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(204)))));
        this.btnRegister.Location = new System.Drawing.Point(180, 280);
        this.btnRegister.Name = "btnRegister";
        this.btnRegister.Size = new System.Drawing.Size(130, 40);
        this.btnRegister.TabIndex = 4;
        this.btnRegister.Text = "Register";
        this.btnRegister.UseVisualStyleBackColor = false;
        this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
        this.btnRegister.MouseEnter += new System.EventHandler(this.BtnRegister_MouseEnter);
        this.btnRegister.MouseLeave += new System.EventHandler(this.BtnRegister_MouseLeave);
        // 
        // lblServerIp
        // 
        this.lblServerIp.AutoSize = true;
        this.lblServerIp.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblServerIp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.lblServerIp.Location = new System.Drawing.Point(30, 20);
        this.lblServerIp.Name = "lblServerIp";
        this.lblServerIp.Size = new System.Drawing.Size(73, 19);
        this.lblServerIp.TabIndex = 6;
        this.lblServerIp.Text = "Server IP";
        // 
        // txtServerIp
        // 
        this.txtServerIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtServerIp.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtServerIp.Location = new System.Drawing.Point(30, 45);
        this.txtServerIp.Name = "txtServerIp";
        this.txtServerIp.Size = new System.Drawing.Size(280, 27);
        this.txtServerIp.TabIndex = 2;
        this.txtServerIp.Text = "127.0.0.1";
        // 
        // panelHeader
        // 
        this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(204)))));
        this.panelHeader.Controls.Add(this.lblTitle);
        this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelHeader.Location = new System.Drawing.Point(0, 0);
        this.panelHeader.Name = "panelHeader";
        this.panelHeader.Size = new System.Drawing.Size(340, 90);
        this.panelHeader.TabIndex = 7;
        // 
        // panelContent
        // 
        this.panelContent.BackColor = System.Drawing.Color.White;
        this.panelContent.Controls.Add(this.txtServerIp);
        this.panelContent.Controls.Add(this.lblServerIp);
        this.panelContent.Controls.Add(this.btnRegister);
        this.panelContent.Controls.Add(this.btnLogin);
        this.panelContent.Controls.Add(this.txtPassword);
        this.panelContent.Controls.Add(this.lblPassword);
        this.panelContent.Controls.Add(this.txtUsername);
        this.panelContent.Controls.Add(this.lblUsername);
        this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelContent.Location = new System.Drawing.Point(0, 90);
        this.panelContent.Name = "panelContent";
        this.panelContent.Size = new System.Drawing.Size(340, 350);
        this.panelContent.TabIndex = 8;
        // 
        // LoginForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(340, 440);
        this.Controls.Add(this.panelContent);
        this.Controls.Add(this.panelHeader);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LoginForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Login / Register";
        this.panelHeader.ResumeLayout(false);
        this.panelHeader.PerformLayout();
        this.panelContent.ResumeLayout(false);
        this.panelContent.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btnLogin;
    private System.Windows.Forms.Button btnRegister;
    private System.Windows.Forms.Label lblServerIp;
    private System.Windows.Forms.TextBox txtServerIp;
    private System.Windows.Forms.Panel panelHeader;
    private System.Windows.Forms.Panel panelContent;
}

