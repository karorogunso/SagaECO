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
            Say(pc, 131, "前面是东区$R;" +
                "是非常危险的地区$R;" +
                "请勿走近！$R;" +
                "$R;" +
                "法伊斯特评议会$R;");

        }
    }
}