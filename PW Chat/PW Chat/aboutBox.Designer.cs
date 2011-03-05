namespace PW_Chat
{
    partial class aboutBox
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
            this.aboutGroup = new System.Windows.Forms.GroupBox();
            this.aboutBrowser = new System.Windows.Forms.WebBrowser();
            this.avabox = new System.Windows.Forms.PictureBox();
            this.aboutGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avabox)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutGroup
            // 
            this.aboutGroup.Controls.Add(this.aboutBrowser);
            this.aboutGroup.Location = new System.Drawing.Point(211, 12);
            this.aboutGroup.Name = "aboutGroup";
            this.aboutGroup.Size = new System.Drawing.Size(192, 192);
            this.aboutGroup.TabIndex = 1;
            this.aboutGroup.TabStop = false;
            this.aboutGroup.Text = "About";
            // 
            // aboutBrowser
            // 
            this.aboutBrowser.AllowNavigation = false;
            this.aboutBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutBrowser.Location = new System.Drawing.Point(3, 16);
            this.aboutBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.aboutBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.aboutBrowser.Name = "aboutBrowser";
            this.aboutBrowser.ScrollBarsEnabled = false;
            this.aboutBrowser.Size = new System.Drawing.Size(186, 173);
            this.aboutBrowser.TabIndex = 0;
            this.aboutBrowser.Url = new System.Uri("http://dl.dropbox.com/u/1178264/pw-chat/about.html", System.UriKind.Absolute);
            this.aboutBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.aboutBrowser_DocumentCompleted);
            // 
            // avabox
            // 
            this.avabox.BackgroundImage = global::PW_Chat.Properties.Resources.ava_small;
            this.avabox.Location = new System.Drawing.Point(12, 12);
            this.avabox.Name = "avabox";
            this.avabox.Size = new System.Drawing.Size(192, 192);
            this.avabox.TabIndex = 0;
            this.avabox.TabStop = false;
            // 
            // aboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 216);
            this.Controls.Add(this.aboutGroup);
            this.Controls.Add(this.avabox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "aboutBox";
            this.Text = "About";
            this.aboutGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avabox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox aboutGroup;
        private System.Windows.Forms.WebBrowser aboutBrowser;
        private System.Windows.Forms.PictureBox avabox;
    }
}