using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30152002
{
    public class S10001709 : Event
    {
        public S10001709()
        {
            this.EventID = 10001709;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30091007, 8, 3);
        }
    }
}