using SagaLib;
using SagaLib.VirtualFileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.Map
{
    public class MapNameFactory : Singleton<MapNameFactory>
    {
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
                    if (!MapInfoFactory.Instance.MapInfo.ContainsKey(uint.Parse(paras[0])))
                        continue;

                    MapInfoFactory.Instance.MapInfo[uint.Parse(paras[0])].name = paras[1];
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
