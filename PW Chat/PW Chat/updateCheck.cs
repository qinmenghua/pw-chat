using System;
using System.Net;

namespace PW_Chat
{
    class updateCheck
    {
        public const int thisVersion = 7;
        public int webVersion;
        public bool IsUpToDate;
        public updateCheck()
        {
            webVersion = getCurrentVersion();
            IsUpToDate = isUpToDate();
        }
        private int getCurrentVersion()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add("user-agent", "PW Chat v"+thisVersion);
                int wdata = Convert.ToInt32(wc.DownloadString("http://pw-chat.googlecode.com/svn/trunk/PW%20Chat/version"));
                return wdata;
            }
            catch (WebException)
            {
                //if it can't get the webversion just report -1
                return -1;
            }
        }
        private bool isUpToDate()
        {
            if (webVersion > thisVersion)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
