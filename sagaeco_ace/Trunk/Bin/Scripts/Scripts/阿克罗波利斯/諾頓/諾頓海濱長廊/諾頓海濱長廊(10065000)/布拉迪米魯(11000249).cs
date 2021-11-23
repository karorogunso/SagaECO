using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000249 : Event
    {
        public S11000249()
        {
            this.EventID = 11000249;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 158, "哎！讨厌高的地方呀！$R;" +
                "我其实有恐高症的$R;" +
                "$R不过这下面有$R;" +
                "诺森市民街，$R;" +
                "怎么看不见呢$R;");
        }
    }
}
