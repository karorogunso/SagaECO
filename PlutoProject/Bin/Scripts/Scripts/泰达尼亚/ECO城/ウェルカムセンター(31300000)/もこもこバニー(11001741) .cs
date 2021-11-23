using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001741 : Event
    {
        public S11001741()
        {
            this.EventID = 11001741;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001741, 131, "……。$R;", "もこもこバニー");
        }


    }
}


