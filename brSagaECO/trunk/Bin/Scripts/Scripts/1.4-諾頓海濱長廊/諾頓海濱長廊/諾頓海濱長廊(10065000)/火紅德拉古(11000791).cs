using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000791 : Event
    {
        public S11000791()
        {
            this.EventID = 11000791;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "…$R;");
        }
    }
}
