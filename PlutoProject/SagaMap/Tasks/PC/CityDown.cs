using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class CityDown : MultiRunTask
    {
        MapClient client;
        public CityDown(MapClient client)
        {
            this.dueTime = 5000;
            this.period = 5000;
            this.client = client;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (this.client.Character.HP > 5)
                    this.client.Character.HP -= (uint)(5);
                else
                    this.client.Character.HP = 1;
                this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.client.Character, true);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.client.Character.Tasks.Remove("CityDown");
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
