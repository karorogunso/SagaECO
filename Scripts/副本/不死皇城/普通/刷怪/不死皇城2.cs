
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
        ActorMob 第八关封印;

        void 不死皇城2刷怪(uint mapid)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第八关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 120, 146, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第八关封印Ondie, 1)[0];
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 97, 81, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 83, 95, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200000, 0, 0, 81, 101, 0, 1, 0, 恶臭木乃伊Info(), 恶臭木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 81, 114, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 83, 121, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 84, 130, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200000, 0, 0, 82, 142, 0, 1, 0, 恶臭木乃伊Info(), 恶臭木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 93, 174, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 102, 172, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 109, 174, 0, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200000, 0, 0, 117, 172, 0, 1, 0, 恶臭木乃伊Info(), 恶臭木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 120, 157, 10, 1, 0, 灾厄木乃伊Info(), 灾厄木乃伊AI(), (MobCallback)第八关小怪死亡Ondie, 1);
        }
        void 第八关小怪死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            pc.Party.Leader.TInt["不死皇城第八关变量"] += 1;
第八关封印.HP = 200000;
            if (pc.Party.Leader.TInt["不死皇城第八关变量"] >= 11)
            {
                if (pc.Party != null)
                {
                    foreach (var item in pc.Party.Members)
                    {
                        if (item.Value.Online)
                        {
                            if (item.Value.Buff.Dead)
                            {
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Value).RevivePC(item.Value);
                            }
                        }
                    }
                }
                第八关封印.HP = 1;
            }
        }
      　 void 第八关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第九关(pc);
        }
    }
}

