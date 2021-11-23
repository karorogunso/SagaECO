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
        public Dictionary<string, IrisGacha> IrisGacha = new Dictionary<string, IrisGacha>();

        public Dictionary<uint, IrisExchangeInfo> IrisExchangeInfo = new Dictionary<uint, IrisExchangeInfo>();

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
                    IrisExchangeInfo item = new IrisExchangeInfo();
                    item.ItemID = uint.Parse(paras[0]);
                    item.Count = uint.Parse(paras[1]);
                    if(!IrisExchangeInfo.ContainsKey(item.ItemID))
                    {
                        IrisExchangeInfo.Add(item.ItemID, item);
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
                    var gacha = new IrisGacha();
                    
                    gacha.PayFlag = uint.Parse(paras[0]);
                    gacha.SessionID = uint.Parse(paras[1]);
                    gacha.SessionName = paras[2];
                    gacha.PageID = uint.Parse(paras[3]);
                    gacha.ItemID = uint.Parse(paras[4]);
                    gacha.Count = IrisExchangeInfo[gacha.ItemID].Count;
                    var gachakey = string.Format("{0},{1},{2}", gacha.PayFlag, gacha.SessionID, gacha.ItemID);
                    if (!IrisGacha.ContainsKey(gachakey))
                        IrisGacha.Add(gachakey, gacha);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
