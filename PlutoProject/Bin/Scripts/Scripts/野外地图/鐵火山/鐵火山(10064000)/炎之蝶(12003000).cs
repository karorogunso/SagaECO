using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S12003000 : Event
    {
        public S12003000()
        {
            this.EventID = 12003000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……$R;");
        }
    }
}