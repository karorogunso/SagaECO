
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
namespace WeeklyExploration
{
    public partial class ShuixiangSpawn : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public ShuixiangSpawn()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(11053000);
                map.SpawnCustomMob(10000000, map.ID, 10140700, 0, 0, 118, 193, 80, 12, 60, 秃鹰Info(), 秃鹰AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10140700, 0, 0, 203, 116, 40, 4, 60, 秃鹰Info(), 秃鹰AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14150000, 0, 0, 54, 190, 80, 12, 60, 寄居鼻涕虫Info(), 寄居鼻涕虫AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14120500, 0, 0, 146, 55, 80, 16, 60, 石龟Info(), 石龟AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10870300, 0, 0, 86, 84, 80, 16, 60, 蓝色水晶海胆Info(), 蓝色水晶海胆AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10620400, 0, 0, 182, 189, 80, 12, 60, 啾啾Info(), 啾啾AI(), null, 0);
            }
        }
    }
}

