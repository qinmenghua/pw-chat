namespace PW_Chat
{
    partial class NewConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.usernamelbl = new System.Windows.Forms.Label();
            this.passwordlbl = new System.Windows.Forms.Label();
            this.loginbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.serverlbl = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.TextBox();
            this.helpbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(77, 17);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(100, 20);
            this.username.TabIndex = 0;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(77, 46);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(100, 20);
            this.password.TabIndex = 1;
            this.password.UseSystemPasswordChar = true;
            // 
            // usernamelbl
            // 
            this.usernamelbl.AutoSize = true;
            this.usernamelbl.Location = new System.Drawing.Point(10, 24);
            this.usernamelbl.Name = "usernamelbl";
            this.usernamelbl.Size = new System.Drawing.Size(58, 13);
            this.usernamelbl.TabIndex = 2;
            this.usernamelbl.Text = "Username:";
            // 
            // passwordlbl
            // 
            this.passwordlbl.AutoSize = true;
            this.passwordlbl.Location = new System.Drawing.Point(12, 51);
            this.passwordlbl.Name = "passwordlbl";
            this.passwordlbl.Size = new System.Drawing.Size(56, 13);
            this.passwordlbl.TabIndex = 3;
            this.passwordlbl.Text = "Password:";
            // 
            // loginbtn
            // 
            this.loginbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.loginbtn.Location = new System.Drawing.Point(197, 17);
            this.loginbtn.Name = "loginbtn";
            this.loginbtn.Size = new System.Drawing.Size(75, 23);
            this.loginbtn.TabIndex = 3;
            this.loginbtn.Text = "Login";
            this.loginbtn.UseVisualStyleBackColor = true;
            this.loginbtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(197, 46);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 4;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // serverlbl
            // 
            this.serverlbl.AutoSize = true;
            this.serverlbl.Location = new System.Drawing.Point(27, 79);
            this.serverlbl.Name = "serverlbl";
            this.serverlbl.Size = new System.Drawing.Size(41, 13);
            this.serverlbl.TabIndex = 6;
            this.serverlbl.Text = "Server:";
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(77, 74);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(100, 20);
            this.server.TabIndex = 2;
            // 
            // helpbtn
            // 
            this.helpbtn.Location = new System.Drawing.Point(197, 74);
            this.helpbtn.Name = "helpbtn";
            this.helpbtn.Size = new System.Drawing.Size(75, 23);
            this.helpbtn.TabIndex = 5;
            this.helpbtn.Text = "Help";
            this.helpbtn.UseVisualStyleBackColor = true;
            this.helpbtn.Click += new System.EventHandler(this.helpbtn_Click);
            // 
            // NewConnection
            // 
            this.AcceptButton = this.loginbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(284, 109);
            this.Controls.Add(this.helpbtn);
            this.Controls.Add(this.server);
            this.Controls.Add(this.serverlbl);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.loginbtn);
            this.Controls.Add(this.passwordlbl);
            this.Controls.Add(this.usernamelbl);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "NewConnection";
            this.Text = "New Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label usernamelbl;
        private System.Windows.Forms.Label passwordlbl;
        private System.Windows.Forms.Button loginbtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label serverlbl;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.Button helpbtn;
    }
}