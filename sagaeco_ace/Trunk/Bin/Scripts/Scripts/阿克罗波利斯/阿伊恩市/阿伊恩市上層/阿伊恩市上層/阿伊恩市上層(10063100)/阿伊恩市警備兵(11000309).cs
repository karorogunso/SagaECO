using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000309 : Event
    {
        public S11000309()
        {
            this.EventID = 11000309;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里是合同大厦$R;");
        }
    }
}