using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001121 : Event
    {
        public S11001121()
        {
            this.EventID = 11001121;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "有时间要清洁一下，$R不然会有异味的$R;");
                return;
            }
            Say(pc, 255, "把我洗得干干净净的$R;");
        }
    }
}