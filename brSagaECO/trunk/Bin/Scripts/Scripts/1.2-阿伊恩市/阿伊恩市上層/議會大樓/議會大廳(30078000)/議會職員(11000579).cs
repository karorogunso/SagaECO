using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30078000
{
    public class S11000579 : Event
    {
        public S11000579()
        {
            this.EventID = 11000579;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "閒人勿進$R;");
        }
    }
}