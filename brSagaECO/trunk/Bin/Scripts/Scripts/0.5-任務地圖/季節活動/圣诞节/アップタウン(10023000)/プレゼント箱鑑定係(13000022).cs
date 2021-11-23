using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.M10023000
{
    public class S13000022 : Event
    {
        public S13000022()
        {
            this.EventID = 13000022;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 13000019, 131, "期間限定『プレゼント箱』の$R;" +
            "開封は我々にお申し付け下さいませ。$R;", "プレゼント箱鑑定係");
        }
    }
}
