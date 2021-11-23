using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001753 : Event
    {
        public S11001753()
        {
            this.EventID = 11001753;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001741, 131, "……。$R;", "もこもこバニー");
        }


    }
}


