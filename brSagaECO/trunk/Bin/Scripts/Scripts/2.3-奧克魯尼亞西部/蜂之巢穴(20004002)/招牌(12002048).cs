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
            Say(pc, 131, "工蜂雖然很弱，可事實牠們很危險呢$R;" +
                "$R因為牠們跟親人、同伴很親密的$R;" +
                "$P辛苦的照顧下一代$R;" +
                "動物跟人類都一樣呢$R;");
        }
    }
}
