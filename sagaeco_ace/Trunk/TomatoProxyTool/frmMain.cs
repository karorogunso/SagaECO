using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using SagaLib;

namespace TomatoProxyTool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            instance = this;
            comboBox1.SelectedIndex = 0;
        }
        static frmMain instance;

        public static frmMain Instance { get { return instance; } }

        public static ProxyClientManager pm;
        static Thread t = new Thread(enterkey);
        static Thread co = new Thread(loop);
        public static SagaLib.Client client;
        public bool nextproxy = false;
        public bool login = false;
        public Dictionary<int, ProxyClientManager.PP> ProxyIDList = new Dictionary<int, ProxyClientManager.PP>();

        private void Launch_Click(object sender, EventArgs e)
        {
            string[] IPPort = ServerIPBox.Text.ToString().Split(':');
            ProxyClientManager.Instance.IP = IPPort[0];
            ProxyClientManager.Instance.port = int.Parse(IPPort[1]);
            ProxyClientManager.Instance.Start();
            ProxyClientManager.Instance.StartNetwork(int.Parse(ToolPortBox.Text.ToString()));
            PacketInfoBox.Clear();
            PacketInfoBox.Text += "Lisening port:" + ProxyClientManager.Instance.port + "....";
            Global.clientMananger = (ClientManager)ProxyClientManager.Instance;

            ProxyClientManager.Instance.packetContainerClient = PacketContainer.Instance.packetsClientLogin;
            ProxyClientManager.Instance.packetContainerServer = PacketContainer.Instance.packetsLogin;
            ProxyClientManager.Instance.packets = PacketContainer.Instance.packets;

            t.Start();
            co.Start();

            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            Stop_Click.Enabled = true;
        }
        static void loop()
        {
            while (true)
            {
                ProxyClientManager.Instance.NetworkLoop(10);
                if (pm != null)
                {
                    if (pm.ready)
                    {
                        pm.NetworkLoop(10);
                    }
                }
                else
                {

                }
                System.Threading.Thread.Sleep(1);
            }
        }
        static void enterkey()
        {
            /*while (true)
            { }*/
        }

        private void PacketsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string one = PacketsList.SelectedItem.ToString();
            string[] ones = one.Split(',');
            string type = ones[2];
            int index = int.Parse(ones[3]);
            byte currentIndex = byte.Parse(ones[4]);

            Packet p = new Packet();
            switch (currentIndex)
            {
                case 0:
                    if (type == "Client")
                        p = ProxyClientManager.Instance.packetContainerClient[index];
                    else if (type == "Server")
                        p = ProxyClientManager.Instance.packetContainerServer[index];
                    break;
                case 1:
                    if (type == "Client")
                        p = pm.packetContainerClient[index];
                    else if (type == "Server")
                        p = pm.packetContainerServer[index];
                    break;
            }
            string tmp = "Opcode:0x{1:X4}\r\nLength:{3}";
            string tmp2 = p.DumpData();
            tmp = string.Format(tmp, type, p.ID, p.ToString(), p.data.Length, "{0}");
            if (ProxyIDList.Keys.Contains(p.ID))
                tmp += string.Format("\r\nClass:{0}\r\nDataLength:{1}", ProxyIDList[p.ID].name, ProxyIDList[p.ID].length);
            PacketInfoBox.Text = tmp;
            PacketDataBox.Text = tmp2;

        }

        private void PacketInfoBox_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string tmp = PacketInfoBox.SelectedText.Replace(" ", "");
                tmp = tmp.Replace("\r\n", "");
                if (tmp.Length >= 9)
                    textBox1.Text = "太大";
                else
                    textBox1.Text = Conversions.ToInteger(tmp).ToString();
                byte[] buf = SagaLib.Conversions.HexStr2Bytes(tmp);
                textBox4.Text = System.Text.Encoding.UTF8.GetString(buf);
            }
            catch (Exception)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PacketsList.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BF = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            string path = DateTime.Now.ToString().Replace('/', '-');
            path = path.Replace(' ', '_');
            path = path.Replace(':', '-');
            System.IO.FileStream fs = new System.IO.FileStream(path + ".dat", System.IO.FileMode.Create);
            BF.Serialize(fs, PacketContainer.Instance);
            fs.Close();*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "日服")
                ServerIPBox.Text = "211.13.229.2:12200";
            else if (comboBox1.SelectedItem.ToString() == "本地")
                ServerIPBox.Text = "127.0.0.1:12006";
            else if (comboBox1.SelectedItem.ToString() == "COF")
                ServerIPBox.Text = "127.0.0.1:12006";

        }

        private void Stop_Click_Click(object sender, EventArgs e)
        {
            t.Abort();
            co.Abort();

            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            Stop_Click.Enabled = false;
            login = false;
        }

        private void SendPacket_Click(object sender, EventArgs e)
        {
            if (MapRB.Checked && ReceiveRB.Checked)
            {
                Packets.Server.SendUniversal p = new TomatoProxyTool.Packets.Server.SendUniversal();
                byte[] buf = Conversions.HexStr2Bytes(PacketDataBox.Text.Replace(" ", "").Replace("\r\n", ""));
                p.data = buf;
                ((ServerSession)(client)).OnSendUniversal(p, true);
            }
        }

        private void PacketDataBox_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string tmp = PacketDataBox.SelectedText.Replace(" ", "");
                tmp = tmp.Replace("\r\n", "");
                if (tmp.Length >= 9)
                    textBox1.Text = "太大";
                else
                    textBox1.Text = Conversions.ToInteger(tmp).ToString();
                byte[] buf = SagaLib.Conversions.HexStr2Bytes(tmp);
                textBox4.Text = System.Text.Encoding.UTF8.GetString(buf);
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextproxy = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true) button1.Enabled = true;
            if (checkBox4.Checked == false) button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = PacketInfoBox.Text;
            textBox2.Text = PacketDataBox.Text;
            textBox5.Text = textBox1.Text;
            textBox4.Text = textBox6.Text;
        }

        private void textBox2_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string tmp = textBox2.SelectedText.Replace(" ", "");
                tmp = tmp.Replace("\r\n", "");
                if (tmp.Length >= 9)
                    textBox5.Text = "太大";
                else
                    textBox5.Text = Conversions.ToInteger(tmp).ToString();
                byte[] buf = SagaLib.Conversions.HexStr2Bytes(tmp);
                textBox6.Text = System.Text.Encoding.UTF8.GetString(buf);
            }
            catch (Exception)
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                ProxyIDList = ProxyClientManager.Instance.LoadProxyListFromLocal("./Server");
                button6.Text = "已完成加载: " + ProxyIDList.Count.ToString() + "个";
                button6.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void showmessage(string text)
        {
            MessageBox.Show(text);
        }
    }
}
