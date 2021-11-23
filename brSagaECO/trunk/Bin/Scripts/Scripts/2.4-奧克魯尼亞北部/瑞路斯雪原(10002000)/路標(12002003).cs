using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S12002003 : Event
    {
        public S12002003()
        {
            this.EventID = 12002003;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "往山區『凍結的耕道』$R;");
        }
    }
}
