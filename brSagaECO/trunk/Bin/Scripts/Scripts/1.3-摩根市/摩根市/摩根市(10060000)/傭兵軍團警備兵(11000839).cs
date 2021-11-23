using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000839 : Event
    {
        public S11000839()
        {
            this.EventID = 11000839;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是傭兵軍團本部$R;");
        }
    }
}