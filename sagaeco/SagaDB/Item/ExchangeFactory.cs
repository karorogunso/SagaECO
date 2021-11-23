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
        Dictionary<uint, List<uint>> exchangeitems = new Dictionary<uint, List<uint>>();
        public Dictionary<uint, List<uint>> ExchangeItems { get { return exchangeitems; } }
        public Dictionary<uint, uint> OriItems = new Dictionary<uint, uint>();
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
                    uint SitemID = uint.Parse(paras[2]);
                    List <uint> TitemID = new List<uint>();
                    for (int i = 2; i < 12; i++)
                    {
                        if (paras[i + 1] != "0")
                        {

                            uint id = 0;
                            uint.TryParse(paras[i + 1], out id);
                            if (id != 0)
                            {
                                TitemID.Add(id);
                                if (!OriItems.ContainsKey(id))
                                    OriItems.Add(id, SitemID);
                            }
                        }
                    }
                    if (!exchangeitems.ContainsKey(SitemID))
                        exchangeitems.Add(SitemID, TitemID);
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
