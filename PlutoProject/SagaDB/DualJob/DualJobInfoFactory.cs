using SagaLib;
using SagaLib.VirtualFileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SagaDB.DualJob
{
    public class DualJobInfoFactory : Singleton<DualJobInfoFactory>
    {
        public Dictionary<byte, DualJobInfo> items = new Dictionary<byte, DualJobInfo>();

        public void Init(string path, System.Text.Encoding encoding)
        {
            using (var sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding))
            {
                DateTime time = DateTime.Now;

                string[] paras;
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();

                    try
                    {
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#") continue;

                        paras = line.Split(',');
                        DualJobInfo item = new DualJobInfo();
                        item.DualJobID = byte.Parse(paras[0]);
                        item.DualJobName = paras[1];
                        item.BaseJobID = byte.Parse(paras[2]);
                        item.ExperJobID = byte.Parse(paras[3]);
                        item.TechnicalJobID = byte.Parse(paras[4]);
                        item.ChronicleJobID = byte.Parse(paras[5]);
                        item.Description = paras[6];

                        if (!items.ContainsKey(item.DualJobID))
                            items.Add(item.DualJobID, item);
                        else
                            items[item.DualJobID] = item;

                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }

                sr.Close();
                sr.Dispose();
            }
        }
    }
}
