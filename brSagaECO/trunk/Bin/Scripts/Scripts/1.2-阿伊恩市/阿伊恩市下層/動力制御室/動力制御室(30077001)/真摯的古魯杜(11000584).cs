using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30077001
{
    public class S11000584 : Event
    {
        public S11000584()
        {
            this.EventID = 11000584;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集石油) && !Job2X_12_mask.Test(Job2X_12.給予石油))//_3A72 && !_3A77)
            {
                Say(pc, 11000589, 131, "古魯杜先生!又開始出力了$R電壓也上升了$R;" +
                    "$R阿~現在活了~!還以爲錯了呢!!$R;" +
                    "$R這大動力火爐是阿伊恩薩烏斯的心臟$R如果心臟停的話就是大事了阿!$R;");
                Say(pc, 11000584, 131, "嗯…從古董商店中找出來的出土品$R再活用部件好像很合適阿$R;" +
                    "$R這個都是托家伙的實力阿$R把阿高普路斯市$R手藝好的機械當獎勵吧$R;");
                Say(pc, 11000584, 131, "啊，是你阿$R太晚了~!太晚了啊!$R;" +
                    "$R…??$R;");

                if (CountItem(pc, 10000701) >= 10)
                {
                    TakeItem(pc, 10000701, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予石油, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集花束, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集石油, false);
                    //_3A77 = true;
                    //_3A73 = true;
                    //_3A72 = false;
                    Say(pc, 11000584, 131, "確實是「油」10份$R沒問題$R;" +
                        "$R按照客人的訂單$R確實的把商品送到$R對我們商人來説是最重要的事情$R;");
                    Say(pc, 11000589, 131, "阿，是弟子嗎?$R「石油」真謝謝了$R;" +
                        "$P這個「石油」是蒸餾精製後$R製作大動力火爐的潤滑油$R;" +
                        "$R總是欠對古魯杜先生人情阿$R;" +
                        "$P古魯杜先生$R收了相當聰明的弟子阿~$R;");
                    Say(pc, 11000584, 131, "不是!!還遠著呢，不要誇了!$R;" +
                        "$R那下次要轉交東西的地方是…$R;" +
                        "可以給在阿高普路斯上城的$R4樓的行會宮殿導遊小姐$R轉交10束「花束」嗎?$R;" +
                        "$P稍後我也會去的$R那就拜託了!$R;");
                    return;
                }
                Say(pc, 11000584, 131, "!!$R不是「油」10份阿!!$R重新拿過來!$R;");
                return;
            }
            Say(pc, 11000584, 131, "可以給在阿高普路斯上城的$R4樓的行會宮殿導遊小姐$R轉交10束「花束」嗎?$R;" +
                "$R那就拜託了$R;");
        }
    }
}