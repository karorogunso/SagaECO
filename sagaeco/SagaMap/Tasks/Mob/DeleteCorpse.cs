using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;


namespace SagaMap.Tasks.Mob
{
    public class DeleteCorpse : MultiRunTask
    {
        private ActorMob npc;
        public DeleteCorpse(ActorMob mob)
        {
            this.dueTime = 5000;
            this.period = 5000;
            this.npc = mob;            
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                npc.Tasks.Remove("DeleteCorpse");
                Manager.MapManager.Instance.GetMap(npc.MapID).DeleteActor(npc);
                this.Deactivate();
            }
            catch (Exception)
            {
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
