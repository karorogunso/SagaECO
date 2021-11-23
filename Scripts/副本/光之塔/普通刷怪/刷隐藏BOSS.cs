
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {

        void 光塔隐藏BOSS刷怪(uint mapid, ActorPC pc)
        {
            SagaMap.Map map;
            map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 15620051, 0, 0, 29, 43, 0, 1, 0, 隐藏BOSSInfo困难(), 隐藏BOSSAI困难(),null,0)[0];
        }

    }
}

