using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLSTool
{
    public partial class NpcPict : Form
    {
        public static NpcPict sMainWin = null;
        public NpcPict()
        {
            sMainWin = this;
            InitializeComponent();
        }
        public string HSColor = "", Ham = "", Shank = "";
        public string[] npcPict;
        private void NpcPict_Shown(object sender, EventArgs e)
        {
            isOpened = true;
        }

        private void NpcPict_Load(object sender, EventArgs e)
        {
            HSColorIDList = new byte[this.HS_Color_ComboBox.Items.Count];
            for (int i = 0; i < this.HSColorIDList.Length; i++)
            {
                HSColorIDList[i] = byte.Parse(this.HS_Color_ComboBox.Items[i].ToString().Substring(0, 2));
            }
        }

        private void HS_Color_ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            HSColor = this.HS_Color_ComboBox.Items[this.HS_Color_ComboBox.SelectedIndex].ToString().Substring(0, 2);
            npcPict = NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID].Split(',');
            if (npcPict[6] != null && npcPict[6] != "" && npcPict[6].Length > 2)
            {
                npcPict[6] = npcPict[6].Substring(0, npcPict[6].Length - 2) + HSColor;
            }
            if (npcPict[23] != null && npcPict[23] != "" && npcPict[23].Length > 2)
            {
                npcPict[23] = npcPict[23].Substring(0, npcPict[6].Length - 2) + HSColor;
            }
            if (npcPict[24] != null && npcPict[24] != "" && npcPict[24].Length > 2)
            {
                npcPict[24] = npcPict[24].Substring(0, npcPict[24].Length - 2) + HSColor;
            }
            if (npcPict[25] != null && npcPict[25] != "" && npcPict[25].Length > 2)
            {
                npcPict[25] = npcPict[25].Substring(0, npcPict[25].Length - 2) + HSColor;
            }
            if (npcPict[26] != null && npcPict[26] != "" && npcPict[26].Length > 2)
            {
                npcPict[26] = npcPict[26].Substring(0, npcPict[26].Length - 2) + HSColor;
            }
            SavePict(npcPict);
            this.propertyGrid2.SelectedObject = new NpcPictPPGSettings(npcPict, this);

        }

        private void AddHS_Button_Click(object sender, EventArgs e)
        {
            if (MainHair_TextBox.Text != "")
            {
                int s;
                if (!int.TryParse(MainHair_TextBox.Text, out s))
                {
                    MessageBox.Show("请输入数字哦", "小番茄");
                    return;
                }
                if (AddHair_TextBox.Text != "")
                {
                    if (!int.TryParse(AddHair_TextBox.Text, out s))
                    {
                        MessageBox.Show("请输入数字哦", "小番茄");
                        return;
                    }
                }
                if (MainHair_TextBox.TextLength > 4 || AddHair_TextBox.TextLength > 4)
                {
                    MessageBox.Show("数值有点大了啦", "小番茄");
                    return;
                }
                npcPict = NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID].Split(',');
                if (this.HS_Color_ComboBox.Text == "")
                    HSColor = "00";
                else
                HSColor = this.HS_Color_ComboBox.Items[this.HS_Color_ComboBox.SelectedIndex].ToString().Substring(0, 2);
                if(MainHair_TextBox.Text != "")
                npcPict[6] = GetRaceGender() + "_03_" + MainHair_TextBox.Text + "_" + HSColor;
                if (AddHair_TextBox.Text != "")
                npcPict[23] = GetRaceGender() + "_04_" + AddHair_TextBox.Text + "_" + HSColor;
                SavePict(npcPict);
                this.propertyGrid2.SelectedObject = new NpcPictPPGSettings(npcPict, this);
            }
            else
            {
                MessageBox.Show("主发型一定要有哦", "小番茄");
                return;
            }
        }
        void SavePict(string[] npcpict)
        {
            string str = "";
            for (int i = 0; i < npcpict.Count<string>() - 1; i++)
            {
                str += npcpict[i] + ",";
            }
            NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID] = str;
        }
        string GetRaceGender()
        {
            string RaceGender = "00";
            switch (TomatoTools.pMainWin.Gender_ComboBox.SelectedIndex)
            {

                case 0:
                    switch (TomatoTools.pMainWin.Race_ComboBox.SelectedIndex)
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
                    switch (TomatoTools.pMainWin.Race_ComboBox.SelectedIndex)
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

        private void pictSaveButton_Click(object sender, EventArgs e)
        {
            SavePict(npcPict);
            this.Close();
        }
       

        private void Face_button_Click(object sender, EventArgs e)
        {
            if (!FaceFactory.Instance.lines.ContainsKey(uint.Parse(FaceID_TextBox.Text)))
            {
                MessageBox.Show("没有找到这个脸型哦~", "大南瓜");
                return;
            }
            uint faceid = uint.Parse(FaceID_TextBox.Text);
            npcPict = NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID].Split(',');
            npcPict[5] = GetRaceGender() + "_" + FaceFactory.Instance.lines[faceid];
            SavePict(npcPict);
            this.propertyGrid2.SelectedObject = new NpcPictPPGSettings(npcPict, this);
            MessageBox.Show("脸型添加完成", "大南瓜 （・ω＜）☆");
        }


        private void Shank_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            npcPict = NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID].Split(',');
            if (npcPict[36] != "" && npcPict.Length > 4)
            {
                string sole = "";
                if (Shank_comboBox.SelectedIndex.ToString().Length < 2)
                    sole = "0" + Shank_comboBox.SelectedIndex.ToString();
                else sole = Shank_comboBox.SelectedIndex.ToString();
                string socks = npcPict[36].Substring(npcPict[36].LastIndexOf('_'), npcPict[36].Length - npcPict[36].LastIndexOf('_'));
                npcPict[36] = GetRaceGender() + "_24_" + sole  + socks;
                SavePict(npcPict);
                this.propertyGrid2.SelectedObject = new NpcPictPPGSettings(npcPict, this);
            }
        }

        private void Sole_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            npcPict = NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID].Split(',');
            if (npcPict[37] != "" && npcPict.Length > 4)
            {
                string sole = "";
                if (Sole_comboBox.SelectedIndex.ToString().Length < 2)
                    sole = "0" + Sole_comboBox.SelectedIndex.ToString();
                else sole = Sole_comboBox.SelectedIndex.ToString();
                string socks = npcPict[37].Substring(npcPict[37].LastIndexOf('_'), npcPict[37].Length - npcPict[37].LastIndexOf('_'));
                npcPict[37] = GetRaceGender() + "_25_" + sole + socks;
                SavePict(npcPict);
                this.propertyGrid2.SelectedObject = new NpcPictPPGSettings(npcPict, this);
            }
        }

        private void Thigh_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            npcPict = NpcFactory.Instance.PictLines[TomatoTools.pMainWin.PictID].Split(',');
            if (npcPict[35] != "" && npcPict.Length > 4)
            {
                string sole = "";
                if (Thigh_comboBox.SelectedIndex.ToString().Length < 2)
                    sole = "0" + Thigh_comboBox.SelectedIndex.ToString();
                else sole = Thigh_comboBox.SelectedIndex.ToString();
                string socks = npcPict[35].Substring(npcPict[35].LastIndexOf('_'), npcPict[35].Length - npcPict[35].LastIndexOf('_'));
                npcPict[35] = GetRaceGender() + "_01_" + sole + socks;
                SavePict(npcPict);
                this.propertyGrid2.SelectedObject = new NpcPictPPGSettings(npcPict, this);
            }
        }

    }
}
