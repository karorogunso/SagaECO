
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Manager;
namespace SagaScript.M30210000
{
    public class S15006666 : Event
    {
        public S15006666()
        {
            this.EventID = 15006666;
        }

        void ClearDaily(ActorPC pc)
        {
            if (pc.Party == null)
            {
                if (pc.AStr["草莓活动每日记录2"] != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    pc.AStr["草莓活动每日记录2"] = DateTime.Now.ToString("yyyy-MM-dd");
                    pc.AInt["草莓活动剩余次数"] = 2;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("你今日的草莓采摘次数回复到2次了");
                }
            }
            else
            {
                foreach (var item in pc.Party.Members.Values)
                {
                    if(item.Online)
                    {
                        if (item.AStr["草莓活动每日记录2"] != DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            item.AStr["草莓活动每日记录2"] = DateTime.Now.ToString("yyyy-MM-dd");
                            item.AInt["草莓活动剩余次数"] = 2;
                            SagaMap.Network.Client.MapClient.FromActorPC(item).SendSystemMessage("你今日的草莓采摘次数回复到2次了");
                        }
                    }
                }
            }
        }

        public override void OnEvent(ActorPC pc)
        {
            if(pc.Account.GMLevel > 20)
            {
                pc.AInt["草莓活动剩余次数"] = 2;
                pc.AInt["草莓活动当前小时"] = 25;
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("[GM]你今日的草莓采摘次数回复到2次了");
            }
            if (DateTime.Now.Day <= 25 && DateTime.Now.Month == 6 && DateTime.Now.Year == 2017)
            {
                ClearDaily(pc);
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                Say(pc, 131, "恩..你问我为什么在这里？$R$R因为死an她找不到合适的NPC了！！", "做料理的番茄");
                Say(pc, 131, "现在正在举办草莓活动哦，$R$R要前往采摘草莓么？$R$R注意：单次限时采摘10分钟，$R进入间隔必须大于1小时，$R每日两次。", "做料理的番茄");
                switch (Select(pc, "怎么办呢？今日剩余次数：", "", "单人前往(50任务点)", "组队前往(50任务点)", "离开"))
                {
                    case 1:
                        if (pc.Party != null)
                        {
                            Say(pc, 131, "组队不能进入单人采摘哦", "做料理的番茄");
                            return;
                        }
                        if (pc.QuestRemaining < 50)
                        {
                            Say(pc, 131, "任务点不够了！", "做料理的番茄");
                            return;
                        }
                        if (pc.AInt["草莓活动剩余次数"] <= 0)
                        {
                            Say(pc, 131, "你今天剩余的次数不够啦", "做料理的番茄");
                            return;
                        }
                        if(pc.AInt["草莓活动当前小时"] == DateTime.Now.Hour)
                        {
                            Say(pc, 131, "你刚刚去过啦！过一会再来吧。", "做料理的番茄");
                            return;
                        }
                        pc.AInt["草莓活动当前小时"] = DateTime.Now.Hour;
                        pc.AInt["草莓活动剩余次数"] -= 1;
                        pc.QuestRemaining -= 50;
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendQuestPoints();
                        pc.TInt["草莓活动MAP"] = CreateMapInstance(10017000, 10017000, 135, 96, true, 0, true);
                        Say(pc, 0, pc.TInt["草莓活动MAP"].ToString());
                        map = MapManager.Instance.GetMap((uint)pc.TInt["草莓活动MAP"]);
                        map.SpawnCustomMob(10000000, (uint)pc.TInt["草莓活动MAP"], 30550000, 0, 0, 125, 125, 200, 300, 0, 活动怪物.活动草莓BInfo(), 活动怪物.活动草莓BAI(), null, 0);
                        Timer timer = new Timer("草莓活动计时器", 0, 600000);
                        timer.OnTimerCall += (s, e) =>
                        {
                            timer.Deactivate();
                            if (pc.TInt["草莓活动MAP"] != 0)
                                DeleteMapInstance(pc.TInt["草莓活动MAP"]);
                        };
                        timer.Activate();
                        
                        Warp(pc, (uint)pc.TInt["草莓活动MAP"], 135, 96);

                        break;
                    case 2:
                        if (pc.Party == null)
                        {
                            Say(pc, 131, "没组队不能组队去哦", "做料理的番茄");
                            return;
                        }
                        if (pc.Party.Leader != pc)
                        {
                            Say(pc, 131, "组队必须队长带队！", "做料理的番茄");
                            return;
                        }
                        foreach (var item in pc.Party.Members.Values)
                        {
                            if (pc.MapID != item.MapID)
                            {
                                Say(pc, 131, "有人不在这里。", "做料理的番茄");
                                return;
                            }
                            if (item.QuestRemaining < 50)
                            {
                                Say(pc, 131, "有人任务点不够了！", "做料理的番茄");
                                return;
                            }
                            if (item.AInt["草莓活动剩余次数"] <= 0)
                            {
                                Say(pc, 131, "有人今天剩余的次数不够啦！", "做料理的番茄");
                                return;
                            }
                            if (item.AInt["草莓活动当前小时"] == DateTime.Now.Hour)
                            {
                                Say(pc, 131, "有人刚刚去过啦！过一会再来吧。", "做料理的番茄");
                                return;
                            }
                        }
                        pc.TInt["草莓活动MAP"] = CreateMapInstance(10017000, 10017000, 135, 96, true, 0, true);
                        Say(pc, 0, pc.TInt["草莓活动MAP"].ToString());
                        Timer timer2 = new Timer("草莓活动计时器", 0, 600000);
                        timer2.OnTimerCall += (s, e) =>
                        {
                            timer2.Deactivate();
                            if (pc.TInt["草莓活动MAP"] != 0)
                                DeleteMapInstance(pc.TInt["草莓活动MAP"]);
                        };
                        timer2.Activate();
                        foreach (var item in pc.Party.Members.Values)
                        {
                            item.AInt["草莓活动当前小时"] = DateTime.Now.Hour;
                            item.AInt["草莓活动剩余次数"] -= 1;
                            item.QuestRemaining -= 50;
                            SagaMap.Network.Client.MapClient.FromActorPC(item).SendQuestPoints();
                            Warp(item, (uint)pc.Party.Leader.TInt["草莓活动MAP"], 135, 96);
                        }
                        map = MapManager.Instance.GetMap((uint)pc.TInt["草莓活动MAP"]);
                        map.SpawnCustomMob(10000000, (uint)pc.TInt["草莓活动MAP"], 30550000, 0, 0, 125, 125, 200, 300, 0, 活动怪物.活动草莓AInfo(), 活动怪物.活动草莓AI(), null, 0);
                        break;

                }
            }
        }
    }
}

