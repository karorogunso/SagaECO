using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Mob;

namespace SagaMap.Partner
{
    public class PartnerSpawnManager : Singleton<PartnerSpawnManager>
    {
        Dictionary<uint, List<ActorPartner>> mobs = new Dictionary<uint, List<ActorPartner>>();
        public PartnerSpawnManager()
        {

        }

        public int LoadAI(string f)
        {
            int total = 0;

            return total;
        }

        public void LoadAnAI(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml");
            int total = 0;
            foreach (string f in file)
            {
                total += LoadAI(f);
            }
            Logger.ShowInfo(total.ToString() + " 加载新的AI...");
        }
    }
}
