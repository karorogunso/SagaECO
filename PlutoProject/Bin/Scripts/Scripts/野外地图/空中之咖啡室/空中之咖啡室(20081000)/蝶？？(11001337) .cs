using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M11001337
{
    public class S11001337 : Event
    {
        public S11001337()
        {
            this.EventID = 11001337;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000260, 131, ".....$R;", "蝶");
        }
    }
}