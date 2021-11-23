using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000651 : Event
    {
        public S11000651()
        {
            this.EventID = 11000651;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000650, 131, "別咬人！$R;");
            Say(pc, 111, "汪！$R;");
            Say(pc, 11000650, 131, "不要跟著陌生人走！$R;");
            Say(pc, 11000652, 111, "汪！！$R;");
            Say(pc, 11000650, 131, "不要亂吃東西！$R;");
            Say(pc, 11000653, 111, "汪！汪…$R;" +
                "吭吭$R;");
            Say(pc, 11000650, 131, "説不定有毒呢！$R;");
        }
    }
}