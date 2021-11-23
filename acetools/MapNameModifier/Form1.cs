using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml;

namespace MapNameModifier
{
    public enum PC_JOB
    {
        NOVICE = 0,
        SWORDMAN = 1,
        BLADEMASTER = 3,
        BOUNTYHUNTER = 5,
        FENCER = 11,
        KNIGHT = 13,
        DARKSTALKER = 15,
        SCOUT = 21,
        ASSASSIN = 23,
        COMMAND = 25,
        ARCHER = 31,
        STRIKER = 33,
        GUNNER = 35,
        WIZARD = 41,
        SORCERER = 43,
        SAGE = 45,
        SHAMAN = 51,
        ELEMENTER = 53,
        ENCHANTER = 55,
        VATES = 61,
        DRUID = 63,
        BARD = 65,
        WARLOCK = 71,
        CABALIST = 73,
        NECROMANCER = 75,
        TATARABE = 81,
        BLACKSMITH = 83,
        MACHINERY = 85,
        FARMASIST = 91,
        ALCHEMIST = 93,
        MARIONEST = 95,
        RANGER = 101,
        EXPLORER = 103,
        TREASUREHUNTER = 105,
        MERCHANT = 111,
        TRADER = 113,
        GAMBLER = 115,
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            OD.ShowDialog();
            string[] files;
            FD.ShowDialog();
            files = System.IO.Directory.GetFiles(FD.SelectedPath);
            List<string> filenames = new List<string>();
            foreach (string i in files)
            {
                filenames.Add(System.IO.Path.GetFileName(i));
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, System.Text.Encoding.GetEncoding(textBox1.Text));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                if (line == "") continue;
                if (line.Substring(0, 1) == "#")
                    continue;
                paras = line.Split(',');

                for (int i = 0; i < paras.Length; i++)
                {
                    if (paras[i] == "")
                        paras[i] = "0";
                }
                uint id = uint.Parse(paras[0]);
                string name = paras[1];
                if (filenames.Contains(id + ".map"))
                {
                    string path = files[filenames.IndexOf(id + ".map")];
                    byte[] buf = System.Text.Encoding.UTF8.GetBytes(name);
                    byte[] buff = new byte[32];
                    int size = buf.Length;
                    if (size > 32) size = 32;
                    Array.Copy(buf, 0, buff, 0, size);
                    System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                    fs.Position = 4;
                    fs.Write(buff, 0, 32);
                    fs.Close();
                }
            }
            sr.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OD.Filter = "*.csv|*.csv|*.ini|*.ini|*.cs|*.cs|*.txt|*.txt";
            OD.ShowDialog();
            System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, System.Text.Encoding.GetEncoding(textBox1.Text));
            string content = sr.ReadToEnd();
            sr.Close();
            //byte[] buff = System.Text.Encoding.GetEncoding("latin1").GetBytes(content);
            //content = System.Text.Encoding.GetEncoding("big5").GetString(buff);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(OD.FileName, false, System.Text.Encoding.GetEncoding("utf-8"));
            sw.Write(content);
            sw.Flush();
            sw.Close();

        }

        public enum QuestType
        {
            HUNT = 1,
            GATHER,
            CAPTURE,
            TRANSPORT,
            SPECIAL,
        }

        class QuestInfo
        {
            public uint id;
            public string name, comment;
            public QuestType type;
            public QuestDetail detail;
            public int time;
            public uint rewarditem;
            public byte rewardcount;
            public byte requiredquestpoint;
            public uint dungeonID;
            public byte minlv, maxlv;
            public string flag;
            public string jobtype, race, gender, job;

        }

        class QuestDetail
        {
            public uint id;
            public string comment;
            public uint exp, jexp, gold;
            public byte fame;
            public uint mapid1, mapid2, mapid3;
            public uint id1, id2, id3;
            public uint count1, count2, count3;
            public uint npcid1, npcid2;
            public bool party;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<uint, QuestDetail> detail = new Dictionary<uint, QuestDetail>();
            Dictionary<string, List<QuestInfo>> quest = new Dictionary<string, List<QuestInfo>>();
            FD.ShowDialog();
            if (System.IO.File.Exists(FD.SelectedPath + "\\quest.csv") && System.IO.File.Exists(FD.SelectedPath + "\\subjugate.csv")
                && System.IO.File.Exists(FD.SelectedPath + "\\gather.csv")
                && System.IO.File.Exists(FD.SelectedPath + "\\transport.csv"))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(FD.SelectedPath + "\\subjugate.csv", System.Text.Encoding.GetEncoding("gbk"));
                int count = 0;
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();
                    string[] paras;
                    QuestDetail item;
                    try
                    {
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#")
                            continue;
                        paras = line.Split(',');

                        for (int i = 0; i < paras.Length; i++)
                        {
                            if (paras[i] == "" || paras[i] == "-1")
                                paras[i] = "0";
                        }
                        item = new QuestDetail();
                        item.id = uint.Parse(paras[0]);
                        item.comment = paras[1];
                        item.exp = uint.Parse(paras[2]);
                        item.jexp = uint.Parse(paras[3]);
                        item.gold = uint.Parse(paras[4]);
                        item.fame = byte.Parse(paras[5]);
                        item.mapid1 = uint.Parse(paras[17].Replace("-", ""));
                        item.mapid2 = uint.Parse(paras[18].Replace("-", ""));
                        item.mapid3 = uint.Parse(paras[19].Replace("-", ""));
                        item.id1 = uint.Parse(paras[20]);
                        item.count1 = uint.Parse(paras[21]);
                        item.id2 = uint.Parse(paras[23]);
                        item.count2 = uint.Parse(paras[24]);
                        item.id3 = uint.Parse(paras[26]);
                        item.count3 = uint.Parse(paras[27]);
                        if (paras[29] == "1")
                            item.party = true;
                        detail.Add(item.id, item);

                        count++;
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                sr.Close();

                sr = new System.IO.StreamReader(FD.SelectedPath + "\\gather.csv", System.Text.Encoding.GetEncoding("gbk"));
                count = 0;
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();
                    try
                    {
                        string[] paras;
                        QuestDetail item;
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#")
                            continue;
                        paras = line.Split(',');

                        for (int i = 0; i < paras.Length; i++)
                        {
                            if (paras[i] == "")
                                paras[i] = "0";
                        }
                        item = new QuestDetail();
                        item.id = uint.Parse(paras[0]);
                        item.comment = paras[1];
                        item.exp = uint.Parse(paras[2]);
                        item.jexp = uint.Parse(paras[3]);
                        item.gold = uint.Parse(paras[4]);
                        item.fame = byte.Parse(paras[5]);
                        item.id1 = uint.Parse(paras[17]);
                        item.count1 = uint.Parse(paras[18]);
                        item.id2 = uint.Parse(paras[20]);
                        item.count2 = uint.Parse(paras[21]);
                        item.id3 = uint.Parse(paras[23]);
                        item.count3 = uint.Parse(paras[24]);

                        detail.Add(item.id, item);

                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                sr.Close();

                sr = new System.IO.StreamReader(FD.SelectedPath + "\\transport.csv", System.Text.Encoding.GetEncoding("gbk"));
                count = 0;
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();
                    try
                    {
                        string[] paras;
                        QuestDetail item;
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#")
                            continue;
                        paras = line.Split(',');

                        for (int i = 0; i < paras.Length; i++)
                        {
                            if (paras[i] == "")
                                paras[i] = "0";
                        }
                        item = new QuestDetail();
                        item.id = uint.Parse(paras[0]);
                        item.comment = paras[1];
                        item.npcid1 = uint.Parse(paras[2]);
                        item.npcid2 = uint.Parse(paras[3]);
                        item.exp = uint.Parse(paras[4]);
                        item.jexp = uint.Parse(paras[5]);
                        item.gold = uint.Parse(paras[6]);
                        item.fame = byte.Parse(paras[7]);
                        item.id1 = uint.Parse(paras[9]);
                        item.count1 = uint.Parse(paras[10]);
                        item.id2 = uint.Parse(paras[11]);
                        item.count2 = uint.Parse(paras[12]);
                        item.id3 = uint.Parse(paras[13]);
                        item.count3 = uint.Parse(paras[14]);

                        detail.Add(item.id, item);

                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                sr.Close();

                sr = new System.IO.StreamReader(FD.SelectedPath + "\\quest.csv", System.Text.Encoding.GetEncoding("gbk"));
                count = 0;
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();
                    string[] paras;
                    QuestInfo item;
                    try
                    {
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#")
                            continue;
                        paras = line.Split(',');

                        for (int i = 0; i < paras.Length; i++)
                        {
                            if (paras[i] == "")
                                paras[i] = "0";
                            if (paras[i] == "CONTAINER4")
                                paras[i] = "10022204";
                            if (paras[i] == "CONTAINER5")
                                paras[i] = "10022205";
                            if (paras[i] == "CONTAINER6")
                                paras[i] = "10022206";
                            if (paras[i] == "TREASURE_BOX11")
                                paras[i] = "10022111";                            
                        }
                        item = new QuestInfo();
                        item.id = uint.Parse(paras[0]);
                        item.name = paras[1];
                        item.comment = paras[2];
                        item.type = (QuestType)int.Parse(paras[3]);
                        item.detail = detail[uint.Parse(paras[4])];
                        item.time = int.Parse(paras[5]);
                        item.job = "NONE";
                        if (paras[7] != "FIGHTER" && paras[7] != "SPELLUSER" && paras[7] != "BACKPACKER" && paras[7] != "0")
                        {
                            item.job = paras[7];
                            item.jobtype = "NOVICE";
                        }
                        else
                        {
                            if (paras[7] == "0")
                                paras[7] = "NOVICE";
                            item.jobtype = paras[7];
                        }
                        if (paras[8] == "HUM")
                            paras[8] = "EMIL";
                        if (paras[8] == "TIT")
                            paras[8] = "TITANIA";
                        if (paras[8] == "DOM")
                            paras[8] = "DOMINION";
                        if (paras[8] == "0")
                            paras[8] = "NONE";
                        item.race = paras[8];
                        if (paras[9] == "0")
                            paras[9] = "NONE";
                        item.gender = paras[9];
                        
                        item.flag = paras[10];
                        item.rewarditem = uint.Parse(paras[12]);
                        item.rewardcount = byte.Parse(paras[13]);
                        item.requiredquestpoint =byte.Parse(paras[14]);
                        item.dungeonID = uint.Parse(paras[15]);
                        item.minlv = (byte)int.Parse(paras[16]);
                        item.maxlv = (byte)int.Parse(paras[17]);

                        List<QuestInfo> list;
                        if (quest.ContainsKey(item.flag))
                            list = quest[item.flag];
                        else
                        {
                            list = new List<QuestInfo>();
                            quest.Add(item.flag, list);
                        } 
                        list.Add(item);

                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                sr.Close();

                int o = 1;
                foreach (string j in quest.Keys)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.AppendChild(xml.CreateComment("Flag:" + j));
                    XmlElement root = xml.CreateElement("QuestDB");
                    XmlAttribute attr = xml.CreateAttribute("GroupID");
                    attr.InnerText = o.ToString();
                    root.Attributes.Append(attr);
                    List<QuestInfo> tmp = quest[j];
                    foreach (QuestInfo i in tmp)
                    {
                        XmlElement child = xml.CreateElement("Quest");
                        XmlElement att;
                        child.AppendChild(xml.CreateComment(i.comment));
                        att = xml.CreateElement("ID");
                        att.InnerText = i.id.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("Name");
                        att.InnerText = i.name;
                        child.AppendChild(att);
                        att = xml.CreateElement("Type");
                        att.InnerText = i.type.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("Time");
                        att.InnerText = i.time.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("RewardItem");
                        att.InnerText = i.rewarditem.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("RewardCount");
                        att.InnerText = i.rewardcount.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("RequiredQuestPoints");
                        att.InnerText = i.requiredquestpoint.ToString();
                        child.AppendChild(att);
                        if (i.dungeonID != 0)
                        {

                        }
                        att = xml.CreateElement("DungeonID");
                        att.InnerText = i.dungeonID.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("MinLV");
                        att.InnerText = i.minlv.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("MaxLv");
                        att.InnerText = i.maxlv.ToString();
                        child.AppendChild(att);
                        att = xml.CreateElement("Job");
                        att.InnerText = i.job;
                        child.AppendChild(att);
                        att = xml.CreateElement("JobType");
                        att.InnerText = i.jobtype;
                        child.AppendChild(att);
                        att = xml.CreateElement("Race");
                        att.InnerText = i.race;
                        child.AppendChild(att);
                        att = xml.CreateElement("Gender");
                        att.InnerText = i.gender;
                        child.AppendChild(att);

                        child.AppendChild(xml.CreateComment("Detail Information"));

                        switch (i.type)
                        {
                            case QuestType.HUNT:
                                child.AppendChild(xml.CreateComment(i.detail.comment));
                                att = xml.CreateElement("EXP");
                                att.InnerText = i.detail.exp.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("JEXP");
                                att.InnerText = i.detail.jexp.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("Gold");
                                att.InnerText = i.detail.gold.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("Fame");
                                att.InnerText = i.detail.fame.ToString();
                                child.AppendChild(att);
                                if (i.detail.mapid1 != 0)
                                {
                                    att = xml.CreateElement("MapID1");
                                    att.InnerText = i.detail.mapid1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.mapid2 != 0)
                                {
                                    att = xml.CreateElement("MapID2");
                                    att.InnerText = i.detail.mapid2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.mapid3 != 0)
                                {
                                    att = xml.CreateElement("MapID3");
                                    att.InnerText = i.detail.mapid3.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id1 != 0)
                                {
                                    att = xml.CreateElement("ObjectID1");
                                    att.InnerText = i.detail.id1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count1 != 0)
                                {
                                    att = xml.CreateElement("Count1");
                                    att.InnerText = i.detail.count1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id2 != 0)
                                {
                                    att = xml.CreateElement("ObjectID2");
                                    att.InnerText = i.detail.id2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count2 != 0)
                                {
                                    att = xml.CreateElement("Count2");
                                    att.InnerText = i.detail.count2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id3 != 0)
                                {
                                    att = xml.CreateElement("ObjectID3");
                                    att.InnerText = i.detail.id3.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count3 != 0)
                                {
                                    att = xml.CreateElement("Count3");
                                    att.InnerText = i.detail.count3.ToString();
                                    child.AppendChild(att);
                                }
                                att = xml.CreateElement("Party");
                                att.InnerText = i.detail.party.ToString();
                                child.AppendChild(att);
                                break;
                            case QuestType.GATHER:
                                child.AppendChild(xml.CreateComment(i.detail.comment));
                                att = xml.CreateElement("EXP");
                                att.InnerText = i.detail.exp.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("JEXP");
                                att.InnerText = i.detail.jexp.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("Gold");
                                att.InnerText = i.detail.gold.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("Fame");
                                att.InnerText = i.detail.fame.ToString();
                                child.AppendChild(att);
                                if (i.detail.id1 != 0)
                                {
                                    att = xml.CreateElement("ObjectID1");
                                    att.InnerText = i.detail.id1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count1 != 0)
                                {
                                    att = xml.CreateElement("Count1");
                                    att.InnerText = i.detail.count1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id2 != 0)
                                {
                                    att = xml.CreateElement("ObjectID2");
                                    att.InnerText = i.detail.id2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count2 != 0)
                                {
                                    att = xml.CreateElement("Count2");
                                    att.InnerText = i.detail.count2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id3 != 0)
                                {
                                    att = xml.CreateElement("ObjectID3");
                                    att.InnerText = i.detail.id3.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count3 != 0)
                                {
                                    att = xml.CreateElement("Count3");
                                    att.InnerText = i.detail.count3.ToString();
                                    child.AppendChild(att);
                                }
                                break;
                            case QuestType.TRANSPORT:
                                child.AppendChild(xml.CreateComment(i.detail.comment));
                                att = xml.CreateElement("EXP");
                                att.InnerText = i.detail.exp.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("JEXP");
                                att.InnerText = i.detail.jexp.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("Gold");
                                att.InnerText = i.detail.gold.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("Fame");
                                att.InnerText = i.detail.fame.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("NPCSource");
                                att.InnerText = i.detail.npcid1.ToString();
                                child.AppendChild(att);
                                att = xml.CreateElement("NPCDestination");
                                att.InnerText = i.detail.npcid2.ToString();
                                child.AppendChild(att);
                                if (i.detail.id1 != 0)
                                {
                                    att = xml.CreateElement("ObjectID1");
                                    att.InnerText = i.detail.id1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count1 != 0)
                                {
                                    att = xml.CreateElement("Count1");
                                    att.InnerText = i.detail.count1.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id2 != 0)
                                {
                                    att = xml.CreateElement("ObjectID2");
                                    att.InnerText = i.detail.id2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count2 != 0)
                                {
                                    att = xml.CreateElement("Count2");
                                    att.InnerText = i.detail.count2.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.id3 != 0)
                                {
                                    att = xml.CreateElement("ObjectID3");
                                    att.InnerText = i.detail.id3.ToString();
                                    child.AppendChild(att);
                                }
                                if (i.detail.count3 != 0)
                                {
                                    att = xml.CreateElement("Count3");
                                    att.InnerText = i.detail.count3.ToString();
                                    child.AppendChild(att);
                                }
                                break;
                        }
                        root.AppendChild(child);
                    }
                    xml.AppendChild(root);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(FD.SelectedPath + "\\QuestDB_" + o.ToString() + ".xml", false, Encoding.UTF8);
                    xml.Save(sw);
                    sw.Flush();
                    sw.Close();
                    o++;
                }
            }
        }

        byte[] Decrypt(byte[] buf, byte key)
        {
            byte v3 = key;
            byte v4 = key;
            for (int i = 0; i < buf.Length; i++)
            {
                byte v9 = buf[i];
                byte v8 = buf[i];
                if (v8 >= 97 && v8 <= 122 || v8 >= 65 && v8 <= 90 || v8 >= 48 && v8 <= 57)
                {
                    v8 = (byte)(v3 ^ v9);
                    if (v8 < 97 || v8 > 122)
                    {
                        if (v8 < 65 || v8 > 90)
                        {
                            if (v8 < 48 || v8 > 57)
                                v8 = buf[i];
                        }
                    }
                }
                buf[i] = v8;
                byte v10 = (byte)(v3 * v8 * (i+1) % v4);
                if (v10 <= 1)
                {
                    v10 = (byte)(v3 * (v8 + key + 1) % v4);
                    if (v10 <= 1)
                    {
                        v10 = (byte)(v3 * v8 % v4 + 2);
                    }
                }
                key++;
                v3 = v10;
            }
            return buf;
        }

        byte[] Encrypt(byte[] buf, byte key)
        {
            byte v3 = key;
            byte v4 = key;
            for (int i = 0; i < buf.Length; i++)
            {
                byte v9 = buf[i];
                byte v8 = buf[i];
                if (v8 >= 97 && v8 <= 122 || v8 >= 65 && v8 <= 90 || v8 >= 48 && v8 <= 57)
                {
                    v8 = (byte)(v3 ^ v9);
                    if (v8 < 97 || v8 > 122)
                    {
                        if (v8 < 65 || v8 > 90)
                        {
                            if (v8 < 48 || v8 > 57)
                                v8 = buf[i];
                        }
                    }
                }
                byte v10 = (byte)(v3 * buf[i] * (i + 1) % v4);
                if (v10 <= 1)
                {
                    v10 = (byte)(v3 * (buf[i] + key + 1) % v4);
                    if (v10 <= 1)
                    {
                        v10 = (byte)(v3 * buf[i] % v4 + 2);
                    }
                }
                buf[i] = v8;                
                key++;
                v3 = v10;
            }
            return buf;
        }

        byte[] test(byte[] buf1, byte[] buf2)
        {
            for (int i = 0; i < buf1.Length; i++)
            {
                buf1[i] = (byte)(buf1[i] ^ buf2[i]);
                
            }
            return buf1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == DialogResult.OK)
            {
                string[] files = System.IO.Directory.GetFiles(FD.SelectedPath);
                foreach (string i in files)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(i, System.Text.Encoding.GetEncoding(textBox1.Text));
                    string content = sr.ReadToEnd();
                    sr.Close();
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(i, false, System.Text.Encoding.GetEncoding("gbk"));
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        Dictionary<uint, uint[]> droptable;
        private void button5_Click(object sender, EventArgs e)
        {
            OD.Filter = "monsterdrop.csv|monsterdrop.csv";
            droptable = new Dictionary<uint, uint[]>();
            OD.ShowDialog();
           
            System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, System.Text.Encoding.GetEncoding("gbk"));
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    uint id = uint.Parse(paras[0]);
                    uint[] drops = new uint[7];
                    drops[0] = uint.Parse(paras[1]);
                    drops[1] = uint.Parse(paras[2]);
                    drops[2] = uint.Parse(paras[3]);
                    drops[3] = uint.Parse(paras[4]);
                    drops[4] = uint.Parse(paras[5]);
                    drops[5] = uint.Parse(paras[6]);
                    drops[6] = uint.Parse(paras[7]);

                    droptable.Add(id, drops);
                    count++;
                }
                catch (Exception)
                {

                }
            }
            sr.Close();

            OD.Filter = "monster.csv|monster.csv";
            OD.ShowDialog();

            sr = new System.IO.StreamReader(OD.FileName, System.Text.Encoding.GetEncoding("utf-8"));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(OD.FileName + ".csv", false, System.Text.Encoding.GetEncoding("utf-8"));

            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "")
                    {
                        sw.WriteLine();
                        continue;
                    }
                    if (line.Substring(0, 1) == "#")
                    {
                        sw.WriteLine(line);
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    uint id = uint.Parse(paras[99]);
                    if (droptable.ContainsKey(id))
                    {
                        uint[] drops = droptable[id];
                        if (paras[100] != "0" && paras[100] != "null")
                            paras[100] = paras[100] + "|" + drops[0].ToString();
                        if (paras[101] != "0" && paras[101] != "null")
                            paras[101] = paras[101] + "|" + drops[1].ToString();
                        if (paras[102] != "0" && paras[102] != "null")
                            paras[102] = paras[102] + "|" + drops[2].ToString();
                        if (paras[103] != "0" && paras[103] != "null")
                            paras[103] = paras[103] + "|" + drops[3].ToString();
                        if (paras[104] != "0" && paras[104] != "null")
                            paras[104] = paras[104] + "|" + drops[4].ToString();
                        if (paras[105] != "0" && paras[105] != "null")
                            paras[105] = paras[105] + "|" + drops[5].ToString();
                        if (paras[106] != "0" && paras[106] != "null")
                            paras[106] = paras[106] + "|" + drops[6].ToString();

                    }

                    string output = "";
                    for (int i = 0; i < paras.Length; i++)
                    {
                        output = output + paras[i] + ",";
                    }
                    output = output.Substring(0, output.Length - 1);
                    sw.WriteLine(output);
                }
                catch (Exception)
                {

                }
            }
            sr.Close();
            sw.Flush();
            sw.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] src = tbSrc.Text.Split('\n');
            string NPCNAME = "";
            string PET = "";
            bool judge = false;
            bool talk = false;
            bool choice = false;
            bool item = false;
            tbDst.Text = "";
            foreach (string i in src)
            {
                try
                {
                    string j = i.Replace("\r", "");
                    string[] buf = j.Split(',');
                    string[] command = buf[2].Split(' ');
                    if (buf[3] != "")
                    {
                        tbDst.Text += ("//" + buf[3] + "\r\n");
                    }
                    if (buf[1] != "" && !choice)
                    {
                        tbDst.Text += ("//" + buf[1] + "\r\n");
                    }
                    switch (command[0].ToLower())
                    {
                        case "judge":
                            judge = true;
                            tbDst.Text += "if(";
                            break;
                        case "flag":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                if (command[1] == "OFF")
                                    tbDst.Text += "!";
                                tbDst.Text += ("_" + command[2] + " ");
                            }
                            else
                            {
                                tbDst.Text += ("_" + command[2] + " = ");
                                if (command[1] == "OFF")
                                    tbDst.Text += "false;\r\n";
                                if (command[1] == "ON")
                                    tbDst.Text += "true;\r\n";
                            }
                            break;
                        case "me.lv":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                tbDst.Text += "pc.Level ";
                                if (command[1] == "=")
                                    tbDst.Text += "=";
                                tbDst.Text += (command[1] + " ");
                                tbDst.Text += command[2];
                                tbDst.Text += " ";
                            }
                            break;
                        case "me.fame":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                tbDst.Text += "pc.Fame ";
                                if (command[1] == "=")
                                    tbDst.Text += "=";
                                tbDst.Text += (command[1] + " ");
                                tbDst.Text += command[2];
                                tbDst.Text += " ";
                            }
                            break;
                        case "me.is_admin":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                tbDst.Text += "pc.Account.GMLevel ";
                                if (command[1] == "=")
                                    tbDst.Text += "=";
                                tbDst.Text += (command[1] + " ");
                                tbDst.Text += command[2];
                                tbDst.Text += " ";
                            }
                            break;
                        case "pre-itemget":
                            {
                                string[] tmp = command[1].Split(':');
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                tbDst.Text += string.Format("CheckInventory(pc, {0}, {1}) ", tmp[0], tmp[1]);
                            }
                            break;
                        case "true":
                            if (judge)
                            {
                                if (item)
                                {
                                    tbDst.Text += ")\r\n{\r\n if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == " + PET + ")\r\n  {\r\n    Call(";
                                    tbDst.Text += command[1];
                                    tbDst.Text += ");\r\n    return;\r\n  }\r\n}\r\n";
                                    item = false;
                                    PET = "";
                                }
                                else
                                {
                                    tbDst.Text += ")\r\n{\r\n    Call(";
                                    tbDst.Text += command[1];
                                    tbDst.Text += ");\r\n    return;\r\n}\r\n";
                                }
                            }
                            break;
                        case "false":
                            if (judge)
                            {
                                if (command[1] != "NONE")
                                {
                                    tbDst.Text += "else\r\n{\r\n    Call(";
                                    tbDst.Text += command[1];
                                    tbDst.Text += ");\r\n    return;\r\n}\r\n";
                                }
                                judge = false;
                            }
                            break;
                        case "item":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                if (command[1] == "GET")
                                {
                                    tbDst.Text += "CountItem(pc, ";
                                    tbDst.Text += command[2];
                                    tbDst.Text += ") >= ";
                                    if (command.Length > 3)
                                        tbDst.Text += (command[3] + " ");
                                    else
                                        tbDst.Text += "1 ";

                                }
                                else if (command[1] == "EQUIP")
                                {
                                    tbDst.Text += "pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET)";
                                    PET = command[2];
                                    item = true;
                                }
                            }
                            else
                            {
                                if (command[1] == "GET")
                                    tbDst.Text += "GiveItem(pc, ";
                                else if (command[1] == "LOST")
                                    tbDst.Text += "TakeItem(pc, ";
                                tbDst.Text += command[2];
                                if (command.Length > 3)
                                    tbDst.Text += (", " + command[3]);
                                else
                                    tbDst.Text += ", 1";
                                tbDst.Text += ");\r\n";

                            }
                            break;
                            
                        case "\"talk":
                            if (command[1] == "START")
                            {
                                talk = true;
                                tbDst.Text = tbDst.Text + "Say(pc, " + command[4] + ", " + command[2] + ", ";
                                NPCNAME = command[5];
                            }
                            else if (command[1] == "END")
                            {
                                talk = false;
                                if (NPCNAME != "")
                                {
                                    NPCNAME = NPCNAME.Replace("\"\"\"", "\\\"");
                                    NPCNAME = NPCNAME.Replace("\"\"", "\\\"");
                                    tbDst.Text = tbDst.Text + ", \"" + NPCNAME + "\");\r\n";
                                    NPCNAME = "";
                                }
                                else
                                    tbDst.Text = tbDst.Text + ");\r\n";
                            }
                            break;

                        case "talk":
                            if (command[1] == "START")
                            {
                                talk = true;
                                tbDst.Text = tbDst.Text + "Say(pc, " + command[4] + ", " + command[2] + ", ";
                                NPCNAME = command[5];
                            }
                            else if (command[1] == "END")
                            {
                                talk = false;
                                if (NPCNAME != "")
                                {
                                    NPCNAME = NPCNAME.Replace("\"\"\"", "\\\"");
                                    NPCNAME = NPCNAME.Replace("\"\"", "\\\"");
                                    tbDst.Text = tbDst.Text + ", \"" + NPCNAME + "\");\r\n";
                                    NPCNAME = "";
                                }
                                else
                                    tbDst.Text = tbDst.Text + ");\r\n";
                            }
                            break;//*/
                        case "se":
                        case "jin":
                            tbDst.Text += "PlaySound(pc, ";
                            tbDst.Text += (command[1] + ", false, ");
                            tbDst.Text += (command[4] + ", ");
                            tbDst.Text += (command[5] + ");\r\n");

                            break;
                        case "\"choice":
                            {
                                command = buf[2].Split('\"');
                                choice = true;
                                tbDst.Text += string.Format("switch(Select(pc, \"{0}\", \"{1}\"", command[3], command[5]);
                            }
                            break;
                        case "selected":
                            if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) == "\"")
                                tbDst.Text += "))\r\n{\r\n";
                            tbDst.Text += string.Format("    case {0}:\r\n        {1} {2};\r\n        break;\r\n", command[1].Replace("@", ""), command[2], command[3]);
                            break;
                        case "choice":
                            if (command[1] == "END")
                            {
                                tbDst.Text += "}\r\n";
                                choice = false;
                            }
                            break;
                        case "waitframe":
                            tbDst.Text += string.Format("Wait(pc, {0});\r\n", int.Parse(command[1]) / 30 * 1000);
                            break;
                        case "skillpbonus":
                            tbDst.Text += string.Format("SkillPointBonus(pc, {0});\r\n", command[1]);
                            break;
                        case "medic":
                            tbDst.Text += "Heal(pc);\r\n";
                            break;
                        case "effect":
                        case "effect_one":
                            switch (int.Parse(command[2]))
                            {
                                case 0:
                                    tbDst.Text += string.Format("ShowEffect(pc, {0}, {1}, {2});\r\n", command[3], command[4], command[1]);
                                    break;
                                case 1:
                                    tbDst.Text += string.Format("ShowEffect(pc, {0});\r\n", command[1]);
                                    break;
                                case 2:
                                    tbDst.Text += string.Format("ShowEffect(pc, {0}, {1});\r\n", command[3], command[1]);
                                    break;
                            }
                            break;
                        case "istrancehost":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += "&& ";
                                tbDst.Text += (" pc.PossesionedActors.Count != 0");
                            }
                            break;
                        case "me.job":
                            if (judge)
                            {
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) != "(")
                                    tbDst.Text += " && ";
                                if (command[1] == "=")
                                {
                                    tbDst.Text += "pc.Job == (PC_JOB)" + command[2];
                                }
                                else if (command[1] == "<" || command[1] == ">")
                                {
                                    tbDst.Text += "pc.Job " + command[1] + "(PC_JOB)" + command[2];
                                }
                            }
                            break;
                        default:
                            if (talk)
                            {
                                NPCNAME = NPCNAME.Replace("\"\"\"", "\\\"");
                                NPCNAME = NPCNAME.Replace("\"\"", "\\\"");
                                if (tbDst.Text.Substring(tbDst.Text.Length - 1, 1) == "\"")
                                    tbDst.Text += " +\r\n    ";
                                string txt = "";
                                foreach (string k in command)
                                {
                                    string a;
                                    a = k.Replace("\"\"\"", "\\\"");
                                    txt += a;
                                }
                                tbDst.Text += "\"" + txt + "$R;\"";
                            }
                            else if (choice)
                            {
                                tbDst.Text += string.Format(", \"{0}\"", command[0]);
                            }
                            else
                                tbDst.Text += ("//" + buf[2] + "\r\n");
                            break;
                    }
                }
                catch { }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tbDst.Text = "";
            tbSrc.Text = "";
        }

        class SpawnInfo
        {
            public byte x, y;
            public uint mob, map;
            public int amount;
            public int delay;
            public int range;
            public string comment = "";
        }

        Dictionary<uint, List<SpawnInfo>> spawns;

        private void button8_Click(object sender, EventArgs e)
        {
            OD.Filter = "monstergroup.csv|monstergroup.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";
            spawns = new Dictionary<uint, List<SpawnInfo>>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("gbk"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    SpawnInfo info = new SpawnInfo();
                    info.map = uint.Parse(paras[0]);
                    int width, height;
                    width = int.Parse(paras[4]) - int.Parse(paras[2]) + 1;
                    height = int.Parse(paras[5]) - int.Parse(paras[3]) + 1;
                    info.x = (byte)(int.Parse(paras[2]) + width / 2);
                    info.y = (byte)(int.Parse(paras[3]) + height / 2);
                    info.range = (width + height) / 4;
                    info.mob = uint.Parse(paras[6]);
                    info.amount = (int)(int.Parse(paras[9]) * 0.7f);
                    if (info.amount == 0) info.amount = 1;
                    width = int.Parse(paras[11]) - int.Parse(paras[10]);
                    width = width / 2;
                    width = int.Parse(paras[10]) + width;
                    width = width * 60;
                    if (width == 0) width = 10;
                    info.delay = width;
                    info.comment = paras[14];
                    if (spawns.ContainsKey(info.map))
                    {
                        spawns[info.map].Add(info);
                    }
                    else
                    {
                        List<SpawnInfo> list = new List<SpawnInfo>();
                        list.Add(info);
                        spawns.Add(info.map, list);
                    }
                }
                catch { }
            }
            sr.Close();
            foreach (uint j in spawns.Keys)
            {
                XmlDocument xml = new XmlDocument();
                XmlElement root = xml.CreateElement("Spawns");
                List<SpawnInfo> tmp = spawns[j];
                foreach (SpawnInfo i in tmp)
                {
                    if (i.comment == "栽培用")
                        continue;
                    if (i.x == 0 && i.y == 0 && i.range == 0)
                        continue;
                    XmlElement child = xml.CreateElement("spawn");
                    XmlElement att;
                    child.AppendChild(xml.CreateComment(i.comment));
                    att = xml.CreateElement("id");
                    att.InnerText = i.mob.ToString();
                    child.AppendChild(att);
                    att = xml.CreateElement("map");
                    att.InnerText = i.map.ToString();
                    child.AppendChild(att);
                    att = xml.CreateElement("x");
                    att.InnerText = i.x.ToString();
                    child.AppendChild(att);
                    att = xml.CreateElement("y");
                    att.InnerText = i.y.ToString();
                    child.AppendChild(att);
                    att = xml.CreateElement("amount");
                    att.InnerText = i.amount.ToString();
                    child.AppendChild(att);
                    att = xml.CreateElement("range");
                    att.InnerText = i.range.ToString();
                    child.AppendChild(att);
                    att = xml.CreateElement("delay");
                    att.InnerText = i.delay.ToString();
                    child.AppendChild(att);

                    root.AppendChild(child);
                }
                xml.AppendChild(root);
                xml.Save(new System.IO.StreamWriter(folder + j.ToString() + ".xml", false, System.Text.Encoding.UTF8));               
            }
        }

        class skillInfo
        {
            public uint id;
            public uint level;
            public int cast, delay;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Dictionary<string, skillInfo> infos = new Dictionary<string, skillInfo>();
            OD.Filter = "skill.csv|skill.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("gbk"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }

                    skillInfo info = new skillInfo();
                    info.id = uint.Parse(paras[0]);
                    info.level = uint.Parse(paras[9]);
                    info.cast = int.Parse(paras[15]);
                    info.delay = int.Parse(paras[16]);

                    infos.Add(info.id + "|" + info.level, info);
                }
                catch { }
            }
            sr.Close();

            OD.Filter = "skillDB.csv|skillDB.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            path = OD.FileName;
            sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("gbk"));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path + ".csv", false, System.Text.Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        sw.WriteLine(line);
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    uint id = uint.Parse(paras[0]);
                    uint level = uint.Parse(paras[4]);
                    if (infos.ContainsKey(id + "|" + level))
                    {
                        skillInfo info = infos[id + "|" + level];
                        paras[8] = info.cast.ToString();
                        paras[9] = info.delay.ToString();
                    }
                    string buf = "";
                    foreach (string i in paras)
                    {
                        buf += (i + ",");
                    }
                    sw.WriteLine(buf.Substring(0, buf.Length - 1));
                }
                catch { }
            }
            sr.Close();
            sw.Flush();
            sw.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> names = new Dictionary<string, string>();
            
            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, System.Text.Encoding.GetEncoding(textBox1.Text));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }

                    string id = paras[0];
                    string[] index = textBox2.Text.Split(',');
                    names.Add(id, paras[int.Parse(index[0])]);
                }
                catch { }
            }
            sr.Close();

            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding(textBox1.Text));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path + ".csv", false, System.Text.Encoding.GetEncoding(textBox1.Text));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        sw.WriteLine(line);
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    string id = paras[0];
                    if (names.ContainsKey(id))
                    {
                        string[] index = textBox2.Text.Split(',');
                        if (index.Length > 1)
                            paras[int.Parse(index[1])] = names[id];
                        else
                            paras[int.Parse(index[0])] = names[id];
                    }
                    string buf = "";
                    foreach (string i in paras)
                    {
                        buf += (i + ",");
                    }
                    sw.WriteLine(buf.Substring(0, buf.Length - 1));
                }
                catch { }
            }
            sr.Close();
            sw.Flush();
            sw.Close();
        }

        class SyntheseInfo
        {
            public uint id;
            public string comment;
            public ushort skillid;
            public byte skilllv;            
            public uint gold;
            public uint requiredTool;
            public List<ItemElement> material = new List<ItemElement>();
            public List<ItemElement> product = new List<ItemElement>();
        }

        class ItemElement
        {
            public uint id;
            public ushort count;
            public int rate;
        }

        List<SyntheseInfo> synthese;
        private void button11_Click(object sender, EventArgs e)
        {
            synthese = new List<SyntheseInfo>();
            OD.Filter = "item_synthe1.csv|item_synthe1.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";
            spawns = new Dictionary<uint, List<SpawnInfo>>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
            string lastComment = "";
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        lastComment = line.Substring(1);
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1" || paras[i] == "null")
                            paras[i] = "0";
                    }

                    SyntheseInfo info = new SyntheseInfo();
                    info.comment = lastComment;
                    info.id = uint.Parse(paras[0]);
                    info.skillid = ushort.Parse(paras[1]);
                    info.skilllv = byte.Parse(paras[2]);
                    info.gold = uint.Parse(paras[3]);
                    info.requiredTool = uint.Parse(paras[4]);
                    for (int i = 0; i < 4; i++)
                    {
                        if (paras[5 + i * 2] == "0")
                            continue;
                        ItemElement item = new ItemElement();
                        item.id = uint.Parse(paras[5 + i * 2]);
                        item.count = ushort.Parse(paras[6 + i * 2]);
                        info.material.Add(item);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        if (paras[13 + i * 3] == "0")
                            continue;
                        ItemElement item = new ItemElement();
                        item.id = uint.Parse(paras[13 + i * 3]);
                        item.count = ushort.Parse(paras[14 + i * 3]);
                        item.rate = int.Parse(paras[15 + i * 3]);
                        info.product.Add(item);
                    }

                    synthese.Add(info);
                    lastComment = "";
                }
                catch { }
            }
            sr.Close();
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("SyntheseDB");
            foreach (SyntheseInfo i in synthese)
            {
                if (i.comment != "")
                {
                    root.AppendChild(xml.CreateComment(i.comment));
                }

                XmlElement child = xml.CreateElement("Synthese");
                XmlElement att;
                att = xml.CreateElement("ID");
                att.InnerText = i.id.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("SkillID");
                att.InnerText = i.skillid.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("SkillLv");
                att.InnerText = i.skilllv.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("Gold");
                att.InnerText = i.gold.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("RequiredTool");
                att.InnerText = i.requiredTool.ToString();
                child.AppendChild(att);

                foreach (ItemElement j in i.material)
                {
                    att = xml.CreateElement("Material");
                    XmlAttribute attr = xml.CreateAttribute("id");
                    attr.InnerText = j.id.ToString();
                    att.Attributes.Append(attr);
                    attr = xml.CreateAttribute("count");
                    attr.InnerText = j.count.ToString();
                    att.Attributes.Append(attr); 
                    child.AppendChild(att);
                }
                foreach (ItemElement j in i.product)
                {
                    att = xml.CreateElement("Product");
                    XmlAttribute attr = xml.CreateAttribute("id");
                    attr.InnerText = j.id.ToString();
                    att.Attributes.Append(attr);
                    attr = xml.CreateAttribute("rate");
                    attr.InnerText = j.rate.ToString();
                    att.Attributes.Append(attr);
                    attr = xml.CreateAttribute("count");
                    attr.InnerText = j.count.ToString();
                    att.Attributes.Append(attr);
                    child.AppendChild(att);
                }
                root.AppendChild(child);
            }
            xml.AppendChild(root);
            xml.Save(new System.IO.StreamWriter(folder +"SyntheseDB.xml", false, System.Text.Encoding.UTF8));

        }

        class ShopInfo
        {
            public uint id;
            public uint sellrate;
            public uint buyrate;
            public uint buylimit;
            public List<uint> goods = new List<uint>();
            public string comment;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            List<ShopInfo> shops = new List<ShopInfo>();
            OD.Filter = "shoplist.csv|shoplist.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";
            spawns = new Dictionary<uint, List<SpawnInfo>>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
            string lastComment = "";
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        lastComment = line.Substring(1);                        
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1" || paras[i] == "null")
                            paras[i] = "0";
                    }

                    ShopInfo info = new ShopInfo();
                    info.id = uint.Parse(paras[0]);
                    info.sellrate = uint.Parse(paras[1]);
                    info.buyrate = uint.Parse(paras[2]);
                    info.buylimit = uint.Parse(paras[3]);
                    info.comment = lastComment;
                    
                    for (int i = 0; i < 12; i++)
                    {
                        if (paras[4 + i] == "0")
                            continue;
                        info.goods.Add(uint.Parse(paras[4 + i]));
                    }

                    shops.Add(info);
                    lastComment = "";
                }
                catch { }
            }
            sr.Close();

            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("ShopDB");
            foreach (ShopInfo i in shops)
            {
                if (i.comment != "")
                {
                    root.AppendChild(xml.CreateComment(i.comment));
                }
                XmlElement child = xml.CreateElement("Shop");
                XmlElement att;
                att = xml.CreateElement("ID");
                att.InnerText = i.id.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("SellRate");
                att.InnerText = i.sellrate.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("BuyRate");
                att.InnerText = i.buyrate.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("BuyLimit");
                att.InnerText = i.buylimit.ToString();
                child.AppendChild(att);

                foreach (uint j in i.goods)
                {
                    att = xml.CreateElement("Goods");
                    att.InnerText = j.ToString();
                    child.AppendChild(att);                   
                }
                
                root.AppendChild(child);
            }
            xml.AppendChild(root);
            xml.Save(new System.IO.StreamWriter(folder + "ShopDB.xml", false, System.Text.Encoding.UTF8));
        }

        class treasureInfo
        {
            public uint id;
            public int rate;
            public int count;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<treasureInfo>> treasures = new Dictionary<string, List<treasureInfo>>();
            List<string> groups = new List<string>();
            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";
           
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1" || paras[i] == "null")
                            paras[i] = "0";
                    }
                    int count = paras.Length / 3;
                    if (treasures.Count == 0)
                    {
                        for (int i = 0; i < (paras.Length + 1) / 3; i++)
                        {
                            treasures.Add(paras[i * 3], new List<treasureInfo>());
                            groups.Add(paras[i * 3]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < (paras.Length + 1) / 3; i++)
                        {
                            string group = groups[i];
                            treasureInfo info = new treasureInfo();
                            info.id = uint.Parse(paras[i * 3]);
                            info.rate = int.Parse(paras[1 + i * 3]);
                            info.count = int.Parse(paras[2 + i * 3]);
                            if (info.rate == 0)
                                continue;                            
                            treasures[group].Add(info);
                        }
                    }
                }
                catch { }
            }
            sr.Close();

            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("TreasureDB");
            foreach (string i in treasures.Keys)
            {
                XmlElement child = xml.CreateElement("TreasureList");
                XmlAttribute att = xml.CreateAttribute("GroupName");
                att.InnerText = i;
                child.Attributes.Append(att);
                foreach (treasureInfo j in treasures[i])
                {
                    XmlElement item = xml.CreateElement("Item");
                    XmlAttribute rate = xml.CreateAttribute("rate");
                    XmlAttribute count = xml.CreateAttribute("count");
                    rate.InnerText = j.rate.ToString();
                    count.InnerText = j.count.ToString();
                    item.InnerText = j.id.ToString();
                    item.Attributes.Append(rate);
                    item.Attributes.Append(count);
                    child.AppendChild(item);
                }
                root.AppendChild(child);
            }
            xml.AppendChild(root);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(folder + System.IO.Path.GetFileNameWithoutExtension(path) + ".xml", false, System.Text.Encoding.UTF8);
            xml.Save(sw);
            xml = null;
            sw.Flush();
            sw.Close();
        }

        class sspHeader
        {
            public uint offset;
            public uint size;
        }

        class effect
        {
            public uint id;
            public string name, desc;
            public byte active, maxLv, lv, range, target, target2, effectRange;
            public ushort mp, sp, ap;
            public int equipFlag, skillFlag;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            List<sspHeader> header = new List<sspHeader>();
            List<effect> skills = new List<effect>();
            OD.Filter = "effect.ssp|effect.ssp";
            System.Text.Encoding encoder = System.Text.Encoding.Unicode;
            if (OD.ShowDialog() == DialogResult.OK)
            {
                string path = OD.FileName;
                string folder = System.IO.Path.GetDirectoryName(path) + "\\";
                string filename = System.IO.Path.GetFileNameWithoutExtension(path);
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                for (int i = 0; i < 10000; i++)
                {
                    sspHeader newHead = new sspHeader();
                    newHead.offset = br.ReadUInt32();
                    if (newHead.offset != 0)
                        header.Add(newHead);
                }
                for (int i = 0; i < 10000; i++)
                {
                    if (i < header.Count)
                    {
                        header[i].size = br.ReadUInt16();
                    }
                    else
                        fs.Position += 2;
                }
                foreach (sspHeader i in header)
                {
                    fs.Position = i.offset;
                    effect skill = new effect();
                    skill.id = br.ReadUInt16();
                    string buf = encoder.GetString(br.ReadBytes(128));
                    skill.name = buf.Remove(buf.IndexOf("\0"));
                    buf = encoder.GetString(br.ReadBytes(504));
                    skill.desc = buf.Remove(buf.IndexOf("\0"));
                    skill.active = br.ReadByte();
                    skill.maxLv = br.ReadByte();
                    skill.lv = br.ReadByte();
                    fs.Position += 1;
                    skill.mp = br.ReadUInt16();
                    skill.sp = br.ReadUInt16();
                    skill.ap = br.ReadUInt16();
                    skill.range = br.ReadByte();
                    skill.target = br.ReadByte();
                    skill.target2 = br.ReadByte();
                    skill.effectRange = br.ReadByte();
                    skill.equipFlag = br.ReadInt32();
                    fs.Position += 4;
                    skill.skillFlag = br.ReadInt32();
                    skills.Add(skill);
                }
                fs.Close();
                if (textBox2.Text == "skill")
                {
                    header.Clear();
                    if (OD.ShowDialog() == DialogResult.OK)
                    {
                        path = OD.FileName;
                        folder = System.IO.Path.GetDirectoryName(path) + "\\";
                        filename = System.IO.Path.GetFileNameWithoutExtension(path);
                        fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                        br = new System.IO.BinaryReader(fs);
                        System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                        for (int i = 0; i < 10000; i++)
                        {
                            sspHeader newHead = new sspHeader();
                            newHead.offset = br.ReadUInt32();
                            if (newHead.offset != 0)
                                header.Add(newHead);
                        }
                        for (int i = 0; i < 10000; i++)
                        {
                            if (i < header.Count)
                            {
                                header[i].size = br.ReadUInt16();
                            }
                            else
                                fs.Position += 2;
                        }
                        foreach (sspHeader i in header)
                        {
                            fs.Position = i.offset;
                            uint id = br.ReadUInt16();
                            var query =
                                from s in skills
                                where s.id == id
                                select s;
                            if (query.Count() != 0)
                            {
                                effect info = query.First();
                                byte[] buf = encoder.GetBytes(info.name + "\0");
                                bw.Write(buf);
                                fs.Position = i.offset + 130;
                                buf = encoder.GetBytes(info.desc + "\0");
                                bw.Write(buf);
                            }
                        }
                        fs.Close();
                    }
                }
                else
                {
                    if (!System.IO.Directory.Exists(folder + "Output"))
                        System.IO.Directory.CreateDirectory(folder + "Output");
                    folder = folder + "Output\\";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(folder + "SkillDB.csv", false, System.Text.Encoding.UTF8);
                    sw.WriteLine("#ID,Name,描述,主动,最大Lv,Lv,MP,SP,AP,射程,目标,目标2,范围,技能释放射程,所需装备,技能Flag");
                    foreach (effect i in skills)
                    {
                        string txt = i.id.ToString();
                        txt += ("," + i.name);
                        txt += ("," + i.desc);
                        txt += ("," + i.active.ToString());
                        txt += ("," + i.maxLv.ToString());
                        txt += ("," + i.lv.ToString());
                        txt += ("," + i.mp);
                        txt += ("," + i.sp);
                        txt += ("," + i.ap);
                        txt += ("," + i.range);
                        txt += ("," + i.target.ToString());
                        txt += ("," + i.target2.ToString());
                        txt += ("," + i.effectRange.ToString());
                        txt += string.Format(",0x{0:X8}", i.equipFlag);
                        txt += string.Format(",0x{0:X8}", i.skillFlag);
                        sw.WriteLine(txt);
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
            MessageBox.Show("打开完成");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Dictionary<PC_JOB, Dictionary<uint, int>> skills = new Dictionary<PC_JOB, Dictionary<uint, int>>();
            OD.Filter = "skill_learn.csv|skill_learn.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";

            foreach (string i in Enum.GetNames(typeof(PC_JOB)))
            {
                skills.Add((PC_JOB)Enum.Parse(typeof(PC_JOB), i), new Dictionary<uint, int>());
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1" || paras[i] == "null")
                            paras[i] = "0";
                    }

                    uint id = uint.Parse(paras[0]);
                    int j = 0;
                    foreach (string i in Enum.GetNames(typeof(PC_JOB)))
                    {
                        PC_JOB job = (PC_JOB)Enum.Parse(typeof(PC_JOB), i);
                        int jobLv = int.Parse(paras[16 + j]);
                        if (jobLv > 0)
                            skills[job].Add(id, jobLv);
                        j++;
                    }
                }
                catch { }
            }
            sr.Close();
            
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("SkillList");
            foreach (PC_JOB i in skills.Keys)
            {
                XmlElement child = xml.CreateElement("Skills");
                XmlAttribute att = xml.CreateAttribute("Job");
                att.InnerText = i.ToString();
                child.Attributes.Append(att);
                foreach (uint j in skills[i].Keys)
                {
                    XmlElement item = xml.CreateElement("SkillID");
                    XmlAttribute jobLv = xml.CreateAttribute("JobLV");
                    jobLv.InnerText = skills[i][j].ToString();
                    item.InnerText = j.ToString();
                    item.Attributes.Append(jobLv);
                   child.AppendChild(item);
                }
                root.AppendChild(child);
            }
            xml.AppendChild(root);
            xml.Save(new System.IO.StreamWriter(folder + "SkillList.xml", false, System.Text.Encoding.UTF8));
            xml = null;
        }

        class mob
        {
            public uint id;
            public string name;
            public int ai;
            public Dictionary<uint, int> skills;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            List<mob> mobs = new List<mob>();
            OD.Filter = "monster.csv|monster.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";

            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1" || paras[i] == "null")
                            paras[i] = "0";
                    }

                    mob mob = new mob();
                    mob.id = uint.Parse(paras[0]);
                    mob.name = paras[1];
                    mob.ai = int.Parse(paras[99]);

                    mob.skills = new Dictionary<uint, int>();
                    for (int i = 0; i < 6; i++)
                    {
                        uint id = uint.Parse(paras[85 + i * 2]);
                        int rate = int.Parse(paras[86 + i * 2]);
                        if (id != 0)
                            mob.skills.Add(id, rate);
                    }

                    mobs.Add(mob);
                }
                catch { }
            }
            sr.Close();

            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("MobAI");
            root.AppendChild(xml.CreateComment("AI Mode:\r\n        Normal,\r\n        Active = 0x1,\r\n        NoAttack = 0x2,\r\n        NoMove = 0x4,\r\n" +
                "        RunAway = 0x8,"));
            foreach (mob i in mobs)
            {
                XmlElement child = xml.CreateElement("Mob");
                XmlComment att = xml.CreateComment(i.name);
                XmlElement child2;
                child.AppendChild(att);

                child2 = xml.CreateElement("ID");
                child2.InnerText = i.id.ToString();
                child.AppendChild(child2);

                child2 = xml.CreateElement("AIMode");
                child2.InnerText = i.ai.ToString();
                child.AppendChild(child2);

                child2 = xml.CreateElement("EventAttackingOnSkillCast");
                XmlAttribute att2 = xml.CreateAttribute("Rate");
                att2.InnerText = "50";
                child2.Attributes.Append(att2);
                
                foreach (uint j in i.skills.Keys)
                {
                    XmlElement item = xml.CreateElement("Skill");
                    XmlAttribute jobLv = xml.CreateAttribute("Rate");
                    jobLv.InnerText = i.skills[j].ToString();
                    item.InnerText = j.ToString();
                    item.Attributes.Append(jobLv);
                    child2.AppendChild(item);
                }
                child.AppendChild(child2);
                root.AppendChild(child);
            }
            xml.AppendChild(root);
            xml.Save(new System.IO.StreamWriter(folder + "MobAI.xml", false, System.Text.Encoding.UTF8));
            xml = null;
        }

        enum gateType
        {
            Entrance,
            East,
            West,
            South,
            North,
            Central,
            Exit,           
        }

        class dungeonGate
        {
            public gateType type;
            public byte x, y;
            public uint npcID;
        }

        class dungeonMap
        {
            public uint id;
            public Dictionary<gateType, dungeonGate> gates = new Dictionary<gateType, dungeonGate>();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            List<mob> mobs = new List<mob>();
            List<dungeonMap> maps = new List<dungeonMap>();
            OD.Filter = "it_warp.csv|it_warp.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";

            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        continue;
                    }
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1" || paras[i] == "null")
                            paras[i] = "0";
                    }

                    dungeonMap map = new dungeonMap();
                    map.id = uint.Parse(paras[0]);

                    dungeonGate entrance = new dungeonGate();
                    entrance.type = gateType.Entrance;
                    entrance.x = byte.Parse(paras[1]);
                    entrance.y = byte.Parse(paras[2]);
                    if (entrance.x != 0 || entrance.y != 0)
                        map.gates.Add(gateType.Entrance, entrance);
                    for (int i = 1; i <= 6; i++)
                    {
                        gateType type = (gateType)i;
                        dungeonGate gate = new dungeonGate();
                        gate.type = type;
                        gate.x = byte.Parse(paras[5 + (i - 1) * 5]);
                        gate.y = byte.Parse(paras[6 + (i - 1) * 5]);
                        gate.npcID = uint.Parse(paras[9 + (i - 1) * 5]);
                        if (gate.x != 0 || gate.y != 0)
                            map.gates.Add(type, gate);
                    }
                    maps.Add(map);
                }
                catch { }
            }
            sr.Close();

            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("DungeonMaps");
            foreach (dungeonMap i in maps)
            {
                XmlElement child = xml.CreateElement("Map");
                XmlElement child2;
                
                child2 = xml.CreateElement("ID");
                child2.InnerText = i.id.ToString();
                child.AppendChild(child2);

                foreach (gateType j in i.gates.Keys)
                {
                    XmlElement item = xml.CreateElement("Gate");
                    XmlAttribute att = xml.CreateAttribute("type");
                    att.InnerText = i.gates[j].type.ToString();
                    item.Attributes.Append(att);
                    att = xml.CreateAttribute("x");
                    att.InnerText = i.gates[j].x.ToString();
                    item.Attributes.Append(att);
                    att = xml.CreateAttribute("y");
                    att.InnerText = i.gates[j].y.ToString();
                    item.Attributes.Append(att);
                    item.InnerText = i.gates[j].npcID.ToString();
                    child.AppendChild(item);
                }
                root.AppendChild(child);
            }
            xml.AppendChild(root);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(folder + "DungeonMaps.xml", false, System.Text.Encoding.UTF8);
            xml.Save(sw);
            sw.Flush();
            sw.Close();
        }

        class Dungeon
        {
            public uint id;
            public string name;
            public int time;
            public string theme;
            public uint startMap, endMap;
            public int maxRoom, maxCross, maxFloor;
            public List<int[]> spawns = new List<int[]>();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            OD.Filter = "it_monstergroup.csv|it_monstergroup.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";
            spawns = new Dictionary<uint, List<SpawnInfo>>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("gbk"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    
                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    SpawnInfo info = new SpawnInfo();
                    uint id = uint.Parse(paras[0]);
                    int width, height;
                    info.mob = uint.Parse(paras[1]);
                    width = int.Parse(paras[8]) - int.Parse(paras[6]) + 1;
                    height = int.Parse(paras[9]) - int.Parse(paras[7]) + 1;
                    info.x = (byte)(int.Parse(paras[6]) + width / 2);
                    info.y = (byte)(int.Parse(paras[7]) + height / 2);
                    info.range = (width + height) / 4;
                    info.amount = (int)(int.Parse(paras[11]) * 0.7f);
                    if (info.amount == 0) info.amount = 1;
                    if (paras[3] == "1")
                        info.delay = 120;
                    else
                        info.delay = 0;
                    info.comment = paras[2];
                    if (spawns.ContainsKey(id))
                    {
                        spawns[id].Add(info);
                    }
                    else
                    {
                        List<SpawnInfo> list = new List<SpawnInfo>();
                        list.Add(info);
                        spawns.Add(id, list);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            sr.Close();

            List<Dungeon> dungeons = new List<Dungeon>();
            OD.Filter = "it_dungeon.csv|it_dungeon.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            path = OD.FileName;
            sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("gbk"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i] == "-1")
                            paras[i] = "0";
                    }
                    Dungeon info = new Dungeon();
                    info.id = uint.Parse(paras[0]);
                    info.name = paras[1];
                    info.time = int.Parse(paras[4]);
                    info.theme = paras[6];
                    info.startMap = uint.Parse(paras[7]);
                    info.endMap = uint.Parse(paras[8]);
                    info.maxRoom = int.Parse(paras[11]) + int.Parse(paras[14]);
                    info.maxCross = int.Parse(paras[12]);
                    info.maxFloor = int.Parse(paras[16]);
                    for (int j = 0; j < 16; j++)
                    {
                        int[] setting = new int[2];
                        string spawn = paras[23 + j * 2];
                        if (spawn.IndexOf("A") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("A"));
                        if (spawn.IndexOf("B") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("B"));
                        if (spawn.IndexOf("C") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("C"));
                        if (spawn.IndexOf("D") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("D"));
                        if (spawn.IndexOf("E") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("E"));
                        if (spawn.IndexOf("F") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("F"));
                        if (spawn.IndexOf("G") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("G"));
                        if (spawn.IndexOf("H") != -1)
                            spawn = spawn.Remove(spawn.IndexOf("H"));
                        setting[0] = int.Parse(spawn);
                        setting[1] = int.Parse(paras[24 + j * 2]);
                        if (setting[0] != 0)
                        {
                            info.spawns.Add(setting);
                        }
                    }
                    dungeons.Add(info);
                }
                catch (Exception ex)
                {
                }
            }
            sr.Close();

            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("Dungeons");

            foreach (Dungeon i in dungeons)
            {
                XmlElement child = xml.CreateElement("Dungeon");
                XmlElement att;
                child.AppendChild(xml.CreateComment(i.name));
                att = xml.CreateElement("ID");
                att.InnerText = i.id.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("TimeLimit");
                att.InnerText = i.time.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("Theme");
                att.InnerText = i.theme.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("StartMap");
                att.InnerText = i.startMap.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("EndMap");
                att.InnerText = i.endMap.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("MaxRoomCount");
                att.InnerText = i.maxRoom.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("MaxCrossCount");
                att.InnerText = i.maxCross.ToString();
                child.AppendChild(att);
                att = xml.CreateElement("MaxFloorCount");
                att.InnerText = i.maxFloor.ToString();
                child.AppendChild(att);

                string spawnfile = "Dungeon_" + i.id.ToString() + ".xml";
                att = xml.CreateElement("SpawnFile");
                att.InnerText = "DB/Spawns/Dungeon/" + spawnfile;
                child.AppendChild(att);
                root.AppendChild(child);

                XmlDocument xml2 = new XmlDocument();
                XmlElement root2 = xml2.CreateElement("Spawns");
                foreach (int[] j in i.spawns)
                {
                    List<SpawnInfo> tmp = spawns[(uint)j[0]];
                    SpawnInfo k = tmp[0];

                    if (k.comment == "栽培用")
                        continue;
                    XmlElement child2 = xml2.CreateElement("spawn");
                    XmlElement att2;
                    XmlAttribute attr = xml2.CreateAttribute("rate");
                    attr.Value = j[1].ToString();
                    child2.Attributes.Append(attr);
                    child2.AppendChild(xml2.CreateComment(k.comment));
                    att2 = xml2.CreateElement("id");
                    att2.InnerText = k.mob.ToString();
                    child2.AppendChild(att2);
                    att2 = xml2.CreateElement("x");
                    att2.InnerText = k.x.ToString();
                    child2.AppendChild(att2);
                    att2 = xml2.CreateElement("y");
                    att2.InnerText = k.y.ToString();
                    child2.AppendChild(att2);
                    att2 = xml2.CreateElement("amount");
                    att2.InnerText = k.amount.ToString();
                    child2.AppendChild(att2);
                    att2 = xml2.CreateElement("range");
                    att2.InnerText = k.range.ToString();
                    child2.AppendChild(att2);
                    att2 = xml2.CreateElement("delay");
                    att2.InnerText = k.delay.ToString();
                    child2.AppendChild(att2);

                    root2.AppendChild(child2);
                }                
                xml2.AppendChild(root2);
                System.IO.StreamWriter sw2 = new System.IO.StreamWriter(folder + spawnfile, false, System.Text.Encoding.UTF8);
                xml2.Save(sw2);
                sw2.Close();
            }
            xml.AppendChild(root);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(folder +"Dungeons.xml", false, System.Text.Encoding.UTF8);
            xml.Save(sw);
            sw.Close();
        }


        private void button19_Click(object sender, EventArgs e)
        {
             OD.Filter = "OTL.csv|OTL.csv";
            if (OD.ShowDialog() != DialogResult.OK)
                return;
            string path = OD.FileName;
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(folder + "\\Output"))
                System.IO.Directory.CreateDirectory(folder + "\\Output");
            folder += "\\Output\\";
            Dictionary<string, SagaDB.Map.MapObject> objs = new Dictionary<string, SagaDB.Map.MapObject>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("gbk"));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                if (line == "") continue;
                if (line.Substring(0, 1) == "#")
                    continue;
                paras = line.Split(',');

                for (int i = 0; i < paras.Length; i++)
                {
                    if (paras[i] == "" || paras[i] == "-1")
                        paras[i] = "0";
                }
                SagaDB.Map.MapObject obj = new SagaDB.Map.MapObject();
                obj.Name = paras[0];
                obj.Width = int.Parse(paras[1]);
                obj.Height = int.Parse(paras[2]);
                obj.CenterX = int.Parse(paras[3]);
                obj.CenterY = int.Parse(paras[4]);
                obj.Flag = new SagaLib.BitMask(int.Parse(paras[6]));
                objs.Add(obj.Name, obj);
            }
            sr.Close();

            if (FD.ShowDialog() != DialogResult.OK)
                return;
            string[] files = System.IO.Directory.GetFiles(FD.SelectedPath, "*.mod");
            Dictionary<string, List<SagaDB.Map.MapObject>> mapobjs = new Dictionary<string, List<SagaDB.Map.MapObject>>();
            foreach (string i in files)
            {
                System.IO.FileStream fs = new System.IO.FileStream(i, System.IO.FileMode.Open, System.IO.FileAccess.Read);                
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                mapobjs.Add(System.IO.Path.GetFileNameWithoutExtension(i), new List<SagaDB.Map.MapObject>());                    
                int num = br.ReadInt32();
                if (br.ReadInt32() == 3)
                    continue;
                for (int j = 0; j < num; j++)
                {
                    fs.Position += 8;
                    byte x = br.ReadByte();
                    byte y = br.ReadByte();
                    byte dir = br.ReadByte();
                    fs.Position += 8;
                    int offset = br.ReadInt32();
                    int pos = (int)fs.Position;
                    fs.Position = offset;
                    int count = 0;
                    while (br.ReadByte() != 0)
                        count++;
                    fs.Position = offset;
                    string name = System.Text.Encoding.ASCII.GetString(br.ReadBytes(count));
                    System.Diagnostics.Debug.Assert(objs.ContainsKey(name), "Cannot find mapobject:" + name);
                    if (objs.ContainsKey(name))
                    {
                        SagaDB.Map.MapObject obj = objs[name].Clone;
                        obj.X = x;
                        obj.Y = y;
                        obj.Dir = dir;
                        mapobjs[System.IO.Path.GetFileNameWithoutExtension(i)].Add(obj);
                    }
                    fs.Position = pos;
                }
                fs.Close();
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.FileStream fs2 = new System.IO.FileStream(folder + "MapObjects.dat", System.IO.FileMode.Create);
            bf.Serialize(fs2, mapobjs);
            fs2.Close();
        }
    }
}
