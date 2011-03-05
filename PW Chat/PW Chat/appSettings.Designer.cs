namespace PW_Chat
{
    partial class appSettings
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
            this.opacityMeter = new System.Windows.Forms.TrackBar();
            this.opacityMeterlbl = new System.Windows.Forms.Label();
            this.opacityMeternfolbl = new System.Windows.Forms.Label();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.savebtn = new System.Windows.Forms.Button();
            this.audionoticelbl = new System.Windows.Forms.Label();
            this.audioenbtn = new System.Windows.Forms.RadioButton();
            this.audiodisbtn = new System.Windows.Forms.RadioButton();
            this.audionoticesmp = new System.Windows.Forms.Button();
            this.textColor = new System.Windows.Forms.ColorDialog();
            this.winColor = new System.Windows.Forms.ColorDialog();
            this.fontColorlbl = new System.Windows.Forms.Label();
            this.fontColorbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.winColorbtn = new System.Windows.Forms.Button();
            this.defaultsBtn = new System.Windows.Forms.Button();
            this.newKeybtn = new System.Windows.Forms.Button();
            this.showKeybtn = new System.Windows.Forms.Button();
            this.encryptionlbl = new System.Windows.Forms.Label();
            this.exportKeybtn = new System.Windows.Forms.Button();
            this.testEncryptionbtn = new System.Windows.Forms.Button();
            this.trayIconlbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.traydisbtn = new System.Windows.Forms.RadioButton();
            this.trayenbtn = new System.Windows.Forms.RadioButton();
            this.importKeybtn = new System.Windows.Forms.Button();
            this.jsonTestbtn = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ramUselbl = new System.Windows.Forms.Label();
            this.gcbtn = new System.Windows.Forms.Button();
            this.cpuUselbl = new System.Windows.Forms.Label();
            this.ramCpuTimer = new System.Windows.Forms.Timer(this.components);
            this.ramUseBox = new System.Windows.Forms.TextBox();
            this.cpuUseBox = new System.Windows.Forms.TextBox();
            this.emptyMsgSend = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.opacityMeter)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // opacityMeter
            // 
            this.opacityMeter.Location = new System.Drawing.Point(103, 12);
            this.opacityMeter.Maximum = 100;
            this.opacityMeter.Name = "opacityMeter";
            this.opacityMeter.Size = new System.Drawing.Size(318, 45);
            this.opacityMeter.TabIndex = 0;
            this.opacityMeter.TickFrequency = 5;
            this.opacityMeter.Value = 100;
            this.opacityMeter.Scroll += new System.EventHandler(this.opacityMeter_Scroll);
            // 
            // opacityMeterlbl
            // 
            this.opacityMeterlbl.AutoSize = true;
            this.opacityMeterlbl.Location = new System.Drawing.Point(427, 22);
            this.opacityMeterlbl.Name = "opacityMeterlbl";
            this.opacityMeterlbl.Size = new System.Drawing.Size(33, 13);
            this.opacityMeterlbl.TabIndex = 1;
            this.opacityMeterlbl.Text = "100%";
            // 
            // opacityMeternfolbl
            // 
            this.opacityMeternfolbl.AutoSize = true;
            this.opacityMeternfolbl.Location = new System.Drawing.Point(12, 22);
            this.opacityMeternfolbl.Name = "opacityMeternfolbl";
            this.opacityMeternfolbl.Size = new System.Drawing.Size(88, 13);
            this.opacityMeternfolbl.TabIndex = 2;
            this.opacityMeternfolbl.Text = "Window Opacity:";
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(304, 209);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 98;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // savebtn
            // 
            this.savebtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.savebtn.Location = new System.Drawing.Point(223, 209);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(75, 23);
            this.savebtn.TabIndex = 97;
            this.savebtn.Text = "Save";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // audionoticelbl
            // 
            this.audionoticelbl.AutoSize = true;
            this.audionoticelbl.Location = new System.Drawing.Point(7, 55);
            this.audionoticelbl.Name = "audionoticelbl";
            this.audionoticelbl.Size = new System.Drawing.Size(93, 13);
            this.audionoticelbl.TabIndex = 5;
            this.audionoticelbl.Text = "Audio Notification:";
            // 
            // audioenbtn
            // 
            this.audioenbtn.AutoSize = true;
            this.audioenbtn.Checked = true;
            this.audioenbtn.Location = new System.Drawing.Point(106, 51);
            this.audioenbtn.Name = "audioenbtn";
            this.audioenbtn.Size = new System.Drawing.Size(64, 17);
            this.audioenbtn.TabIndex = 1;
            this.audioenbtn.TabStop = true;
            this.audioenbtn.Text = "Enabled";
            this.audioenbtn.UseVisualStyleBackColor = true;
            // 
            // audiodisbtn
            // 
            this.audiodisbtn.AutoSize = true;
            this.audiodisbtn.Location = new System.Drawing.Point(176, 51);
            this.audiodisbtn.Name = "audiodisbtn";
            this.audiodisbtn.Size = new System.Drawing.Size(66, 17);
            this.audiodisbtn.TabIndex = 2;
            this.audiodisbtn.Text = "Disabled";
            this.audiodisbtn.UseVisualStyleBackColor = true;
            // 
            // audionoticesmp
            // 
            this.audionoticesmp.Location = new System.Drawing.Point(248, 48);
            this.audionoticesmp.Name = "audionoticesmp";
            this.audionoticesmp.Size = new System.Drawing.Size(75, 23);
            this.audionoticesmp.TabIndex = 3;
            this.audionoticesmp.Text = "Sample";
            this.audionoticesmp.UseVisualStyleBackColor = true;
            this.audionoticesmp.Click += new System.EventHandler(this.audionoticesmp_Click);
            // 
            // fontColorlbl
            // 
            this.fontColorlbl.AutoSize = true;
            this.fontColorlbl.Location = new System.Drawing.Point(42, 95);
            this.fontColorlbl.Name = "fontColorlbl";
            this.fontColorlbl.Size = new System.Drawing.Size(61, 13);
            this.fontColorlbl.TabIndex = 9;
            this.fontColorlbl.Text = "Font Color: ";
            // 
            // fontColorbtn
            // 
            this.fontColorbtn.Location = new System.Drawing.Point(109, 90);
            this.fontColorbtn.Name = "fontColorbtn";
            this.fontColorbtn.Size = new System.Drawing.Size(110, 23);
            this.fontColorbtn.TabIndex = 4;
            this.fontColorbtn.Text = "Black";
            this.fontColorbtn.UseVisualStyleBackColor = true;
            this.fontColorbtn.Click += new System.EventHandler(this.fontColorbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Window Color: ";
            // 
            // winColorbtn
            // 
            this.winColorbtn.Location = new System.Drawing.Point(307, 90);
            this.winColorbtn.Name = "winColorbtn";
            this.winColorbtn.Size = new System.Drawing.Size(110, 23);
            this.winColorbtn.TabIndex = 5;
            this.winColorbtn.Text = "Gray";
            this.winColorbtn.UseVisualStyleBackColor = true;
            this.winColorbtn.Click += new System.EventHandler(this.winColorbtn_Click);
            // 
            // defaultsBtn
            // 
            this.defaultsBtn.Location = new System.Drawing.Point(385, 209);
            this.defaultsBtn.Name = "defaultsBtn";
            this.defaultsBtn.Size = new System.Drawing.Size(75, 23);
            this.defaultsBtn.TabIndex = 99;
            this.defaultsBtn.Text = "Defaults";
            this.defaultsBtn.UseVisualStyleBackColor = true;
            this.defaultsBtn.Click += new System.EventHandler(this.defaultsBtn_Click);
            // 
            // newKeybtn
            // 
            this.newKeybtn.Location = new System.Drawing.Point(109, 119);
            this.newKeybtn.Name = "newKeybtn";
            this.newKeybtn.Size = new System.Drawing.Size(75, 23);
            this.newKeybtn.TabIndex = 6;
            this.newKeybtn.Text = "New Key";
            this.newKeybtn.UseVisualStyleBackColor = true;
            this.newKeybtn.Click += new System.EventHandler(this.newKeybtn_Click);
            // 
            // showKeybtn
            // 
            this.showKeybtn.Location = new System.Drawing.Point(190, 119);
            this.showKeybtn.Name = "showKeybtn";
            this.showKeybtn.Size = new System.Drawing.Size(75, 23);
            this.showKeybtn.TabIndex = 7;
            this.showKeybtn.Text = "Show Key";
            this.showKeybtn.UseVisualStyleBackColor = true;
            this.showKeybtn.Click += new System.EventHandler(this.showKeybtn_Click);
            // 
            // encryptionlbl
            // 
            this.encryptionlbl.AutoSize = true;
            this.encryptionlbl.Location = new System.Drawing.Point(42, 124);
            this.encryptionlbl.Name = "encryptionlbl";
            this.encryptionlbl.Size = new System.Drawing.Size(60, 13);
            this.encryptionlbl.TabIndex = 16;
            this.encryptionlbl.Text = "Encryption:";
            // 
            // exportKeybtn
            // 
            this.exportKeybtn.Location = new System.Drawing.Point(271, 119);
            this.exportKeybtn.Name = "exportKeybtn";
            this.exportKeybtn.Size = new System.Drawing.Size(75, 23);
            this.exportKeybtn.TabIndex = 8;
            this.exportKeybtn.Text = "Export Key";
            this.exportKeybtn.UseVisualStyleBackColor = true;
            this.exportKeybtn.Click += new System.EventHandler(this.exportKeybtn_Click);
            // 
            // testEncryptionbtn
            // 
            this.testEncryptionbtn.Location = new System.Drawing.Point(352, 119);
            this.testEncryptionbtn.Name = "testEncryptionbtn";
            this.testEncryptionbtn.Size = new System.Drawing.Size(75, 23);
            this.testEncryptionbtn.TabIndex = 10;
            this.testEncryptionbtn.Text = "Test";
            this.testEncryptionbtn.UseVisualStyleBackColor = true;
            this.testEncryptionbtn.Click += new System.EventHandler(this.testEncryptionbtn_Click);
            // 
            // trayIconlbl
            // 
            this.trayIconlbl.AutoSize = true;
            this.trayIconlbl.Location = new System.Drawing.Point(3, 3);
            this.trayIconlbl.Name = "trayIconlbl";
            this.trayIconlbl.Size = new System.Drawing.Size(58, 13);
            this.trayIconlbl.TabIndex = 100;
            this.trayIconlbl.Text = "Tray Icon: ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.traydisbtn);
            this.panel1.Controls.Add(this.trayIconlbl);
            this.panel1.Controls.Add(this.trayenbtn);
            this.panel1.Location = new System.Drawing.Point(42, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 20);
            this.panel1.TabIndex = 101;
            // 
            // traydisbtn
            // 
            this.traydisbtn.AutoSize = true;
            this.traydisbtn.Location = new System.Drawing.Point(134, 1);
            this.traydisbtn.Name = "traydisbtn";
            this.traydisbtn.Size = new System.Drawing.Size(66, 17);
            this.traydisbtn.TabIndex = 103;
            this.traydisbtn.Text = "Disabled";
            this.traydisbtn.UseVisualStyleBackColor = true;
            // 
            // trayenbtn
            // 
            this.trayenbtn.AutoSize = true;
            this.trayenbtn.Checked = true;
            this.trayenbtn.Location = new System.Drawing.Point(64, 1);
            this.trayenbtn.Name = "trayenbtn";
            this.trayenbtn.Size = new System.Drawing.Size(64, 17);
            this.trayenbtn.TabIndex = 102;
            this.trayenbtn.TabStop = true;
            this.trayenbtn.Text = "Enabled";
            this.trayenbtn.UseVisualStyleBackColor = true;
            // 
            // importKeybtn
            // 
            this.importKeybtn.Location = new System.Drawing.Point(271, 148);
            this.importKeybtn.Name = "importKeybtn";
            this.importKeybtn.Size = new System.Drawing.Size(75, 23);
            this.importKeybtn.TabIndex = 9;
            this.importKeybtn.Text = "Import Key";
            this.importKeybtn.UseVisualStyleBackColor = true;
            this.importKeybtn.Click += new System.EventHandler(this.importKeybtn_Click);
            // 
            // jsonTestbtn
            // 
            this.jsonTestbtn.Location = new System.Drawing.Point(352, 148);
            this.jsonTestbtn.Name = "jsonTestbtn";
            this.jsonTestbtn.Size = new System.Drawing.Size(75, 23);
            this.jsonTestbtn.TabIndex = 11;
            this.jsonTestbtn.Text = "JSON Test";
            this.jsonTestbtn.UseVisualStyleBackColor = true;
            this.jsonTestbtn.Click += new System.EventHandler(this.jsonTestbtn_Click);
            // 
            // ramUselbl
            // 
            this.ramUselbl.AutoSize = true;
            this.ramUselbl.Location = new System.Drawing.Point(34, 153);
            this.ramUselbl.Name = "ramUselbl";
            this.ramUselbl.Size = new System.Drawing.Size(68, 13);
            this.ramUselbl.TabIndex = 104;
            this.ramUselbl.Text = "RAM Usage:";
            this.toolTip.SetToolTip(this.ramUselbl, "Displays the physical memory usage.\r\nUpdates every 5 seconds.");
            // 
            // gcbtn
            // 
            this.gcbtn.Location = new System.Drawing.Point(215, 148);
            this.gcbtn.Name = "gcbtn";
            this.gcbtn.Size = new System.Drawing.Size(34, 23);
            this.gcbtn.TabIndex = 13;
            this.gcbtn.Text = "GC";
            this.toolTip.SetToolTip(this.gcbtn, "Force a Garbage Collection");
            this.gcbtn.UseVisualStyleBackColor = true;
            this.gcbtn.Click += new System.EventHandler(this.gcbtn_Click);
            // 
            // cpuUselbl
            // 
            this.cpuUselbl.AutoSize = true;
            this.cpuUselbl.Location = new System.Drawing.Point(36, 178);
            this.cpuUselbl.Name = "cpuUselbl";
            this.cpuUselbl.Size = new System.Drawing.Size(66, 13);
            this.cpuUselbl.TabIndex = 105;
            this.cpuUselbl.Text = "CPU Usage:";
            this.toolTip.SetToolTip(this.cpuUselbl, "Retrieve the current system CPU usage.\r\nInitialization freezes the thread for a f" +
                    "ew seconds.\r\nUpdates every 5 seconds.");
            // 
            // ramCpuTimer
            // 
            this.ramCpuTimer.Enabled = true;
            this.ramCpuTimer.Interval = 5000;
            this.ramCpuTimer.Tick += new System.EventHandler(this.ramTimer_Tick);
            // 
            // ramUseBox
            // 
            this.ramUseBox.Location = new System.Drawing.Point(109, 150);
            this.ramUseBox.Name = "ramUseBox";
            this.ramUseBox.ReadOnly = true;
            this.ramUseBox.Size = new System.Drawing.Size(100, 20);
            this.ramUseBox.TabIndex = 12;
            this.ramUseBox.TabStop = false;
            this.ramUseBox.Text = "0M";
            // 
            // cpuUseBox
            // 
            this.cpuUseBox.Location = new System.Drawing.Point(109, 175);
            this.cpuUseBox.Name = "cpuUseBox";
            this.cpuUseBox.ReadOnly = true;
            this.cpuUseBox.Size = new System.Drawing.Size(100, 20);
            this.cpuUseBox.TabIndex = 106;
            this.cpuUseBox.Text = "Click to enable...";
            this.cpuUseBox.Click += new System.EventHandler(this.cpuUseBox_Click);
            // 
            // emptyMsgSend
            // 
            this.emptyMsgSend.AutoSize = true;
            this.emptyMsgSend.Location = new System.Drawing.Point(324, 51);
            this.emptyMsgSend.Name = "emptyMsgSend";
            this.emptyMsgSend.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.emptyMsgSend.Size = new System.Drawing.Size(146, 17);
            this.emptyMsgSend.TabIndex = 107;
            this.emptyMsgSend.Text = ":Empty Message Sending";
            this.toolTip.SetToolTip(this.emptyMsgSend, "Will cause client to throw an error\r\nwhen attempting to send empty message.");
            this.emptyMsgSend.UseVisualStyleBackColor = true;
            // 
            // appSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 244);
            this.Controls.Add(this.emptyMsgSend);
            this.Controls.Add(this.cpuUseBox);
            this.Controls.Add(this.cpuUselbl);
            this.Controls.Add(this.gcbtn);
            this.Controls.Add(this.ramUseBox);
            this.Controls.Add(this.ramUselbl);
            this.Controls.Add(this.jsonTestbtn);
            this.Controls.Add(this.importKeybtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.testEncryptionbtn);
            this.Controls.Add(this.exportKeybtn);
            this.Controls.Add(this.encryptionlbl);
            this.Controls.Add(this.showKeybtn);
            this.Controls.Add(this.newKeybtn);
            this.Controls.Add(this.defaultsBtn);
            this.Controls.Add(this.winColorbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fontColorbtn);
            this.Controls.Add(this.fontColorlbl);
            this.Controls.Add(this.audionoticesmp);
            this.Controls.Add(this.audiodisbtn);
            this.Controls.Add(this.audioenbtn);
            this.Controls.Add(this.audionoticelbl);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.opacityMeternfolbl);
            this.Controls.Add(this.opacityMeterlbl);
            this.Controls.Add(this.opacityMeter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "appSettings";
            this.Text = "Application Settings";
            ((System.ComponentModel.ISupportInitialize)(this.opacityMeter)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar opacityMeter;
        private System.Windows.Forms.Label opacityMeterlbl;
        private System.Windows.Forms.Label opacityMeternfolbl;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Label audionoticelbl;
        private System.Windows.Forms.RadioButton audioenbtn;
        private System.Windows.Forms.RadioButton audiodisbtn;
        private System.Windows.Forms.Button audionoticesmp;
        private System.Windows.Forms.ColorDialog textColor;
        private System.Windows.Forms.ColorDialog winColor;
        private System.Windows.Forms.Label fontColorlbl;
        private System.Windows.Forms.Button fontColorbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button winColorbtn;
        private System.Windows.Forms.Button defaultsBtn;
        private System.Windows.Forms.Button newKeybtn;
        private System.Windows.Forms.Button showKeybtn;
        private System.Windows.Forms.Label encryptionlbl;
        private System.Windows.Forms.Button exportKeybtn;
        private System.Windows.Forms.Button testEncryptionbtn;
        private System.Windows.Forms.Label trayIconlbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton traydisbtn;
        private System.Windows.Forms.RadioButton trayenbtn;
        private System.Windows.Forms.Button importKeybtn;
        private System.Windows.Forms.Button jsonTestbtn;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label ramUselbl;
        private System.Windows.Forms.Timer ramCpuTimer;
        private System.Windows.Forms.TextBox ramUseBox;
        private System.Windows.Forms.Button gcbtn;
        private System.Windows.Forms.Label cpuUselbl;
        private System.Windows.Forms.TextBox cpuUseBox;
        private System.Windows.Forms.CheckBox emptyMsgSend;
    }
}