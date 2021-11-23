using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XLSTool
{
    public class LoadNpcCSV
    {
        public static void LoadNpcCsv(byte[] buf)
        {
            string[] paras;
            string line;
            MemoryStream ms = new MemoryStream(buf);
            StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GBK"));
            uint count = 0;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                paras = line.Split(',');
                if (line.IndexOf('#') != -1)
                    line = line.Substring(0, line.IndexOf('#'));
                if (line == "") continue;
                if (NpcFactory.Instance.lines.Keys.Contains(uint.Parse(paras[0])))
                    NpcFactory.Instance.lines[uint.Parse(paras[0])] = line;
                else
                NpcFactory.Instance.lines.Add(uint.Parse(paras[0]), line);
                //NpcFactory.Instance.IDList.Add(uint.Parse(paras[0]));
                count++;
            }
        }
        public static void LoadNpcPictCsv(byte[] buf)
        {
                string[] paras;
                string line;
                MemoryStream ms = new MemoryStream(buf);
                StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GBK"));

                while (!sr.EndOfStream)
                {

                    line = sr.ReadLine();
                    
                    if (line.IndexOf('#') != -1)
                        line = line.Substring(0, line.IndexOf('#'));
                    int s = line.IndexOf('ｓ');
                    if (line.IndexOf('ｓ') == 0) continue;
                    if (line.IndexOf('s') == 0) continue;
                    if (line == "") continue;
                    paras = line.Split(',');
                    if (paras.Length < 40)
                    {
                        string[] p = new string[40];
                        paras.CopyTo(p, 0);
                        paras = p;
                        line = String.Join(",", paras);
                    }
                    if (NpcFactory.Instance.PictLines.ContainsKey(uint.Parse(paras[0])))
                        NpcFactory.Instance.PictLines[uint.Parse(paras[0])] = line;
                    else
                        NpcFactory.Instance.PictLines.Add(uint.Parse(paras[0]), line);

                }
                ms.Close();
                sr.Close();

        }
    }
}
