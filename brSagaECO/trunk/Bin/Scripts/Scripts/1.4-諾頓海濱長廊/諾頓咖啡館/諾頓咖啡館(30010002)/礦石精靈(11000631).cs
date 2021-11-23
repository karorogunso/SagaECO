using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010002
{
    public class S11000631 : Event
    {
        public S11000631()
        {
            this.EventID = 11000631;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嗯…$R;" +
                "不能再吃了$R;");
        }
    }
}
