using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;

namespace SagaDB.Item
{
    public class ExchangeFactory : Singleton<ExchangeFactory>
    {
        public Dictionary<uint, Exchange> ExchangeItems = new Dictionary<uint, Exchange>();
        public void Init(string path, System.Text.Encoding encoding)
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
                    var ex = new Exchange();
                    ex.SelectID = short.Parse(paras[0]);
                    ex.Returnable = short.Parse(paras[1]);
                    ex.OriItemID = uint.Parse(paras[2]);
                    ex.Type = short.Parse(paras[19]);
                    ex.OriItemName = paras[20];
                    uint[] itemids = new uint[15];
                    string[] itemnames = new string[15];

                    for (int i = 2; i < 18; i++)
                    {
                        if (paras[i + 1] != "0")
                        {

                            uint id = 0;
                            uint.TryParse(paras[i + 1], out id);
                            if (id != 0)
                            {
                                itemids[(i + 1) - 3] = id;
                                itemnames[(i + 1) - 3] = paras[18 + (i + 1)];
                            }
                        }
                    }

                    ex.ItemsID = itemids;
                    ex.ItemsName = itemnames;

                    if (!ExchangeItems.ContainsKey(ex.OriItemID))
                        ExchangeItems.Add(ex.OriItemID, ex);
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
