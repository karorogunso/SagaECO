using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S12001107 : Event
    {
        public S12001107()
        {
            this.EventID = 12001107;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "稻草人在奸笑$R;");
        }
    }
}