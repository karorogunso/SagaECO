using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20032002
{
    public class S10001713 : Event
    {
        public S10001713()
        {
            this.EventID = 10001713;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 20129001, 8, 1);
        }
    }
}