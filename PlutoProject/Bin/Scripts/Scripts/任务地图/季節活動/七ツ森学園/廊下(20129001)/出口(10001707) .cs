using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M20129001
{
    public class S10001707 : Event
    {
        public S10001707()
        {
            this.EventID = 10001707;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30091007, 8, 3);

        }
    }
}
