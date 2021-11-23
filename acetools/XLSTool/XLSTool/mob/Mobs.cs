using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XLSTool
{
    public class LoadMobCSV
    {
        public static void LoadMobCsv(byte[] buf)
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
                MobFactory.Instance.lines.Add(count, line);
                //MobFactory.Instance.IDList.Add(uint.Parse(paras[0]));
                count++;
            }
            ms.Close();
            sr.Close();
        }
    }
}
