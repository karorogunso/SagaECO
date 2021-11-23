
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
        ActorMob 第五关封印;

        void 皇宫2F刷怪(uint mapid)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第五关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 20, 2, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第五关封印Ondie, 1)[0];
            map.SpawnCustomMob(10000000, map.ID, 10680300, 0, 0, 37, 11, 10, 2, 0, 蓝色鬼魂Info(), 蓝色鬼魂AI(), (MobCallback)第五关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10220200, 0, 0, 37, 11, 10, 2, 0, 幽灵鬼火Info(), 幽灵鬼火AI(), (MobCallback)第五关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 4, 11, 10, 2, 0, 骷髅射手Info(), 骷髅射手AI(), (MobCallback)第五关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 4, 11, 10, 2, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第五关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 37, 11, 10, 1, 0, 死亡守卫Info(), 死亡守卫AI(), (MobCallback)第五关小怪死亡Ondie, 1);

        }
        void 第五关小怪死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            pc.Party.Leader.TInt["不死皇城第五关变量"] += 1;
第五关封印.HP = 200000;
            if (pc.Party.Leader.TInt["不死皇城第五关变量"] >= 9)
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
                第五关封印.HP = 1;
            }
        }
        void 第五关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第六关(pc);
        }
    }
}

