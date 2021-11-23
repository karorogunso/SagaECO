
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
namespace SagaScript.M30210000
{
    public partial class 暗鸣 : Event
    {
        void 右岛刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10060000, 0, 0, 189, 51, 29, 5, 0, 飞鱼Info(), 飞鱼AI(), Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 15810000, 0, 0, 189, 51, 29, 3, 0, 蓝袍河鲀Info(), 蓝袍河鲀AI(), Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 15180000, 0, 0, 181, 33, 0, 1, 0, 贵族鱼人Info(), 贵族鱼人AI(), Ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 15180000, 0, 0, 208, 59, 0, 1, 0, 贵族鱼人Info(), 贵族鱼人AI(), Ondie, 1);

            if (Global.Random.Next(0, 100) < 30 && DateTime.Now.Year == 2017 && ((DateTime.Now.Month == 1 && DateTime.Now.Day >= 11) || (DateTime.Now.Month == 2 && DateTime.Now.Day <= 3)))
            {
                ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 10370000, 0, 0, 205, 51, 0, 1, 0, 迎春宝箱Info(), 迎春宝箱AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["副本新春宝箱死亡次数"]++;
            }
        }


        void 左岛刷怪(uint mapid, ActorPC pc, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(19010032, map.ID, 19010032, 0, 0, 97, 78, 0, 1, 0, 正体不明Info(single), 正体不明AI(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += SQuest_Defending;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning;
            ((MobEventHandler)mob.e).Dying += (f, g) =>
            {
                string day = DateTime.Now.ToString("d");
                SInt[day + "鱼缸团的异变老四死亡次数"]++;
                f.mob.AttackedForEvent = 4;
                senddigtomember(pc, 20021);//发AAA！
                senddigtomember(pc, 20022);//发AAA！
                if (pc.Party == null)
{
                                if (Global.Random.Next(0, 100) < 70)
                                    GiveItem(pc, 910000028, 1);
                    TitleProccess(pc, 4, 1);
}
                if (pc.Party != null)
                {
                    foreach (var item in pc.Party.Members)
                    {
                        if (item.Value.Online)
                        {
                            TitleProccess(item.Value, 4, 1);

                            item.Value.TInt["鱼缸岛危机老四死亡"] = 1;
                            设置复活次数(item.Value);

                            if (item.Value.Buff.Dead)
                            {
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Value).RevivePC(item.Value);
                            }

                            if (item.Value.CInt["雲切任务"] <= 2 && CountItem(item.Value, 910000098) < 1)
                            {
                                if (Global.Random.Next(0, 100) < 20)
                                {
                                    GiveItem(item.Value, 910000098, 1);
                                    item.Value.CInt["雲切任务"] = 2;
                                    Say(item.Value, 0, "这……似乎是天天说的$R$R【进阶雲切秘籍】!?");
                                }
                            }

                                if (Global.Random.Next(0, 100) < 70)
                                    GiveItem(item.Value, 910000028, 1);
                                /*if (item.Value.CInt["异变平息者称号获得"] != 1)
                                {
                                    GiveItem(item.Value, 130000023, 1);
                                    item.Value.CInt["异变平息者称号获得"] = 1;
                                }*/
                            

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

            if (Global.Random.Next(0, 100) < 30 && DateTime.Now.Year == 2017 && ((DateTime.Now.Month == 1 && DateTime.Now.Day >= 11) || (DateTime.Now.Month == 2 && DateTime.Now.Day <= 3)))
            {
                ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 10370000, 0, 0, 102, 90, 0, 1, 0, 迎春宝箱Info(), 迎春宝箱AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["副本新春宝箱死亡次数"]++;
            }
        }

        void Ondie(MobEventHandler e, ActorPC pc)
        {
            if (pc.Party == null)
            {
                pc.TInt["岛1小怪死亡数"]++;
                if (pc.TInt["岛1小怪死亡数"] == 6)
                {
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)pc.TInt["S10054100"]);
                    ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18020000, 0, 0, 154, 49, 0, 1, 0, 守护者安塔利亚Info(true), 守护者安塔利亚AI(), null, 0)[0];
                    mob.TInt["playersize"] = 1500;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                }
                return;
            }
            if (pc.Party.Leader == null) return;
            pc.Party.Leader.TInt["岛1小怪死亡数"]++;
            if (pc.Party.Leader.TInt["岛1小怪死亡数"] == 6)
            {
                Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)pc.Party.Leader.TInt["S10054100"]);
                ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18020000, 0, 0, 154, 49, 0, 1, 0, 守护者安塔利亚Info(false), 守护者安塔利亚AI(), null, 0)[0];
                ((MobEventHandler)mob.e).Defending += SQuest_Defending3;
                ((MobEventHandler)mob.e).Returning += SQuest_Returning3;
                ((MobEventHandler)mob.e).Dying += (k, y) =>
                {
                    string day = DateTime.Now.ToString("d");
                    SInt[day + "鱼缸团的异变老一死亡次数"]++;
                    senddigtomember(pc, 20006);//发AAA！
                    senddigtomember(pc, 20007);//发AAA！
                    if (pc.Party != null)
                    {
                        foreach (var item in pc.Party.Members)
                        {
                            if (item.Value.Online)
                            {
                                item.Value.TInt["鱼缸岛危机老一死亡"] = 1;
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
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
            }
        }
        private void SQuest_Returning3(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "你走吧");
            }
        }

        private void SQuest_Defending3(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)//遇见BOSS时
            {
                ActorMob mob = eh.mob;
                senddigtomember(pc, 20004);//发AAA！
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
                            item.TInt["鱼缸岛危机老一死亡"] = 0;
                        }
                    }
                }
            }
            if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.5f)//BOSS血量为50%时
                {
                    senddigtomember(pc, 20005);//发AAA！
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

        private void SQuest_Returning(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "你走吧");
            }
        }

        private void SQuest_Defending(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)//遇见BOSS时
            {
                ActorMob mob = eh.mob;
                senddigtomember(pc, 20018);//发AAA！
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
                            item.TInt["鱼缸岛危机老四死亡"] = 0;
                        }
                    }
                }
            }
            if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.9f)//BOSS血量为50%时
                {
                    senddigtomember(pc, 20019);//发AAA！
                    eh.mob.AttackedForEvent = 2;
                }
            }
            if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.5f)//BOSS血量为50%时
                {
                    senddigtomember(pc, 20020);//发AAA！
                    eh.mob.AttackedForEvent = 3;
                }
            }
            if (eh.mob.AttackedForEvent == 3)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < 200)//BOSS血量少于200时
                {
                }
            }
        }
    }
}