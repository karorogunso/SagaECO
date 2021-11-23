using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30099001
{
    public class S11000864 : Event
    {
        public S11000864()
        {
            this.EventID = 11000864;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a69)
            {
                Say(pc, 131, "不对呀，这和约定的不一样$R;" +
                    "还没达到委托的一半！$R;");
                Say(pc, 11000858, 131, "对…对不起$R;" +
                    "$R因为上次的事，$R;" +
                    "实在弄不到摩戈炭了$R;");
                return;
            }
            */
            Say(pc, 131, "那么拜托了$R;");
            Say(pc, 11000858, 131, "知道了…辛苦您了$R;");
        }
    }
}