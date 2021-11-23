using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20080012
{
    public class S11000904 : Event
    {
        public S11000904()
        {
            this.EventID = 11000904;

            this.notEnoughQuestPoint = "调查顺利进行$R;" +
    "现在没什么要帮忙的$R;" +
    "以后再来吧$R;";
            this.leastQuestPoint = 3;
            this.questFailed = "失败了吗？$R;" +
    "不要太介意喔$R;" +
    "安全回来，已经是最大的成果了$R;";
            this.alreadyHasQuest = "不好意思$R;" +
    "任务还没结束!$R;";
            this.gotNormalQuest = "这里给『队伍』$R;" +
    "介绍击退任务的喔$R;" +
    "$P队伍里，只要有一人接受相同$R;" +
    "任务的话$R;" +
    "就可以共享击退魔物的数量$R;" +
    "$R齐心协力!加油吧！$R;" +
    "$R那…多多指教了!$R;";
            this.questCompleted = "辛苦了$R;" +
    "好像成功了$R;" +
    "$R请收下报酬吧$R;";
            this.questCanceled = "是吗？$R;" +
    "没办法了$R;";
            this.questTooEasy = "觉得危险就不要勉强$R;" +
    "请马上撤退明白了吗？$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<INSD> mask = pc.CMask["INSD"];
            if (!mask.Test(INSD.東軍團員第一次對話))//_0c67)
            {
                mask.SetValue(INSD.東軍團員第一次對話, true);
                //_0c67 = true;
                Say(pc, 131, "感谢您的帮助$R;" +
                    "$R这里是地下遗迹的入口$R;" +
                    "遗迹的东、西、北边都有入口$R;" +
                    "离开时请使用南方入口吧$R;" +
                    "$P这次需要您帮忙的是$R;" +
                    "击退妨碍遗迹调查工作的魔物$R;" +
                    "是击退任务吧$R;");
            }

            else
            {
                Say(pc, 131, "因为里面的魔物$R;" +
                    "调查工作正面临困难阿$R;" +
                    "$R到底从哪儿冒出来的呢？$R;");
                Say(pc, 131, "请您帮帮我们吧$R;");
            }

            switch (Select(pc, "做什么呢？", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    Say(pc, 131, "消耗任务点数「3」$R;");

                    HandleQuest(pc, 38);
                    break;
                case 2:
                    break;
            }
        }
    }
}