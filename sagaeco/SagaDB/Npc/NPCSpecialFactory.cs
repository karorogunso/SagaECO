using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;

namespace SagaDB.Npc
{
    public class NPCSpecialFactory : Singleton<NPCSpecialFactory>
    {
        public List<uint> NpcSpecialList = new List<uint>();

        public void Init(string path, Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

            DateTime time = DateTime.Now;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.IndexOf('#') != -1)
                        line = line.Substring(0, line.IndexOf('#'));
                    if (line == "") continue;
                    paras = line.Split(',');
                    if (paras.Length < 2)
                        continue;
                    uint ID = uint.Parse(paras[0]);
                    if (!NpcSpecialList.Contains(ID))
                        NpcSpecialList.Add(ID);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
