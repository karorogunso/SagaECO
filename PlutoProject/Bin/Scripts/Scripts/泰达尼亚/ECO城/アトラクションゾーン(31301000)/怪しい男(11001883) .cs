using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001883 : Event
    {
        public S11001883()
        {
            this.EventID = 11001883;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "ecoin……、ecoinっと……。$R;", "怪しい男");
}
}

        
    }


