using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000564 : Event
    {
        public S11000564()
        {
            this.EventID = 11000564;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "数量虽然少了，$R;" +
                "但还是有凶恶的魔物，$R;" +
                "要小心啊$R;");
        }
    }
}