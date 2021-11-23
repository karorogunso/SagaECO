
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        ActorMob 第十四关封印;

        void 礼拜堂2F刷怪(uint mapid)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第十四关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 30, 20, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第十四关封印Ondie, 1)[0];
            第十四关封印.HP = 200000;
        }
        void 第十四关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第十五关(pc);
        }
    }
}

