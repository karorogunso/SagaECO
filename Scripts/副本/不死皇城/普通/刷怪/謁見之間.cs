
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
        ActorMob 第七关封印;

        void 謁見之間刷怪(uint mapid,ActorPC pc)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第七关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 3, 21, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第七关封印Ondie,1)[0];
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 17110000, 0, 0, 16, 3, 0, 1, 0, BOSS3Info(), BOSS3AI(), (MobCallback)第七关BOSS死亡Ondie, 1)[0];

            pc.Party.Leader.TInt["副本BOSSID"] = (int)mob.ActorID;
        }
        void 第七关BOSS死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            第七关封印.HP = 1;
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            if (pc.Party !=null)
            {
                foreach (var item in pc.Party.Members)
                {
                    if(item.Value.Online)
                    {
                        if(item.Value.Buff.Dead)
                        {
                            SagaMap.Network.Client.MapClient.FromActorPC(item.Value).RevivePC(item.Value);
                        }
                    }
                }
            }
        }
        void 第七关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第八关(pc);
        }
    }
}

