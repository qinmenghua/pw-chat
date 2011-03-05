namespace PW_Chat
{
    partial class connectionSettings
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
            this.encryptionlbl = new System.Windows.Forms.Label();
            this.encryptionenbtn = new System.Windows.Forms.RadioButton();
            this.encryptiondisbtn = new System.Windows.Forms.RadioButton();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.idleMaxlbl = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.idleMaxTime = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // encryptionlbl
            // 
            this.encryptionlbl.AutoSize = true;
            this.encryptionlbl.Location = new System.Drawing.Point(15, 13);
            this.encryptionlbl.Name = "encryptionlbl";
            this.encryptionlbl.Size = new System.Drawing.Size(60, 13);
            this.encryptionlbl.TabIndex = 0;
            this.encryptionlbl.Text = "Encryption:";
            // 
            // encryptionenbtn
            // 
            this.encryptionenbtn.AutoSize = true;
            this.encryptionenbtn.Checked = true;
            this.encryptionenbtn.Location = new System.Drawing.Point(81, 11);
            this.encryptionenbtn.Name = "encryptionenbtn";
            this.encryptionenbtn.Size = new System.Drawing.Size(58, 17);
            this.encryptionenbtn.TabIndex = 1;
            this.encryptionenbtn.TabStop = true;
            this.encryptionenbtn.Text = "Enable";
            this.encryptionenbtn.UseVisualStyleBackColor = true;
            // 
            // encryptiondisbtn
            // 
            this.encryptiondisbtn.AutoSize = true;
            this.encryptiondisbtn.Location = new System.Drawing.Point(145, 11);
            this.encryptiondisbtn.Name = "encryptiondisbtn";
            this.encryptiondisbtn.Size = new System.Drawing.Size(60, 17);
            this.encryptiondisbtn.TabIndex = 2;
            this.encryptiondisbtn.Text = "Disable";
            this.encryptiondisbtn.UseVisualStyleBackColor = true;
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.Location = new System.Drawing.Point(80, 71);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 98;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(161, 71);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 99;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // idleMaxlbl
            // 
            this.idleMaxlbl.AutoSize = true;
            this.idleMaxlbl.Location = new System.Drawing.Point(25, 37);
            this.idleMaxlbl.Name = "idleMaxlbl";
            this.idleMaxlbl.Size = new System.Drawing.Size(50, 13);
            this.idleMaxlbl.TabIndex = 100;
            this.idleMaxlbl.Text = "Idle Max:";
            this.toolTip.SetToolTip(this.idleMaxlbl, "Determines how long the client waits (in minutes)\r\nbefore deciding to disconnect " +
                    "from the server.\r\nThis will also clear `Last Connection` settings.\r\n(Currently d" +
                    "oes nothing)");
            // 
            // idleMaxTime
            // 
            this.idleMaxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.idleMaxTime.FormattingEnabled = true;
            this.idleMaxTime.Items.AddRange(new object[] {
            "Never",
            "5",
            "10",
            "15",
            "30",
            "45",
            "60"});
            this.idleMaxTime.Location = new System.Drawing.Point(81, 34);
            this.idleMaxTime.Name = "idleMaxTime";
            this.idleMaxTime.Size = new System.Drawing.Size(58, 21);
            this.idleMaxTime.TabIndex = 3;
            // 
            // connectionSettings
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(248, 106);
            this.Controls.Add(this.idleMaxTime);
            this.Controls.Add(this.idleMaxlbl);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.encryptiondisbtn);
            this.Controls.Add(this.encryptionenbtn);
            this.Controls.Add(this.encryptionlbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "connectionSettings";
            this.Text = "Connection Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label encryptionlbl;
        private System.Windows.Forms.RadioButton encryptionenbtn;
        private System.Windows.Forms.RadioButton encryptiondisbtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label idleMaxlbl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox idleMaxTime;
    }
}