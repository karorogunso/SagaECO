using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001853 : Event
    {
        public S11001853()
        {
            this.EventID = 11001853;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "視線が気になって$R;" +
            "着替えられない……。$R;", "勘の良い女");
        }


    }
}


