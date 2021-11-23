using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace XLSTool
{
    public class NpcPictPPGSettings
    {
        public string[] paras;
        public NpcPictPPGSettings(string[] paras, NpcPict npcpict)
        {
            NpcPict.sMainWin.npcPict = paras;
            this.paras = paras;
            string hairStyle = this.paras[6];
            if (hairStyle != "")
            {
                string hairStyleColor = hairStyle.Substring(hairStyle.Length - 2, 2);
                for (byte i = 0; i < npcpict.HS_Color_ComboBox.Items.Count - 1; i++)
                {
                    if (npcpict.HS_Color_ComboBox.Items[i].ToString().Substring(0, 2) == hairStyleColor)
                        npcpict.HS_Color_ComboBox.SelectedIndex = i;
                }
            }
            if (paras[35] != "")
            {
                if (paras[35] == "null")
                {
                    npcpict.Thigh_comboBox.SelectedText = "null";
                }
                else
                {
                    string s = paras[35].Substring(paras[35].LastIndexOf('_') - 2, 2);
                    int index = int.Parse(s);
                    try
                    {
                        npcpict.Thigh_comboBox.SelectedIndex = index;
                    }
                    catch { }
                }
            }
            if (paras[36] != "")
            {
                if (paras[36] == "null")
                {
                    npcpict.Shank_comboBox.SelectedText = "null";
                }
                else
                {
                    string s = paras[36].Substring(paras[36].LastIndexOf('_') - 2, 2);
                    int index = int.Parse(s);
                    try
                    {
                        npcpict.Shank_comboBox.SelectedIndex = index;
                    }
                    catch { }
                }
            }
            if (paras[37] != "")
            {
                if (paras[37] == "null")
                {
                    npcpict.Sole_comboBox.SelectedText = "null";
                }
                else
                {
                    string s = paras[37].Substring(paras[37].LastIndexOf('_') - 2, 2);
                    int index = int.Parse(s);
                    try
                    {
                        npcpict.Sole_comboBox.SelectedIndex = index;
                    }
                    catch { }
                }
            }
        }
        string GetString(int idx)
        {
            if (idx < this.paras.Length)
                return this.paras[idx];
            else
                return "";
        }
        int GetInteger(int idx)
        {
            if (idx < this.paras.Length)
                return int.Parse(this.paras[idx]);
            else
                return 0;
        }
        [CategoryAttribute("常规"), DescriptionAttribute("NPCPict的ID")]//////////////
        public uint A_NPCPictID
        {
            get { return uint.Parse(this.paras[0]); }
            set { this.paras[0] = value.ToString();  }
        }
        [CategoryAttribute("常规"), DescriptionAttribute("Pict的名称")]
        public string Pict名称
        {
            get { return GetString(1); }
            set { this.paras[1] = value; }
        }
        [CategoryAttribute("D主要")]
        public string 上半身
        {
            get { return GetString(2); }
            set { this.paras[2] = value; }
        }
        [CategoryAttribute("D主要")]
        public string 下半身
        {
            get { return GetString(3); }
            set { this.paras[3] = value; }
        }
        [CategoryAttribute("D主要")]
        public string 靴子
        {
            get { return GetString(4); }
            set { this.paras[4] = value; }
        }
        [CategoryAttribute("D主要")]
        public string 脸型
        {
            get { return GetString(5); }
            set { this.paras[5] = value; }
        }
        [CategoryAttribute("D主要")]
        public string 发型
        {
            get { return GetString(6); }
            set { this.paras[6] = value; }
        }
        [CategoryAttribute("D主要")]
        public string 帽子
        {
            get { return GetString(7); }
            set { this.paras[7] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发饰_上
        {
            get { return GetString(8); }
            set { this.paras[8] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发饰_左
        {
            get { return GetString(9); }
            set { this.paras[9] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发饰_右
        {
            get { return GetString(10); }
            set { this.paras[10] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发饰_下
        {
            get { return GetString(11); }
            set { this.paras[11] = value; }
        }
        [CategoryAttribute("头部")]
        public string 脸饰
        {
            get { return GetString(12); }
            set { this.paras[12] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 右小腿
        {
            get { return GetString(13); }
            set { this.paras[13] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 右肩
        {
            get { return GetString(14); }
            set { this.paras[14] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 左肩
        {
            get { return GetString(15); }
            set { this.paras[15] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 胸膛
        {
            get { return GetString(16); }
            set { this.paras[16] = value; }
        }
        [CategoryAttribute("手部细节")]
        public string 右手握物
        {
            get { return GetString(17); }
            set { this.paras[17] = value; }
        }
        [CategoryAttribute("手部细节")]
        public string 左手背
        {
            get { return GetString(18); }
            set { this.paras[18] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 背部
        {
            get { return GetString(19); }
            set { this.paras[19] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 左小腿
        {
            get { return GetString(20); }
            set { this.paras[20] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 右腿背
        {
            get { return GetString(21); }
            set { this.paras[21] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 尾巴
        {
            get { return GetString(22); }
            set { this.paras[22] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发片_上
        {
            get { return GetString(23); }
            set { this.paras[23] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发片_左
        {
            get { return GetString(24); }
            set { this.paras[24] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发片_右
        {
            get { return GetString(25); }
            set { this.paras[25] = value; }
        }
        [CategoryAttribute("头部")]
        public string 发片_下
        {
            get { return GetString(26); }
            set { this.paras[26] = value; }
        }
        [CategoryAttribute("手部细节")]
        public string 右手背
        {
            get { return GetString(27); }
            set { this.paras[27] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 左腿背
        {
            get { return GetString(28); }
            set { this.paras[28] = value; }
        }
        [CategoryAttribute("手部细节")]
        public string 左手握物
        {
            get { return GetString(29); }
            set { this.paras[29] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 腰部
        {
            get { return GetString(30); }
            set { this.paras[30] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 腰饰
        {
            get { return GetString(31); }
            set { this.paras[31] = value; }
        }
        [CategoryAttribute("身体细节")]
        public string 裤袜子
        {
            get { return GetString(32); }
            set { this.paras[32] = value; }
        }
        [CategoryAttribute("特效")]
        public string 攻击目标特效ID
        {
            get { return GetString(33); }
            set { this.paras[33] = value; }
        }
        [CategoryAttribute("特效")]
        public string 拖尾特效ID
        {
            get { return GetString(34); }
            set { this.paras[34] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 大腿
        {
            get { return GetString(35); }
            set { this.paras[35] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 小腿
        {
            get { return GetString(36); }
            set { this.paras[36] = value; }
        }
        [CategoryAttribute("腿部细节")]
        public string 脚掌
        {
            get { return GetString(37); }
            set { this.paras[37] = value; }
        }
    }
}