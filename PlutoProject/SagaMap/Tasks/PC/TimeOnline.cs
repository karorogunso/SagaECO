using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class TimeOnline : MultiRunTask
    {
        ActorPC pc;
        public TimeOnline(ActorPC pc)
        {
            this.period = 1000;
            this.pc = pc;
        }

        public override void CallBack()
        {
            if (pc.Status == null)
            {
                this.Deactivate();
                return;
            }
            ClientManager.EnterCriticalArea();
            try
            {
                if(pc.TimeOnline[0].Day == DateTime.Now.Day)
                    pc.TimeOnline[1].AddSeconds(1);
                else
                {
                    pc.TimeOnline[0] = DateTime.Now;
                    pc.TimeOnline[1] = new DateTime();
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
