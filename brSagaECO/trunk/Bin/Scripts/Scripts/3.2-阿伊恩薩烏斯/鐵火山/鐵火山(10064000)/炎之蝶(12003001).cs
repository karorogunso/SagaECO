using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S12003001 : Event
    {
        public S12003001()
        {
            this.EventID = 12003001;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……$R;");
        }
    }
}