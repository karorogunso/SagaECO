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
            this.period = 300000;
            this.pc = pc;            
        }

        public override void CallBack()
        {
            if (pc == null)
            {
                this.Deactivate();
                return;
            }
            if(!pc.Online)
            {
                this.Deactivate();
                return;
            }
            ClientManager.EnterCriticalArea();
            try
            {
                DateTime now = DateTime.Now;
                MapServer.charDB.SaveChar(pc, false);
                Logger.ShowInfo("Autosaving " + pc.Name + "'s data, 耗时:" + (DateTime.Now - now).TotalMilliseconds +"ms");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
