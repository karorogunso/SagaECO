using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002021 : Event
    {
        public S11002021()
        {
            this.EventID = 11002021;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "仕事の邪魔です。$R;" +
            "話なら後にして下さい。$R;", "ガラージュ");

        }
    }
}


        
   


