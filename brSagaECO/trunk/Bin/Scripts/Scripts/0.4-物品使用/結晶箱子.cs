using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class S90000029 : Event
    {
        public S90000029()
        {
            this.EventID = 90000029;
        }

        public override void OnEvent(ActorPC pc)
        {
                if (CountItem(pc, 10049052) > 0)
                {
                GiveRandomTreasure(pc, "SHOPBOX");
                TakeItem(pc, 10049052, 1);
                }
        }
    }
}