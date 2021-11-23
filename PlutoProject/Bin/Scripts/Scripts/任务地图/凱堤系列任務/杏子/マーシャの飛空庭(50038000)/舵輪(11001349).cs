using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M50038000
{
    public class S11001349 : Event
    {
        public S11001349()
        {
            this.EventID = 11001349;
        }

        public override void OnEvent(ActorPC pc)
        {
            StartEvent(pc, 11001345);
        }
    }
}