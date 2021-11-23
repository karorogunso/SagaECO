using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;

namespace SagaDB.Mob
{
    public class DropGroupFactory : Singleton<DropGroupFactory>
    {
        Dictionary<ushort, string[]> Dropgroup = new Dictionary<ushort, string[]>();

        public DropGroupFactory()
        {

        }

        public string[] GetDropRate(ushort groupid)
        {
            if (Dropgroup.ContainsKey(groupid))
                return Dropgroup[groupid];
            else
                return new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "什么也不掉" };
        }

        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;

#if !Web
            string label = "load dropgroup data...";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            string line = "";

            while (!sr.EndOfStream)
            {
                try
                {
                    line = sr.ReadLine();
                    if (line == "")
                        continue;
                    if (line.Substring(0, 1) == "#")
                        continue;

                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }

                    string[] data = new string[paras.Length - 1];
                    for (int i = 1; i < paras.Length; i++)
                    {
                        data[i - 1] = paras[i];
                    }
                    if (Dropgroup.ContainsKey(ushort.Parse(paras[0])))
                        Logger.ShowError("the group id:" + paras[0] + "is exist, skip it");
                    else
                        Dropgroup.Add(ushort.Parse(paras[0]), data);

#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif

                    count++;
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing dropgroup db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " DropGroup loaded.");
#endif
            sr.Close();
        }
    }
}
