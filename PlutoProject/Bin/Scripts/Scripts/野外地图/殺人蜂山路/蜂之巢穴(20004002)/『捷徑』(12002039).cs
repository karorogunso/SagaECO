using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002039 : Event
    {
        public S12002039()
        {
            this.EventID = 12002039;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "往『杀人蜂山谷』的捷径$R;" +
                "通行时要注意！！$R;");
        }
    }
}
