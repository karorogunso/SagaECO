using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S12002038 : Event
    {
        public S12002038()
        {
            this.EventID = 12002038;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "『蜂之故鄉』$R;" +
                "$R是一個有很多蜜蜂的危險洞窟$R;" +
                "怕蟲的話要注意喔$R;");
        }
    }
}
