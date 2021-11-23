using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M20129001
{
    public class S18000215 : Event
    {
        public S18000215()
        {
            this.EventID = 18000215;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "崩れた階段がある……。$R;" +
            "$P完全に埋まってしまって、$R;" +
            "先に進むことは$R;" +
            "できないようだ。$R;", "");

        }
    }
}
