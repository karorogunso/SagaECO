
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        ActorMob 谷仓地带封印;
        void 谷仓地带刷怪(uint mapid)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            //map.SpawnCustomMob(10000000, map.ID, 16290000, 0,0,100,100, 200, 10, 0, 迷你兔Info(), 迷你兔AI(),null,1);
            谷仓地带封印= map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 199, 215, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)Ondie,1)[0];
            谷仓地带封印.HP = 10000;
        }
        void Ondie(MobEventHandler e, ActorPC pc)
        {
            第二关(pc);
        }
    }
}

