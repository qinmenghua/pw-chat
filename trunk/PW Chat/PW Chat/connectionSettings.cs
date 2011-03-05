using System;
using System.Windows.Forms;

namespace PW_Chat
{
    public partial class connectionSettings : Form
    {
        public connectionSettings()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
            idleMaxTime.SelectedIndex = 0;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (encryptionenbtn.Checked)
            {
                dataHandler.encryption = true;
            }
            else
            {
                dataHandler.encryption = false;
            }
            if (idleMaxTime.SelectedIndex == 0)
            {
                mainForm.idleMax = -1;
            }
            else
            {
                mainForm.idleMax = Convert.ToInt32(idleMaxTime.SelectedItem.ToString())*60;
                //MessageBox.Show((string)idleMaxTime.SelectedItem.ToString());
                //MessageBox.Show(Convert.ToString(mainForm.idleMax));
            }
            this.Close();
        }
    }
}
