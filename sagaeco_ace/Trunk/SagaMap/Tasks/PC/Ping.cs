using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class Ping : MultiRunTask
    {
        MapClient pc;
        public Ping(MapClient pc)
        {
            this.period = 10000;
            this.pc = pc;            
        }

        public override void  CallBack()
        {
            if ((pc.ping - DateTime.Now).TotalSeconds > 120)
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    pc.netIO.Disconnect();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
