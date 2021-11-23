using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30170001
{
    public class S11000582 : Event
    {
        public S11000582()
        {
            this.EventID = 11000582;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集解毒果) && !Job2X_12_mask.Test(Job2X_12.給予解毒果))//_3A70 && !_3A75)
            {
                Say(pc, 11000582, 131, "怎麽這麽慢啊!!$R;" +
                    "$R比我還慢!!$R到底怎麽搞得?!$R;");
                Say(pc, 11000587, 131, "古…古魯杜先生$R沒有人走的偏僻地方又冷…$R所以無能爲力啊$R;");
                Say(pc, 11000582, 131, "嗨嗨!!$R姿勢不行啊!姿勢!$R我年輕的時候啊…嗯?$R;");

                if (CountItem(pc, 10003200) >= 10)
                {
                    TakeItem(pc, 10003200, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予解毒果, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集黃麥粉, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集解毒果, false);
                    //_3A75 = true;
                    //_3A71 = true;
                    //_3A70 = false;
                    Say(pc, 11000582, 131, "「解毒果實」10個$R沒有錯$R;" +
                        "$R按照顧客們的訂購$R準確的把商品轉交$R對商人來説是最重要的事情$R;");
                    Say(pc, 11000582, 131, "那下次要轉交的地方是$R嗨嗨牢騷還真多!$R;" +
                        "$P可以替我給東邊村子的嘎佈利耶爾那家伙轉交$R10份「黃麥粉」嗎?$R;" +
                        "$R我稍後也會跟著過去的…$R那就拜託了$R;");
                    return;
                }
                Say(pc, 11000582, 131, "不是「解毒果實」10個阿!!$R重新拿過來吧!$R;");
                return;
            }
            Say(pc, 11000582, 131, "給東邊村子的嘎佈利耶爾那家伙轉交$R10份「黃麥粉」吧$R;" +
                "$R那就拜託了$R;");
        }
    }
}