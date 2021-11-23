using SagaLib;
using SagaLib.VirtualFileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.MasterEnchance
{
    public class MasterEnhanceMaterialFactory : Singleton<MasterEnhanceMaterialFactory>
    {
        public Dictionary<uint, MasterEnhanceMaterial> Items = new Dictionary<uint, MasterEnhanceMaterial>();

        public void Init(string path, System.Text.Encoding encoding)
        {
            using (var sr = new StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    try
                    {
                        if (line == "")
                            continue;
                        if (line.Substring(0, 1) == "#")
                            continue;
                        var paras = line.Split(',');
                        var tmp = new MasterEnhanceMaterial();
                        tmp.ID = uint.Parse(paras[0]);
                        tmp.Name = paras[1];
                        tmp.Ability = (MasterEnhanceType)int.Parse(paras[2]);
                        tmp.MinValue = short.Parse(paras[3]);
                        tmp.MaxValue = short.Parse(paras[4]);
                        Items.Add(tmp.ID, tmp);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        continue;
                    }
                }
            }
        }
    }
}
