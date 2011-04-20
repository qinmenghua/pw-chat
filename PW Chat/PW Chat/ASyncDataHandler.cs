using System;
using System.Text;
using CodeTitans.JSon;

namespace PW_Chat
{
    class ASyncDataHandler : dataHandler
    {
        delegate IJSonObject serverData(String data, String servmethod, String aid = null);
        //there is no encrypt only constructor for ASync, old dataHandler is used for that
        public ASyncDataHandler(String u, String p, String s) : base(u, p, s) { }
        public void ASyncSendToServer(String data, String servmethod, String aid = null)
        {

        }
        public void ASyncReadFromServer(IAsyncResult ar)
        {
            
        }
    }
}
