using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010004
{
    public class S11000271 : Event
    {
        public S11000271()
        {
            this.EventID = 11000271;

            this.notEnoughQuestPoint = "來看看。$R;" +
                                       "$R現在沒事要幫忙$R;" +
                                       "不如先去冒個險吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "……$P失敗了嗎？$R;" +
                               "$R一定很難過吧$R;" +
                               "我也不知道該說什麼好阿$R;" +
                               "$P這一次沒辦法了$R;" +
                               "下一次要成功呀！知道嗎?$R;";
            this.alreadyHasQuest = "任務順利嗎？$R;";
            this.gotNormalQuest = "那麼就拜託您了$R;" +
                                  "$R任務結束了$R;" +
                                  "再來跟我說吧$R;";
            this.gotTransportQuest = "都搜集來了嗎？$R;";
            this.questCompleted = "辛苦了。$R;" +
                                  "$R任務成功了，$R領取報酬吧。$R;";
            this.transport = "哦哦…全部收來了嗎？;";
            this.questCanceled = "還以為如果是您的話$R;" +
                                 "會做好的……$R;";
            this.questTooEasy = "這個對您來說，可能太簡單了。$R;" +
                                "$R沒關係嗎？$R;";
            this.questTooHard = "對您來說可能太過困難了……$R;" +
                                "可以嗎?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎來到咖啡館二號店！", "", "買東西", "賣東西", "任務服務台", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    Say(pc, 111, "再來玩啊！$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 4);
                    Say(pc, 111, "再來玩啊！$R;");
                    break;
                case 3:
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    Say(pc, 111, "再來玩啊！$R;");
                    break;
            }
        }
    }
}