using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XLSTool
{
    public class LoadItemCSV
    {
        public static void LoadItemCsv(byte[] buf)
        {
            string[] paras;
            string line;
            MemoryStream ms = new MemoryStream(buf);
            StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GBK"));
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                paras = line.Split(',');
                if (line.IndexOf('#') != -1)
                    line = line.Substring(0, line.IndexOf('#'));
                if (line == "") continue;
                ItemFactory.Instance.lines.Add(uint.Parse(paras[0]), line);
                //ItemFactory.Instance.lines.Add(line);
                ItemFactory.Instance.IDList.Add(uint.Parse(paras[0]));
            }
        }
        public static void LoadItemPictCsv(byte[] buf)
        {
            string[] paras;
            string line;
            MemoryStream ms = new MemoryStream(buf);
            StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GBK"));
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                paras = line.Split(',');
                if (line.IndexOf('#') != -1)
                    line = line.Substring(0, line.IndexOf('#'));
                if (line == "") continue;
                ItemFactory.Instance.PictLines.Add(uint.Parse(paras[0]), line);
            }
        }
    }
}
