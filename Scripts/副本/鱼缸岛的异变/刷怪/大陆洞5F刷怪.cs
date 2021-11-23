
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
        void 大陆B5F刷怪(uint mapid, ActorPC pc,bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18690000, 0, 0, 19, 16, 0, 1, 0, 鱼人阿鲁玛Info(single), 鱼人阿鲁玛AI(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += SQuest_Defending2;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning2;
            ((MobEventHandler)mob.e).Dying += (y, e) =>
            {
                senddigtomember(pc, 20010);//发AAA！
                senddigtomember(pc, 20011);//发AAA！
                if (pc.Party != null)
                {
                    foreach (var item in pc.Party.Members)
                    {
                        if (item.Value.Online)
                        {
                            item.Value.TInt["鱼缸岛危机老二死亡"] = 1;
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
            map.SpawnCustomMob(10000000, map.ID, 26100001, 0, 0, 104, 47, 0, 1, 0, 丁字裤鱼人Info(), 丁字裤鱼人AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 26100001, 0, 0, 24, 59, 0, 1, 0, 丁字裤鱼人Info(), 丁字裤鱼人AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 26100003, 0, 0, 64, 87, 43, 3, 0, 木鱼Info(), 木鱼AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 26100000, 0, 0, 64, 87, 43, 3, 0, 雷鱼Info(), 雷鱼AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 26100051, 0, 0, 64, 87, 43, 3, 0, 水鬼Info(), 水鬼AI(), null, 0);
            if (Global.Random.Next(0, 100) < 30 && DateTime.Now.Year == 2017 && ((DateTime.Now.Month == 1 && DateTime.Now.Day >= 11) || (DateTime.Now.Month == 2 && DateTime.Now.Day <= 3)))
            {
                ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 10370000, 0, 0, 22, 14, 0, 1, 0, 迎春宝箱Info(), 迎春宝箱AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["副本新春宝箱死亡次数"]++;
            }
        }

        private void SQuest_Returning2(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "无噢噢噢噢！！！");
            }
        }

        private void SQuest_Defending2(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)//遇见BOSS时
            {
                ActorMob mob = eh.mob;
                senddigtomember(pc, 20009);//发AAA！
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
                            item.TInt["鱼缸岛危机老二死亡"] = 0;
                        }
                    }
                }
            }
            if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < 200)//BOSS血量少于200时
                {
                }
            }
        }
    }
}

