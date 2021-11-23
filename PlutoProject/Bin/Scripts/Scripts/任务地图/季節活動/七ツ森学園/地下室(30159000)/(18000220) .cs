using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30159000
{
    public class S18000220 : Event
    {
        public S18000220()
        {
            this.EventID = 18000220;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2426, false, 50, 50);
            PlaySound(pc, 2518, false, 60, 50);
        }
    }
}