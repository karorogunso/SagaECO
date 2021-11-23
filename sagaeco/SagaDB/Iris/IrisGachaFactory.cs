using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Item
{
    public class IrisGachaFactory : Singleton<IrisGachaFactory>
    {
        public Dictionary<uint, IrisGacha> IrisGacha = new Dictionary<uint, IrisGacha>();

        public void InitBlack(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

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
                    IrisGacha item = new Iris.IrisGacha();
                    item.itemid = uint.Parse(paras[0]);
                    item.count = uint.Parse(paras[1]);
                    if(!IrisGacha.ContainsKey(item.itemid))
                    {
                        IrisGacha.Add(item.itemid, item);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }

        public void InitWindow(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

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
                    uint itemid = uint.Parse(paras[4]);
                    if (IrisGacha.ContainsKey(itemid))
                        IrisGacha[itemid].pageID = uint.Parse(paras[3]);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
