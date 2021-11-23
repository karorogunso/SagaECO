using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S90000207 : Event
    {
        public S90000207()
        {
            this.EventID = 90000207;
        }

        public override void OnEvent(ActorPC pc)
        {
            pc.QuestRemaining += 5;
            TakeItem(pc, 16000000, 1);

        }
    }
}