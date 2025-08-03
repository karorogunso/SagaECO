using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010003
{
    public class S11000270 : Event
    {
        public S11000270()
        {
            this.EventID = 11000270;

            this.notEnoughQuestPoint = "来看看。$R;" +
                                       "$R现在没事要帮忙$R;" +
                                       "不如先去冒个险吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "……$P失败了吗？$R;" +
                               "$R一定很难过吧$R;" +
                               "我也不知道该说什么好阿$R;" +
                               "$P这一次没办法了$R;" +
                               "下一次要成功呀！知道吗?$R;";
            this.alreadyHasQuest = "任务顺利吗？$R;";
            this.gotNormalQuest = "那么就拜托您了$R;" +
                                  "$R任务结束了$R;" +
                                  "再来跟我说吧$R;";
            this.gotTransportQuest = "都搜集来了吗？$R;";
            this.questCompleted = "辛苦了。$R;" +
                                  "$R任务成功了，$R领取报酬吧。$R;";
            this.transport = "哦哦…全部收来了吗？;";
            this.questCanceled = "还以为如果是您的话$R;" +
                                 "会做好的……$R;";
            this.questTooEasy = "这个对您来说，可能太简单了。$R;" +
                                "$R没关系吗？$R;";
            this.questTooHard = "对您来说可能太过困难了……$R;" +
                                "可以吗?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "欢迎来到酒馆一号店！", "", "买东西", "卖东西", "任务服务台", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 04);
                    break;
                case 2:
                    OpenShopSell(pc, 04);
                    break;
                case 3:
                    HandleQuest(pc, 6);
                    return;
            }
            Say(pc, 111, "再来玩啊$R;");
        }
    }
}