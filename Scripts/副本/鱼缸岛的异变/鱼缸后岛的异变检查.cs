
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public partial class 暗鸣: Event
    {
        void 鱼缸后岛的异变普通(ActorPC pc)
        {
            switch (Select(pc, "鱼缸后岛的异变普通(普通)", "", "进入普通模式[复活次数:4]", "进入挑战模式[复活次数:0]", "离开"))
            {
                case 1:
                    if (checkparty(pc))
                    {
                        if (进入申请(pc))
                            if (创建(pc))
                            {
                                传送(pc);
                                剧情一(pc);
                            }
                    }
                    break;
                case 2:
                    if (checkparty(pc))
                    {
                        pc.Party.TInt["普通挑战模式"] = 1;
                        if (进入申请(pc))
                            if (创建(pc))
                            {
                                
                                传送(pc);
                                剧情一(pc);
                            }
                    }
                    break;
            }
        }
        void 剧情一(ActorPC pc)
        {

        }
        void 设置复活次数(ActorPC pc)
        {
            byte count = 4;
            if (pc.Party.TInt["普通挑战模式"] == 1)
                count = 0;
            pc.TInt["副本复活标记"] = 1;
            pc.Party.Leader.TInt["复活次数"] = count;
            pc.Party.Leader.TInt["设定复活次数"] = count;
        }
        void 清记录(ActorPC pc)
        {
            pc.TInt["岛1小怪死亡数"] = 0;
            pc.TInt["鱼缸岛危机老一死亡"] = 0;
            pc.TInt["鱼缸岛危机老二死亡"] = 0;
            pc.TInt["鱼缸岛危机老三死亡"] = 0;
            pc.TInt["鱼缸岛危机老四死亡"] = 0;
        }
        void 传送(ActorPC pc)
        {
            pc.Party.MaxMember = 4;
            foreach (var item in pc.Party.Members.Values)
            {
                if (item != null)
                {
                    设置复活次数(item);
                    Warp(item, (uint)pc.Party.Leader.TInt["S10054100"], 225, 87);
                    SetNextMoveEvent(item, 87000000);//AAA剧情
                    清记录(item);
                }
            }
        }
        bool 创建(ActorPC pc)
        {
            if (pc.Party == null)
                return false;
            if (pc != pc.Party.Leader)
                return false;
            foreach (var item in pc.Party.Members.Values)
            {
                if (item != null)
                {
                    item.CStr["鱼缸后岛的异变普通队长名"] = item.Party.Leader.Name;
                }
            }
            pc.Party.Leader.TInt["S10054100"] = CreateMapInstance(10054100, 30131001, 6, 8, true, 0, true);//后岛
            pc.Party.Leader.TInt["S20004000"] = CreateMapInstance(20004000, 30131001, 6, 8, true, 0, true);//大陆B5F
            pc.Party.Leader.TInt["S20003000"] = CreateMapInstance(20003000, 30131001, 6, 8, true, 0, true);//大陆B4F
            pc.Party.Leader.TInt["S20002000"] = CreateMapInstance(20002000, 30131001, 6, 8, true, 0, true);//大陆B3F
            pc.Party.Leader.TInt["S20001000"] = CreateMapInstance(20001000, 30131001, 6, 8, true, 0, true);//大陆B2F
            pc.Party.Leader.TInt["S20000000"] = CreateMapInstance(20000000, 30131001, 6, 8, true, 0, true);//大陆B1F
            pc.Party.Leader.TInt["S30131002"] = CreateMapInstance(30131002, 30131001, 6, 8, true, 0, true);//屋子

            右岛刷怪((uint)pc.Party.Leader.TInt["S10054100"], pc);
            大陆B5F刷怪((uint)pc.Party.Leader.TInt["S20004000"], pc, false);
            大陆B4F刷怪((uint)pc.Party.Leader.TInt["S20003000"], pc);
            大陆B3F刷怪((uint)pc.Party.Leader.TInt["S20002000"], pc);
            大陆B2F刷怪((uint)pc.Party.Leader.TInt["S20001000"], pc);
            大陆B1F刷怪((uint)pc.Party.Leader.TInt["S20000000"], pc, false);
            左岛刷怪((uint)pc.Party.Leader.TInt["S10054100"], pc, false);
            return true;
        }
        bool 进入申请(ActorPC pc)
        {
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.Online && item.MapID != 30131001)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("有玩家不在这里，进入取消");
                    return false;
                }
                if (item.Level < 45)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "等级不足，要求45级。进入取消。");
                    return false;
                }
                if (item.QuestRemaining < 10)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "任务点不足，进入取消。");
                    return false;
                }

            }
            string y = "[鱼缸后岛的异变]普通";
            if (pc.Party.TInt["普通挑战模式"] == 1)
                y += "挑战模式";
            foreach (var item in pc.Party.Members.Values)
            {
                if (item != pc.Party.Leader)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("等待玩家" + item.Name + " 接受中..");
                    if (Select(item, "队长申请了y，是否接受？", "", "接受[需要10任务点]", "不接受！！") == 1)
                    {
                        foreach (var item2 in pc.Party.Members.Values)
                            SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "接受了y");
                    }
                    else
                    {
                        foreach (var item2 in pc.Party.Members.Values)
                            SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "拒绝了y，进入取消。");
                        return false;
                    }
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.QuestRemaining < 10)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "任务点不足，进入取消。");
                    return false;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.QuestRemaining >= 10)
                {
                        SagaMap.Network.Client.MapClient.FromActorPC(item).SendSystemMessage("消耗了10点任务点。");
                    item.QuestRemaining -= 10;
                    SagaMap.Network.Client.MapClient.FromActorPC(item).SendQuestPoints();
                }
            }
            return true;
        }
        bool checkparty(ActorPC pc)
        {
            if (pc.Party == null)
            {
                Say(pc, 0, "这趟任务太——危险啦！$R去组个队伍吧！", "暗鸣");
                return false;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 0, "请让队长来找我！", "暗鸣");
                return false;
            }
            if (pc.Party.MemberCount > 4)
            {
                Say(pc, 0, "人太多了！", "暗鸣");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if(item.QuestRemaining < 10)
                {
                    Say(pc, 0,item.Name + "的任务点太少了！！", "暗鸣");
                    return false;
                }
            }
            return true;
        }
    }
}