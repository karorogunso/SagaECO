
using System;
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
namespace SagaScript.M30210000
{
    public partial class 暗鸣 : Event
    {
        void 大陆B1F刷怪(uint mapid, ActorPC pc, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 17980000, 0, 0, 62, 16, 0, 1, 0, 守护者斯芬喵喵Info(single), 守护者斯芬喵喵AI(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += SQuest_Defending1;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning1;
            ((MobEventHandler)mob.e).Dying += (y, e) =>
            {
                senddigtomember(pc, 20016);//发AAA！
                if (pc.Party != null)
                {
                    foreach (var item in pc.Party.Members)
                    {
                        if (item.Value.Online)
                        {
                            item.Value.TInt["鱼缸岛危机老三死亡"] = 1;
                            设置复活次数(item.Value);
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
            };
            mob.TInt["playersize"] = 1500;
            map.SpawnCustomMob(10000000, map.ID, 10019408, 0, 0, 39, 84, 26, 7, 0, 小偷鱼人Info(), 小偷鱼人AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10019402, 0, 0, 23, 46, 0, 1, 0, 金色鱼人Info(), 金色鱼人AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10019402, 0, 0, 105, 38, 0, 1, 0, 金色鱼人Info(), 金色鱼人AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10019402, 0, 0, 94, 69, 0, 1, 0, 金色鱼人Info(), 金色鱼人AI(), null, 0);
            if (Global.Random.Next(0, 100) < 30 && DateTime.Now.Year == 2017 && ((DateTime.Now.Month == 1 && DateTime.Now.Day >= 11) || (DateTime.Now.Month == 2 && DateTime.Now.Day <= 3)))
            {
                ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 10370000, 0, 0, 58, 40, 0, 1, 0, 迎春宝箱Info(), 迎春宝箱AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["副本新春宝箱死亡次数"]++;
            }
        }

        private void SQuest_Returning1(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "你走吧");
            }
        }

        private void SQuest_Defending1(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)//遇见BOSS时
            {
                ActorMob mob = eh.mob;
                senddigtomember(pc, 20014);//发AAA！
                eh.mob.AttackedForEvent = 1;
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
                            item.TInt["鱼缸岛危机老三死亡"] = 0;
                        }
                    }
                }
            }
            if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.5f)//BOSS血量为50%时
                {
                    senddigtomember(pc, 20015);//发AAA！
                    eh.mob.AttackedForEvent = 2;
                }
            }
            if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < 200)//BOSS血量少于200时
                {
                    eh.mob.AttackedForEvent = 3;
                }
            }
        }
    }
}

