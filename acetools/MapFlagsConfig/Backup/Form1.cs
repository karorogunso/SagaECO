using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace MAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<string[]> Data = new List<string[]>();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "选择文件";
            fileDialog.Filter = "CSV FILE (*.CSV)|*.CSV";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Data.Clear();
                FileStream Csv = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader File = new StreamReader(Csv, Encoding.Default);
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

                dataGridView1.Rows.Clear();

                foreach (string[] hang in Data)
                {
                    string name = "null";
                    if (hang[4] != "0")
                        foreach (string[] reader in Data)
                        {
                            if ((reader[0].IndexOf(hang[4]) != -1))
                            {
                                name = reader[1] + "(" + hang[4] + ")";
                            }
                        }
                    dataGridView1.Rows.Add(hang[0], hang[1],name, 0, 0, 0, 0, 0, 0, 0);
                }
                MessageBox.Show("文件读取结束","OK");
            }
        }

        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Value.ToString() == "0")
                dataGridView1.CurrentCell.Value = "1";
            else if (dataGridView1.CurrentCell.Value.ToString() == "1")
                dataGridView1.CurrentCell.Value = "0";    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "选择文件";
            fileDialog.Filter = "XML FILE (*XML)|*.XML";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileDialog.FileName);
                if (dataGridView1.RowCount != 0)
                    for (int j = 0; ds.Tables[0].Rows.Count > j; j++)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            try
                            {
                                int bji = int.Parse(ds.Tables[0].Rows[j].ItemArray[1].ToString());
                                string we = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                string er = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                                if (we == er)
                                {
                                    if (bji / 64 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[9].Value = "1";
                                    }
                                    if ((bji % 64) / 32 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[8].Value = "1";
                                    }
                                    if (((bji % 64) % 32) / 16 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[7].Value = "1";
                                    }
                                    if ((((bji % 64) % 32) % 16) / 8 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[6].Value = "1";
                                    }
                                    if (((((bji % 64) % 32) % 16) % 8) / 4 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[5].Value = "1";
                                    }
                                    if ((((((bji % 64) % 32) % 16) % 8) % 4) / 2 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[4].Value = "1";
                                    }
                                    if (((((((bji % 64) % 32) % 16) % 8) % 4) % 2) / 1 >= 1)
                                    {
                                        dataGridView1.Rows[i].Cells[3].Value = "1";
                                    }
                                    break;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                else
                {
                    for (int j = 0; ds.Tables[0].Rows.Count > j; j++)
                    {
                        int bji = int.Parse(ds.Tables[0].Rows[j].ItemArray[1].ToString());
                        string er = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                        byte ms_0 = 0, ms_1 = 0, ms_2 = 0, ms_3 = 0, ms_4 = 0, ms_5 = 0, ms_6 = 0;
                        if (bji / 64 >= 1)
                        {
                            ms_0 = 1;
                        }
                        if ((bji % 64) / 32 >= 1)
                        {
                            ms_1 = 1;
                        }
                        if (((bji % 64) % 32) / 16 >= 1)
                        {
                            ms_2 = 1;
                        }
                        if ((((bji % 64) % 32) % 16) / 8 >= 1)
                        {
                            ms_3 = 1;
                        }
                        if (((((bji % 64) % 32) % 16) % 8) / 4 >= 1)
                        {
                            ms_4 = 1;
                        }
                        if ((((((bji % 64) % 32) % 16) % 8) % 4) / 2 >= 1)
                        {
                            ms_5 = 1;
                        }
                        if (((((((bji % 64) % 32) % 16) % 8) % 4) % 2) / 1 >= 1)
                        {
                            ms_6 = 1;
                        }
                        dataGridView1.Rows.Add(er, "null", "null", ms_6, ms_5, ms_4, ms_3, ms_2, ms_1, ms_0);
                    }
 
                }
                MessageBox.Show("文件读取结束", "OK");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1 == null)
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
                    s.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<!--\r\n        Healing = 1,\r\n        Cold = 2,\r\n        Hot = 4,\r\n        Wet = 8,\r\n        Wrp = 16,\r\n        Dominion = 32,\r\n        FGarden = 64,\r\n<Map MapID=\"\">1</Map>\r\n-->\r\n<MapFlags>\r\n");
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string ms_0 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        string ms_1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        int ms = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) + int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())*2 +int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())*4 +int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString())*8 +int.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString())*16 +int.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString())*20 +int.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString())*64;
                        s.Write("  <Map MapID=\"{0}\">{1}</Map>//{2}\r\n", ms_0, ms, ms_1);
                    }
                    s.Write("</MapFlags>");
                    s.Close();
                    st.Close();

                    MessageBox.Show("保存成功", "OK");
                }
            }
        }
    }
}
