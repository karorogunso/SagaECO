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
    public class S11000811 : Event
    {
        public S11000811()
        {
            this.EventID = 11000811;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這是開採摩根炭的地方$R;");
        }
    }
}