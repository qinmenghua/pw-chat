using System;
using System.Windows.Forms;

namespace PW_Chat
{
    static class Program
    {
        //This was done so other classes can actually call functions in
        //mainForm since default (in MSVC# anyway) is to make form Anonymous... (which is stupid)
        public static mainForm mform;
        [STAThread] //can't use File Open/Save dialogs w/o this
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Ensure that user settable keys exist
            if (Properties.Settings.Default.cryptKey.Length == 0 || Properties.Settings.Default.cryptIV.Length == 0)
            {
                Properties.Settings.Default.cryptKey = Properties.Settings.Default.defaultCryptKey;
                Properties.Settings.Default.cryptIV = Properties.Settings.Default.defaultIV;
                Properties.Settings.Default.Save();
            }
            //check for updates if it has been longer then a week since last check
            if ((mainForm.getNow() - 604800) > Properties.Settings.Default.lucheck)
            {
                //dont open mainForm if user clicked ok (which brings up download list)
                if (updateChecker(true))
                {
                    Properties.Settings.Default.lucheck = mainForm.getNow();
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Application.Exit();
                }
            }
            mform = new mainForm();
            Application.Run(mform);
        }
        internal static bool updateChecker(bool onlyShowIfUpdateAvail = false)
        {
            updateCheck uc = new updateCheck();
            String version = Convert.ToString(uc.webVersion);
            String thisVersion = Convert.ToString(updateCheck.thisVersion);
            String upToDate = uc.IsUpToDate ? "You are up to date." : "You should update.\nPress OK To update";
            MessageBoxButtons dialogButtons = uc.IsUpToDate ? MessageBoxButtons.OK : MessageBoxButtons.OKCancel;
            DialogResult dr;
            bool r = true;
            if (uc.webVersion != -1)
            {
                if (uc.IsUpToDate && onlyShowIfUpdateAvail)
                {
                    return true;
                }
                else
                {
                    dr = MessageBox.Show(String.Format("Current Version: {0}\nWeb Version: {1}\n{2}", thisVersion, version, upToDate), "Update Check", dialogButtons, MessageBoxIcon.Information);
                }
            }
            else
            {
                dr = MessageBox.Show("Unable to get current version", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!uc.IsUpToDate && dr == DialogResult.OK)
            {
                //set to false so mainForm doesn't open on init updateCheck
                r = false;
                Help.ShowHelp(Form.ActiveForm, "http://code.google.com/p/pw-chat/downloads/list");
            }
            return r;
        }
    }
}
