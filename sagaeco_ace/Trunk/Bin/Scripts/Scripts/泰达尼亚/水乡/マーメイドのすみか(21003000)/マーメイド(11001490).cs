using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001490 : Event
    {
        public S11001490()
        {
            this.EventID = 11001490;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 134, "一点点的话、能够明白。$R;", "美人鱼");
        }


    }
}


