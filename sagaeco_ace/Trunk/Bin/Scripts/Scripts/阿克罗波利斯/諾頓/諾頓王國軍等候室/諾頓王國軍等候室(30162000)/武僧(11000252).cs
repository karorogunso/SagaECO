using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162000
{
    public class S11000252 : Event
    {
        public S11000252()
        {
            this.EventID = 11000252;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "最近好像身体不太好阿？$R;" +
                "$R常常发呆喔$R;" +
                "注意健康呀$R;");
            Say(pc, 11000251, 131, "是$R;" +
                "知道了$R;");
        }
    }
}
