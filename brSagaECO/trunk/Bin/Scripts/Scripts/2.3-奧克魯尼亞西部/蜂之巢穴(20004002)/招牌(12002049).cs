using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002049 : Event
    {
        public S12002049()
        {
            this.EventID = 12002049;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "因為蜜蜂翅膀發出的聲音$R我開始有點神經質了$R;" +
                "$R該怎麼辦？$R;");
        }
    }
}
