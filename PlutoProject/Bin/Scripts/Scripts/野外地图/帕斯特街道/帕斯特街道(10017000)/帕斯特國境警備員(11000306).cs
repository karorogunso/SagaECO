using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017000
{
    public class S11000306 : Event
    {
        public S11000306()
        {
            this.EventID = 11000306;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从这里开始是禁止通行$R回去吧$R;");
        }
    }
}
