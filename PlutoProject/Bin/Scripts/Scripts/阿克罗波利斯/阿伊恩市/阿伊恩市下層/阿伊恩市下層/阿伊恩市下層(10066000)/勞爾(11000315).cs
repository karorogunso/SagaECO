using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000315 : Event
    {
        public S11000315()
        {
            this.EventID = 11000315;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "再讲讲别的吧$R;");
        }
    }
}