using SagaLib;
using SagaLib.VirtualFileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.Item
{
    public class ItemExchangeListFactory : Singleton<ItemExchangeListFactory>
    {
        public Dictionary<uint, ItemExchangeList> ExchangeList = new Dictionary<uint, ItemExchangeList>();

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
                    var exl = new ItemExchangeList();
                    exl.ItemID = uint.Parse(paras[0]);
                    exl.OriItemID = uint.Parse(paras[1]);

                    if (!ExchangeList.ContainsKey(exl.ItemID))
                        ExchangeList.Add(exl.ItemID, exl);
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
