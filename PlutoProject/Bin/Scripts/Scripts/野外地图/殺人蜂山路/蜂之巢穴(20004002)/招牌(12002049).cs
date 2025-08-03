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
            Say(pc, 131, "因为蜜蜂翅膀发出的声音$R我开始有点神经质了$R;" +
                "$R该怎么办？$R;");
        }
    }
}
