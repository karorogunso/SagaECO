using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054001
{
    public class S12002059 : Event
    {
        public S12002059()
        {
            this.EventID = 12002059;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里开始是法伊斯特市$R;" +
                "                     法伊斯特评议会$R;");
        }
    }
}