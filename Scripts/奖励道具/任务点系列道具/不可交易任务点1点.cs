using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000100 : Event
    {
        public S910000100()
        {
            this.EventID = 910000100;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000100) > 0)
            {
                pc.QuestRemaining += 1;
                TakeItem(pc, 910000100, 1);
                if(pc.QuestRemaining > 500)
                  pc.QuestRemaining = 500;
                  SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("恢复了1点任务点。");
                  SagaMap.Network.Client.MapClient.FromActorPC(pc).SendQuestPoints();
            }
        }
    }
}

