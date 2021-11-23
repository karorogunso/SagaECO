    
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
    public partial class YugangdaoBCSpawn : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public YugangdaoBCSpawn()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10049000);
                map.SpawnCustomMob(10000000, map.ID, 10000301, 0, 0, 132, 103, 100, 3, 300, 冰冻的皮露露Info(), 冰冻的皮露露AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10030400, 0, 0, 132, 103, 100, 65, 10, 北极熊Info(), 北极熊AI(), null, 0);

            }
        }
    }
}

