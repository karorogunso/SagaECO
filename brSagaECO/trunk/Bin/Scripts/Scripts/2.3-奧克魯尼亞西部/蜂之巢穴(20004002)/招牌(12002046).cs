using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002046 : Event
    {
        public S12002046()
        {
            this.EventID = 12002046;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "從這巢的縫看進去的話$R;" +
                "湧出陣陣蜂蜜的香氣$R;" +
                "$R所以飛蟲都聚集過來了$R;");
        }
    }
}
