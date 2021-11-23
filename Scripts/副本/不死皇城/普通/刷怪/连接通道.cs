
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
        ActorMob 第十三关封印;

        void 连接通道刷怪(uint mapid,ActorPC pc)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            第十三关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 1, 1, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第十三关封印Ondie, 1)[0];
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18000000, 0, 0, 16, 3, 0, 1, 0, BOSS2Info(), BOSS2AI(), (MobCallback)第十三关BOSS死亡Ondie, 1)[0];

            pc.Party.Leader.TInt["副本BOSSID"] = (int)mob.ActorID;

            ((MobEventHandler)mob.e).Defending += 第十三关BOSS_Defending;
            ((MobEventHandler)mob.e).Returning += 第十三关BOSS_Returning1;
        }

        private void 第十三关BOSS_Returning1(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "虽然不知道你们是怎么进来的，但请你们不要再来打扰小姐了！");
            }
        }

        private void 第十三关BOSS_Defending(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.99f)
                {
                SkillHandler.Instance.ActorSpeak(mob, "你、你们是怎么进来的！？");
                eh.mob.AttackedForEvent = 1;
		}
            }
            else if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.95f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "不，不行，前面是小姐的寝宫，你们不能过去！");
                    eh.mob.AttackedForEvent = 2;
                }
            }
            else if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.6f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "你们再不走我就可不客气了！");
                    eh.mob.AttackedForEvent = 3;
                }
            }
            else if (eh.mob.AttackedForEvent == 3)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.3f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "不...不行，你们休想..从这里..过去！");
                    eh.mob.AttackedForEvent = 4;
                }
            }
        }
        void 第十三关BOSS死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            ActorMob mob = e.mob;
            SkillHandler.Instance.ActorSpeak(mob, "小...小姐...");
            第十三关封印.HP = 1;
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
                    }
                }
            }
        }
        void 第十三关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第十四关(pc);
        }
    }
}

