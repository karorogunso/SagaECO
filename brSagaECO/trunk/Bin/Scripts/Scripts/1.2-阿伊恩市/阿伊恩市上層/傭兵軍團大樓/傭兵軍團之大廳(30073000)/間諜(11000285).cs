using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30073000
{
    public class S11000285 : Event
    {
        public S11000285()
        {
            this.EventID = 11000285;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我們是傭兵軍團。$R;");
        }
    }
}