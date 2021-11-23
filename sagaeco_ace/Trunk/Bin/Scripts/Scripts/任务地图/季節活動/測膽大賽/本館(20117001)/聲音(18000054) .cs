using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30154002
{
    public class S18000054 : Event
    {
        public S18000054()
        {
            this.EventID = 18000054;
        }

        public override void OnEvent(ActorPC pc)
        {

            PlaySound(pc, 3261, false, 70, 50);

         }
     }
}
