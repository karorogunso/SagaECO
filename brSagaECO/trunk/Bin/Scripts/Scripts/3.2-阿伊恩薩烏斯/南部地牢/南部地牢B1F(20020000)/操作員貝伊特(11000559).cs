using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000559 : Event
    {
        public S11000559()
        {
            this.EventID = 11000559;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這是貨運列車經過的地方，$R;" +
                "要小心啊$R;" +
                "$R要是撞倒就不好了$R;");
        }
    }
}