using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 測試
{
    class CSVFILE
    {
        public List<string[]> Data = new List<string[]>();
        public List<string[]> temp = new List<string[]>();
        public bool Load(Encoding encoding)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "选择文件";
            fileDialog.Filter = "csv files (*.csv)|*.csv";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Data.Clear();
                FileStream Csv = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader File = new StreamReader(Csv, encoding);
                //StreamReader warp = new StreamReader(fileName, Encoding.Default);
                while (!File.EndOfStream)
                {
                    string[] paras;
                    string line;
                    line = File.ReadLine();
                    try
                    {
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#")
                            continue;

                        paras = line.Split(',');
                        Data.Add(paras);
                    }
                    catch
                    {
                    }
                }
                File.Close();
                Csv.Close();
                return true;
            }
            else
                return false;
        }

        public bool Search(string comboBox, string textBox, int mode)
        {
            temp.Clear();
            bool ars = System.Text.RegularExpressions.Regex.Match(textBox, "[0-9]").Success;
            foreach (string[] reader in Data)
            {
                if (reader[4] != comboBox && comboBox != " 所有(ALL)")
                    continue;
                if ((reader[0].IndexOf(textBox) != -1 || reader[3].IndexOf(textBox) != -1))
                {
                    if (reader.LongLength > mode)
                    {
                        string[] li = { reader[0], reader[3], reader[mode] };
                        temp.Add(li);
                    }
                    else
                    {
                        string[] li = { reader[0], reader[3], "" };
                        temp.Add(li);
                    }
                }
            }
            return true;
        }

        public string Treasure()
        {
            string R = "";

            R = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<TreasureDB>";
            for (int b = 0; b < Data[0].Length; b += 3)
            {
                if (Data[0][b].Substring(0, 1) == "#")
                    continue;
                R += "\r\n  <TreasureList GroupName=\"" + Data[0][b] + "\">";
                for (int a = 1; a < Data.Count; a++)
                {
                    try
                    {
                        if (Data[a][b] == "0" ||
                            Data[a][b + 1] == "0" ||
                            Data[a][b + 2] == "0" ||
                            Data[a][b + 2] == "" ||
                            Data[a][b] == "" ||
                            Data[a][b + 1] == "" ||
                            Data[a][b + 2] == null ||
                            Data[a][b] == null ||
                            Data[a][b + 1] == null ||
                            Data[a][b].Substring(0, 1) == "#" ||
                            Data[a][b + 1].Substring(0, 1) == "#" ||
                            Data[a][b + 2].Substring(0, 1) == "#")
                            continue;
                        R += "\r\n    <Item rate=\"" + Data[a][b + 1] + "\" count=\"" + Data[a][b + 2] + "\">" + Data[a][b] + "</Item>";
                    }
                    catch
                    {
                    }
                }
                R += "\r\n  </TreasureList>";
            }
            R += "\r\n</TreasureDB>";
            return R;
        }

        public string Monster()
        {
            string R = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<MobAI>" +
                  "\r\n  <!--AI Mode:\r\n        Normal,\r\n        Active = 0x1,\r\n        NoAttack = 0x2,\r\n        NoMove = 0x4,\r\n        RunAway = 0x8,-->";
            for (int a = 0; a < Data.Count; a++)
            {
                int AI = 0;
                string G = "";
                bool SKI = false;
                if (Data[a][61] == "0" ||
                    Data[a][61] == "21" ||
                    Data[a][61] == "71" ||
                    Data[a][61] == "83")
                    AI = 6;
                else if (Data[a][61] == "10" ||
                    Data[a][61] == "51")
                    AI = 9;
                else if (Data[a][61] == "3" ||
                    Data[a][61] == "4" ||
                    Data[a][61] == "7" ||
                    Data[a][61] == "14" ||
                    Data[a][61] == "15" ||
                    Data[a][61] == "22" ||
                    Data[a][61] == "25" ||
                    Data[a][61] == "26" ||
                    Data[a][61] == "27" ||
                    Data[a][61] == "28" ||
                    Data[a][61] == "32" ||
                    Data[a][61] == "35" ||
                    Data[a][61] == "42" ||
                    Data[a][61] == "63" ||
                    Data[a][61] == "64" ||
                    Data[a][61] == "65" ||
                    Data[a][61] == "66" ||
                    Data[a][61] == "72" ||
                    Data[a][61] == "73" ||
                    Data[a][61] == "78" ||
                    Data[a][61] == "80" ||
                    Data[a][61] == "81" ||
                    Data[a][61] == "82" ||
                    Data[a][61] == "84" ||
                    Data[a][61] == "85" ||
                    Data[a][61] == "86" ||
                    Data[a][61] == "87" ||
                    Data[a][61] == "88")
                    AI = 1;
                else if (Data[a][61] == "43" ||
                    Data[a][61] == "44" ||
                    Data[a][61] == "45" ||
                    Data[a][61] == "46")
                    AI = 5;
                else if (Data[a][61] == "47" ||
                    Data[a][61] == "48" ||
                    Data[a][61] == "68" ||
                    Data[a][61] == "69" ||
                    Data[a][61] == "70")
                    AI = 3;
                if (Data[a][85] != "0")
                {
                    int AKLL = int.Parse(Data[a][86]);
                    if (AKLL == 0)
                        AKLL = 20;
                    else if (AKLL == 1)
                        AKLL = 10;
                    G += "\r\n      <Skill Rate=\"" + AKLL + "\">" + Data[a][85] + "</Skill>";
                    SKI = true;
                }
                if (Data[a][87] != "0")
                {
                    if (Data[a][87] != Data[a][85])
                    {
                        int AKLL = int.Parse(Data[a][88]);
                        if (AKLL == 0)
                            AKLL = 20;
                        else if (AKLL == 1)
                            AKLL = 10;
                        G += "\r\n      <Skill Rate=\"" + AKLL + "\">" + Data[a][87] + "</Skill>";
                        SKI = true;
                    }
                }
                if (Data[a][89] != "0")
                {
                    if (Data[a][89] != Data[a][85] &&
                        Data[a][89] != Data[a][87])
                    {
                        int AKLL = int.Parse(Data[a][90]);
                        if (AKLL == 0)
                            AKLL = 20;
                        else if (AKLL == 1)
                            AKLL = 10;
                        G += "\r\n      <Skill Rate=\"" + AKLL + "\">" + Data[a][89] + "</Skill>";
                        SKI = true;
                    }
                }
                if (Data[a][91] != "0")
                {
                    if (Data[a][91] != Data[a][85] &&
                        Data[a][91] != Data[a][87] &&
                        Data[a][91] != Data[a][89])
                    {
                        int AKLL = int.Parse(Data[a][92]);
                        if (AKLL == 0)
                            AKLL = 20;
                        else if (AKLL == 1)
                            AKLL = 10;
                        G += "\r\n      <Skill Rate=\"" + AKLL + "\">" + Data[a][91] + "</Skill>";
                        SKI = true;
                    }
                }
                if (Data[a][93] != "0")
                {
                    if (Data[a][93] != Data[a][85] &&
                        Data[a][93] != Data[a][87] &&
                        Data[a][93] != Data[a][89] &&
                        Data[a][93] != Data[a][91])
                    {
                        int AKLL = int.Parse(Data[a][94]);
                        if (AKLL == 0)
                            AKLL = 20;
                        else if (AKLL == 1)
                            AKLL = 10;
                        G += "\r\n      <Skill Rate=\"" + AKLL + "\">" + Data[a][93] + "</Skill>";
                        SKI = true;
                    }
                }
                if (Data[a][95] != "0")
                {
                    if (Data[a][95] != Data[a][85] &&
                        Data[a][95] != Data[a][87] &&
                        Data[a][95] != Data[a][89] &&
                        Data[a][95] != Data[a][91] &&
                        Data[a][95] != Data[a][93])
                    {
                        int AKLL = int.Parse(Data[a][96]);
                        if (AKLL == 0)
                            AKLL = 20;
                        else if (AKLL == 1)
                            AKLL = 10;
                        G += "\r\n      <Skill Rate=\"" + AKLL + "\">" + Data[a][95] + "</Skill>";
                        SKI = true;
                    }
                }
                if (SKI)
                {
                    R += "\r\n  <Mob>" + "\r\n    <!--" + Data[a][1] + "-->" + "\r\n    <ID>" + Data[a][0] + "</ID>" + "\r\n    <AIMode>" + AI + "</AIMode>" + "\r\n    <EventAttackingOnSkillCast Rate=\"20\">";
                    R += G;
                    R += "\r\n    </EventAttackingOnSkillCast>";
                }
                else
                {
                    R += "\r\n  <Mob>" + "\r\n    <!--" + Data[a][1] + "-->" + "\r\n    <ID>" + Data[a][0] + "</ID>" + "\r\n    <AIMode>" + AI + "</AIMode>" + "\r\n    <EventAttackingOnSkillCast Rate=\"20\" />";
                }
                R += "\r\n  </Mob>";
            }
            R += "\r\n</MobAI>";

            return R;
        }
    }
}
