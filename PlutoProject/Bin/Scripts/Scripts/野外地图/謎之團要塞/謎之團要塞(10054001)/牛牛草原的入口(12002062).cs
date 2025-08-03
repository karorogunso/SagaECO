using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054001
{
    public class S12002062 : Event
    {
        public S12002062()
        {
            this.EventID = 12002062;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "前面是哞哞草原$R;");
        }
    }
}