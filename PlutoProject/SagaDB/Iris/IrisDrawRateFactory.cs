using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaLib.VirtualFileSystem;
using System.Xml;

namespace SagaDB.Iris
{
    public class IrisDrawRateFactory : Singleton<IrisDrawRateFactory>
    {
        public Dictionary<string, IrisDrawRate> DrawRate = new Dictionary<string, IrisDrawRate>();


        public void Init(string path, Encoding encoding)
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
                    var drawrate = new IrisDrawRate();

                    drawrate.SessionID = uint.Parse(paras[0]);
                    drawrate.PayFlag = uint.Parse(paras[1]);
                    drawrate.ItemID = uint.Parse(paras[2]);
                    drawrate.CommonRate = int.Parse(paras[3]);
                    drawrate.UnCommonRate = int.Parse(paras[4]);
                    drawrate.RatityRate = int.Parse(paras[5]);
                    drawrate.SuperRatityRate = int.Parse(paras[6]);

                    var gachakey = string.Format("{0},{1},{2}", drawrate.PayFlag, drawrate.SessionID, drawrate.ItemID);
                    if (!DrawRate.ContainsKey(gachakey))
                        DrawRate.Add(gachakey, drawrate);
                    else
                        DrawRate[gachakey] = drawrate;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }

            }
        }
    }
}
