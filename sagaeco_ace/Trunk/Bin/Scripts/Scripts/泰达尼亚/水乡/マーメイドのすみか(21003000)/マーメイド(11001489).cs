using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001489 : Event
    {
        public S11001489()
        {
            this.EventID = 11001489;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 132, "……。$R;" +
            "……啵。$R;", "美人鱼");

        }


    }
}


