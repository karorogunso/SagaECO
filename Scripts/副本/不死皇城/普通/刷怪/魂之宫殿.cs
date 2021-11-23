
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

        void 魂之宫殿刷怪(uint mapid, ActorPC pc)
        {
            SagaMap.Map map;
            map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 16420000, 0, 0, 25, 10, 0, 1, 0, BOSS1Info(), BOSS1AI(), (MobCallback)第十五关BOSS死亡Ondie, 1)[0];
            ((MobEventHandler)mob.e).Defending += GQuest_Defending;
            ((MobEventHandler)mob.e).Returning += GQuest_Returning;
            pc.Party.Leader.TInt["副本BOSSID"] = (int)mob.ActorID;

        }

        private void GQuest_Returning(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "请慢走~！");
            }
        }

        private void GQuest_Defending(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.99f)
                {
                SkillHandler.Instance.ActorSpeak(mob, "原来是你们这群家伙！");
                eh.mob.AttackedForEvent = 1;
		}
            }
            else if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.5f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "呃...好痛啊，你们这群家伙，本小姐可饶不了你们！");
                    eh.mob.AttackedForEvent = 2;
                }
            }
            else if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.25f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "什么！？你、你们这群家伙认错人了！本小姐才不是什么番茄！...");
                    eh.mob.AttackedForEvent = 3;
                }
            }
            else if (eh.mob.AttackedForEvent == 3)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.1f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "不、不行..我还不能倒下...");
                    eh.mob.AttackedForEvent = 4;
                }
            }
        }

        void 第十五关BOSS死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            ActorMob mob = e.mob;
            SkillHandler.Instance.ActorSpeak(mob, "沙..沙月...对不起....");
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
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
                        string s = "队友" + item.Value.Name + " 在本次副本总共造成伤害：" + item.Value.TInt["伤害统计"].ToString() + " 受到伤害：" + item.Value.TInt["受伤害统计"].ToString() +
                            " 共治疗：" + item.Value.TInt["治疗统计"].ToString() + " 共受到治疗：" + item.Value.TInt["受治疗统计"].ToString() + " 总死亡次数：" + item.Value.TInt["死亡统计"];
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

