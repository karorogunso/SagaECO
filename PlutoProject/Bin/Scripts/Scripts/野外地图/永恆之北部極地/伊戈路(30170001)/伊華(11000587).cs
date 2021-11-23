using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30170001
{
    public class S11000587 : Event
    {
        public S11000587()
        {
            this.EventID = 11000587;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (Job2X_12_mask.Test(Job2X_12.收集黃麥粉))
            {
                Say(pc, 131, "真的谢谢「解毒果实」∼$R;");
                return;
            }

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集解毒果) && !Job2X_12_mask.Test(Job2X_12.給予解毒果))//_3A70 && !_3A75)
            {
                Say(pc, 11000582, 131, "怎么这么慢啊!!$R;" +
                    "$R比我还慢!!$R到底怎么搞得?!$R;");
                Say(pc, 11000587, 131, "古…古鲁杜先生$R没有人走的偏僻地方又冷…$R所以无能为力啊$R;");
                Say(pc, 11000582, 131, "嗨嗨!!$R姿势不行啊!姿势!$R我年轻的时候啊…嗯?$R;");

                if (CountItem(pc, 10003200) >= 10)
                {
                    TakeItem(pc, 10003200, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予解毒果, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集黃麥粉, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集解毒果, false);
                    //_3A75 = true;
                    //_3A71 = true;
                    //_3A70 = false;
                    Say(pc, 11000582, 131, "「解毒果实」10个$R没有错$R;" +
                        "$R按照顾客们的订购$R准确的把商品转交$R对商人来说是最重要的事情$R;");
                    Say(pc, 11000582, 131, "那下次要转交的地方是…$R嗨嗨牢骚还真多!$R;" +
                        "$P可以替我给东边村子的嘉布利那家伙转交$R10份「巨麦粉」吗?$R;" +
                        "$R我稍后也会跟着过去的…$R那就拜托了$R;");
                    return;
                }
                Say(pc, 11000582, 131, "不是「解毒果实」10个啊!!$R重新拿过来吧!$R;");
                return;
            }
            Say(pc, 11000582, 131, "给东边村子的嘉布利那家伙转交$R10份「巨麦粉」吧$R;" +
                "$R那就拜托了$R;");
        }
    }
}