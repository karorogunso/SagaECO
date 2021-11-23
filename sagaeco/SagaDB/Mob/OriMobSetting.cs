using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;

namespace SagaDB.Mob
{
    public class OriMobSetting : Singleton<OriMobSetting>
    {
        public class OriMob
        {
            public bool isboss;
            public List<uint> BoundsItem;
        }
        public OriMobSetting()
        {
        }
        public Dictionary<uint, OriMob> Items = new Dictionary<uint, OriMob>();

        public void InitOriMonsterSetting(string path, Encoding encoding)
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
                    uint MobId;
                    uint.TryParse(paras[0], out MobId);
                    OriMob om = new OriMob();
                    om.isboss = paras[1] == "1" ? true : false;
                    //TODO:增加对额外掉落的读取
                    if (!Items.ContainsKey(MobId))
                        Items.Add(MobId, om);
                    else
                        Logger.ShowWarning("已有重复的OriMonster设定!ID:" + MobId);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
