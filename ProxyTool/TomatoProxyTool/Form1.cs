using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using SagaLib;
using System.Text;

namespace TomatoProxyTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            instance = this;
            comboBox1.SelectedIndex = 0;
        }
        static Form1 instance;

        public static Form1 Instance { get { return instance; } }

        public static ProxyClientManager pm;
        static Thread t;
        static Thread co;
        public static SagaLib.Client client;
        public bool nextproxy = false;
        public bool login = false;
        public Dictionary<int, ProxyClientManager.PP> ProxyIDList = new Dictionary<int, ProxyClientManager.PP>();

        public Dictionary<uint, string> Pictlines = new Dictionary<uint, string>();
        public Dictionary<uint, string> FacePicts = new Dictionary<uint, string>();
        DatFile dat;
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

            t = new Thread(enterkey);
            co = new Thread(loop);
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
            if (PacketsList.SelectedItem == null) return;
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
            if (p.ID == 0x020D && !button8.Enabled)
            {
                ushort offset;
                string tmp3 = "";
                tmp3 += "--------------基础信息------------\r\n";
                tmp3 += "ActorID:" + p.GetUInt(2).ToString() + ",";
                tmp3 += "CharID:" + p.GetUInt(6).ToString() + ",";

                byte size;
                byte[] buf;
                size = p.GetByte(10);
                buf = p.GetBytes(size, 11);
                string res = Global.Unicode.GetString(buf);
                res = res.Replace("\0", "");
                tmp3 += "角色名:" + res + "\r\n";
                tmp3 += "-------------基础外形ID-----------\r\n";
                offset = (ushort)(11 + size);
                tmp3 += "种族:" + getb(p, offset) + " DEM形态:" + getb(p, offset + 1) + " 性别:" + getb(p, offset + 2) + "\r\n";
                tmp3 += "发型ID:" + getus(p, offset + 3) + " 发色:" + getb(p, offset + 5) + " 附发:" + getus(p, offset + 6) + " 脸型:" + getus(p, offset + 9) + "\r\n";
                tmp3 += "尾巴:" + getb(p, offset + 12) + " 翅膀:" + getb(p, offset + 13) + " 翅膀颜色:" + getb(p, offset + 14) + "\r\n";
                tmp3 += "-------------道具外形ID-----------\r\n";
                for (int j = 0; j < 14; j++)
                    tmp3 += ((EnumEquipSlot)j).ToString() + ":" + getui(p, offset + 16 + j * 4) + "\r\n";
                textBox7.Text = tmp3;

                //PICT部分
                offset = (ushort)(11 + size);
                string RaceGender = GetRaceFlag(p.GetByte(offset), p.GetByte((ushort)(offset + 2)));
                string pictline = "10000000," + res + "," + RaceGender + "_00_00_00," + RaceGender + "_01_00_00,," + RaceGender + "_02_00_00," + RaceGender + "_03_01_50,,,,,,,,,,,,,,,,,,,,,,,,,,,,,," + RaceGender + "_01_00_00," + RaceGender + "_24_00_00," + RaceGender + "_25_00_00,";
                string[] npcPict = pictline.Split(',');
                if (p.GetUShort((ushort)(offset + 3)) < 30000)
                    npcPict[6] = RaceGender + "_03_" + getus(p, offset + 3) + "_" + getb(p, offset + 5);
                if (p.GetUShort((ushort)(offset + 6)) < 30000)
                    npcPict[23] = RaceGender + "_04_" + getus(p, offset + 6) + "_" + getb(p, offset + 5);
                if (FacePicts.ContainsKey(p.GetUShort((ushort)(offset + 9))))
                    npcPict[5] = RaceGender + "_" + FacePicts[p.GetUShort((ushort)(offset + 9))];
                for (int j = 0; j < 14; j++)
                {
                    uint pictid = p.GetUInt((ushort)(offset + 16 + j * 4));
                    if (pictid != 0 && Pictlines.ContainsKey(pictid))
                    {
                        string[] ItemPict = Pictlines[pictid].Split(',');
                        if (ItemPict.Length >= 36)
                            for (int i = 36; i <= 38; i++)
                                if (ItemPict[i] == "null")
                                    npcPict[i] = "null";
                        if (ItemPict.Length >= 33)
                        {
                            if (ItemPict[33] != "")
                            {
                                if (npcPict[36].Substring(npcPict[36].Length - 2) == "00" && npcPict[36] != "null")
                                    npcPict[36] = npcPict[36].Substring(0, npcPict[36].Length - 2) + ItemPict[33];
                                if (npcPict[37].Substring(npcPict[37].Length - 2) == "00" && npcPict[37] != "null")
                                    npcPict[37] = npcPict[37].Substring(0, npcPict[37].Length - 2) + ItemPict[33];
                                if (npcPict[38].Substring(npcPict[38].Length - 2) == "00" && npcPict[38] != "null")
                                    npcPict[38] = npcPict[38].Substring(0, npcPict[38].Length - 2) + ItemPict[33];
                            }
                        }
                        for (int i = 2; i < ItemPict.Length; i++)
                        {
                            if (ItemPict[i] != "" && ItemPict[i] != "null" && ItemPict != null)
                            {
                                if (i == 33 || i == 32)
                                    npcPict[i] = ItemPict[i];
                                else
                                    npcPict[i] = RaceGender + "_" + ItemPict[i];
                            }
                        }
                    }
                }
                string tmp4 = "";
                for (int i = 0; i < npcPict.Length; i++)
                    tmp4 += npcPict[i] + ",";
                textBox8.Text = tmp4;
            }

            if (p.ID == 0x01FF && !button8.Enabled)
            {
                ushort offset;
                string tmp3 = "";
                tmp3 += "--------------基础信息------------\r\n";
                tmp3 += "ActorID:" + p.GetUInt(2).ToString() + ",";
                tmp3 += "CharID:" + p.GetUInt(6).ToString() + ",";

                byte size;
                byte[] buf;
                size = p.GetByte(15);
                buf = p.GetBytes(size, 16);
                string res = Global.Unicode.GetString(buf);
                res = res.Replace("\0", "");
                tmp3 += "角色名:" + res + "\r\n";
                tmp3 += "-------------基础外形ID-----------\r\n";
                offset = (ushort)(16 + size);
                tmp3 += "种族:" + getb(p, offset) + " DEM形态:" + getb(p, offset + 1) + " 性别:" + getb(p, offset + 2) + "\r\n";
                tmp3 += "发型ID:" + getus(p, offset + 3) + " 发色:" + getb(p, offset + 5) + " 附发:" + getus(p, offset + 6) + " 脸型:" + getus(p, offset + 9) + "\r\n";
                tmp3 += "尾巴:" + getb(p, offset + 12) + " 翅膀:" + getb(p, offset + 13) + " 翅膀颜色:" + getb(p, offset + 14) + "\r\n";
                tmp3 += "-------------道具外形ID-----------\r\n";
                for (int j = 0; j < 14; j++)
                    tmp3 += ((EnumEquipSlot)j).ToString() + ":" + getui(p, offset + 139 + j * 4) + "\r\n";
                textBox7.Text = tmp3;

                //PICT部分
                offset = (ushort)(16 + size);
                string RaceGender = GetRaceFlag(p.GetByte(offset), p.GetByte((ushort)(offset + 2)));
                string pictline = "10000000,请改名," + RaceGender + "_00_00_00," + RaceGender + "_01_00_00,," + RaceGender + "_02_00_00," + RaceGender + "_03_01_50,,,,,,,,,,,,,,,,,,,,,,,,,,,,,," + RaceGender + "_01_00_00," + RaceGender + "_24_00_00," + RaceGender + "_25_00_00,";
                string[] npcPict = pictline.Split(',');
                if (p.GetUShort((ushort)(offset + 3)) < 30000)
                    npcPict[6] = RaceGender + "_03_" + getus(p, offset + 3) + "_" + getb(p, offset + 5);
                if (p.GetUShort((ushort)(offset + 6)) < 30000)
                    npcPict[23] = RaceGender + "_04_" + getus(p, offset + 6) + "_" + getb(p, offset + 5);
                if (FacePicts.ContainsKey(p.GetUShort((ushort)(offset + 9))))
                    npcPict[5] = RaceGender + "_" + FacePicts[p.GetUShort((ushort)(offset + 9))];
                for (int j = 0; j < 14; j++)
                {
                    uint pictid = p.GetUInt((ushort)(offset + 139 + j * 4));
                    if (pictid != 0 && Pictlines.ContainsKey(pictid))
                    {
                        string[] ItemPict = Pictlines[pictid].Split(',');
                        if (ItemPict.Length >= 36)
                            for (int i = 36; i <= 38; i++)
                                if (ItemPict[i] == "null")
                                    npcPict[i] = "null";
                        if (ItemPict.Length >= 33)
                        {
                            if (ItemPict[33] != "")
                            {
                                if (npcPict[36].Substring(npcPict[36].Length - 2) == "00" && npcPict[36] != "null")
                                    npcPict[36] = npcPict[36].Substring(0, npcPict[36].Length - 2) + ItemPict[33];
                                if (npcPict[37].Substring(npcPict[37].Length - 2) == "00" && npcPict[37] != "null")
                                    npcPict[37] = npcPict[37].Substring(0, npcPict[37].Length - 2) + ItemPict[33];
                                if (npcPict[38].Substring(npcPict[38].Length - 2) == "00" && npcPict[38] != "null")
                                    npcPict[38] = npcPict[38].Substring(0, npcPict[38].Length - 2) + ItemPict[33];
                            }
                        }
                        for (int i = 2; i < ItemPict.Length; i++)
                        {
                            if (ItemPict[i] != "" && ItemPict[i] != "null" && ItemPict != null)
                            {
                                if (i == 33 || i == 32)
                                    npcPict[i] = ItemPict[i];
                                else
                                    npcPict[i] = RaceGender + "_" + ItemPict[i];
                            }
                        }
                    }
                }
                string tmp4 = "";
                for (int i = 0; i < npcPict.Length; i++)
                    tmp4 += npcPict[i] + ",";
                textBox8.Text = tmp4;
            }
        }
        string GetRaceFlag(byte race, byte gender)
        {
            string RaceGender = "";
            switch (gender)
            {
                case 0:
                    switch (race)
                    {
                        case 0:
                            RaceGender = "00";
                            break;
                        case 1:
                            RaceGender = "02";
                            break;
                        case 2:
                            RaceGender = "04";
                            break;
                        case 3:
                            RaceGender = "06";
                            break;
                    }
                    break;
                case 1:
                    switch (race)
                    {
                        case 0:
                            RaceGender = "01";
                            break;
                        case 1:
                            RaceGender = "03";
                            break;
                        case 2:
                            RaceGender = "05";
                            break;
                        case 3:
                            RaceGender = "07";
                            break;
                    }
                    break;
            }
            return RaceGender;
        }
        public enum EnumEquipSlot
        {
            HEAD,
            HEAD_ACCE,
            FACE,
            FACE_ACCE,
            CHEST_ACCE,
            UPPER_BODY,
            LOWER_BODY,
            BACK,
            RIGHT_HAND,
            LEFT_HAND,
            SHOES,
            SOCKS,
            PET,
            EFFECT,
            Unknown,
            /*HEAD = 6,
            CHEST_ACCE = 10,
            RIGHT = 14,
            SHOES = 16,*/
        }
        string getb(Packet p, int offset)
        {
            string s = p.GetByte((ushort)offset).ToString();
            if (s.Length < 2) s = "0" + s;
            return s;
        }
        string getus(Packet p, int offset)
        {
            return p.GetUShort((ushort)offset).ToString();
        }
        string gets(Packet p, int offset)
        {
            return p.GetShort((ushort)offset).ToString();
        }
        string getui(Packet p, int offset)
        {
            return p.GetUInt((ushort)offset).ToString();
        }
        string geti(Packet p,int offset)
        {
            return p.GetInt((ushort)offset).ToString();
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
            catch (Exception ex)
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
                ServerIPBox.Text = "211.13.229.49:12200";
            else if (comboBox1.SelectedItem.ToString() == "本地")
                ServerIPBox.Text = "127.0.0.1:12006";
            else if (comboBox1.SelectedItem.ToString() == "COF")
                ServerIPBox.Text = "127.0.0.1:12006";
            else if (comboBox1.SelectedItem.ToString() == "阿里云")
                ServerIPBox.Text = "101.200.197.1:12006";

        }

        private void Stop_Click_Click(object sender, EventArgs e)
        {
            t.Abort();
            co.Abort();
            ProxyClientManager.Instance.Stop();
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
            catch (Exception ex)
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
            catch (Exception ex)
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            string one = listBox1.SelectedItem.ToString();
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

        private void button7_Click(object sender, EventArgs e)
        {
            //listBox2.Items.Add(PacketsList.SelectedItem);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            string one = listBox2.SelectedItem.ToString();
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
            if (p.ID == 0x020D && !button8.Enabled)
            {
                ushort offset;
                string tmp3 = "";
                tmp3 += "--------------基础信息------------\r\n";
                tmp3 += "ActorID:" + p.GetUInt(2).ToString() + ",";
                tmp3 += "CharID:" + p.GetUInt(6).ToString() + ",";

                byte size;
                byte[] buf;
                size = p.GetByte(10);
                buf = p.GetBytes(size, 11);
                string res = Global.Unicode.GetString(buf);
                res = res.Replace("\0", "");
                tmp3 += "角色名:" + res + "\r\n";
                tmp3 += "-------------基础外形ID-----------\r\n";
                offset = (ushort)(11 + size);
                tmp3 += "种族:" + getb(p, offset) + " DEM形态:" + getb(p, offset + 1) + " 性别:" + getb(p, offset + 2) + "\r\n";
                tmp3 += "发型ID:" + getus(p, offset + 3) + " 发色:" + getb(p, offset + 5) + " 附发:" + getus(p, offset + 6) + " 脸型:" + getus(p, offset + 9) + "\r\n";
                tmp3 += "尾巴:" + getb(p, offset + 12) + " 翅膀:" + getb(p, offset + 13) + " 翅膀颜色:" + getb(p, offset + 14) + "\r\n";
                tmp3 += "-------------道具外形ID-----------\r\n";
                for (int j = 0; j < 14; j++)
                    tmp3 += ((EnumEquipSlot)j).ToString() + ":" + getui(p, offset + 16 + j * 4) + "\r\n";
                textBox7.Text = tmp3;

                //PICT部分
                offset = (ushort)(11 + size);
                string RaceGender = GetRaceFlag(p.GetByte(offset), p.GetByte((ushort)(offset + 2)));
                string pictline = "10000000," + res + "," + RaceGender + "_00_00_00," + RaceGender + "_01_00_00,," + RaceGender + "_02_00_00," + RaceGender + "_03_01_50,,,,,,,,,,,,,,,,,,,,,,,,,,,,,," + RaceGender + "_01_00_00," + RaceGender + "_24_00_00," + RaceGender + "_25_00_00,";
                string[] npcPict = pictline.Split(',');
                if (p.GetUShort((ushort)(offset + 3)) < 30000)
                    npcPict[6] = RaceGender + "_03_" + getus(p, offset + 3) + "_" + getb(p, offset + 5);
                if (p.GetUShort((ushort)(offset + 6)) < 30000)
                    npcPict[23] = RaceGender + "_04_" + getus(p, offset + 6) + "_" + getb(p, offset + 5);
                if (FacePicts.ContainsKey(p.GetUShort((ushort)(offset + 9))))
                    npcPict[5] = RaceGender + "_" + FacePicts[p.GetUShort((ushort)(offset + 9))];
                for (int j = 0; j < 14; j++)
                {
                    uint pictid = p.GetUInt((ushort)(offset + 16 + j * 4));
                    if (pictid != 0 && Pictlines.ContainsKey(pictid))
                    {
                        string[] ItemPict = Pictlines[pictid].Split(',');
                        if (ItemPict.Length >= 36)
                            for (int i = 36; i <= 38; i++)
                                if(ItemPict.Length - 1 >= i)
                                if (ItemPict[i] == "null")
                                    npcPict[i] = "null";
                        if (ItemPict.Length >= 33)
                        {
                            if (ItemPict[33] != "")
                            {
                                if (npcPict[36].Substring(npcPict[36].Length - 2) == "00" && npcPict[36] != "null")
                                    npcPict[36] = npcPict[36].Substring(0, npcPict[36].Length - 2) + ItemPict[33];
                                if (npcPict[37].Substring(npcPict[37].Length - 2) == "00" && npcPict[37] != "null")
                                    npcPict[37] = npcPict[37].Substring(0, npcPict[37].Length - 2) + ItemPict[33];
                                if (npcPict[38].Substring(npcPict[38].Length - 2) == "00" && npcPict[38] != "null")
                                    npcPict[38] = npcPict[38].Substring(0, npcPict[38].Length - 2) + ItemPict[33];
                            }
                        }
                        for (int i = 2; i < ItemPict.Length; i++)
                        {
                            if (ItemPict[i] != "" && ItemPict[i] != "null" && ItemPict != null)
                            {
                                if (i == 33 || i == 32)
                                    npcPict[i] = ItemPict[i];
                                else
                                    npcPict[i] = RaceGender + "_" + ItemPict[i];
                            }
                        }
                    }
                }
                string tmp4 = "";
                for (int i = 0; i < npcPict.Length; i++)
                    tmp4 += npcPict[i] + ",";
                textBox8.Text = tmp4;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog1.FileName) != ".hed")
                    throw new ArgumentException("Can only open header(.hed) file");
                dat = new DatFile();
                dat.Open(openFileDialog1.FileName);
                LoadItemPictCsv(dat.Extract("itempict.csv"));
                LoadFaceCsv(dat.Extract("face_info.csv"));
                label12.Text = "已加载pict数量:" + Pictlines.Count.ToString();
                textBox8.Text = "Pict已加载，选中0x020D/0x01FF封包时，会自动在此显示PICT";
                button8.Enabled = false;
                dat.Close();
            }
        }
        public void LoadItemPictCsv(byte[] buf)
        {
            string[] paras;
            string line;
            MemoryStream ms = new MemoryStream(buf);
            StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GBK"));
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                paras = new string[38];
                paras = line.Split(',');
                if (line.IndexOf('#') != -1)
                    line = line.Substring(0, line.IndexOf('#'));
                if (line == "") continue;
                if (!Pictlines.ContainsKey(uint.Parse(paras[0])))
                    Pictlines.Add(uint.Parse(paras[0]), line);
            }
        }
        public void LoadFaceCsv(byte[] buf)
        {
            string[] paras;
            string line;
            uint faceid;
            string facepict = "";
            MemoryStream ms = new MemoryStream(buf);
            StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GBK"));
            try
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    paras = line.Split(',');
                    if (line.IndexOf('#') != -1)
                        line = line.Substring(0, line.IndexOf('#'));
                    if (line == "") continue;
                    faceid = uint.Parse(paras[1]);
                    if (paras[3] == "")
                    {
                        if (faceid < 1015 && faceid >= 1000)//人族初始脸型
                        {
                            if (paras[2].Length < 2)
                                facepict = "02_00_0" + paras[2];
                            else
                                facepict = "02_00_" + paras[2];
                        }
                        else if (faceid < 1115 && faceid >= 1100)//天族初始脸型
                        {
                            if (paras[2].Length < 2)
                                facepict = "02_01_0" + paras[2];
                            else
                                facepict = "02_01_" + paras[2];
                        }
                        else if (faceid < 1215 && faceid >= 1200)//魔族初始脸型
                        {
                            if (paras[2].Length < 2)
                                facepict = "02_02_0" + paras[2];
                            else
                                facepict = "02_02_" + paras[2];
                        }
                        else if (faceid < 1315 && faceid >= 1300)//DEM初始脸型
                        {
                            if (paras[2].Length < 2)
                                facepict = "02_03_0" + paras[2];
                            else
                                facepict = "02_03_" + paras[2];
                        }
                    }
                    else
                    {
                        if (paras[2].Length < 4)
                        {
                            if (paras[3].Length < 2)
                                facepict = "02_0" + paras[2] + "_0" + paras[3];
                            else
                                facepict = "02_0" + paras[2] + "_" + paras[3];
                        }
                        else
                        {
                            if (paras[3].Length < 2)
                                facepict = "02_" + paras[2] + "_0" + paras[3];
                            else
                                facepict = "02_" + paras[2] + "_" + paras[3];
                        }
                    }
                    if (!FacePicts.ContainsKey(faceid))
                        FacePicts.Add(faceid, facepict);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ms.Close();
            sr.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (Stop_Click.Enabled)
            {
                t.Abort();
                co.Abort();
                ProxyClientManager.Instance.Stop();
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                Stop_Click.Enabled = false;
                login = false;
            }
        }
    }
}
