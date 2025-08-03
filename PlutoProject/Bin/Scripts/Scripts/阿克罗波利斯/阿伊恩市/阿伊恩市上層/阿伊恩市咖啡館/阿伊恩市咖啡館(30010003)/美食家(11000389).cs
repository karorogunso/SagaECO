using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30010003
{
    public class S11000389 : Event
    {
        public S11000389()
        {
            this.EventID = 11000389;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "滴 滴…$R;");
        }
    }
}