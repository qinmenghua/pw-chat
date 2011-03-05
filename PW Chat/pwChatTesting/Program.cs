using System;
using PW_Chat;

namespace pwChatTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //this is intended just for me...
            //most of the tests done can be done in the GUI also
            //I just have this since its easier for me...
            dataHandler dh = new dataHandler();
            String f;
            if (args.Length >= 1)
            {
                f = args[0];
            } else 
            {
                f = "test.txt";
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(f);
            String rs = dh.saltGen();
            Console.WriteLine(rs);
            sw.WriteLine(rs);
            String lj = "Running login JSon creation(testUser, testPass) :\n";
            Console.WriteLine(lj);
            sw.WriteLine(lj);
            String username = "testUser";
            String password = "testPass";
            String jstring = "{\"username\" : \"" + username + "\", \"password\" : \"" + password + "\"}";
            String json = dh.formJson(jstring);
            Console.WriteLine(jstring);
            sw.WriteLine(jstring);
            Console.WriteLine(json);
            sw.WriteLine(json);
            String ljc = "Login JSon Complete, running Hello World text test:\n";
            Console.WriteLine(ljc);
            sw.WriteLine(ljc);
            String hw = dh.EncryptIt("Hello World");
            String dhw = dh.DecryptIt(hw);
            String e = "Encrypted:" + hw;
            Console.WriteLine(e);
            sw.WriteLine(e);
            String d = "Decrypted:" + dhw;
            Console.WriteLine(d);
            sw.WriteLine(d);
            String hwc = "Hello World test complete, running Hello JSon test:\n";
            Console.WriteLine(hwc);
            sw.WriteLine(hwc);
            String jss = "{\"action\" : \"test\", \"message\" : \"Hello World\"}";
            String jhw = dh.formJson(jss);
            String rjhw = dh.readJson(jhw)["message"].StringValue;
            String ijhw = dh.formJson(jss, true);
            String irjhw = dh.readJson(ijhw)["message"].StringValue;
            dataHandler.encryption = false;
            String ujhw = dh.formJson(jss);
            String urjhw = dh.readJson(ujhw)["message"].StringValue;
            dataHandler.encryption = true;
            String writeline = String.Format("Original Json:{0}\nTransfer Json message (no unique iv):{1}\nDecoded Json Message (no unique iv):{2}\n\nTransfer Json message (unique iv):{3}\nDecoded Json Message(unique iv):{4}\n\nTranfer Json Message(unencrypted):{5}\nDecoded Json Message(unencrypted):{6}\n",
                jss, jhw, rjhw, ijhw, irjhw, ujhw, urjhw);
            Console.WriteLine(writeline);
            sw.WriteLine(writeline);
            String hjc = "\nHello JSon test complete\n";
            Console.WriteLine(hjc);
            sw.WriteLine(hjc);
            sw.Close();
            Console.WriteLine("Press any key to exit.");

            
        }
    }
}
