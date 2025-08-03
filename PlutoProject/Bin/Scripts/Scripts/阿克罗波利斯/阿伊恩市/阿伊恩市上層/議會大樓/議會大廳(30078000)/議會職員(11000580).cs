using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30078000
{
    public class S11000580 : Event
    {
        public S11000580()
        {
            this.EventID = 11000580;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "闲人勿扰$R;");
        }
    }
}