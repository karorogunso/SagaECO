using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002048 : Event
    {
        public S12002048()
        {
            this.EventID = 12002048;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "工蜂虽然很弱，可事实它们很危险呢$R;" +
                "$R因为它们跟亲人、同伴很亲密的$R;" +
                "$P辛苦的照顾下一代$R;" +
                "动物跟人类都一样呢$R;");
        }
    }
}
