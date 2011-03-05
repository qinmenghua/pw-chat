using System;
using System.Drawing;
using System.Media;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace PW_Chat
{
    public partial class mainForm : Form
    {

        public static bool loggedIn = false;
        public static String username;
        public static String password;
        public static String servername;
        //when was the last user interactivity time?
        public static int connectedTime;
        //max idle time in seconds
        public static int connectedAtTime;
        public static int idleMax;
        public static long now;
        private static bool hidden = false;
        private static bool speech = Properties.Settings.Default.speech;
        //intended to be the ONLY instance of dataHandler,
        //I purposely did not make it a static class
        //as there may be times I don't want to use this instance
        public static dataHandler dh = new dataHandler();
        private SpeechSynthesizer s = new SpeechSynthesizer();
        public mainForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
            trayIcon.Visible = Properties.Settings.Default.tray;
            setBackColor(Properties.Settings.Default.wincolor);
            setFontColor(Properties.Settings.Default.textcolor);
            if (!loggedIn)
            {
                disableMsgFields();
            }
            trayIcon.Icon = Properties.Resources.icon;
            //note about opacity in appSettings class
            this.Opacity = Properties.Settings.Default.opacity * 0.01;
            speechSynthToolStripMenuItem.Checked = speech;
            #if DEBUG
                gCToolStripMenuItem.Visible = true;
            #endif
        }
        private long getNow()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void onlineManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(Form.ActiveForm, Properties.Settings.Default.manPage);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox abb = new aboutBox();
            abb.Show();
        }

        public void setConInfo(String info)
        {
            connectionInfoBar.Text = info;
            
        }

        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewConnection ncw = new NewConnection();
            ncw.Show();
        }

        private void clearConSettings()
        {
            Properties.Settings.Default.lhost = null;
            Properties.Settings.Default.lpassword = null;
            Properties.Settings.Default.lusername = null;
            Properties.Settings.Default.Save();
        }

        private void clearSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearConSettings();
        }

        private void applicationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            appSettings aps = new appSettings();
            aps.Show();
        }
        public void playAlert()
        {
            SoundPlayer wav = new SoundPlayer(PW_Chat.Properties.Resources.alert);
            wav.Play();
        }

        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            if (msgSendBox.Text == "" && !Properties.Settings.Default.emptymsg)
            {
                MessageBox.Show("Message Sending Failed\nEmpty Message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!dh.SendMessage(msgSendBox.Text))
            {
                MessageBox.Show("Message Sending Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DateTime now = DateTime.Now;
                String nw = String.Format("{0: yyyy-mm-dd HH:mm:ss}", now);
                //its not actually added to server logs... (might end up adding sql insert for this though)
                mainTextBox.AppendText(String.Format("U:{0}, T:{1}, CD:{2}, D:{3}, MSG:{4}\n", -1, "Chat", 9, nw, msgSendBox.Text));
                msgSendBox.Text = "";
            }
        }

        public void enableMsgFields()
        {
            msgSendBox.Enabled = true;
            sendMsgBtn.Enabled = true;
        }
        public void disableMsgFields()
        {
            msgSendBox.Enabled = false;
            sendMsgBtn.Enabled = false;
        }

        private void closeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            disableMsgFields();
            dh.logout();
            setConInfo("Not Connected");
            mainTextBox.Text = "";
            chatChecker.Enabled = false;
            servername = null;
            username = null;
            password = null;
            closeConnectionToolStripMenuItem.Enabled = false;
            
        }

        public void setBackColor(Color winColor)
        {
            this.BackColor = winColor;
            mainMenu.BackColor = winColor;
            //Not sure if I want these to change...
            /*msgSendBox.BackColor = winColor;
            sendMsgBtn.BackColor = winColor;
            msgArea.BackColor = winColor;*/
        }
        public void setFontColor(Color fontColor)
        {
            this.ForeColor = fontColor;
            mainMenu.ForeColor = fontColor;
            //Not sure if I want these to change...
            /*msgSendBox.ForeColor = fontColor;
            sendMsgBtn.ForeColor = fontColor;
            msgArea.ForeColor = fontColor;*/
        }

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //figure out if window is already hidden, if it is show if not hide
            if (hidden)
            {
                this.Show();
                hidden = false;
            }
            else
            {
                this.Hide();
                hidden = true;
            }
        }

        private void trayIconMenuShow_Click(object sender, EventArgs e)
        {
            this.Show();
            //force window on top briefly so user can see it
            this.TopMost = true;
            this.TopMost = false;
        }
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void trayIconMenuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updateCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateCheck uc = new updateCheck();
            String version = Convert.ToString(uc.webVersion);
            String thisVersion = Convert.ToString(updateCheck.thisVersion);
            String upToDate = uc.IsUpToDate ? "You are up to date." : "You should update.\nPress OK To update";
            MessageBoxButtons dialogButtons = uc.IsUpToDate ? MessageBoxButtons.OK : MessageBoxButtons.OKCancel;
            DialogResult dr;
            if (uc.webVersion != -1)
            {
                dr = MessageBox.Show(String.Format("Current Version: {0}\nWeb Version: {1}\n{2}", thisVersion, version, upToDate), "Update Check", dialogButtons, MessageBoxIcon.Information);
            }
            else
            {
                dr = MessageBox.Show("Unable to get current version", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!uc.IsUpToDate && dr == DialogResult.OK)
            {
                Help.ShowHelp(Form.ActiveForm, "http://dl.dropbox.com/u/1178264/pw-chat/release_nosrc.zip");
            }
        }

        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectionSettings cs = new connectionSettings();
            cs.Show();
        }

        private void idleChecker_Tick(object sender, EventArgs e)
        {
            now = getNow();
            if (connectedTime < (now - idleMax))
            {
                clearConSettings();
                MessageBox.Show("Disconnected due to Idle Time");
            }
        }

        private void speechSynthToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool ss = speechSynthToolStripMenuItem.Checked;
            if (ss)
            {
                //speak("Speech enabled");
                speak("Speech is still in testing. Other then this message no other speaking occurs.");
            }
            Properties.Settings.Default.speech = ss;
            Properties.Settings.Default.Save();
        }
        private void speak(String message, bool async = true)
        {
            if (async)
            {
                s.SpeakAsync(message);
            }
            else
            {
                s.Speak(message);
            }
        }

        private void lastConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewConnection().Login(Properties.Settings.Default.lusername, Properties.Settings.Default.lpassword, Properties.Settings.Default.lhost);
        }

        private void gCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                MessageBox.Show(Convert.ToString(dataHandler.cid));
                dh.getChats(dataHandler.cid);
                int tlen = mainTextBox.Lines.Length-1;
                MessageBox.Show(Convert.ToString(tlen));
                if (tlen % 1000 == 0)
                {

                }
            }
            else
            {
                MessageBox.Show("Log in first dummy");
            }
        }
        private void chatChecker_Tick(object sender, EventArgs e)
        {
            dh.getChats(dataHandler.cid);
            textBoxLineCount.Text = "Lines: " + Convert.ToString(mainTextBox.Lines.Length - 1);

        }

        private void clearTextBox_Click(object sender, EventArgs e)
        {
            mainTextBox.Text = "";
            textBoxLineCount.Text = "Lines: 0";

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(mainTextBox.Text);
        }
     
    }
}
