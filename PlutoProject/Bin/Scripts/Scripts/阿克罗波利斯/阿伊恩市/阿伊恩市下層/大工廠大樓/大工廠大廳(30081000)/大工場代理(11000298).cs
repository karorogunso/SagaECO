using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11000298 : Event
    {
        public S11000298()
        {
            this.EventID = 11000298;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];

            if (pc.Job == PC_JOB.TATARABE
              ||pc.Job == PC_JOB.BLACKSMITH
              ||pc.Job == PC_JOB.MACHINERY) 
            {
                if (CountItem(pc, 10018900) > 0)
                {
                    Say(pc, 131, "您拿来的加特林机关炮马上就要坏掉了。$R;" +
                        "劝您分解以后重组吧。$R;" +
                        "还有，给您一个特别服务阿，$R;" +
                        "可以改造您的加特林机关炮，$R提升它的威力呢。$R;" +
                        "$P要装备和改造您的加特林机关炮吗?$R;");
                    switch (Select(pc, "怎么办呢？", "", "好", "算了"))
                    {
                        case 1:
                            Say(pc, 131, "谢谢啦$R;" +
                                "$R分解装备和改造，$R费用一共10万金币唷$R;");
                            switch (Select(pc, "支付10万金币装备改造吗？", "", "做", "不做"))
                            {
                                case 1:
                                    if (pc.Gold > 99999)
                                    {
                                        PlaySound(pc, 2030, false, 100, 50);
                                        Say(pc, 131, "宠物随着成长，$R外貌也会不同喔$R;" +
                                            "$R请注意$R;");
                                        switch (Select(pc, "怎么办呢？", "", "做", "算了"))
                                        {
                                            case 1:
                                                Say(pc, 131, "谢谢$R;" +
                                                    "$R那就开始了$R;");
                                                PlaySound(pc, 4001, false, 100, 50);
                                                //FADE OUT BLACK
                                                Wait(pc, 5000);
                                                //FADE IN
                                                Wait(pc, 1000);
                                                pc.Gold -= 100000;
                                                //PETCHANGE TRANSFORM 10018901 ALL
                                                Say(pc, 131, "完成了。$R;" +
                                                    "$R好好感受一下新加特林机关炮的威力吧！$R;");
                                                break;
                                            case 2:
                                                Say(pc, 131, "是吗？可惜啊$R;");
                                                break;
                                        }
                                        return;
                                    }
                                    Say(pc, 131, "钱好像不够啊$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "是吗？可惜啊$R;");
                                    break;
                            }
                            break;
                        case 2:
                            Say(pc, 131, "是吗？可惜啊$R;");
                            break;
                    }
                    return;
                }
            }

            if (mask.Test(AYEFlags.與老闆對話))//_2a50)
            {
                switch (Select(pc, "欢迎光临大工厂！", "", "买东西", "买加特林机关炮", "洽谈", "什么也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 98);
                        Say(pc, 131, "感谢！$R;");
                        break;
                    case 2:
                        凱特靈炮(pc);
                        break;
                    case 3:
                        Say(pc, 131, "我们公司会拜托$R;" +
                            "有名的冒险者做事唷。$R;" +
                            "$R不好意思，请问您是……?$R;" +
                            "$P做这些事，或是击退魔物，$R;" +
                            "是不是还没有什么经验呢??$R;");
                        break;
                }
                return;
            }

            switch (Select(pc, "欢迎光临大工厂！", "", "任务服务台", "买东西", "买加特林机关炮", "什么也不做"))
            {
                case 1:
                    HandleQuest(pc, 21);
                    break;
                case 2:
                    OpenShopBuy(pc, 98);
                    Say(pc, 131, "感谢！$R;");
                    break;
                case 3:
                    凱特靈炮(pc);
                    break;
            }
        }

        void 凱特靈炮(ActorPC pc)
        {
            switch (Select(pc, "有许可证吗？", "", "拿出许可证", "许可证是什么?"))
            {
                case 1:
                    if (CountItem(pc, 10048000) >= 1)
                    {
                        Say(pc, 131, "确认了。$R;" +
                            "加特林机关炮一个30万金币$R;");
                        switch (Select(pc, "30万金币，买吗 ？", "", "买", "太贵了"))
                        {
                            case 1:
                                if (pc.Gold > 299999)
                                {
                                    if (CheckInventory(pc, 10018900, 1))
                                    {
                                        GiveItem(pc, 10018900, 1);
                                        TakeItem(pc, 10048000, 1);
                                        pc.Gold -= 300000;
                                        Say(pc, 131, "谢谢，$R;" +
                                            "很重的，小心啊$R;");
                                        return;
                                    }
                                    Say(pc, 131, "行李好像太多了，整理后再来吧$R;");
                                    return;
                                }
                                Say(pc, 131, "钱不够啊$R;");
                                break;
                            case 2:
                                Say(pc, 131, "嗯，有点贵呢。$R;" +
                                    "这个是秘密，别告诉人$R;" +
                                    "我可以给您开其他条件喔$R;" +
                                    "$P由现在开始，委托您的事情中$R;" +
                                    "只要您成功完成最少一件$R;" +
                                    "就送您一个凯特灵炮作报酬哦$R;" +
                                    "怎么样？这个条件可以吧?$R;");
                                if (pc.Quest != null)
                                {
                                    Say(pc, 131, "先完成现在进行的任务$R;" +
                                        "然后再来吧$R;");
                                    return;
                                }
                                Say(pc, 131, "这个任务$R;" +
                                    "消耗任务点数『3』$R;");
                                if (pc.QuestRemaining < 3)
                                {
                                    Say(pc, 131, "任务点数，累积超过『3』的话，$R;" +
                                        "再来找我吧。$R;");
                                    return;
                                }
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "好像没有许可证吧$R;" +
                        "$R对不起，没有许可证的话$R;" +
                        "不能把加特林机关炮卖给您的。$R;");
                    break;
                case 2:
                    Say(pc, 131, "许可証是『合同大厦』发行的$R;" +
                        "『凯特灵销售许可证』。$R;" +
                        "$R没有许可证的话$R;" +
                        "不能把加特林机关炮卖给您的。$R;" +
                        "$P合同大厦是上面很大的建筑。$R;" +
                        "$R去看看吗?$R;");
                    break;
            }
        }
    }
}