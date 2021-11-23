
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
    public class S10000366: Event
    {
        public S10000366()
        {
            this.EventID = 10000366;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "看来。。。是游不上去了呢");
        }
    }
}

