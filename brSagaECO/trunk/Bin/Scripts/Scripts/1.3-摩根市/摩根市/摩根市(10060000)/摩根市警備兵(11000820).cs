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
    public class S11000820 : Event
    {
        public S11000820()
        {
            this.EventID = 11000820;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這是摩根市$R;");
        }
    }
}