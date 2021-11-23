using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000760 : Event
    {
        public S11000760()
        {
            this.EventID = 11000760;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "…$R;" +
                "$P…想休息一会…$R;");
        }
    }
}