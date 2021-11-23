using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S12002025 : Event
    {
        public S12002025()
        {
            this.EventID = 12002025;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "看看下面！$R;");
        }
    }
}