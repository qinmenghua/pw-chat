using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PW_Chat
{
    public partial class getRoleName : Form
    {
        public getRoleName()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
        }

        private void lookuptbn_Click(object sender, EventArgs e)
        {
            if (mainForm.loggedIn)
            {
                Regex r = new Regex(@"[^0-9]*$");
                String tboxText = r.Split(rIdTextbox.Text)[0];
                Console.WriteLine();
                if (tboxText != "")
                {
                    String d = String.Format("ID: {0} Name: {1}\n", tboxText, mainForm.dh.getRoleName(int.Parse(tboxText)));
                    lookupHistory.AppendText(d);
                    rIdTextbox.Text = "";
                }
                else
                {
                    MessageBox.Show("Invalid role ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You must login first to use this", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
