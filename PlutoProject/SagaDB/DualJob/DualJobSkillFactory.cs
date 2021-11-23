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
    public class DualJobSkillFactory : Singleton<DualJobSkillFactory>
    {
        public Dictionary<ushort, List<DualJobSkill>> items = new Dictionary<ushort, List<DualJobSkill>>();

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
                        for (int i = 0; i < paras.Length; i++)
                        {
                            if (paras[i] == "")
                                paras[i] = "0";
                        }
                        DualJobSkill item = new DualJobSkill();
                        item.DualJobID = byte.Parse(paras[0]);
                        item.SkillID = ushort.Parse(paras[1]);
                        item.SkillName = paras[2];
                        item.SkillJobID = byte.Parse(paras[3]);
                        for (int i = 4; i <= 13; i++)
                        {
                            item.LearnSkillLevel.Add(byte.Parse(paras[i]));
                        }

                        if (items.ContainsKey(item.DualJobID))
                        {
                            if (items[item.DualJobID] == null)
                                items[item.DualJobID] = new List<DualJobSkill>();

                            items[item.DualJobID].Add(item);
                        }
                        else
                        {
                            items.Add(item.DualJobID, new List<DualJobSkill>() { item });
                        }

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
