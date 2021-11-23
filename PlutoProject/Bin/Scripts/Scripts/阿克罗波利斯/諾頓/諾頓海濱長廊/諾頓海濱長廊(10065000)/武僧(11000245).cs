using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000245 : Event
    {
        public S11000245()
        {
            this.EventID = 11000245;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowEffect(pc, 4091);
            PlaySound(pc, 3011, false, 100, 50);
            Say(pc, 111, "啊嚏！$R;");
            Say(pc, 131, "啊！对不起$R;" +
                "跟着喷嚏一起发出火焰属性魔法…$R;" +
                "$R哈哈…$R;" +
                "城门的警备太冷了不喜欢，$R;" +
                "得赶紧学会加热呀$R;");

            //EVT1100024500
            /*
            Say(pc, 131, "實實？$R;" +
                "$R原來如此$R;" +
                "咖啡館有奇怪的熊？$R;");
            */
            //EVENTEND
        }
    }
}
