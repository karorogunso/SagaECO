using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001511 : Event
    {
        public S11001511()
        {
            this.EventID = 11001511;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "斯噼~…斯噼~……。$R;", "参古亀");
        }
    }
}


