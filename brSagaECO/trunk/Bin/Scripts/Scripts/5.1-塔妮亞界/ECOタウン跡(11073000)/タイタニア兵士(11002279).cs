using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11073000
{
    public class S11002279 : Event
    {
        public S11002279()
        {
            this.EventID = 11002279;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 65535, "ココより先、部外者の侵入は$R;" +
            "ご連慮頂きたい。$R;" +
            "$P一応、警告はしたからな！$R;", "タイタニア兵士");
        }


    }
}


