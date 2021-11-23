using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30159000
{
    public class S10001711 : Event
    {
        public S10001711()
        {
            this.EventID = 10001711;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30091007, 8, 3);
        }
    }
}