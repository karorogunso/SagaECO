using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30113001
{
    public class S11000590 : Event
    {
        public S11000590()
        {
            this.EventID = 11000590;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (Job2X_12_mask.Test(Job2X_12.索取拜金使的紋章))
            {
                Say(pc, 131, "收下『拜金使的紋章』了!$R;" +
                    "$R祝賀啊!!$R;");
                return;
            }

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集花束) && !Job2X_12_mask.Test(Job2X_12.給予花束))//_3A73 && !_3A78)
            {
                Say(pc, 11000590, 131, "古魯杜先生…這樣的話就困難了!$R現在是正在做事的時候啊…$R突然約吃飯的話…$R;");
                Say(pc, 11000590, 131, "啊!來客人了!$R;" +
                    "$R歡…歡迎光臨!$R歡迎來到行會宮殿~~!!$R;");
                Say(pc, 11000585, 131, "…?$R…這…真是!四個家伙啊!$R來…來的也真快…怎麽搞得?!$R;" +
                    "沒辦法$R;");

                if (CountItem(pc, 10005400) >= 10)
                {
                    TakeItem(pc, 10005400, 10);
                    Job2X_12_mask.SetValue(Job2X_12.收集結束, true);
                    Job2X_12_mask.SetValue(Job2X_12.給予花束, true);
                    Job2X_12_mask.SetValue(Job2X_12.索取拜金使的紋章, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集花束, false);
                    //_3A64 = true;
                    //_3A78 = true;
                    //_3A74 = true;
                    //_3A73 = false;
                    Say(pc, 11000585, 131, "嗯…「花束」10束$R確實拿過來了$R;" +
                        "$R按照客人的訂單$R確實的把商品送到$R對我們商人來説是最重要的事情$R;" +
                        "$P…什麽?不是的…$R「花束」不是禮物$R呵，真是…都說不是了$R;" +
                        "$R是吧?小姐$R;");
                    Say(pc, 11000590, 131, "是的…真的不是的$R是因爲我想要「花束」$R所以拜託的$R;" +
                        "$P事實是…$R我媽媽因為生病還在躺著呢…$R;" +
                        "$R要消除壓力，用鮮花的精華是最好的$R古魯杜先生教的$R;" +
                        "$P對古魯杜先生一直都抱有感謝之心$R;" +
                        "$R但是…招待吃飯…有點困難…$R;");
                    Say(pc, 11000585, 131, "知道了?$R商人給客戶轉交的不僅僅是東西$R;" +
                        "$R其他需要什麽，自己找吧$R;" +
                        "$R還好承受住了拜金使的苦練…$R辛苦了$R;" +
                        "$R雖然還有很多不足$R但還是認定你為『拜金使』$R;" +
                        "$P到阿高普路斯上城$R商人行會總管那裡$R拿『拜金使的紋章』吧$R;");
                    return;
                }
                Say(pc, 11000585, 131, "!!!$R不是「花束」10束啊!!$R重新再來吧!$R;");
                return;
            }

            Say(pc, 11000585, 131, "請在阿高普路斯上城的$R商人行會總管$R拿『拜金使紋章』吧$R;");
        }
    }
}
