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
            Say(pc, 131, "前面是違反協定者的要塞$R;" +
                "是非常危險的地區$R;" +
                "最好不要接近$R;" +
                "                     帕斯特評議會$R;");
        }
    }
}