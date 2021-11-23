
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
    public partial class KitaAreaBOSS : Event
    {
        ActorMob mob;

        void 刷怪()
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(20212000);
            mob = map.SpawnCustomMob(10000000, 20212000, 16290000, 0, 0, 60, 24, 0, 1, 0, 外塔Info(), 外塔AI(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += KitaAreaBOSS_Defending;
        }

        private void KitaAreaBOSS_Defending(MobEventHandler eh, ActorPC pc)
        {
            ActorMob mob = eh.mob;
            if (mob.HP < 50 && eh.mob.AttackedForEvent != 1)
            {
                SkillHandler.Instance.ActorSpeak(mob, "T_T");
                SagaMap.Map map;
                map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                List<Actor> actors = map.GetActorsArea(mob, 5000, false);
                eh.mob.AttackedForEvent = 1;
                foreach (var item in actors)
                {
                    if (item.type == ActorType.PC)
                    {
                        ActorPC m = (ActorPC)item;
                        if (m.Online && m != null)
                        {
                            if (m.Buff.Dead)
                                SagaMap.Network.Client.MapClient.FromActorPC(m).RevivePC(m);
                            string s = "玩家 " + m.Name + " 在本次战斗总共造成伤害：" + m.TInt["伤害统计"].ToString() + " 受到伤害：" + m.TInt["受伤害统计"].ToString() +
                                   " 共治疗：" + m.TInt["治疗统计"].ToString() + " 共受到治疗：" + m.TInt["受治疗统计"].ToString();
                            foreach (var item2 in actors)
                            {
                                if (item2.type == ActorType.PC)
                                {
                                    ActorPC m2 = (ActorPC)item2;
                                    if (m2.Online && m2 != null)
                                        SagaMap.Network.Client.MapClient.FromActorPC(m2).SendSystemMessage(s);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

