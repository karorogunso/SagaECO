using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class maimai_quest : Event
    {
        public maimai_quest()
        {
            this.EventID = 11001443;
            this.notEnoughQuestPoint = "调查顺利进行$R现在没什么要帮忙的$R以后再来吧$R";
            this.leastQuestPoint = 1;
            this.questFailed = "失败了吗？$R不要太介意喔$R安全回来，已经是最大的成果了$R";
            this.alreadyHasQuest = "不好意思$R任务还没结束!$R";
            this.gotNormalQuest = "这里给『队伍』$R介绍击退任务的喔$R$P队伍里，只要有一人接受相同$R任务的话$R就可以共享击退魔物的数量$R$R齐心协力!加油吧！$R$R那…多多指教了!$R";
            this.questCompleted = "辛苦了$R好像成功了$R$R请收下报酬吧$R;";
            this.questCanceled = "是吗？$R没办法了$R;";
            this.questTooEasy = "觉得危险就不要勉强$R请马上撤退明白了吗？$R;";
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "因为里面的魔物$R调查工作正面临困难啊$R$R到底从哪儿冒出来的呢？$R;");
            Say(pc, 131, "请您救救我们吧$R;");
            switch (Select(pc, "做什么呢？", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    {
                        Say(pc, 131, "消耗任务点数「1」$R;");
                        HandleQuest(pc, 72);
                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
        }
    }
    public class maimai_entry : Event
    {
        public maimai_entry()
        {
            this.EventID = 10001446;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (GetPossibleDungeons(pc).Count == 0)
            {
                if (pc.Quest != null)
                {
                    if (pc.Quest.ID == 26510002)
                    {
                        if (CountItem(pc, 10021000) >= 1)
                        {
                            TakeItem(pc, 10021000, 1);
                            CreateDungeon(pc, pc.Quest.Detail.DungeonID, 20164000, 219, 123);
                            WarpToDungeon(pc);
                        }
                    }
                    else
                    {
                        Say(pc, 131, "您接的任务不能从这里进入。$R");
                    }
                }
                else
                {
                    Say(pc, 131, "您还没有接任何任务哦。$R;");
                }
            }
            else
            {
                WarpToDungeon(pc);
            }
        }
    }
    public class P11001452 : Event
    {
        public P11001452()
        {
            this.EventID = 11001452;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "根据现在的情报$R只能对作战进行这样的部署了...$R可是....");
            Say(pc, 131, "进去的人都被吃掉了...");
        }
    }
    public class P11001446 : Event
    {
        //126,40
        public P11001446()
        {
            this.EventID = 11001446;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "现在貌似传点都坏掉了");
        }
    }
    public class P10001449 : Event
    {
        public P10001449()
        {
            this.EventID = 10001449;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "毫无反应的样子...");
        }
    }
    public class P11001444 : Event
    {
        //37,125
        public P11001444()
        {
            this.EventID = 11001444;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "现在貌似传点都坏掉了");
        }
    }
    public class P10001447 : Event
    {
        public P10001447()
        {
            this.EventID = 10001447;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "毫无反应的样子...");
        }
    }
    public class P11001445 : Event
    {
        //124,216
        public P11001445()
        {
            this.EventID = 11001445;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "现在貌似传点都坏掉了");
        }
    }
    public class P10001448 : Event
    {
        public P10001448()
        {
            this.EventID = 10001448;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "毫无反应的样子...");
        }
    }
}