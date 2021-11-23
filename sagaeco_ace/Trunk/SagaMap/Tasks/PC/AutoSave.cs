using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class AutoSave : MultiRunTask
    {
        ActorPC pc;
        public AutoSave(ActorPC pc)
        {
            this.period = 600000;
            this.pc = pc;            
        }

        public override void  CallBack()
        {
            if (pc.Status == null)
            {
                this.Deactivate();
                return;
            }
            ClientManager.EnterCriticalArea();
            try
            {
                Logger.ShowInfo("Autosaving " + pc.Name + "'s data...");

                MapServer.charDB.SaveChar(pc, false);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
