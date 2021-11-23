using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S12002057 : Event
    {
        public S12002057()
        {
            this.EventID = 12002057;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "前面是违反协定者的要塞$R;" +
                "是非常危险的地区$R;" +
                "最好不要接近$R;" +
                "                     法伊斯特评议会$R;");
        }
    }
}