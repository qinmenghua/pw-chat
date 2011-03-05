using System;
using System.Windows.Forms;


namespace PW_Chat
{
    public partial class NewConnection : Form
    {
        public NewConnection()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
        }

        private void helpbtn_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(Form.ActiveForm, Properties.Settings.Default.manPage+"#newcon");
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (username.Text != "" && password.Text != "" && server.Text != "")
            {
                if (Login(username.Text, password.Text, server.Text))
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Username, Password and Server required", "Invalid Login Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        public bool Login(String user, String pass, String serv)
        {
            mainForm.dh = new dataHandler(serv, user, pass);
            Program.mform.setConInfo("Logging in...");
            if (mainForm.dh.login())
            {
                //Program.mform.Text = "PW Chat: " + mainForm.username + "@" + mainForm.server;
                Program.mform.setConInfo("Logged in @ " + serv);
                Program.mform.enableMsgFields();
                Properties.Settings.Default.lhost = serv;
                Properties.Settings.Default.lusername = user;
                Properties.Settings.Default.lpassword = pass;
                mainForm.servername = serv;
                mainForm.username = user;
                mainForm.password = pass;
                mainForm.loggedIn = true;
                Program.mform.closeConnectionToolStripMenuItem.Enabled = true;
                mainForm.dh.getChats();
                Program.mform.chatChecker.Enabled = true;
                Properties.Settings.Default.Save();
                return true;
            }
            else
            {
                mainForm.dh = new dataHandler();
                Program.mform.setConInfo("Error: Login Failed. Not Connected.");
                return false;
            }
        }

    }
}
