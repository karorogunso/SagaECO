
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
namespace SagaScript.M30210000
{
    public partial class PVPTEST : Event
    {
        ActorMob 南部外塔;
        ActorMob 南部内塔;
        ActorMob 南部主基地;
        ActorMob 北部外塔;
        ActorMob 北部内塔;
        ActorMob 北部主基地;

        void 刷怪(uint mapid)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            南部主基地 = map.SpawnCustomMob(40053000, map.ID, 30500000, 0, 0, 41, 25, 0, 1, 0, 南国石像Info(), 南国石像AI(), (MobCallback)南部主基地Ondie, 1)[0];
            北部主基地 = map.SpawnCustomMob(40054000, map.ID, 30530000, 0, 0, 7, 25, 0, 1, 0, 北国石像Info(), 北国石像AI(), (MobCallback)北部主基地Ondie, 1)[0];
        }
        void 南部外塔Ondie(MobEventHandler e, ActorPC pc)
        {
        }
        void 南部内塔Ondie(MobEventHandler e, ActorPC pc)
        {
        }
        void 南部主基地Ondie(MobEventHandler e, ActorPC pc)
        {
            Announce("恭喜北国队在本次PVP中赢得胜利！");
        }
        void 北部外塔Ondie(MobEventHandler e, ActorPC pc)
        {
        }
        void 北部内塔Ondie(MobEventHandler e, ActorPC pc)
        {
        }
        void 北部主基地Ondie(MobEventHandler e, ActorPC pc)
        {
            Announce("恭喜南国队在本次PVP中赢得胜利！");
        }
    }
}

