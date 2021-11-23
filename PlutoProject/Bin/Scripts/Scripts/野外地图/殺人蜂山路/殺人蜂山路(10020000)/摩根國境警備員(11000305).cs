using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S11000305 : Event
    {
        public S11000305()
        {
            this.EventID = 11000305;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从这里开始是禁止通行$R回去吧$R;");
        }
    }
}
