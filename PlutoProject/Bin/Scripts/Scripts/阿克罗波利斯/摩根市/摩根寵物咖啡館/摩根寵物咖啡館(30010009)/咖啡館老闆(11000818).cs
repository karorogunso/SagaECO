using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010009
{
    public class S11000818 : Event
    {
        public S11000818()
        {
            this.EventID = 11000818;

            this.notEnoughQuestPoint = "看看…$R;" +
    "$R现在没有事情要帮忙$R;" +
    "去享受一下冒险吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "…$P失败?$R;" +
    "$R真为难啊！要向客人怎么交待…$R;" +
    "$P这次无可奈何$R;" +
    "但下次开始小心点！$R;";
            this.alreadyHasQuest = "任务顺利吗?$R;";
            this.gotNormalQuest = "那就拜托了$R;" +
    "$R完成任务后，再跟我説吧$R;";
            this.gotTransportQuest = "是啊，道具太重了吧$R;" +
                "所以不能一次送来的话$R;" +
                "分几次给就可以！;";
            this.questCompleted = "辛苦了$R;" +
    "$R任务成功!$R请收下报酬!$R;";
            this.transport = "都集齐了吗？;";
            this.questCanceled = "如果是您的话，相信您能做得到…$R;";
            this.questTooEasy = "唔…但是对你来说$R;" +
                "说不定是太简单的事情$R;" +
                "$R那样也没关系嘛?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "来，快来呀！", "", "买东西", "卖东西", "任务服务台", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 167);
                    Say(pc, 131, "欢迎再来$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 167);
                    break;
                case 3:
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    break;
            }
        }
    }
}