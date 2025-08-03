using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000563 : Event
    {
        public S11000563()
        {
            this.EventID = 11000563;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从这里开始$R;" +
                "会很危险的，请小心！！$R;");
        }
    }
}