using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051000
{
    public class S11000202 : Event
    {
        public S11000202()
        {
            this.EventID = 11000202;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "…$R;");
        }
    }
}
