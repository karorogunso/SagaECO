using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000900 : Event
    {
        public S11000900()
        {
            this.EventID = 11000900;

            this.leastQuestPoint = 3;
            this.notEnoughQuestPoint = "现在不需要实验对象，$R;" +
    "下次再来吧$R;";
            this.questFailed = "失败了？$R;" +
    "$R原来您经不起意外阿$R;" +
    "可以保住小命，$R已经是不幸中之大幸了$R;";
            this.questTooHard = "以您现在的实力太勉强了。$R;" +
    "就算不执行任务$R;" +
    "如果是队员的话，也可以去看看$R;" +
    "去看看吗？$R;";
            this.questTooEasy = "您看起来很强啊，$R;" +
    "只要有勇气$R;" +
    "应该不会倒下吧$R;";
            this.gotNormalQuest = "这里有人数限制$R;" +
    "最多可以有8人进去$R;" +
    "$R队伍中如果有人接受一样任务的话$R;" +
    "可以共享击退魔物的数量哦$R;" +
    "$P超过限定时间$R;" +
    "就会自动回到这里$R;" +
    "所以不要担心啊$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYEFlags"];
            if (!mask.Test(AYEFlags.響鈴第一次對話))//_0c56)
            {
                mask.SetValue(AYEFlags.響鈴第一次對話, true);
                //_0c56 = true;
                Say(pc, 131, "我真的是天才啊$R;" +
                    "能够造这样的东西，很棒吧？$R;" +
                    "$R哦？$R;" +
                    "正好！在那边的您啊！$R;" +
                    "您要不要成为实验对象啊？$R;" +
                    "$P什么实验？$R;" +
                    "只是暂时去趟$R;" +
                    "假想空间的实验。放心吧！$R;" +
                    "$R怎么样？挺有趣吧？$R;");
            }
            else
            {
                Say(pc, 131, "呵呵，您太厉害了$R;" +
                    "$R试验对象总是受欢迎的阿$R;");
            }
            Say(pc, 131, "怎么样?要做一次吗？$R;");

            switch (Select(pc, "做什么呢？", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    //HandleQuest(pc, 37);
                    break;
                case 2:
                    break;
            }
        }
    }
}