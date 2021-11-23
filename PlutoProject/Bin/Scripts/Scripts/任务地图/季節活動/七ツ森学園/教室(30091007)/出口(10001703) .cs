using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30091007
{
    public class S10001703 : Event
    {
        public S10001703()
        {
            this.EventID = 10001703;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10023000, 181, 101);
        }
    }
}