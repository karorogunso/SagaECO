using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000310 : Event
    {
        public S11000310()
        {
            this.EventID = 11000310;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是合同大廈$R;");
        }
    }
}