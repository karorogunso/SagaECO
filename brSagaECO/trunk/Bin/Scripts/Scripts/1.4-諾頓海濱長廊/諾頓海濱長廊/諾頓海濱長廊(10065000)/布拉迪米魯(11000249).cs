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
            Say(pc, 158, "哎！討厭高的地方呀！$R;" +
                "我其實有懼高症的$R;" +
                "$R不過這下面有$R;" +
                "諾頓市民街，$R;" +
                "怎麼看不見呢$R;");
        }
    }
}
