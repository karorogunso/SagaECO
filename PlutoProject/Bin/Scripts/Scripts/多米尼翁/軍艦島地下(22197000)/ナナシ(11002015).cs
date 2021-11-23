using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002015 : Event
    {
        public S11002015()
        {
            this.EventID = 11002015;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……。$R;" +
            "$P…俺にかまうな。$R;" +
            "悪いが女は苦手なんだ。$R;", "ナナシ");
        }
    }
}


        
   


