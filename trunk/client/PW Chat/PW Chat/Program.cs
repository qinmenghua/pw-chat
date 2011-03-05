using System;
using System.Windows.Forms;

namespace PW_Chat
{
    static class Program
    {
        //This was done so other classes can actually call functions in
        //mainForm since default (in MSVC# anyway) is to make form Anonymous... (which is stupid)
        public static mainForm mform;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Check to see if the user has made keys yet, if not set them
            if (Properties.Settings.Default.cryptKey == "")
            {
                Properties.Settings.Default.cryptKey = Properties.Settings.Default.defaultCryptKey;
                Properties.Settings.Default.cryptIV = Properties.Settings.Default.defaultIV;
                Properties.Settings.Default.Save();
            }
            //See note above public static mainForm mform
            mform = new mainForm();
            Application.Run(mform);
        }
    }
}
