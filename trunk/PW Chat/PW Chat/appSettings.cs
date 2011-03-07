using System;
using System.Drawing;
using System.Windows.Forms;

namespace PW_Chat
{
    public partial class appSettings : Form
    {
        //make proc persistent during class execution
        private System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
        private System.Diagnostics.PerformanceCounter cpu = new System.Diagnostics.PerformanceCounter();
        private bool cpuBoxE = false;
        //yes these two are different
        private bool CpuBoxE = false;
        public appSettings()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.icon;
            opacityMeter.Value = Properties.Settings.Default.opacity;
            opacityMeterlbl.Text = Properties.Settings.Default.opacity + "%";
            //I know you can't see the text...
            fontColorbtn.Text = fontColorGet();
            winColorbtn.Text = winColorGet();
            fontColorbtnSet(Properties.Settings.Default.textcolor);
            winColorbtnSet(Properties.Settings.Default.wincolor);
            ramUseBox.Text = getMemUse();
            cpu.CategoryName = "Processor";
            cpu.CounterName = "% Processor Time";
            cpu.InstanceName = "_Total";           
            if (Properties.Settings.Default.sound)
            {
                audioenbtn.Checked = true;
                audiodisbtn.Checked = false;
            }
            else
            {
                audioenbtn.Checked = false;
                audiodisbtn.Checked = true;
            }
            if (Properties.Settings.Default.tray)
            {
                trayenbtn.Checked = true;
                traydisbtn.Checked = false;
            }
            else
            {
                trayenbtn.Checked = false;
                traydisbtn.Checked = true;
            }
            emptyMsgSend.Checked = Properties.Settings.Default.emptymsg;
        }

        private String getMemUse()
        {
            //using windows 1 million bytes = 1 mb method instead of 1048576
            //also casting to floats so you can see the decimal point
            float memUse = (float)proc.WorkingSet64 / (float)1000000;
            return Convert.ToString(memUse)+"M";
        }
        private String getCpuUse()
        {
            return Convert.ToString(cpu.NextValue()) + "%";
        }
        private void opacityMeter_Scroll(object sender, EventArgs e)
        {
            int opacity = opacityMeter.Value;
            opacityMeterlbl.Text = opacity + "%";
            Properties.Settings.Default.opacity = opacity;
            //it uses a float between 0 and 1 unlike an int between 0 and 100 like I thought...
            Program.mform.Opacity = (float)opacity*0.01;
            
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            bool audio = false;
            bool tray = false;
            if (audioenbtn.Checked)
            {
                audio = true;
            }
            if (trayenbtn.Checked)
            {
                tray = true;
            }
            Properties.Settings.Default.sound = audio;
            Properties.Settings.Default.tray = tray;
            Properties.Settings.Default.emptymsg = emptyMsgSend.Checked;
            Program.mform.trayIcon.Visible = tray;
            Program.mform.backColor = Properties.Settings.Default.wincolor;
            Program.mform.foreColor = Properties.Settings.Default.textcolor;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void audionoticesmp_Click(object sender, EventArgs e)
        {
            Program.mform.playAlert();
        }

        private void fontColorbtn_Click(object sender, EventArgs e)
        {
            textColor.Color = Properties.Settings.Default.textcolor;
            textColor.ShowDialog();
            Properties.Settings.Default.textcolor = textColor.Color;
            fontColorbtn.Text = fontColorGet();
            fontColorbtnSet(textColor.Color);
        }

        private void winColorbtn_Click(object sender, EventArgs e)
        {
            winColor.Color = Properties.Settings.Default.wincolor;
            winColor.ShowDialog();
            Properties.Settings.Default.wincolor = winColor.Color;
            winColorbtn.Text = winColorGet();
            winColorbtnSet(winColor.Color);
            
        }

        private String fontColorGet()
        {
            String[] tColor = new String[3];
            tColor[0] = Properties.Settings.Default.textcolor.R.ToString();
            tColor[1] = Properties.Settings.Default.textcolor.G.ToString();
            tColor[2] = Properties.Settings.Default.textcolor.B.ToString();
            return String.Format("R:{0} G:{1} B:{2}", tColor[0], tColor[1], tColor[2]);
        }
        private String winColorGet()
        {
            String[] wColor = new String[3];
            wColor[0] = Properties.Settings.Default.wincolor.R.ToString();
            wColor[1] = Properties.Settings.Default.wincolor.G.ToString();
            wColor[2] = Properties.Settings.Default.wincolor.B.ToString();
            return String.Format("R:{0} G:{1} B:{2}", wColor[0], wColor[1], wColor[2]);
        }
        private void fontColorbtnSet(Color textColor)
        {
            fontColorbtn.ForeColor = textColor;
            fontColorbtn.BackColor = textColor;
        }
        private void winColorbtnSet(Color winColor)
        {
            winColorbtn.ForeColor = winColor;
            winColorbtn.BackColor = winColor;
        }

        private void defaultsBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This Resets ALL Settings, including encryption keys!\nThis DOES NOT save after resetting.\nPress OK to continue.", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                fontColorbtnSet(Properties.Settings.Default.dtextcolor);
                winColorbtnSet(Properties.Settings.Default.dwincolor);
                Properties.Settings.Default.textcolor = Properties.Settings.Default.dtextcolor;
                Properties.Settings.Default.wincolor = Properties.Settings.Default.dwincolor;
                Properties.Settings.Default.cryptKey = Properties.Settings.Default.defaultCryptKey;
                Properties.Settings.Default.cryptIV = Properties.Settings.Default.defaultIV;
                Properties.Settings.Default.opacity = 100;
                Program.mform.Opacity = 100;
                opacityMeter.Value = 100;
                opacityMeterlbl.Text = "100%";
                audioenbtn.Checked = true;
                audiodisbtn.Checked = false;
                trayenbtn.Checked = true;
                traydisbtn.Checked = false;
            }
        }

        private void newKeybtn_Click(object sender, EventArgs e)
        {
            dataHandler dh = new dataHandler();
            Object[] t =  dh.genKey();
            String key = System.Convert.ToBase64String((byte[])t[0]);
            String IV = System.Convert.ToBase64String((byte[])t[1]); 
            //Not sure if I want to display hex instead
            //String hexKey = BitConverter.ToString((byte[])t[0]);
            //String hexIV = BitConverter.ToString((byte[])t[1]);
            DialogResult keyBox = MessageBox.Show(String.Format("Key: {0}\nIV: {1}\nKey Size: {2} bit\nPress Cancel to keep old keys", key, IV, t[2]), "Key Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (keyBox == DialogResult.OK)
            {
                Properties.Settings.Default.cryptKey = key;
                Properties.Settings.Default.cryptIV = IV;
            }

        }

        private void showKeybtn_Click(object sender, EventArgs e)
        {
            String ksize = (System.Convert.FromBase64String(Properties.Settings.Default.cryptKey).Length*8).ToString();
            MessageBox.Show(String.Format("Key: {0}\nIV: {1}\nKey Size: {2} bit", Properties.Settings.Default.cryptKey, Properties.Settings.Default.cryptIV, ksize), "Key Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportKeybtn_Click(object sender, EventArgs e)
        {
            String keyData = String.Format("{0}\r\n{1}\r\n", Properties.Settings.Default.cryptKey, Properties.Settings.Default.cryptIV);
            SaveFileDialog keySaver = new SaveFileDialog();
            keySaver.Title = "Key Save Location...";
            keySaver.Filter = "Key files (*.key)|*.key";
            if (keySaver.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter keyWriter = new System.IO.StreamWriter(keySaver.FileName);
                keyWriter.Write(keyData);
                keyWriter.Close();
            }

        }
        private void importKeybtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog keyOpener = new OpenFileDialog();
            keyOpener.Title = "Key Open Location...";
            keyOpener.Filter = "Key files(*.key)|*.key";
            if (keyOpener.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader keyReader = new System.IO.StreamReader(keyOpener.FileName);
                String key = keyReader.ReadLine();
                String iv = keyReader.ReadLine();
                String data = String.Format("Key:{0}\nIV:{1}\nPress OK to import.", key, iv);
                DialogResult mKeyBox = MessageBox.Show(data,"Key Info",MessageBoxButtons.OKCancel, MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);
                if (mKeyBox == DialogResult.OK)
                {
                    Properties.Settings.Default.cryptKey = key;
                    Properties.Settings.Default.cryptIV = iv;
                }

            }


        }

        private void testEncryptionbtn_Click(object sender, EventArgs e)
        {
            dataHandler dh = new dataHandler();
            String ee = dh.EncryptIt("Hello World");
            String d = String.Format("Encrypted:{0}\nDecrypted:{1}\n`?test=YOURAUTHKEY` should show the same!", ee, dh.DecryptIt(ee));
            MessageBox.Show(d,"Encryption Test Results", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void jsonTestbtn_Click(object sender, EventArgs e)
        {
            dataHandler dh = new dataHandler();
            //Form json without using a unique IV as this is for testing json with server
            String tson = dh.formJson("{\"message\" : \"Hello World\"}", false);
            //CodeTitans.JSon.IJSonObject tsson = dh.readJson(tson);
            String tson1 = dh.readJson(tson)["message"].StringValue; //yay no references to the dll here!
            //String tson1 = tsson["message"].StringValue;
            String msg = String.Format("JSON Message:{0}\nOriginal Message:{1}\n`?jtest=YOURAUTHKEY` should show the same!", tson, tson1);
            MessageBox.Show(msg, "JSON Test Results", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void ramTimer_Tick(object sender, EventArgs e)
        {
            ramUseBox.Text = getMemUse();
            if (cpuBoxE)
            {
                cpuUseBox.Text = getCpuUse();
            }
        }

        private void gcbtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This may have adverse effects on performance and stability!\nOnly use this if memory usage is very high for an unknown reason.\nThis is NOT guaranteed to change RAM usage in ANY way.\nPress OK to confirm.", "Warning!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                GC.Collect();
                ramUseBox.Text = getMemUse();
            }
        }

        private void cpuUseBox_Click(object sender, EventArgs e)
        {
            if (!CpuBoxE)
            {
                cpuUseBox.Text = "Retrieving...";
                CpuBoxE = true;
            }
            cpuBoxE = true;
        }    
    }
}
