using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30154002
{
    public class S18000055 : Event
    {
        public S18000055()
        {
            this.EventID = 18000055;
        }

        public override void OnEvent(ActorPC pc)
        {

            
                PlaySound(pc, 2429, false, 70, 50);
     

            PlaySound(pc, 2514, false, 70, 50);

            PlaySound(pc, 2420, false, 70, 50);

         }
     }
}
