using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;


namespace SagaMap.Tasks.Item
{
    public class DeleteItem : MultiRunTask
    {
        private ActorItem npc;
        public DeleteItem(ActorItem item)
        {
            this.dueTime = 60000;
            this.period = 60000;
            this.npc = item;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                npc.Tasks.Remove("DeleteItem");
                Manager.MapManager.Instance.GetMap(npc.MapID).DeleteActor(npc);
                this.Deactivate();
            }
            catch (Exception)
            {
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
