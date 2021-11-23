using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001882 : Event
    {
        public S11001882()
        {
            this.EventID = 11001882;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "解せぬ……。$R;", "気になる男");
}
}

        
    }


