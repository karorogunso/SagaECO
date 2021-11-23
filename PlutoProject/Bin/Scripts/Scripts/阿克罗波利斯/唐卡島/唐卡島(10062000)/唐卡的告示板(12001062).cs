using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S12001062 : Event
    {
        public S12001062()
        {
            this.EventID = 12001062;
        }

        public override void OnEvent(ActorPC pc)
        {
            OpenBBS(pc, 11, 0);
        }
    }
}