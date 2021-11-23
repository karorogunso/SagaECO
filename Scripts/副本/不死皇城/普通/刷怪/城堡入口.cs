
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

        ActorMob 第三关封印;

        void 城堡入口刷怪(uint mapid)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第三关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 8, 2, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第三关封印Ondie, 1)[0];
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 6, 26, 0, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 9, 26, 0, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 4, 15, 0, 1, 0, 骷髅射手Info(), 骷髅射手AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 4, 18, 0, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 13, 15, 0, 1, 0, 骷髅射手Info(), 骷髅射手AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 13, 18, 0, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 6, 5, 0, 1, 0, 骷髅射手Info(), 骷髅射手AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 6, 3, 0, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 10, 5, 0, 1, 0, 骷髅射手Info(), 骷髅射手AI(), (MobCallback)第三关小怪死亡Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 10, 3, 0, 1, 0, 骷髅击剑士Info(), 骷髅击剑士AI(), (MobCallback)第三关小怪死亡Ondie, 1);

        }
        void 第三关小怪死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            pc.Party.Leader.TInt["不死皇城第三关变量"] += 1;
第三关封印.HP = 200000;
            if (pc.Party.Leader.TInt["不死皇城第三关变量"] >= 10)
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
                第三关封印.HP = 1;
            }
        }
        void 第三关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第四关(pc);
        }
    }
}

