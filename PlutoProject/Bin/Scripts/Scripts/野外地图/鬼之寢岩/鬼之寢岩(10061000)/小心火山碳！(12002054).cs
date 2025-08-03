using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10061000
{
    public class S12002054 : Event
    {
        public S12002054()
        {
            this.EventID = 12002054;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从这里开始注意火山灰！$R;");
        }
    }
}