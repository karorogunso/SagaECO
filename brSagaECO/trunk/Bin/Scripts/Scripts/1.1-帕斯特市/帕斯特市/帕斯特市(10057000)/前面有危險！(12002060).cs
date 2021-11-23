using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S12002060 : Event
    {
        public S12002060()
        {
            this.EventID = 12002060;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "前面是東域$R;" +
                "是非常危險的地區$R;" +
                "請勿走近！$R;" +
                "$R;" +
                "帕斯特評議會$R;");

        }
    }
}