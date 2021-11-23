using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020004
{
    public class S11000583 : Event
    {
        public S11000583()
        {
            this.EventID = 11000583;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集黃麥粉) && !Job2X_12_mask.Test(Job2X_12.給予黃麥粉))//_3A71 && !_3A76)
            {
                Say(pc, 11000583, 131, "爲什麽那麽慢啊!!$R;" +
                    "$R汪汪牠們…汪汪牠們…$R在餓著肚子呢!!$R;" +
                    "$P「黃麥粉」是製作寵物食物的時候$R必不可少的材料!$R;");
                Say(pc, 11000588, 131, "古魯杜叔叔…爲什麽那麽生氣啊?$R平時是那麽親切...$R;");
                Say(pc, 11000583, 131, "啊…不是那個…那個…那$R嗯…嗯…$R呼…小鬼就安靜點$R;" +
                    "$R嗯…?$R;");

                if (CountItem(pc, 10001100) >= 10)
                {
                    TakeItem(pc, 10001100, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予黃麥粉, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集石油, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集黃麥粉, false);
                    //_3A76 = true;
                    //_3A72 = true;
                    //_3A71 = false;
                    Say(pc, 11000583, 131, "嗯「黃麥粉」10份$R確實是拿過來了!$R;" +
                        "$R按照顧客們的訂購$R準確的把商品轉交$R對商人來説是最重要的事情$R;");
                    Say(pc, 11000583, 131, "那下次要轉交的地方是…$R喂…不要一直發牢騷!$R;" +
                        "$P替我給阿伊恩薩烏斯的普蘭茨$R轉交10份「石油」吧$R;" +
                        "$R我稍後也會過去的…$R那就拜託了$R;");
                    return;
                }
                Say(pc, 11000583, 131, "!!$R這不是「黃麥子粉末」10個阿!!$R重新拿過來吧!$R;");
                return;
            }
            Say(pc, 11000583, 131, "給阿伊恩薩烏斯的普蘭茨$R轉交10份「油」吧$R;" +
                "$R拜託了$R;");
        }
    }
}