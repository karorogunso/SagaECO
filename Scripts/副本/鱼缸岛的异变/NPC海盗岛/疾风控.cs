
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
    public class S80000215 : Event
    {
        public S80000215()
        {
            this.EventID = 80000215;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "……", "疾风控");
        }
    }
}

