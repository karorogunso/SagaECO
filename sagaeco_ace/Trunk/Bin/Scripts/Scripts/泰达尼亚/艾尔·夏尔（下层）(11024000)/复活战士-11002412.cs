using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10023100
{
    public class S11002412 : 復活戰士
    {
        public S11002412()
        {
            byte x, y;

            x = (byte)Global.Random.Next(118, 120);
            y = (byte)Global.Random.Next(219, 221);

            Init(11002412, 11024000, x, y);
        }
    }
    //public class yard_quest : Event
    //{
    //    //public yard_quest()
    //    //{
    //    //    this.EventID = 11002412;
    //    //    //this.notEnoughQuestPoint = "调查顺利进行$R现在没什么要帮忙的$R以后再来吧$R";
    //    //    //this.leastQuestPoint = 1;
    //    //    //this.questFailed = "失败了吗？$R不要太介意喔$R安全回来，已经是最大的成果了$R";
    //    //    //this.alreadyHasQuest = "不好意思$R任务还没结束!$R";
    //    //    //this.gotNormalQuest = "这里给『队伍』$R介绍击退任务的喔$R$P队伍里，只要有一人接受相同$R任务的话$R就可以共享击退魔物的数量$R$R齐心协力!加油吧！$R$R那…多多指教了!$R";
    //    //    //this.questCompleted = "辛苦了$R好像成功了$R$R请收下报酬吧$R;";
    //    //    //this.questCanceled = "是吗？$R没办法了$R;";
    //    //    //this.questTooEasy = "觉得危险就不要勉强$R请马上撤退明白了吗？$R;";
    //    //}
    //    //public override void OnEvent(ActorPC pc)
    //    //{
    //    //    Say(pc, 131, "因为里面的魔物$R调查工作正面临困难啊$R$R到底从哪儿冒出来的呢？$R;");
    //    //    Say(pc, 131, "请您救救我们吧$R;");
    //    //    switch (Select(pc, "做什么呢？", "", "任务服务台", "什么也不做"))
    //    //    {
    //    //        case 1:
    //    //            {
    //    //                Say(pc, 131, "消耗任务点数「1」$R;");
    //    //                HandleQuest(pc, 80);
    //    //                break;
    //    //            }
    //    //        case 2:
    //    //            {
    //    //                break;
    //    //            }
    //    //    }
    //    //}

    //}
}
//所在地图:艾尔·夏尔（下层）(11024000)NPC基本信息:11002412-复活战士- X:119 Y:218
