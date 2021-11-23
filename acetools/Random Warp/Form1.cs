using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Random_Warp
{
    public partial class Form1 : Form
    {
        string [,]Read_Data_03 = new string[10000, 10];
        string[,] Read_mapname_Data03 = new string[100000000, 2];
        int i;
        int num = 0;
        int num01 = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //設定可以選取的檔案類型
            openFileDialog1.Filter = "CSV (逗點分隔) (*.csv)|*.csv";

            //呼叫檔案對話視窗
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Load_Message.Text = openFileDialog1.FileName;
            }
        }

        private void btnLoad_MapName_Click(object sender, EventArgs e)
        {
            //設定可以選取的檔案類型
            openFileDialog2.Filter = "CSV (逗點分隔) (*.csv)|*.csv";

            //呼叫檔案對話視窗
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                MapName_Message.Text = openFileDialog2.FileName;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName.Equals("warp.csv"))
            {
                MessageBox.Show("請設定warp.csv檔案路徑!!", "錯誤");
                return;
            }

            if (openFileDialog2.FileName.Equals("mapname.csv"))
            {
                MessageBox.Show("請設定mapname.csv檔案路徑!!", "錯誤");
                return;
            }

            //設定可以儲存的檔案類型
            saveFileDialog1.Filter = "C# 檔(*.cs)|*.cs";

            //呼叫檔案對話視窗
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //讀取warp.csv檔案
                FileStream File_Data = new FileStream(openFileDialog1.FileName, FileMode.Open);
                StreamReader Read_Data = new StreamReader(File_Data, Encoding.UTF8);

                char[] c = { ',' };
                string Read_Data_01;
                string[] Read_Data_02 = new string[10];
                string number00;
                int number01;
                num = 0;

                //切割資料，並儲存置記憶體
                Read_Data_01 = Read_Data.ReadLine();

                while (Read_Data_01 != null)
                {
                    Read_Data_02 = Read_Data_01.Split(c);

                    //註解判斷
                    number00 = Read_Data_02[0].Substring(0, 1);
                    if (!number00.Equals("#"))
                    {
                        //資料判斷
                        if (!Read_Data_02[1].Equals("") && !Read_Data_02[2].Equals("") && !Read_Data_02[3].Equals("") && !Read_Data_02[4].Equals("") && !Read_Data_02[5].Equals(""))
                        {
                            for (i = 0; i < 6; i++)
                            {
                                //不足補零
                                if (i == 0)
                                {
                                    number00 = Read_Data_02[0];
                                    number01 = int.Parse(number00);

                                    if (number01 < 10)
                                        Read_Data_02[0] = "000" + Read_Data_02[0];
                                    else if (number01 < 100)
                                        Read_Data_02[0] = "00" + Read_Data_02[0];
                                    else if (number01 < 1000)
                                        Read_Data_02[0] = "0" + Read_Data_02[0];
                                }

                                Read_Data_03[num, i] = Read_Data_02[i];
                            }

                            num++;
                        }
                    }

                    Read_Data_01 = Read_Data.ReadLine();
                }

                Read_Data.Close();
                File_Data.Close();

                //讀取mapname.csv檔案
                FileStream File_mapname_Data = new FileStream(openFileDialog2.FileName, FileMode.Open);
                StreamReader Read_mapname_Data = new StreamReader(File_mapname_Data, Encoding.UTF8);

                string Read_mapname_Data01;
                string[] Read_mapname_Data02 = new string[10];
                num01 = 0;
                number01 = 0;
                //切割資料，並儲存置記憶體
                Read_mapname_Data01 = Read_mapname_Data.ReadLine();

                while (Read_mapname_Data01 != null)
                {
                    Read_mapname_Data02 = Read_mapname_Data01.Split(c);

                    //註解判斷
                    number00 = Read_mapname_Data02[0].Substring(0, 1);
                    if (!number00.Equals("#"))
                    {
                        //資料判斷
                        if (!Read_mapname_Data02[1].Equals(""))
                        {
                            for (i = 0; i < 2; i++)
                            {
                                if (i == 0)
                                {
                                    number00 = Read_mapname_Data02[0];
                                    number01 = int.Parse(number00);
                                    Read_mapname_Data03[number01, i] = Read_mapname_Data02[i];
                                }
                                else
                                {
                                    Read_mapname_Data03[number01, i] = Read_mapname_Data02[i];
                                }
                            }

                            num01++;
                        }
                    }

                    Read_mapname_Data01 = Read_mapname_Data.ReadLine();
                }

                Read_mapname_Data.Close();
                File_mapname_Data.Close();

                //寫入檔案
                StreamWriter Write_Data = new StreamWriter(saveFileDialog1.FileName, true, Encoding.UTF8);

                Write_Data.WriteLine("using System;");
                Write_Data.WriteLine("using System.Collections.Generic;");
                Write_Data.WriteLine("using System.Text;");
                Write_Data.WriteLine("");
                Write_Data.WriteLine("using SagaDB.Actor;");
                Write_Data.WriteLine("using SagaMap.Scripting;");
                Write_Data.WriteLine("");
                Write_Data.WriteLine("using SagaLib;");
                Write_Data.WriteLine("using SagaScript.Chinese.Enums;");
                Write_Data.WriteLine("");
                Write_Data.WriteLine("namespace SagaScript");
                Write_Data.WriteLine("{");

                for (i = 0; i < num; i++)
                {
                    Write_Data.WriteLine("    public class P1000" + Read_Data_03[i, 0] + " : Portal");
                    Write_Data.WriteLine("    {");
                    Write_Data.WriteLine("        public P1000" + Read_Data_03[i, 0] + "()");
                    Write_Data.WriteLine("        {");
                    Write_Data.WriteLine("            Init(1000" + Read_Data_03[i, 0] + "," + Read_Data_03[i, 1] + "," + Read_Data_03[i, 2] + "," + Read_Data_03[i, 3] + "," + Read_Data_03[i, 4] + "," + Read_Data_03[i, 5] + ");");
                    Write_Data.WriteLine("        }");
                    Write_Data.WriteLine("    }");
                    Write_Data.WriteLine("    //原始地圖:");

                    number00 = Read_Data_03[i, 1];
                    number01 = int.Parse(number00);

                    Write_Data.WriteLine("    //目標地圖:" + Read_mapname_Data03[number01, 1] + "(" + Read_Data_03[i, 1] + ")");
                    Write_Data.WriteLine("    //目標坐標:(" + Read_Data_03[i, 2] + "," + Read_Data_03[i, 3] + ") ~ (" + Read_Data_03[i, 4] + "," + Read_Data_03[i, 5] + ")");
                    Write_Data.WriteLine("");
                }

                Write_Data.WriteLine("}");

                Write_Data.Close();

                MessageBox.Show("資料轉換完成!!", "完成");
            }
        }
    }
}
