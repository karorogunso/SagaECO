
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
    public class S10002291 : Event
    {
        public S10002291()
        {
            this.EventID = 10002291;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10080000, 47, 82);
        }
    }
}

