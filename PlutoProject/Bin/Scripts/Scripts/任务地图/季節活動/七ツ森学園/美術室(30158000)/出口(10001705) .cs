using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30158000
{
    public class S10001705 : Event
    {
        public S10001705()
        {
            this.EventID = 10001705;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30091007, 8, 3);

        }
    }
}