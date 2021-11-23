using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class SpRecover : MultiRunTask
    {
        MapClient client;
        public SpRecover(MapClient client)
        {
            this.dueTime = 5000;
            this.period = 5000;
            this.client = client;
        }

        public override void CallBack()
        {
            if (this.client != null && this.client.Character != null)
            {
                ClientManager.EnterCriticalArea();
                try
                {

                    if (this.client.Character.EP != this.client.Character.MaxEP)
                    {
                        if (this.client.Character.Status.Additions.ContainsKey("居合姿态启动")) 
                            this.client.Character.EP += 5;
                        this.client.Character.EP += 5;
                        if (this.client.Character.EP > this.client.Character.MaxEP)
                            this.client.Character.EP = this.client.Character.MaxEP;
                        this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.client.Character, true);
                    }

                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.client.Character.Tasks.Remove("EpRecover");
                    this.Deactivate();
                }
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
