using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000801 : Event
    {
        public S11000801()
        {
            this.EventID = 11000801;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……$R;");
        }
    }
}