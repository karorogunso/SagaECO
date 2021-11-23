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
    public class S12001057 : Event
    {
        public S12001057()
        {
            this.EventID = 12001057;

        }

        public override void OnEvent(ActorPC pc)
        {
            OpenBBS(pc, 8, 0);
        }
    }
}