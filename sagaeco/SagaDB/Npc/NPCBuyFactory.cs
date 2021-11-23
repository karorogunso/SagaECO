using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
namespace SagaDB.Item
{
    public class NPCBuyFactory : Singleton<PlayerTitleFactory>
    {
        public class Price
        {
            public uint minPrice ,maxPrice;
        }
        Dictionary<uint, Price> NpcBuyList = new Dictionary<uint, Price>();
        public void Init(string path, Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

            DateTime time = DateTime.Now;

            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    Price item = new Price();
                    uint id = uint.Parse(paras[0]);
                    item.minPrice = uint.Parse(paras[1]);
                    item.maxPrice = uint.Parse(paras[2]);
                    if (!NpcBuyList.ContainsKey(id))
                        NpcBuyList.Add(id, item);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            sr.Close();
        }
    }
}
