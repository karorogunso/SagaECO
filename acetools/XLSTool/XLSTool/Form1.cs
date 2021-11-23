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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public DatFile dat;
        LoadTGA tga = new LoadTGA();
        private void Form1_Load(object sender, EventArgs e)
        {
            if(dat != null)
                dat.Close();
            dat = new DatFile();
            dat.Open("data\\xls\\table.hed");
            tga.Open("data\\sprite\\item\\item.hed");
            LoadTGA.Files = tga.headers;
            LoadCSV.LoadCsv(dat.Extract("item.csv"));
            foreach (string s in ItemFactory.Instance.lines.Values)
            {
                string[] paras = s.Split(',');
                ItemlistBox.Items.Add(paras[0] + "," + paras[3]);
            }
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
            for (int i = ItemlistBox.SelectedIndex + 1; i < ItemlistBox.Items.Count; i++)
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
                    if(it.NONE.Contains<string>(paras[4]))
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
            //显示预览
            string ItemName = (string)ItemlistBox.SelectedItem;
            uint id = uint.Parse(ItemName.Substring(0, ItemName.IndexOf(',')));
            string[] paras = ItemFactory.Instance.lines[id].Split(',');
            pictureBox1.Image = ItemFactory.Instance.ShowTGA(paras, tga);

            //显示数据
            propertyGrid1.SelectedObject = new PPGSettings(paras);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }




    }
}
