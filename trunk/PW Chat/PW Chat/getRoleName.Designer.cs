namespace PW_Chat
{
    partial class getRoleName
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
            this.getRoleNameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.roleIdlbl = new System.Windows.Forms.Label();
            this.lookupHistory = new System.Windows.Forms.TextBox();
            this.lookuptbn = new System.Windows.Forms.Button();
            this.rIdTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // roleIdlbl
            // 
            this.roleIdlbl.AutoSize = true;
            this.roleIdlbl.Location = new System.Drawing.Point(12, 9);
            this.roleIdlbl.Name = "roleIdlbl";
            this.roleIdlbl.Size = new System.Drawing.Size(49, 13);
            this.roleIdlbl.TabIndex = 0;
            this.roleIdlbl.Text = "Role ID: ";
            this.getRoleNameToolTip.SetToolTip(this.roleIdlbl, "User ID to get the name of");
            // 
            // lookupHistory
            // 
            this.lookupHistory.BackColor = System.Drawing.SystemColors.Window;
            this.lookupHistory.Location = new System.Drawing.Point(12, 33);
            this.lookupHistory.Multiline = true;
            this.lookupHistory.Name = "lookupHistory";
            this.lookupHistory.ReadOnly = true;
            this.lookupHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lookupHistory.Size = new System.Drawing.Size(235, 235);
            this.lookupHistory.TabIndex = 3;
            this.lookupHistory.TabStop = false;
            // 
            // lookuptbn
            // 
            this.lookuptbn.Location = new System.Drawing.Point(172, 4);
            this.lookuptbn.Name = "lookuptbn";
            this.lookuptbn.Size = new System.Drawing.Size(75, 23);
            this.lookuptbn.TabIndex = 2;
            this.lookuptbn.Text = "Lookup";
            this.lookuptbn.UseVisualStyleBackColor = true;
            this.lookuptbn.Click += new System.EventHandler(this.lookuptbn_Click);
            // 
            // rIdTextbox
            // 
            this.rIdTextbox.Location = new System.Drawing.Point(67, 6);
            this.rIdTextbox.Name = "rIdTextbox";
            this.rIdTextbox.Size = new System.Drawing.Size(99, 20);
            this.rIdTextbox.TabIndex = 1;
            // 
            // getRoleName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 280);
            this.Controls.Add(this.rIdTextbox);
            this.Controls.Add(this.lookuptbn);
            this.Controls.Add(this.lookupHistory);
            this.Controls.Add(this.roleIdlbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "getRoleName";
            this.Text = "Get Role Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip getRoleNameToolTip;
        private System.Windows.Forms.Label roleIdlbl;
        private System.Windows.Forms.TextBox lookupHistory;
        private System.Windows.Forms.Button lookuptbn;
        private System.Windows.Forms.TextBox rIdTextbox;
    }
}