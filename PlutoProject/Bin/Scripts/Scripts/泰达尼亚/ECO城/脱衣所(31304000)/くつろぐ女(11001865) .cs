using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001865 : Event
    {
        public S11001865()
        {
            this.EventID = 11001865;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "うとうと……。$R;", "くつろぐ女");
        }


    }
}


