using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class S31080000 : Event
    {
        public S31080000()
        {
            this.EventID = 31080000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "什麼都沒有發生。", "");

        }
    }
}
