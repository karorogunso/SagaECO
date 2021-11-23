using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XLSTool
{
    public class LoadFaceCSV
    {
        public static void LoadFaceCsv(byte[] buf)
        {
            string[] paras;
            string line;
            uint faceid;
            string facepict ="";
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
                    if(!FaceFactory.Instance.lines.ContainsKey(faceid))
                    FaceFactory.Instance.lines.Add(faceid, facepict);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            ms.Close();
            sr.Close();
        }
    }
}
