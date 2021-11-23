
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000600 : Event
    {
        public S80000600()
        {
            this.EventID = 80000600;
        }

        public override void OnEvent(ActorPC pc)
        {
            HandleQuest(pc, 4);
        }
    }
}

