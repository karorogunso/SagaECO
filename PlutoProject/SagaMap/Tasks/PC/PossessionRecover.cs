using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class PossessionRecover : MultiRunTask
    {
        MapClient client;
        public PossessionRecover(MapClient client)
        {
            this.dueTime = 10000;
            this.period = 10000;
            this.client = client;            
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (client.Character.PossessionTarget == 0)
                {
                    this.client.Character.Tasks.Remove("PossessionRecover");
                    this.Deactivate();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                this.client.Character.HP += (uint)(this.client.Character.MaxHP * (100 + ((this.client.Character.Vit +
                    this.client.Character.Status.vit_item +
                    this.client.Character.Status.vit_rev) / 3)) / 1000);
                if (this.client.Character.HP > this.client.Character.MaxHP)
                    this.client.Character.HP = this.client.Character.MaxHP;
                this.client.Character.MP += (uint)(this.client.Character.MaxMP * (100 + ((this.client.Character.Mag +
                 this.client.Character.Status.mag_item +
                 this.client.Character.Status.mag_rev) / 3)) / 1000);
                if (this.client.Character.MP > this.client.Character.MaxMP)
                    this.client.Character.MP = this.client.Character.MaxMP;
                this.client.Character.SP += (uint)(this.client.Character.MaxSP * (100 + ((this.client.Character.Int +
       this.client.Character.Vit + this.client.Character.Status.int_item + 
       this.client.Character.Status.int_rev + this.client.Character.Status.vit_rev +
       this.client.Character.Status.vit_item) / 6)) / 1000);
                if (this.client.Character.SP > this.client.Character.MaxSP)
                    this.client.Character.SP = this.client.Character.MaxSP;
                this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.client.Character, true);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.client.Character.Tasks.Remove("PossessionRecover");
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
