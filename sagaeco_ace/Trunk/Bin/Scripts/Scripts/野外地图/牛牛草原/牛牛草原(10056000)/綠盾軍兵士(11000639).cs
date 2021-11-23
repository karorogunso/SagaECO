using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000639 : Event
    {
        public S11000639()
        {
            this.EventID = 11000639;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从这里开始非常危险$R;" +
                "请绕道吧$R;");
        }
    }
}