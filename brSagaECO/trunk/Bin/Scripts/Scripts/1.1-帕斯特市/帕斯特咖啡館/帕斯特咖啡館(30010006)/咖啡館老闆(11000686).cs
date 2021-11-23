using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010006
{
    public class S11000686 : Event
    {
        public S11000686()
        {
            this.EventID = 11000686;

            this.notEnoughQuestPoint = "看看…$R;" +
    "$R現在没有事情要幫忙$R;" +
    "去享受一下冒險吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "…$P失敗?$R;" +
    "$R真為難阿！要向客人怎麼交待…$R;" +
    "$P這次無可奈何$R;" +
    "但下次開始小心點！$R;";
            this.alreadyHasQuest = "任務順利嗎?$R;";
            this.gotNormalQuest = "那就拜託了$R;" +
    "$R完成任務後，再跟我説吧$R;";
            this.gotTransportQuest = "是阿，道具太重了吧$R;" +
                "所以不能一次傳送的話$R;" +
                "分幾次給就可以！;";
            this.questCompleted = "辛苦了$R;" +
    "$R任務成功!$R請收下報酬!$R;";
            this.transport = "都集齊了嗎？;";
            this.questCanceled = "如果是您的話，相信您能做得到…$R;";
            this.questTooEasy = "唔…但是對你來說$R;" +
                "說不定是太簡單的事情$R;" +
                "$R那樣也沒關係嘛?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            switch (Select(pc, "呵呵!歡迎光臨!", "", "買東西", "賣東西", "任務服務台", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 04);
                    Say(pc, 131, "歡迎再次光臨！$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 04);
                    break;
                case 3:
                    if (mask.Test(PSTFlags.咖啡館老闆第一次任務))
                    {
                        Say(pc, 131, "什麽？想執行任務？$R;" +
                            "$R在這裡，不論職業和經驗$R;" +
                            "都可以選擇各種任務$R;" +
                            "$P其中也有難度高的$R;" +
                            "好好想想之後，才選擇吧！$R;");
                        mask.SetValue(PSTFlags.咖啡館老闆第一次任務, true);
                       // _0A77 = true;
                    }
                    HandleQuest(pc, 6);
                    break;
            }
        }
    }
}