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
    public partial class TomatoTools : Form
    {
        public static TomatoTools pMainWin = null;
        public TomatoTools()
        {
            InitializeComponent();
            pMainWin = this;
        }
        string ItemName = "", NpcName = "";
        public uint Itemid = 0, PictID = 0, MonsterID = 0;
        int Npcindex = 0;
        public string[] itemparas, npcparas, pictparas, mobparas;
        public DatFile dat;
        LoadTGA tga = new LoadTGA();

        NpcPict npcPict = new NpcPict();

        //NPC输入框自动保存锁
        bool ChangeLock = true;

        private void Form1_Load(object sender, EventArgs e)
        {

            if (dat != null)
                dat.Close();
            dat = new DatFile();
            dat.Open("data\\xls\\table.hed");
            tga.Open("data\\sprite\\item\\item.hed");
            LoadTGA.Files = tga.headers;
            LoadItemCSV.LoadItemCsv(dat.Extract("item.csv"));
            LoadItemCSV.LoadItemPictCsv(dat.Extract("itempict.csv"));
            foreach (string item in ItemFactory.Instance.lines.Values)
            {
                string[] paras = item.Split(',');
                ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
            }

            LoadNpcCSV.LoadNpcCsv(dat.Extract("npc.csv"));

            foreach (string npc in NpcFactory.Instance.lines.Values)
            {
                string[] paras = npc.Split(',');
                NpcListBox.Items.Add(paras[0] + "," + paras[1]);
            }

            LoadNpcCSV.LoadNpcPictCsv(dat.Extract("npcpict.csv"));
            LoadNpcCSV.LoadNpcPictCsv(dat.Extract("monsterpict.csv"));
            LoadNpcCSV.LoadNpcPictCsv(dat.Extract("furniturepict.csv"));

            LoadMobCSV.LoadMobCsv(dat.Extract("monster.csv"));
            foreach (string mob in MobFactory.Instance.lines.Values)
            {
                string[] paras = mob.Split(',');
                MobListBox.Items.Add(paras[0] + "," + paras[1]);
            }

            LoadFaceCSV.LoadFaceCsv(dat.Extract("face_info.csv"));
            dat.Close();

        }

        /// <summary>
        /// 搜索功能
        /// </summary>
        private void Search_Click(object sender, EventArgs e)
        {
            string tagString = SearchTag.Text;
            if (tagString == "")
                MessageBox.Show("请输入搜索内容", "☆（＞ω・） 我是番茄 （・ω＜）☆");
            int index = -1;
            for (int i = 0; i < ItemlistBox.Items.Count; i++)
            {
                if (ItemlistBox.Items[i].ToString().Contains(tagString))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
                ItemlistBox.SetSelected(index, true);
            else
                MessageBox.Show("结果没有找到", "☆（＞ω・） 我是番茄 （・ω＜）☆");
        }

        /// <summary>
        /// 筛选
        /// </summary>
        private void Filter_Click(object sender, EventArgs e)
        {
            ItemlistBox.Items.Clear();
            ItemType it = new ItemType();
            #region 筛选
            foreach (string s in ItemFactory.Instance.lines.Values)
            {
                string[] paras = s.Split(',');
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    if (it.NONE.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    if (it.POTION.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    if (it.FOOD.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox4.CheckState == CheckState.Checked)
                {
                    if (it.IRIS_CARD.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox5.CheckState == CheckState.Checked)
                {
                    if (it.MARIO.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox6.CheckState == CheckState.Checked)
                {
                    if (it.SEED.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox7.CheckState == CheckState.Checked)
                {
                    if (it.SCROLL.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox8.CheckState == CheckState.Checked)
                {
                    if (it.HEADWEAR.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox9.CheckState == CheckState.Checked)
                {
                    if (it.SHIELD.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox10.CheckState == CheckState.Checked)
                {
                    if (it.SHOES.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox11.CheckState == CheckState.Checked)
                {
                    if (it.WEAPON.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox12.CheckState == CheckState.Checked)
                {
                    if (it.ARMOR.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox13.CheckState == CheckState.Checked)
                {
                    if (it.BAG.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox14.CheckState == CheckState.Checked)
                {
                    if (it.SOCKS.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox15.CheckState == CheckState.Checked)
                {
                    if (it.USE.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox16.CheckState == CheckState.Checked)
                {
                    if (it.NECKLACE.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox17.CheckState == CheckState.Checked)
                {
                    if (it.PET.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox18.CheckState == CheckState.Checked)
                {
                    if (it.DEMPARTS.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox19.CheckState == CheckState.Checked)
                {
                    if (it.DEMSKILL.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox20.CheckState == CheckState.Checked)
                {
                    if (it.FURNITURE.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox21.CheckState == CheckState.Checked)
                {
                    if (it.BOX.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox22.CheckState == CheckState.Checked)
                {
                    if (it.LONGBOOTS.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox23.CheckState == CheckState.Checked)
                {
                    if (it.DRESS.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox24.CheckState == CheckState.Checked)
                {
                    if (it.SLACKS.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
                if (checkBox25.CheckState == CheckState.Checked)
                {
                    if (it.OTHER.Contains<string>(paras[4]))
                        ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
            }
            if (ItemlistBox.Items.Count == 0)
            {
                foreach (string s in ItemFactory.Instance.lines.Values)
                {
                    string[] paras = s.Split(',');
                    ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
                }
            }
            #endregion
        }

        private void ItemlistBox_DoubleClick(object sender, EventArgs e)
        {
            //临时保存内容
            if (ItemName != "" && Itemid != 0)
            {
                string str = "";
                for (int i = 0; i < itemparas.Count<string>() - 1; i++)
                {
                    str += itemparas[i] + ",";
                }
                ItemFactory.Instance.lines[Itemid] = str;
            }

            //显示预览
            ItemName = (string)ItemlistBox.SelectedItem;
            Itemid = uint.Parse(ItemName.Substring(0, ItemName.IndexOf(',')));
            itemparas = ItemFactory.Instance.lines[Itemid].Split(',');

            //图片
            pictureBox1.Image = ItemFactory.Instance.ShowTGA(itemparas, tga);

            //显示数据

            propertyGrid1.SelectedObject = new ItemPPGSettings(itemparas);

        }

        private void NpcListBox_DoubleClick(object sender, EventArgs e)
        {
            //自动保存锁
            ChangeLock = true;
            /*if (NpcName != "" && Npcindex != 0)
            {
                string str ="";
                for (int i = 0; i < npcparas.Count<string>() - 1; i++)
                {
                    str += npcparas[i] + ",";
                }
                NpcFactory.Instance.lines[(uint)Npcindex] = str;
            }*/

            //
            NpcName = (string)NpcListBox.SelectedItem;

            Npcindex = NpcListBox.SelectedIndex;

            //根据索引查找values
            string element = NpcFactory.Instance.lines.Values.ElementAt(NpcListBox.SelectedIndex);
            npcparas = element.Split(',');

            //显示属性数据
            Npc_Box_Basic_NpcID.Text = npcparas[0];
            Npc_Box_Basic_Name.Text = npcparas[1];
            Npc_Box_Basic_MapID.Text = npcparas[2];
            Npc_Box_Basic_MapX.Text = npcparas[3];
            Npc_Box_Basic_MapY.Text = npcparas[4];
            Npc_Box_Basic_Dir.Text = npcparas[5];
            Npc_Box_Basic_Scale.Text = npcparas[6];
            Npc_Box_Basic_EventID.Text = npcparas[7];
            Npc_Box_Basic_Notouch.Text = npcparas[8];
            Npc_Box_Basic_PictID.Text = npcparas[9];
            Npc_Box_Basic_Motion.Text = npcparas[10];
            Npc_Box_Basic_Transparency.Text = npcparas[11];
            Npc_Box_Basic_Unknown1.Text = npcparas[12];
            Npc_Box_Basic_Unknown2.Text = npcparas[13];
            Npc_Box_Basic_Flag.Text = npcparas[14];
            Npc_Box_Basic_Unknown3.Text = npcparas[15];

            //自动保存锁
            ChangeLock = false;

            if (NpcFactory.Instance.PictLines.ContainsKey(uint.Parse(npcparas[9])))
            {
                CreatePictButton.Text = "显示";
                string checkPict = NpcFactory.Instance.PictLines[uint.Parse(npcparas[9])].Split(',')[2];
                if (checkPict.IndexOf('_') > -1)
                {
                    checkPict = checkPict.Substring(0, checkPict.IndexOf('_'));
                    if (checkPict.Length != 2)
                    {
                        Race_ComboBox.SelectedIndex = 4;
                        Gender_ComboBox.SelectedIndex = 2;
                    }
                    else
                    {
                        switch (checkPict)
                        {
                            case "00":
                                Gender_ComboBox.SelectedIndex = 0;
                                Race_ComboBox.SelectedIndex = 0;
                                break;
                            case "01":
                                Gender_ComboBox.SelectedIndex = 1;
                                Race_ComboBox.SelectedIndex = 0;
                                break;
                            case "02":
                                Gender_ComboBox.SelectedIndex = 0;
                                Race_ComboBox.SelectedIndex = 1;
                                break;
                            case "03":
                                Gender_ComboBox.SelectedIndex = 1;
                                Race_ComboBox.SelectedIndex = 1;
                                break;
                            case "04":
                                Gender_ComboBox.SelectedIndex = 0;
                                Race_ComboBox.SelectedIndex = 2;
                                break;
                            case "05":
                                Gender_ComboBox.SelectedIndex = 1;
                                Race_ComboBox.SelectedIndex = 2;
                                break;
                            case "06":
                                Gender_ComboBox.SelectedIndex = 0;
                                Race_ComboBox.SelectedIndex = 3;
                                break;
                            case "07":
                                Gender_ComboBox.SelectedIndex = 1;
                                Race_ComboBox.SelectedIndex = 3;
                                break;
                        }
                    }
                }
            }
            else
            {
                //MessageBox.Show("这个NPC没有模型", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                CreatePictButton.Text = "创建";
            }
            PictID = uint.Parse(npcparas[9]);
        }

        private void Npc_Box_Basic_TextChanged(object sender, EventArgs e)
        {
            if (NpcListBox.SelectedIndex != -1)
            {
                if (!ChangeLock)
                {
                    string value = "";
                    value = Npc_Box_Basic_NpcID.Text + "," + Npc_Box_Basic_Name.Text + "," + Npc_Box_Basic_MapID.Text + "," + Npc_Box_Basic_MapX.Text +
                        "," + Npc_Box_Basic_MapY.Text + "," + Npc_Box_Basic_Dir.Text + "," + Npc_Box_Basic_Scale.Text + "," + Npc_Box_Basic_EventID.Text +
                        "," + Npc_Box_Basic_Notouch.Text + "," + Npc_Box_Basic_PictID.Text + "," + Npc_Box_Basic_Motion.Text + "," + Npc_Box_Basic_Transparency.Text +
                        "," + Npc_Box_Basic_Unknown1.Text + "," + Npc_Box_Basic_Unknown2.Text + "," + Npc_Box_Basic_Flag.Text + "," + Npc_Box_Basic_Unknown3.Text + ",";

                    uint key = NpcFactory.Instance.lines.ElementAt(NpcListBox.SelectedIndex).Key;
                    NpcFactory.Instance.lines[key] = value;
                    NpcListBox.Items[NpcListBox.SelectedIndex] = Npc_Box_Basic_NpcID.Text + "," + Npc_Box_Basic_Name.Text;
                }
            }
        }
        private void NpcPict_Box_Basic_TextChanged(object sender, EventArgs e)
        {
            if (!ChangeLock)
            {
                string value = "";
                value = Npc_Box_Basic_NpcID.Text + "," + Npc_Box_Basic_Name.Text + "," + Npc_Box_Basic_MapID.Text + "," + Npc_Box_Basic_MapX.Text +
                    "," + Npc_Box_Basic_MapY.Text + "," + Npc_Box_Basic_Dir.Text + "," + Npc_Box_Basic_Scale.Text + "," + Npc_Box_Basic_EventID.Text +
                    "," + Npc_Box_Basic_Notouch.Text + "," + Npc_Box_Basic_PictID.Text + "," + Npc_Box_Basic_Motion.Text + "," + Npc_Box_Basic_Transparency.Text +
                    "," + Npc_Box_Basic_Unknown1.Text + "," + Npc_Box_Basic_Unknown2.Text + "," + Npc_Box_Basic_Flag.Text + "," + Npc_Box_Basic_Unknown3.Text + ",";

                uint key = NpcFactory.Instance.lines.ElementAt(NpcListBox.SelectedIndex).Key;
                NpcFactory.Instance.lines[key] = value;
                NpcListBox.Items[NpcListBox.SelectedIndex] = Npc_Box_Basic_NpcID.Text + "," + Npc_Box_Basic_Name.Text;
            }
            if (NpcFactory.Instance.PictLines.ContainsKey(uint.Parse(Npc_Box_Basic_PictID.Text)) && Npc_Box_Basic_PictID.Text != "")
            {
                CreatePictButton.Text = "显示";
            }
            else
                CreatePictButton.Text = "创建";
        }
        private void Save_Click(object sender, EventArgs e)
        {
            //保存临时内容
            if (ItemName != "" && Itemid != 0)
            {
                string str = "";
                for (int i = 0; i < itemparas.Count<string>() - 1; i++)
                {
                    str += itemparas[i] + ",";
                }
                ItemFactory.Instance.lines[Itemid] = str;
            }

            dat = new DatFile();
            dat.Open("data\\xls\\table.hed");
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ms, Encoding.GetEncoding("gbk"));
                sw.AutoFlush = true;
                foreach (string i in ItemFactory.Instance.lines.Values)
                {
                    sw.WriteLine(i);
                }
                dat.Add("item.csv", ms.ToArray());
                dat.Close();
                sw.Close();
            }
            /*NpcListBox.Items.Clear();
            foreach (string s in NpcFactory.Instance.lines.Values)
            {
                string[] paras = s.Split(',');
                ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
            }*/
            MessageBox.Show("item.csv保存完毕", "小番茄");
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            uint id = 10000000;
            while (ItemFactory.Instance.IDList.Contains(id))
            {
                id++;
                continue;
            }
            ItemFactory.Instance.IDList.Add(id);
            ItemFactory.Instance.lines.Add(id, id.ToString() + ",0,10000000,新的道具,NONE,0,0,0,0,0,-1,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,何のアイテムかは不明,0,0,0,0,");
            ItemlistBox.Items.Add(id.ToString() + ",新的道具");
            ItemlistBox.SelectedIndex = ItemlistBox.Items.Count - 1;
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            if (ItemlistBox.SelectedItem != null)
            {
                int index = ItemlistBox.SelectedItem.ToString().IndexOf(',');
                uint id = uint.Parse(ItemlistBox.SelectedItem.ToString().Substring(0, index));
                ItemlistBox.Items.RemoveAt(ItemlistBox.SelectedIndex);
                ItemFactory.Instance.IDList.Remove(id);
                ItemFactory.Instance.lines.Remove(id);

            }
            else
            {
                MessageBox.Show("要选择需要删除的道具哦~", "☆（＞ω・） 我是番茄 （・ω＜）☆");
            }
        }
        private void DeleteNpcButton_Click(object sender, EventArgs e)
        {
            ChangeLock = true;
            if (NpcListBox.SelectedIndex == -1) return;
            uint nid = uint.Parse(NpcListBox.SelectedItem.ToString().Substring(0, NpcListBox.SelectedItem.ToString().IndexOf(',')));
            NpcFactory.Instance.lines.Remove(nid);
            NpcListBox.SelectedIndex = NpcListBox.SelectedIndex - 1;
            NpcListBox.Items.RemoveAt(NpcListBox.SelectedIndex + 1);
        }
        private void AddNpcButton_Click(object sender, EventArgs e)
        {
            ChangeLock = true;
            uint id = 0;
            while (NpcFactory.Instance.lines.ContainsKey(id))
            {
                id++;
                continue;
            }
            //NpcFactory.Instance.IDList.Add(id);*/
            /*
            uint id = 23333500, x1 = 12, n1 = 0; //
            while (NpcFactory.Instance.lines.Keys.Contains(id))//
            {
                n1++;
                x1++;
                id++;//
                continue;//
            }
            //NpcFactory.Instance.IDList.Add(id);//*/

            uint count = (uint)NpcFactory.Instance.lines.Count;
            //NpcFactory.Instance.lines.Add(id, id.ToString() + ",脚掌" + n1.ToString() + ",11073000," + x1.ToString() + ",107,0,1,"+id.ToString()+",0," + id.ToString() + ",111,0,1,0,0,0,"); //
            //NpcListBox.Items.Add(id.ToString() + ",脚掌" + n1.ToString()); //
            NpcFactory.Instance.lines.Add(id, "0,新的NPC,0,0,0,0,1,0,0,0,111,0,1,0,0,0,");
            NpcListBox.Items.Add("0,新的NPC");
            NpcListBox.SelectedIndex = NpcListBox.Items.Count - 1;

            /*if (NpcName != "" && Npcindex != 0)
            {
                string str = "";
                for (int i = 0; i < npcparas.Count<string>() - 1; i++)
                {
                    str += npcparas[i] + ",";
                }
                NpcFactory.Instance.lines[(uint)Npcindex] = str;
            }
            NpcName = (string)NpcListBox.SelectedItem;
            Npcindex = NpcListBox.SelectedIndex;*/

            //根据索引查找values
            string element = NpcFactory.Instance.lines.Values.ElementAt(NpcListBox.SelectedIndex);
            npcparas = element.Split(',');

            //显示属性数据
            Npc_Box_Basic_NpcID.Text = npcparas[0];
            Npc_Box_Basic_Name.Text = npcparas[1];
            Npc_Box_Basic_MapID.Text = npcparas[2];
            Npc_Box_Basic_MapX.Text = npcparas[3];
            Npc_Box_Basic_MapY.Text = npcparas[4];
            Npc_Box_Basic_Dir.Text = npcparas[5];
            Npc_Box_Basic_Scale.Text = npcparas[6];
            Npc_Box_Basic_EventID.Text = npcparas[7];
            Npc_Box_Basic_Notouch.Text = npcparas[8];
            Npc_Box_Basic_PictID.Text = npcparas[9];
            Npc_Box_Basic_Motion.Text = npcparas[10];
            Npc_Box_Basic_Transparency.Text = npcparas[11];
            Npc_Box_Basic_Unknown1.Text = npcparas[12];
            Npc_Box_Basic_Unknown2.Text = npcparas[13];
            Npc_Box_Basic_Flag.Text = npcparas[14];
            Npc_Box_Basic_Unknown3.Text = npcparas[15];

            ChangeLock = false;
        }

        private void CreatePictButton_Click(object sender, EventArgs e)
        {
            //uint PictID = 0;
            TextBox t = Npc_Box_Basic_PictID;
            if (tabControl1.SelectedTab.Text == "怪物")
            {
                t = MobPictID;
            }

            try
            {
                PictID = uint.Parse(t.Text);
            }
            catch
            {
                MessageBox.Show("请用正确的字符作为PictID哟", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            if (t.Text == "0" || t.Text.IndexOf("0") == 0)
            {
                MessageBox.Show("请补牙使用0或以0作开头为ID哦", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            if (t.Text.Length < 5 || t.Text.Length > 10)
            {
                MessageBox.Show("请保持查长度大于5小于10~", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            if (NpcFactory.Instance.PictLines.Keys.Contains(PictID))
            {
                OpenNpcPict();
                npcPict.propertyGrid2.SelectedObject = new NpcPictPPGSettings(NpcFactory.Instance.PictLines[PictID].Split(','), npcPict);
                return;
            }
            if (Race_ComboBox.Text == "" || Gender_ComboBox.Text == "")
            {
                MessageBox.Show("要先选择种族和性别~", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            string RaceGender = GetRaceFlag();
            if (NpcName != "" && Npcindex != 0)
            {
                string str = "";
                for (int i = 0; i < npcparas.Count<string>() - 1; i++)
                {
                    str += npcparas[i] + ",";
                }
                NpcFactory.Instance.lines[(uint)Npcindex] = str;
            }
            //NpcFactory.Instance.PictLines.Add(PictID, PictID.ToString() + ",新的Pict,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,," + RaceGender + "_01_00_00," + RaceGender + "_24_00_00," + RaceGender + "_25_00_00,");
            NpcFactory.Instance.PictLines.Add(PictID, PictID.ToString() + "," + Npc_Box_Basic_Name.Text + "," + RaceGender + "_00_00_00," + RaceGender + "_01_00_00,," + RaceGender + "_02_00_00," + RaceGender + "_03_01_50,,,,,,,,,,,,,,,,,,,,,,,,,,,,," + RaceGender + "_01_00_00," + RaceGender + "_24_00_00," + RaceGender + "_25_00_00,");

            OpenNpcPict();
            npcPict.propertyGrid2.SelectedObject = new NpcPictPPGSettings(NpcFactory.Instance.PictLines[PictID].Split(','), npcPict);
        }

        string GetRaceFlag()
        {
            string RaceGender = "";
            switch (Gender_ComboBox.SelectedIndex)
            {
                case 0:
                    switch (Race_ComboBox.SelectedIndex)
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
                    switch (Race_ComboBox.SelectedIndex)
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
        private void AddPictButton_Click(object sender, EventArgs e)
        {
            string RaceGender = "";
            if (Paras.Instance.paras == null)
            {
                MessageBox.Show("没有选择道具", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            uint ItempictId = uint.Parse(Paras.Instance.paras[1]);
            if (ItempictId == 0)
            {
                MessageBox.Show("这个道具没有外观哦", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            if (!ItemFactory.Instance.PictLines.ContainsKey(ItempictId))
            {
                MessageBox.Show("找不到这个道具的外观呢/r/n请确认道具的ImageID哦", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            if (Npc_Box_Basic_PictID.Text == "")
            {
                MessageBox.Show("还没有在NPC选项卡里选择NPC呢！", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            if (Gender_ComboBox.SelectedIndex > 1 || Gender_ComboBox.SelectedIndex < 0 || Gender_ComboBox.SelectedIndex < 0 || Race_ComboBox.SelectedIndex > 4)
            {
                MessageBox.Show("NPC的性别或种族无法确定！", "☆（＞ω・） 我是番茄 （・ω＜）☆");
                return;
            }
            RaceGender = GetRaceFlag();
            string[] ItemPict = ItemFactory.Instance.PictLines[ItempictId].Split(',');
            string[] NpcPict = NpcFactory.Instance.PictLines[PictID].Split(',');
            if (itemparas[4] == "SOCKS")
            {
                NpcPict[35] = RaceGender + "_01_00_" + ItemPict[33];
                NpcPict[36] = RaceGender + "_24_00_" + ItemPict[33];
                NpcPict[37] = RaceGender + "_25_00_" + ItemPict[33];
            }
            else
            {
                for (int i = 2; i < ItemPict.Length; i++)
                {
                    if (ItemPict[i] != "" && ItemPict[i] != "null" && ItemPict != null)
                    {
                        NpcPict[i] = RaceGender + "_" + ItemPict[i];
                    }
                }
            }
            string str = "";
            for (int i = 0; i < NpcPict.Count<string>() - 1; i++)
            {
                str += NpcPict[i] + ",";
            }
            NpcFactory.Instance.PictLines[PictID] = str;
            OpenNpcPict();
            npcPict.propertyGrid2.SelectedObject = new NpcPictPPGSettings(NpcPict, npcPict);
        }
        public void OpenNpcPict()
        {
            if (!npcPict.isOpened)
                npcPict.Show();
            if (npcPict.IsDisposed)
            {
                npcPict = new NpcPict();
                npcPict.Show();
            }

            return;
        }

        private void NpcPictSave_Button_Click(object sender, EventArgs e)
        {
            //保存临时内容
            /*if (NpcName != "" && Npcindex != 0)
            {
                string str = "";
                for (int i = 0; i < npcparas.Count<string>() - 1; i++)
                {
                    str += npcparas[i] + ",";
                }
                NpcFactory.Instance.lines[(uint)Npcindex] = str;
            }
            */
            dat = new DatFile();
            dat.Open("data\\xls\\table.hed");
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ms, Encoding.GetEncoding("gbk"));
                sw.AutoFlush = true;
                foreach (string i in NpcFactory.Instance.lines.Values)
                {
                    sw.WriteLine(i);
                }
                dat.Add("npc.csv", ms.ToArray());
                sw.Close();
            }
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ms, Encoding.GetEncoding("gbk"));
                sw.AutoFlush = true;
                foreach (string i in NpcFactory.Instance.PictLines.Values)
                {
                    sw.WriteLine(i);
                }
                dat.Add("npcpict.csv", ms.ToArray());
                dat.Close();
                sw.Close();
            }
            /*ItemlistBox.Items.Clear();
            foreach (string s in ItemFactory.Instance.lines.Values)
            {
                string[] paras = s.Split(',');
                NpcListBox.Items.Add(paras[0] + "," + paras[1]);
            }*/
            MessageBox.Show("NPC.csv与Npcpict.csv保存完毕", "小番茄");
        }

        private void MobListBox_Click(object sender, EventArgs e)
        {
            ChangeLock = true;
            SaveMobPPG();
            LoadMob();
            ChangeLock = false;
        }
        void SaveMobPPG()
        {
            if (MobListBox.SelectedIndex != -1 && MonsterID != 0)
            {
                MobFactory.Instance.lines[MonsterID] = String.Join(",", mobparas);
            }
        }
        void LoadMob()
        {
            int mobindex = MobListBox.SelectedIndex;
            string[] paras = MobFactory.Instance.lines.ElementAt(mobindex).Value.Split(',');
            MobID.Text = paras[0];
            MobName.Text = paras[1];
            MobPictID.Text = paras[2];
            MobType.Text = paras[3];
            MobSize.Text = paras[4];
            if (paras[7] == "1") MobFly.Checked = true; else MobFly.Checked = false;
            if (paras[8] == "1") MobUndead.Checked = true; else MobUndead.Checked = false;
            MobLevel.Text = paras[10];
            MobHp.Text = paras[19];
            MobMp.Text = paras[21];

            MobMoveSpeed.Text = paras[22];
            MobMinATK.Text = paras[23];
            MobMaxATK.Text = paras[24];
            MobAttackType.Text = paras[25];
            MobMinMATK.Text = paras[26];
            MobMaxMATK.Text = paras[27];
            MobRightDEF.Text = paras[28];
            MobLeftDEF.Text = paras[29];
            MobRightMDEF.Text = paras[30];
            MobLeftMDEF.Text = paras[31];
            MobHIT.Text = paras[34];
            MobAvoidMelee.Text = paras[37];
            MobAvoidRanged.Text = paras[38];
            MobCure.Text = paras[42];
            MobASPD.Text = paras[44];
            MobCSPD.Text = paras[45];
            MobRange.Text = paras[46];
            MobNeutral.Text = paras[47];
            MobFire.Text = paras[48];
            MobWater.Text = paras[49];
            MobWind.Text = paras[50];
            MobEarth.Text = paras[41];
            MobHoly.Text = paras[52];
            MobDark.Text = paras[53];
            MobPoisen.Text = paras[54];
            MobStone.Text = paras[55];
            MobParalyse.Text = paras[56];
            MobSleep.Text = paras[57];
            MobSilence.Text = paras[58];
            MobBluntness.Text = paras[59];
            MobConfused.Text = paras[60];
            MobFrosen.Text = paras[61];
            MobStun.Text = paras[62];
            MobEXP.Text = paras[108];
            MobJEXP.Text = paras[109];
            mobparas = paras;
            MobPPG.SelectedObject = new MobPPGSettings(mobparas);
        }



        private void Mob_TextChanged(object sender, EventArgs e)
        {
            if (!ChangeLock)
            {
                uint mid;
                if (MobListBox.SelectedIndex == -1)
                {
                    return;
                }
                int mobindex = MobListBox.SelectedIndex;
                string[] paras = MobFactory.Instance.lines.ElementAt(mobindex).Value.Split(',');
                if (uint.TryParse(MobID.Text, out mid))
                {
                    paras[0] = MobID.Text;
                }
                paras[1] = MobName.Text;
                paras[2] = MobPictID.Text;
                paras[3] = MobType.Text;
                paras[4] = MobSize.Text;
                if (MobFly.Checked) paras[7] = "1"; else paras[7] = "0";
                if (MobUndead.Checked) paras[8] = "1"; else paras[8] = "0";
                paras[10] = MobLevel.Text;
                paras[19] = MobHp.Text;
                paras[21] = MobMp.Text;
                paras[22] = MobMoveSpeed.Text;
                paras[23] = MobMinATK.Text;
                paras[24] = MobMaxATK.Text;
                paras[25] = MobAttackType.Text;
                paras[26] = MobMinMATK.Text;
                paras[27] = MobMaxMATK.Text;
                paras[28] = MobRightDEF.Text;
                paras[29] = MobLeftDEF.Text;
                paras[30] = MobRightMDEF.Text;
                paras[31] = MobLeftMDEF.Text;
                paras[34] = MobHIT.Text;
                paras[37] = MobAvoidMelee.Text;
                paras[38] = MobAvoidRanged.Text;
                paras[42] = MobCure.Text;
                paras[44] = MobASPD.Text;
                paras[45] = MobCSPD.Text;
                paras[46] = MobRange.Text;
                paras[47] = MobNeutral.Text;
                paras[48] = MobFire.Text;
                paras[49] = MobWater.Text;
                paras[50] = MobWind.Text;
                paras[41] = MobEarth.Text;
                paras[52] = MobHoly.Text;
                paras[53] = MobDark.Text;
                paras[54] = MobPoisen.Text;
                paras[55] = MobStone.Text;
                paras[56] = MobParalyse.Text;
                paras[57] = MobSleep.Text;
                paras[58] = MobSilence.Text;
                paras[59] = MobBluntness.Text;
                paras[60] = MobConfused.Text;
                paras[61] = MobFrosen.Text;
                paras[62] = MobStun.Text;
                paras[108] = MobEXP.Text;
                paras[109] = MobJEXP.Text;
                MobFactory.Instance.lines[(uint)mobindex] = String.Join(",", paras);
                MobListBox.Items[mobindex] = paras[0] + "," + paras[1];
            }
            if (MobPictID.Text != "")
            {
                if (NpcFactory.Instance.PictLines.ContainsKey(uint.Parse(MobPictID.Text)))
                {
                    MobPictButton.Text = "显示";
                }
                else
                    MobPictButton.Text = "创建";
            }
        }

        private void AddMonster_Click(object sender, EventArgs e)
        {
            ChangeLock = true;
            uint count = (uint)MobFactory.Instance.lines.Count;
            MobFactory.Instance.lines.Add(count, "10000000,プルル,10000000,PLANT,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,");
            MobListBox.Items.Add("10000000,プルル");
            MobListBox.SelectedIndex = MobListBox.Items.Count - 1;
            SaveMobPPG();
            //NpcName = (string)NpcListBox.SelectedItem;
            //Npcindex = NpcListBox.SelectedIndex;

            //根据索引查找values
            //string element = MobFactory.Instance.lines.Values.ElementAt(MobListBox.SelectedIndex);
            LoadMob();

            ChangeLock = false;
        }

        private void Npc_Search_Button_Click(object sender, EventArgs e)
        {
            string tagString = Npc_SearchBox.Text;
            if (tagString == "")
                MessageBox.Show("请输入搜索内容", "☆（＞ω・） 我是番茄 （・ω＜）☆");
            int index = -1;
            for (int i = 0; i < NpcListBox.Items.Count; i++)
            {
                if (NpcListBox.Items[i].ToString().Contains(tagString))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
                NpcListBox.SetSelected(index, true);
            else
                MessageBox.Show("结果没有找到", "☆（＞ω・） 我是番茄 （・ω＜）☆");
        }

        private void MobSearch_Button_Click(object sender, EventArgs e)
        {
            string tagString = MobSearch_Box.Text;
            if (tagString == "")
                MessageBox.Show("请输入搜索内容", "☆（＞ω・） 我是番茄 （・ω＜）☆");
            int index = -1;
            for (int i = 0; i < MobListBox.Items.Count; i++)
            {
                if (MobListBox.Items[i].ToString().Contains(tagString))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
                MobListBox.SetSelected(index, true);
            else
                MessageBox.Show("结果没有找到", "☆（＞ω・） 我是番茄 （・ω＜）☆");
        }

        private void MobSaveButton_Click(object sender, EventArgs e)
        {
            dat = new DatFile();
            dat.Open("data\\xls\\table.hed");
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ms, Encoding.GetEncoding("gbk"));
                sw.AutoFlush = true;
                foreach (string i in MobFactory.Instance.lines.Values)
                {
                    sw.WriteLine(i);
                }
                dat.Add("monster.csv", ms.ToArray());
                dat.Close();
                sw.Close();
            }
            MessageBox.Show("monster.csv保存完毕", "小番茄");
        }

        private void UpMove_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (NpcListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("没选择哦", "an");
                    return;
                }

                if (NpcListBox.SelectedIndex > 0)
                {
                    int idx = NpcListBox.SelectedIndex;
                    NpcListBox.Items.Insert(NpcListBox.SelectedIndex - 1, NpcListBox.SelectedItem.ToString());
                    NpcListBox.Items.RemoveAt(NpcListBox.SelectedIndex);
                    NpcListBox.SelectedIndex = idx - 1;

                }
            }

            catch { }
        }

        private void DownMove_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (NpcListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("没选择哦", "an");
                    return;
                }
                if (NpcListBox.SelectedIndex < NpcListBox.Items.Count - 1)
                {
                    NpcListBox.Items.Insert(NpcListBox.SelectedIndex, NpcListBox.Items[NpcListBox.SelectedIndex + 1].ToString());
                    NpcListBox.Items.RemoveAt(NpcListBox.SelectedIndex + 1);
                }
            }

            catch { }
        }
    }
}
