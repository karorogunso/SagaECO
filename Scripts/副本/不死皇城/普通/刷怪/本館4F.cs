
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
        ActorMob 第九关封印;

        void 本館4F刷怪(uint mapid)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第九关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 13, 2, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第九关封印Ondie, 1)[0];
            map.SpawnCustomMob(10000000, map.ID, 10680300, 0, 0, 8, 10, 8, 1, 0, 绿色僵尸Info(), 绿色僵尸AI(), (MobCallback)第九关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10220200, 0, 0, 8, 10, 8, 1, 0, 幽灵鬼火Info(), 幽灵鬼火AI(), (MobCallback)第九关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 8, 10, 8, 1, 0, 骷髅射手Info(), 骷髅射手AI(), (MobCallback)第九关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 8, 10, 8, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第九关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 8, 10, 8, 1, 0, 死亡守卫Info(), 死亡守卫AI(), (MobCallback)第九关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 8, 10, 8, 1, 0, 监狱守卫Info(), 监狱守卫AI(), (MobCallback)第九关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 8, 10, 8, 1, 0, 永恒追随者Info(), 永恒追随者AI(), (MobCallback)第九关小怪死亡Ondie, 1);
        }
        void 第九关小怪死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            pc.Party.Leader.TInt["不死皇城第九关变量"] += 1;
第九关封印.HP = 200000;
            if (pc.Party.Leader.TInt["不死皇城第九关变量"] >= 7)
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
                第九关封印.HP = 1;
            }
        }
        void 第九关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第十关(pc);
        }
    }
}

