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
                Say(pc, 131, "不對呀，這個跟約定不同呢$R;" +
                    "還不到委託的一半呀！$R;");
                Say(pc, 11000858, 131, "對…對不起$R;" +
                    "$R因為上次的事，$R;" +
                    "實在弄不到摩根炭了$R;");
                return;
            }
            */
            Say(pc, 131, "那麼拜託了$R;");
            Say(pc, 11000858, 131, "知道了…辛苦您了$R;");
        }
    }
}