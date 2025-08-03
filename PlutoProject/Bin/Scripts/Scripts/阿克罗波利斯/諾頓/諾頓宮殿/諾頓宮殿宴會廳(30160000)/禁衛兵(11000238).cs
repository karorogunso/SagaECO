using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000238 : Event
    {
        public S11000238()
        {
            this.EventID = 11000238;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我们诺森王国勤卫兵发誓，$R;" +
                "永远效忠女王陛下，$R;" +
                "我们是自豪的骑士$R;");
        }
    }
}