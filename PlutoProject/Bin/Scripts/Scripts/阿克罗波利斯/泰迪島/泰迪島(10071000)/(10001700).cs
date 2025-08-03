using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S10001700 : Event
    {
        public S10001700()
        {
            this.EventID = 10001700;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10071000, 184, 180);
        }
    }
}