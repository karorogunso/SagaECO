
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

        void 光之塔老一刷怪(uint mapid, ActorPC pc)
        {
            SagaMap.Map map;
            map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 19010157, 0, 0, 77, 74, 0, 1, 0, 老一Info(), 老一AI(), (MobCallback)BOSS1死亡Ondie, 1)[0];
            ((MobEventHandler)mob.e).Defending += SQuest_Defending;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning;
            pc.Party.Leader.TInt["副本BOSSID"] = (int)mob.ActorID;

        }

        private void SQuest_Returning(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "姐姐大人，姐姐大人...");
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                mob.Buff.三转植物寄生 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, mob, true);

                if (eh.AI.Mode.SkillOfLong.ContainsKey(31034))
                    eh.AI.Mode.SkillOfLong[31034].CD = 1;
                if (eh.AI.Mode.SkillOfShort.ContainsKey(31034))
                    eh.AI.Mode.SkillOfShort[31034].CD = 1;
                if (eh.AI.Mode.SkillOfShort.ContainsKey(31033))
                    eh.AI.Mode.SkillOfShort[31033].CD = 30;
            }
        }

        private void SQuest_Defending(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                mob.TInt["森罗万触-始"] = 1;
                SkillHandler.Instance.ActorSpeak(mob, "...姐姐大人？");
                eh.mob.AttackedForEvent = 1;
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                mob.Buff.三转植物寄生 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, mob, true);

                if (pc.Party != null)
                {
                    foreach (var item in pc.Party.Members.Values)
                    {
                        if (item != null)
                        {
                            item.TInt["伤害统计"] = 0;
                            item.TInt["受伤害统计"] = 0;
                            item.TInt["治疗统计"] = 0;
                            item.TInt["受治疗统计"] = 0;
                        }
                    }
                }
            }
            else if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.95f)
                {
                    //SkillHandler.Instance.ActorSpeak(mob, "你不是姐姐大人！！");
                    eh.mob.AttackedForEvent = 2;
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                    mob.Buff.三转植物寄生 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, mob, true);
                }
            }
            else if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.1f)
                {
                    if (eh.AI.Mode.SkillOfShort.ContainsKey(31033))
                        eh.AI.Mode.SkillOfShort[31033].CD = 10;
                    SkillHandler.Instance.ActorSpeak(mob, "还没见到姐姐大人，还不能倒下。");
                    eh.mob.AttackedForEvent = 3;
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                    mob.Buff.三转植物寄生 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, mob, true);
                }
            }
            else if (eh.mob.AttackedForEvent == 3)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < 500)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "姐...姐姐大人....");
                    eh.mob.AttackedForEvent = 4;
                    光之塔老二刷怪((uint)pc.Party.Leader.TInt["S20146000"], pc);

                    Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
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
                                string s = "队友" + item.Value.Name + " 在本次战斗总共造成伤害：" + item.Value.TInt["伤害统计"].ToString() + " 受到伤害：" + item.Value.TInt["受伤害统计"].ToString() +
                                    " 共治疗：" + item.Value.TInt["治疗统计"].ToString() + " 共受到治疗：" + item.Value.TInt["受治疗统计"].ToString();
                                foreach (var item2 in pc.Party.Members)
                                {
                                    if (item2.Value.Online)
                                        SagaMap.Network.Client.MapClient.FromActorPC(item2.Value).SendSystemMessage(s);
                                }
                            }
                        }
                    }
                }
            }
        }

        void BOSS1死亡Ondie(MobEventHandler e, ActorPC pc)
        {

        }
    }
}

