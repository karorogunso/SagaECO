using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000757 : Event
    {
        public S11000757()
        {
            this.EventID = 11000757;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "粉红色松鼠还没好吗…？$R;" +
                "今天想早点回家$R;" +
                "$R落日之后，南边的城里$R;" +
                "有恐怖的东西出来啊！$R;");
        }
    }
}