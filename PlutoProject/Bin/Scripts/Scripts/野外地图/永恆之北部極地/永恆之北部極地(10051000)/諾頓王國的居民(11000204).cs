using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051000
{
    public class S11000204 : Event
    {
        public S11000204()
        {
            this.EventID = 11000204;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "…$R;");
        }
    }
}
