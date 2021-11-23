using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051000
{
    public class S11000206 : Event
    {
        public S11000206()
        {
            this.EventID = 11000206;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "…$R;");
        }
    }
}
