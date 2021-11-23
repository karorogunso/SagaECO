
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

        void 光之塔老二刷怪困难(uint mapid, ActorPC pc)
        {
            SagaMap.Map map;
            map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18630000, 0, 0, 92, 164, 0, 1, 0, 老二Info困难(), 老二AI困难(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += SQuest_Defending2困难;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning2困难;
            pc.Party.Leader.TInt["副本BOSSID"] = (int)mob.ActorID;

        }

        private void SQuest_Returning2困难(MobEventHandler eh, ActorPC pc)
        {
            ActorMob mob = eh.mob;
            SkillHandler.Instance.ActorSpeak(mob, "我是——万碎爷！！");
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
        }

        private void SQuest_Defending2困难(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "钻头才是女人的浪漫！");
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                eh.mob.AttackedForEvent = 1;
                mob.TInt["零件数"] = 0;
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
                if (mob.HP < 50)
                {
                    eh.mob.AttackedForEvent = 2;
                    SkillHandler.Instance.ActorSpeak(mob, "怎、怎么会输给他");

                    光之塔老三刷怪困难((uint)pc.Party.Leader.TInt["S20163000"], pc);

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
    }
}

