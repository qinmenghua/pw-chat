using System.Windows.Forms;

namespace PW_Chat
{
    public partial class aboutBox : Form
    {
        public aboutBox()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
            avabox.BackgroundImage = Properties.Resources.ava_small;
        }

        private void aboutBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
