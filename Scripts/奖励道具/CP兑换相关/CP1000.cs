
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000116 : Event
    {
        public S910000116()
        {
            this.EventID = 910000116;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000116) > 0)
            {
                pc.CP += 1000;
                TakeItem(pc, 910000116, 1);
            }
        }
    }
}

