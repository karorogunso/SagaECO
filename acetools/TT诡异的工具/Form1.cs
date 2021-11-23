using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 測試
{
    public partial class Form1 : Form
    {
        CSVFILE ITEM = new CSVFILE();
        int mod = 157;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "")
            {
                MessageBox.Show("未選擇模式", "错误");
                return;
            }
            CSVFILE SUJU = new CSVFILE();
            if (SUJU.Load(Encoding.Default))
            {
                if (this.comboBox1.Text == "開箱子(Treasure)")
                {
                    this.textBox1.Text = SUJU.Treasure();
                }
                else if (this.comboBox1.Text == "怪物數據(monster)")
                {
                    textBox1.Text = SUJU.Monster();
                }
                else
                    MessageBox.Show("選擇模式錯誤");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("没有需要保存的数据", "警告");
                return;
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.Title = "保存文件";
            if ((saveFileDialog1.ShowDialog()) == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != null)
                {
                    FileStream st = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    StreamWriter s = new StreamWriter(st, System.Text.Encoding.GetEncoding("utf-8"));
                    s.Write(this.textBox1.Text);
                    s.Close();
                    st.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            NPCSAY sdf = new NPCSAY();
            sdf.Transform(tbSrc.Text);
            tbDst.Text = sdf.svvv;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (ITEM.Data.Count == 0)
            {
                MessageBox.Show("文件未打开", "错误");
            }
            else
            {
                dataGridView1.Rows.Clear();
                ITEM.Search(comboBox2.Text, textBox2.Text, mod);
                foreach (string[] aa in ITEM.temp)
                    dataGridView1.Rows.Add(aa[0], aa[1], aa[2]);
                label2.Text = "总共找到" + (int.Parse(dataGridView1.Rows.Count.ToString()) - 1) + "个结果.";

                if (this.dataGridView1.Rows.Count != 0)
                {
                    for (int i = 1; i < this.dataGridView1.Rows.Count; )
                    {
                        this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
                        i += 2;
                        
                    }
                }
            }
        }

        private void 重新打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mod = 157;
            if (ITEM.Load(Encoding.Default))
            {
                List<string> TEM = new List<string>();
                foreach (string asdfe in comboBox2.Items)
                    TEM.Add(asdfe);
                try
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add(" 所有(ALL)");
                    foreach (string[] reader in ITEM.Data)
                    {
                        if (comboBox2.Items.IndexOf(reader[4]) < 0)
                        {
                            comboBox2.Items.Add(reader[4]);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("数据读取错误");
                    comboBox2.Items.Add(" 所有(ALL)");
                    foreach (string asdfe in TEM)
                        comboBox2.Items.Add(asdfe);
                }
                TEM.Clear();
                if (comboBox2.Text == "")
                    comboBox2.Text = " 所有(ALL)";
            }
        }

        private void 用日文编码打开文件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mod = 159;
            /*
            for (int aaa = 0; aaa < ITEM.Data.Count;aaa++ )
            {
                for (int bbb = 0; bbb < ITEM.Data[aaa].LongLength; bbb++)
                {
                    byte[] temp;
                    temp = Encoding.Default.GetBytes(ITEM.Data[aaa][bbb]);
                    temp = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.GetEncoding("Shift_jis"), temp);
                    ITEM.Data[aaa][bbb] = Encoding.Default.GetString(temp);
                }
            }//*/

            if (ITEM.Load(Encoding.GetEncoding("Shift_JIS")))
            {
                List<string> TEM = new List<string>();
                foreach (string asdfe in comboBox2.Items)
                    TEM.Add(asdfe);
                try
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add(" 所有(ALL)");
                    foreach (string[] reader in ITEM.Data)
                    {
                        if (comboBox2.Items.IndexOf(reader[4]) < 0)
                        {
                            comboBox2.Items.Add(reader[4]);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("数据读取错误");
                    comboBox2.Items.Add(" 所有(ALL)");
                    foreach (string asdfe in TEM)
                        comboBox2.Items.Add(asdfe);
                }
                TEM.Clear();
                if (comboBox2.Text == "")
                    comboBox2.Text = " 所有(ALL)";
            }//*/
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            tbDst.Text = "";
            tbSrc.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            }
            catch
            {
                //MessageBox.Show("复制出错!", "错误");
            }
        }
    }
}