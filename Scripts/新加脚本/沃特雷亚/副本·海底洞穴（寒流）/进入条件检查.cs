
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace 海底副本
{
    public partial class 海底副本 : Event
    {
        bool 检查单人(ActorPC pc, Difficulty diff)
        {
            if (pc.Party != null)
            {
                Say(pc, 0, "有队伍无法进入单人模式");
                return false;
            }
            if (pc.QuestRemaining < QuestPoint)
            {
                Say(pc, 0, "任务点不足");
                return false;
            }
            return true;
        }
            bool 检查多人(ActorPC pc, Difficulty diff)
        {
            if (pc.Party == null)
            {
                Say(pc, 0, "没有队伍无法进入多人模式");
                return false;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 0, "请让队长带领进入");
                return false;
            }
            if (pc.Party.MemberCount > MaxMember)
            {
                Say(pc, 0, "人数超过限制");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.QuestRemaining < QuestPoint)
                {
                    Say(pc, 0, item.Name + "的任务点不足");
                    return false;
                }
            }

            return true;
        }
        bool 进入申请(ActorPC pc, Difficulty diff)
        {
            if (pc.Party == null) return false;//如果玩家没有队伍，则返回false
            foreach (var item in pc.Party.Members.Values)//遍历玩家队伍成员
            {
                if (item.MapID != 11053000)//如果这个成员不在暮色地图
                {
                    SendSystemMessageToMember(item, "有玩家不在这里，进入取消");
                    return false;//失败
                }
                if (item.Level < lv)//如果这个成员等级不足
                {
                    SendSystemMessageToMember(item, item.Name + "的等级不足，进入取消");
                    return false;
                }
                if (item.QuestRemaining < QuestPoint)//如果这个成员任务点不足
                {
                    SendSystemMessageToMember(item, item.Name + "的任务点不足，进入取消");
                    return false;
                }
            }
            string RN = "[" + GetDiffName(diff) + "]" + RealmName;

            foreach (var item in pc.Party.Members.Values)//第二次遍历队伍成员，因为是检查之后进行的，所以不能把内容和上面一个遍历写在一起
            {
                if (item != pc.Party.Leader)//如果当前遍历的队员不是队伍的队长
                {
                    SendSystemMessageToMember(item, "等待玩家" + item.Name + " 接受中..");//先发送信息给全体队友
                    if (Select(item, "队长申请了" + RN + " ，是否接受？", "", "接受", "拒绝") == 1)//如果玩家选了第一个选项
                        SendSystemMessageToMember(item, item.Name + " 接受了 " + RN);
                    else//如果玩家没选第一个选项
                    {
                        SendSystemMessageToMember(item, item.Name + " 拒绝了 " + RN + " ，进入取消");
                        return false;//如果有玩家拒绝，要返回失败
                    }
                }
            }

            foreach (var item in pc.Party.Members.Values)//进入前的第三次检查，以防有人在选择时用掉了任务点，导致任务点溢出
            {
                if (item.QuestRemaining < QuestPoint)//如果这个成员任务点不足
                {
                    SendSystemMessageToMember(item, item.Name + "的任务点不足，进入取消");
                    return false;
                }
            }

            foreach (var item in pc.Party.Members.Values)//扣除任务点
            {
                if (item.QuestRemaining >= QuestPoint)
                {
                    SagaMap.Network.Client.MapClient.FromActorPC(item).SendSystemMessage("消耗了" + QuestPoint.ToString() + "点任务点。");//QuestPoint .ToString()的用法是，将变量强行转换成字符串，方便发送给玩家
                    item.QuestRemaining -= QuestPoint;
                    SagaMap.Network.Client.MapClient.FromActorPC(item).SendQuestPoints();//发送任务点更新封包
                }
            }
            return true;
        }
    }
}