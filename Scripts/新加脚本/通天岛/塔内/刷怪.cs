
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace Exploration
{
    public partial class 塔内 : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public 塔内()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(20170000);
                List<ActorMob> mob = map.SpawnCustomMob(10000000, map.ID, 14110000, 0, 0, 29, 60, 12, 5, 50, 秩序之龙Info(), 秩序之龙AI(), null, 0);
                foreach (var item in mob)
                    item.TInt["playersize"] = 200;
                mob = map.SpawnCustomMob(10000000, map.ID, 14110000, 0, 0, 2, 31, 15, 5, 50, 秩序之龙Info(), 秩序之龙AI(), null, 0);
                foreach (var item in mob)
                    item.TInt["playersize"] = 200;
                mob = map.SpawnCustomMob(10000000, map.ID, 14110000, 0, 0, 32, 2, 15, 5, 50, 秩序之龙Info(), 秩序之龙AI(), null, 0);
                foreach (var item in mob)
                    item.TInt["playersize"] = 200;
                mob = map.SpawnCustomMob(10000000, map.ID, 14110000, 0, 0, 61, 32, 15, 5, 50, 秩序之龙Info(), 秩序之龙AI(), null, 0);
                foreach (var item in mob)
                    item.TInt["playersize"] = 200;
                ActorMob mob2 = map.SpawnCustomMob(10000000, map.ID, 14110002, 0, 0, 9, 32, 0, 1, 8000, 泰达尼亚龙Info(), 泰达尼亚龙AI(), null, 0)[0];
               
            }
        }
    }
}

