
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        void 暴走族与光头(ActorPC pc)
        {
            switch (Select(pc, "暴走族与光头集团", "", "从头开始进入副本", "中途加入副本", "离开"))
            {
                case 1:
                    if (checkparty2(pc))
                        光头本创建(pc);
                    break;
                case 2:
                    break;
                case 3:
                    return;
            }
        }
        bool checkparty2(ActorPC pc)
        {
            if (pc.Party == null)
            {
                Say(pc, 131, "这个地方太危险啦，$R请组队后一起前往吧。", "暴走族与光头集团");
                return false;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 131, "对不起，$R你不是队长耶$R$R请由队长来向我申请吧", "暴走族与光头集团");
                return false;
            }
            if (pc.Party.MemberCount > 4)
            {
                Say(pc, 131, "人数似乎太多了呢..$R坐不下哦", "暴走族与光头集团");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.AStr["暴走族与光头集团限制"] == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    Say(pc, 131, item.Name + " 去过了", "暴走族与光头集团");
                    return false;
                }
            }
            return true;
        }
    }
}

