
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

        void 光之塔老三刷怪困难(uint mapid, ActorPC pc)
        {
            SagaMap.Map map;
            map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18610050, 0, 0, 149, 63, 0, 1, 0, 老三Info困难(), 老三AI困难(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += SQuest_Defending3困难;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning3困难;
            pc.Party.Leader.TInt["副本BOSSID"] = (int)mob.ActorID;

        }

        private void SQuest_Returning3困难(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "你走吧");
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                mob.Buff.恶魂 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, mob, true);
            }
        }

        private void SQuest_Defending3困难(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                mob.Status.min_atk1 = 1800;//最低物理攻擊
                mob.Status.min_atk2 = 1800;//最低物理攻擊
                mob.Status.min_atk3 = 1800;//最低物理攻擊
                mob.Status.max_atk1 = 3200;//最高物理攻擊
                mob.Status.max_atk2 = 3200;//最高物理攻擊
                mob.Status.max_atk3 = 3200;//最高物理攻擊
                SkillHandler.Instance.ActorSpeak(mob, "何方神圣！");
                eh.mob.AttackedForEvent = 1;
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                mob.Buff.恶魂 = false;
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
            if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.3f)
                {
                    mob.Status.min_atk1 = 2800;//最低物理攻擊
                    mob.Status.min_atk2 = 2800;//最低物理攻擊
                    mob.Status.min_atk3 = 2800;//最低物理攻擊
                    mob.Status.max_atk1 = 4500;//最高物理攻擊
                    mob.Status.max_atk2 = 4500;//最高物理攻擊
                    mob.Status.max_atk3 = 4500;//最高物理攻擊
                    SkillHandler.Instance.ActorSpeak(mob, "鬼神？吾乃阿修罗！");
                    eh.mob.AttackedForEvent = 2;
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                    mob.Buff.恶魂 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, mob, true);
                }
            }
            if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < 200)
                {
                    eh.mob.AttackedForEvent = 3;
                    SkillHandler.Instance.ActorSpeak(mob, "呃啊——");
                    SkillHandler.Instance.ShowEffectOnActor(mob, 7958);
                    SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                    if (pc.Party != null)
                    {
                        foreach (var item in pc.Party.Members)
                        {
                            if (item.Value.Online)
                            {
                                TitleProccess(item.Value, 61, 1);
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
    }
}

