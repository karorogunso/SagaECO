using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class Marionette : MultiRunTask
    {
        MapClient client;
        public Marionette(MapClient client, int duration)
        {
            this.dueTime = duration * 1000;
            this.period = duration * 1000;
            this.client = client;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                client.MarionetteDeactivate();
                if (client.Character.Tasks.ContainsKey("Marionette"))
                    client.Character.Tasks.Remove("Marionette");
                this.Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
