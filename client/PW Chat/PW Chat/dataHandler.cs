using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CodeTitans.JSon; //this should be the only class that uses this

namespace PW_Chat
{
    public class dataHandler
    {
        //This is to enable or disable encryption for network data
        public static bool encryption = true;
        //authkey that is defined in the server since php sessions are
        //a bit buggy with this
        public static String authkey = "";
        public static byte[] authiv = new byte[0];
        private String servername;
        private String username;
        private String password;
        private byte[] cryptKey = Convert.FromBase64String(Properties.Settings.Default.cryptKey);
        private byte[] cryptIV = Convert.FromBase64String(Properties.Settings.Default.cryptIV);
        public static int cid = -1;
        public dataHandler()
        {
            //dummy constructor used for encryption only initialization
        }
        public dataHandler(String s, String u, String p)
        {
            //constructor used for encryption and data sending
            servername = s;
            username = u;
            password = p;
        }
        public dataHandler(String s)
        {
            //sending data no auth constructor, never used, possible future use
            servername = s;
        }
        //create a salt to send with messages
        //helps protect encryption key
        //32 is equal to one block (8 bit chars * 32 = 256)
        //48 is used since it is guaranteed to split between at least 2 blocks
        public String saltGen(int amount = 48)
        {
            String saltSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String randSalt = null;
            //inline new Random().Next() doesn't work right
            Random r = new Random();
            for(int i = 0; i < amount; i++)
            {
                randSalt += saltSet.Substring(r.Next(0, (saltSet.Length)-1), 1);
            }
            return randSalt;
        }
        public bool login()
        {
            String jstring = "{\"username\" : \"" + username + "\", \"password\" : \"" + password + "\", \"salt\" : \"" + saltGen() + "\"}";
            String json = formJson(jstring);
            Console.WriteLine(jstring);
            Console.WriteLine(json);

            IJSonObject sr = readJson(sendToServer(json, "login"));
            if (sr["login"].BooleanValue)
            {
                authkey = sr["aid"].StringValue;
                authiv = Convert.FromBase64String(sr["aiv"].StringValue);
                authkey = EncryptIt(authkey, null, authiv);
                Console.WriteLine(authkey);
                return true;
            }
            return false;
            
        }
        public bool logout()
        {
            /*IJSonObject sr = readJson(sendToServer(formJson("{\"logout\" : 1, \"salt\" : \"" + saltGen() + "\"}"), "logout"));
            if (sr["logout"].BooleanValue)
            {*/
                //changed auth behavior so a 'logout' isn't needed anymore
                //its pretty superficial now...
                //other then nulling the auth params
                authkey = "";
                authiv = new byte[0];
                Console.WriteLine("Logout sucess");
                return true;
            /*}
            Console.WriteLine("Logout fail");
            return false;*/
        }
        public bool SendMessage(String message)
        {
            String json = formJson("{\"msg\" : \"" + message + "\", \"username\" : \"" + username + "\", \"password\" : \"" + password + "\", \"salt\" : \"" + saltGen() + "\"}");
            IJSonObject sr = readJson(sendToServer(json, "sendmsg"));
            Console.WriteLine(json);
            bool r = sr["broadcast"].BooleanValue;
            Console.WriteLine("Send Message " + (r ? "Success" : "Fail"));
            return r;
        }
        public void getChats(int cid = -1, int num = -100, int limit = -1)
        {
            IJSonObject sr = readJson(sendToServer(formJson("{\"num\" : " + cid + ", \"limit\" : \""+ num +"\"}"), "getmsgs"));
            //do nothing if there aren't any chats in the first place
            if (sr != null)
            {
                int l = sr.Length;
                for (int i = 0; i < l; i++)
                {
                    try
                    {
                        Program.mform.mainTextBox.AppendText(String.Format("U:{0}, T:{1}, CD:{2}, D:{3}, MSG:{4}\n", sr[i]["uid"].Int32Value, sr[i]["type"].StringValue, sr[i]["chldst"].Int32Value, sr[i]["time"].StringValue, sr[i]["msg"].StringValue));
                        dataHandler.cid = sr[i]["cid"].Int32Value;
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Error appending text to mainTextBox");
                    }
                }
                if (Properties.Settings.Default.sound)
                {
                    Program.mform.playAlert();
                }
            }
        }
        public String sendToServer(String data, String servmethod, String aid = null)
        {
            aid = aid ?? authkey;
            //don't forget the Uri.EscapseDataString...
            aid = "auth=" + Uri.EscapeDataString(aid);
            data = "json=" + Uri.EscapeDataString(data);
            String aiv = "aiv=" + Uri.EscapeDataString(Convert.ToBase64String(authiv));
            Console.WriteLine(data);
            if (!servername.ToLower().Contains("http://"))
            {
                //add http:// in front of url if it doesn't have it
                servername = "http://" + servername;
            }
            //noerr is for my php server... it turns off error reporting
            //String thisServ = servername + "?" + servmethod + "&noerr" + "&" + aid + "&" + aiv;
            String thisServ = String.Format("{0}?{1}&{2}&{3}&noerr", servername, servmethod, aid, aiv);
            Console.WriteLine(thisServ);
            try
            {   
                //cast needed because HttpWebRequest is an extension of WebRequest
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(thisServ);
                req.UserAgent = "PW Chat v" + updateCheck.thisVersion;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] bdata = Encoding.ASCII.GetBytes(data);
                req.ContentLength = bdata.Length;
                Stream sw = req.GetRequestStream();
                sw.Write(bdata, 0, bdata.Length);
                sw.Flush();
                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(rep.GetResponseStream());
                String r = sr.ReadToEnd();
                Console.WriteLine(r);
                sw.Close();
                sr.Close();
                return r;
            }
            catch (WebException)
            {
                //any errors caused by the network won't make a kersplode
                Console.WriteLine("Unable to connect to server");
                //yes I know this is weird, I might change this eventually (doubt it)
                return formJson("{\"connectfail\" : 1, \"login\" : 0, \"broadcast\" : 0, \"getmsgs\" : 0}");
            }

        }
        public Object[] genKey()
        {
            RijndaelManaged key = new RijndaelManaged();
            key.BlockSize = 256;
            key.GenerateKey();
            key.GenerateIV();
            Object[] d = new Object[3];
            d[0] = key.Key;
            d[1] = key.IV;
            d[2] = key.KeySize;
            return d;

        }
        public String EncryptIt(String s, byte[] key = null, byte[] IV = null, PaddingMode padding = PaddingMode.PKCS7)
        {
            String result;
            //magically assign key and IV if one isn't given as an argument
            key = key ?? cryptKey;
            IV = IV ?? cryptIV;
            RijndaelManaged rijn = new RijndaelManaged();
            rijn.Mode = CipherMode.CBC;
            rijn.Padding = padding;
            rijn.BlockSize = 256;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = rijn.CreateEncryptor(key, IV))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(s);
                        }
                    }
                }
                result = Convert.ToBase64String(msEncrypt.ToArray());
            }
            rijn.Clear();

            return result;
        }
        public String DecryptIt(String s, byte[] key = null, byte[] IV = null, PaddingMode padding = PaddingMode.PKCS7)
        {
            String result;
            //magically assign key and IV if one isn't given as an argument
            key = key ?? cryptKey;
            IV = IV ?? cryptIV;
            RijndaelManaged rijn = new RijndaelManaged();
            rijn.Mode = CipherMode.CBC;
            rijn.Padding = padding;
            rijn.BlockSize = 256;

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(s)))
            {
                using (ICryptoTransform decryptor = rijn.CreateDecryptor(key, IV))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                        {
                            result = swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            rijn.Clear();
            return result;
        }
        //data parameter MUST be a valid JSON object already
        //the iv parameter specifies if a new IV should be generated and sent with json message
        //by default for security a new IV is used each message
        public String formJson(String data, bool iv = true)
        {
            JSonWriter jw = new JSonWriter();
            Object payload;
            SHA512Managed checksum = new System.Security.Cryptography.SHA512Managed();
            String sha512 = System.Convert.ToBase64String(checksum.ComputeHash(Encoding.ASCII.GetBytes(data)));
            if (encryption)
            {
                String e;
                if (iv)
                {
                    RijndaelManaged rijn = new RijndaelManaged();
                    rijn.BlockSize = 256;
                    e = EncryptIt(data, Convert.FromBase64String(Properties.Settings.Default.cryptKey), rijn.IV);
                    payload = new Object[4] { Convert.ToInt32(encryption), sha512, e, Convert.ToBase64String(rijn.IV) };
                }
                else
                {
                    e = EncryptIt(data);
                    payload = new Object[3] { Convert.ToInt32(encryption), sha512, e };
                }
            }
            else 
            {
                payload =  new Object[3]{ Convert.ToInt32(encryption), sha512, Convert.ToBase64String(Encoding.ASCII.GetBytes(data)) };
            }
            jw.Write(payload);
            return (jw.ToString());
        }
        //this returns the json object as defined for data transfer
        public IJSonObject readJson(String data)
        {
            JSonReader jr = new JSonReader();
            IJSonObject ijo = jr.ReadAsJSonObject(data);
            String jdata;
            SHA512Managed checksum = new System.Security.Cryptography.SHA512Managed();
            if (ijo[0].BooleanValue)
            {
                //using ijo[3].StringValue != null throws ArrayOutOfBounds exception
                if (ijo.Count > 3)
                {
                    jdata = DecryptIt(ijo[2].StringValue, Convert.FromBase64String(Properties.Settings.Default.cryptKey), Convert.FromBase64String(ijo[3].StringValue));
                }
                else
                {
                    jdata = DecryptIt(ijo[2].StringValue);
                }
            }
            else
            {
                jdata = Encoding.ASCII.GetString(Convert.FromBase64String(ijo[2].StringValue));
            }
            String sha512 = Convert.ToBase64String(checksum.ComputeHash(Encoding.ASCII.GetBytes(jdata)));
            String osha512 = ijo[1].StringValue;
            IJSonObject r;
            //make sure the message is identical to original
            if (osha512 == sha512)
            {
                r = jr.ReadAsJSonObject(jdata);
                               
            }
            else
            {
                //if this comes up data doesn't match the checksum
                r = null;
            }
            return r;
        }
    }
}
