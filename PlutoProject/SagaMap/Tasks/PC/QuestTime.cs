using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class QuestTime : MultiRunTask
    {
        MapClient client;
        public QuestTime(MapClient client)
        {
            this.dueTime = 60000;
            this.period = 60000;
            this.client = client;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            if (client.Character.Quest != null)
                client.SendQuestTime();
            ClientManager.LeaveCriticalArea();
        }
    }
}
