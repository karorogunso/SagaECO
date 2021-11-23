using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001875 : Event
    {
        public S11001875()
        {
            this.EventID = 11001875;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "……俺はただこうして噴水を眺めながら$R;" +
            "ツキが回ってくるのを$R;" +
            "ひたすらまっているのさ……。$R;", "噴水を眺める男");
}
}

        
    }


