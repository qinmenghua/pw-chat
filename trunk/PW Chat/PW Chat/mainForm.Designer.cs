namespace PW_Chat
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getRoleNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speechSynthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionInfoBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.msgSendBox = new System.Windows.Forms.TextBox();
            this.sendMsgBtn = new System.Windows.Forms.Button();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayIconMenuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIconMenuDivider = new System.Windows.Forms.ToolStripSeparator();
            this.trayIconMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.idleChecker = new System.Windows.Forms.Timer(this.components);
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.mainTextBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBoxLineCount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.clearTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.chatChecker = new System.Windows.Forms.Timer(this.components);
            this.mainMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.trayIconMenu.SuspendLayout();
            this.mainTextBoxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.gCToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(584, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeConnectionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConnectionToolStripMenuItem,
            this.lastConnectionToolStripMenuItem,
            this.clearSettingsToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // newConnectionToolStripMenuItem
            // 
            this.newConnectionToolStripMenuItem.Name = "newConnectionToolStripMenuItem";
            this.newConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newConnectionToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.newConnectionToolStripMenuItem.Text = "New Connection";
            this.newConnectionToolStripMenuItem.Click += new System.EventHandler(this.newConnectionToolStripMenuItem_Click);
            // 
            // lastConnectionToolStripMenuItem
            // 
            this.lastConnectionToolStripMenuItem.Name = "lastConnectionToolStripMenuItem";
            this.lastConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.N)));
            this.lastConnectionToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.lastConnectionToolStripMenuItem.Text = "Last Connection";
            this.lastConnectionToolStripMenuItem.Click += new System.EventHandler(this.lastConnectionToolStripMenuItem_Click);
            // 
            // clearSettingsToolStripMenuItem
            // 
            this.clearSettingsToolStripMenuItem.Name = "clearSettingsToolStripMenuItem";
            this.clearSettingsToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.clearSettingsToolStripMenuItem.Text = "Clear Settings";
            this.clearSettingsToolStripMenuItem.Click += new System.EventHandler(this.clearSettingsToolStripMenuItem_Click);
            // 
            // closeConnectionToolStripMenuItem
            // 
            this.closeConnectionToolStripMenuItem.Enabled = false;
            this.closeConnectionToolStripMenuItem.Name = "closeConnectionToolStripMenuItem";
            this.closeConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.closeConnectionToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.closeConnectionToolStripMenuItem.Text = "Close Connection";
            this.closeConnectionToolStripMenuItem.Click += new System.EventHandler(this.closeConnectionToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.connectionSettingsToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem,
            this.speechSynthToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.ToolTipText = "Copies ALL lines below to clipboard.";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muteToolStripMenuItem,
            this.getRoleNameToolStripMenuItem});
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // muteToolStripMenuItem
            // 
            this.muteToolStripMenuItem.Name = "muteToolStripMenuItem";
            this.muteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.muteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.muteToolStripMenuItem.Text = "Mute";
            this.muteToolStripMenuItem.Click += new System.EventHandler(this.muteToolStripMenuItem_Click);
            // 
            // getRoleNameToolStripMenuItem
            // 
            this.getRoleNameToolStripMenuItem.Name = "getRoleNameToolStripMenuItem";
            this.getRoleNameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.getRoleNameToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.getRoleNameToolStripMenuItem.Text = "Get Role Name";
            this.getRoleNameToolStripMenuItem.Click += new System.EventHandler(this.getRoleNameToolStripMenuItem_Click);
            // 
            // connectionSettingsToolStripMenuItem
            // 
            this.connectionSettingsToolStripMenuItem.Name = "connectionSettingsToolStripMenuItem";
            this.connectionSettingsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.connectionSettingsToolStripMenuItem.Text = "Connection Settings";
            this.connectionSettingsToolStripMenuItem.Click += new System.EventHandler(this.connectionSettingsToolStripMenuItem_Click);
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.applicationSettingsToolStripMenuItem.Text = "Application Settings";
            this.applicationSettingsToolStripMenuItem.Click += new System.EventHandler(this.applicationSettingsToolStripMenuItem_Click);
            // 
            // speechSynthToolStripMenuItem
            // 
            this.speechSynthToolStripMenuItem.CheckOnClick = true;
            this.speechSynthToolStripMenuItem.Name = "speechSynthToolStripMenuItem";
            this.speechSynthToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.speechSynthToolStripMenuItem.Text = "Speech Synth";
            this.speechSynthToolStripMenuItem.ToolTipText = "Enable or Disable the speech synth to read messages.\r\nAdds approximately 20MB of " +
                "RAM usage until application restart.";
            this.speechSynthToolStripMenuItem.CheckedChanged += new System.EventHandler(this.speechSynthToolStripMenuItem_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineManualToolStripMenuItem,
            this.updateCheckToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // onlineManualToolStripMenuItem
            // 
            this.onlineManualToolStripMenuItem.Name = "onlineManualToolStripMenuItem";
            this.onlineManualToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.onlineManualToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.onlineManualToolStripMenuItem.Text = "Online Manual";
            this.onlineManualToolStripMenuItem.Click += new System.EventHandler(this.onlineManualToolStripMenuItem_Click);
            // 
            // updateCheckToolStripMenuItem
            // 
            this.updateCheckToolStripMenuItem.Name = "updateCheckToolStripMenuItem";
            this.updateCheckToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.updateCheckToolStripMenuItem.Text = "Update Check";
            this.updateCheckToolStripMenuItem.Click += new System.EventHandler(this.updateCheckToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Enabled = false;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gCToolStripMenuItem
            // 
            this.gCToolStripMenuItem.Name = "gCToolStripMenuItem";
            this.gCToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.gCToolStripMenuItem.Text = "GC";
            this.gCToolStripMenuItem.ToolTipText = "You shouldn\'t see me...";
            this.gCToolStripMenuItem.Visible = false;
            this.gCToolStripMenuItem.Click += new System.EventHandler(this.gCToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionInfoBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 340);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // connectionInfoBar
            // 
            this.connectionInfoBar.Name = "connectionInfoBar";
            this.connectionInfoBar.Size = new System.Drawing.Size(88, 17);
            this.connectionInfoBar.Text = "Not Connected";
            // 
            // msgSendBox
            // 
            this.msgSendBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.msgSendBox.Location = new System.Drawing.Point(13, 28);
            this.msgSendBox.Name = "msgSendBox";
            this.msgSendBox.Size = new System.Drawing.Size(478, 20);
            this.msgSendBox.TabIndex = 2;
            this.msgSendBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.msgSendBox_KeyUp);
            // 
            // sendMsgBtn
            // 
            this.sendMsgBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMsgBtn.Location = new System.Drawing.Point(497, 28);
            this.sendMsgBtn.Name = "sendMsgBtn";
            this.sendMsgBtn.Size = new System.Drawing.Size(75, 20);
            this.sendMsgBtn.TabIndex = 4;
            this.sendMsgBtn.Text = "Send";
            this.sendMsgBtn.UseVisualStyleBackColor = true;
            this.sendMsgBtn.Click += new System.EventHandler(this.sendMsgBtn_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayIconMenu;
            this.trayIcon.Text = "PW Chat";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayIconMenuShow,
            this.hideToolStripMenuItem,
            this.trayIconMenuDivider,
            this.trayIconMenuQuit});
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(104, 76);
            // 
            // trayIconMenuShow
            // 
            this.trayIconMenuShow.Name = "trayIconMenuShow";
            this.trayIconMenuShow.Size = new System.Drawing.Size(103, 22);
            this.trayIconMenuShow.Text = "Show";
            this.trayIconMenuShow.Click += new System.EventHandler(this.trayIconMenuShow_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // trayIconMenuDivider
            // 
            this.trayIconMenuDivider.Name = "trayIconMenuDivider";
            this.trayIconMenuDivider.Size = new System.Drawing.Size(100, 6);
            // 
            // trayIconMenuQuit
            // 
            this.trayIconMenuQuit.Name = "trayIconMenuQuit";
            this.trayIconMenuQuit.Size = new System.Drawing.Size(103, 22);
            this.trayIconMenuQuit.Text = "Quit";
            this.trayIconMenuQuit.Click += new System.EventHandler(this.trayIconMenuQuit_Click);
            // 
            // idleChecker
            // 
            this.idleChecker.Interval = 5000;
            this.idleChecker.Tick += new System.EventHandler(this.idleChecker_Tick);
            // 
            // mainTextBox
            // 
            this.mainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.mainTextBox.CausesValidation = false;
            this.mainTextBox.ContextMenuStrip = this.mainTextBoxMenu;
            this.mainTextBox.Location = new System.Drawing.Point(13, 54);
            this.mainTextBox.Multiline = true;
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.ReadOnly = true;
            this.mainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainTextBox.ShortcutsEnabled = false;
            this.mainTextBox.Size = new System.Drawing.Size(559, 272);
            this.mainTextBox.TabIndex = 5;
            // 
            // mainTextBoxMenu
            // 
            this.mainTextBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBoxLineCount,
            this.toolStripSeparator,
            this.clearTextBox});
            this.mainTextBoxMenu.Name = "mainTextBoxMenu";
            this.mainTextBoxMenu.Size = new System.Drawing.Size(149, 54);
            // 
            // textBoxLineCount
            // 
            this.textBoxLineCount.Enabled = false;
            this.textBoxLineCount.Name = "textBoxLineCount";
            this.textBoxLineCount.Size = new System.Drawing.Size(148, 22);
            this.textBoxLineCount.Text = "Lines: 0";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(145, 6);
            // 
            // clearTextBox
            // 
            this.clearTextBox.Name = "clearTextBox";
            this.clearTextBox.Size = new System.Drawing.Size(148, 22);
            this.clearTextBox.Text = "Clear Text Box";
            this.clearTextBox.Click += new System.EventHandler(this.clearTextBox_Click);
            // 
            // chatChecker
            // 
            this.chatChecker.Interval = 5000;
            this.chatChecker.Tick += new System.EventHandler(this.chatChecker_Tick);
            // 
            // mainForm
            // 
            this.AcceptButton = this.sendMsgBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.mainTextBox);
            this.Controls.Add(this.sendMsgBtn);
            this.Controls.Add(this.msgSendBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "mainForm";
            this.Text = "PW Chat";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.trayIconMenu.ResumeLayout(false);
            this.mainTextBoxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel connectionInfoBar;
        private System.Windows.Forms.ToolStripMenuItem clearSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.Button sendMsgBtn;
        public System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem trayIconMenuShow;
        private System.Windows.Forms.ToolStripSeparator trayIconMenuDivider;
        private System.Windows.Forms.ToolStripMenuItem trayIconMenuQuit;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateCheckToolStripMenuItem;
        public System.Windows.Forms.Timer idleChecker;
        private System.Windows.Forms.ToolStripMenuItem speechSynthToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem closeConnectionToolStripMenuItem;
        internal System.Windows.Forms.TextBox mainTextBox;
        private System.Windows.Forms.ToolStripMenuItem gCToolStripMenuItem;
        public System.Windows.Forms.Timer chatChecker;
        private System.Windows.Forms.ContextMenuStrip mainTextBoxMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem clearTextBox;
        public System.Windows.Forms.ToolStripMenuItem textBoxLineCount;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getRoleNameToolStripMenuItem;
        internal System.Windows.Forms.TextBox msgSendBox;
    }
}

