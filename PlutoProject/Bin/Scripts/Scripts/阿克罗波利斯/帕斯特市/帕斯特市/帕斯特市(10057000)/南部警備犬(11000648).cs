using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000648 : Event
    {
        public S11000648()
        {
            this.EventID = 11000648;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "汪汪          $R;");
        }
    }
}