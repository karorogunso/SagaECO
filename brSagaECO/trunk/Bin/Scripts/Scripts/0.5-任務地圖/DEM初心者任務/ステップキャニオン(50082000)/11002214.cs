using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50082000
{
    public class S11002214 : Event
    {
        public S11002214()
        {
            this.EventID = 11002214;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "仲間がいるのは$R;" +
            "そっちではない。$R;", "");
        }
    }
}