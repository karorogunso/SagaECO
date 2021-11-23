using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11002240
{
    public class S11002240 : Event
    {
        public S11002240()
        {
            this.EventID = 11002240;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "あんたも、タルパ$R;" +
            "いじめる？……いじめる？$R;", "ビリー・タルパ");

        }
    }
}


        
   


