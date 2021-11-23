using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class Regeneration : MultiRunTask
    {
        MapClient client;
        public Regeneration(MapClient client)
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
                if (client.Character.Mode == PlayerMode.KNIGHT_EAST)//除夕活动
                {
                    this.Deactivate();
                    this.client.Character.Tasks.Remove("Regeneration");
                }
                //if (this.client != null)
                {
                    this.client.Character.HP += (uint)(0.1f * this.client.Character.MaxHP); ;
                    if (this.client.Character.HP > this.client.Character.MaxHP)
                        this.client.Character.HP = this.client.Character.MaxHP;
                    if (client.Character.Job != PC_JOB.HAWKEYE)
                    this.client.Character.MP += (uint)(0.1f * this.client.Character.MaxMP);
                    if (this.client.Character.MP > this.client.Character.MaxMP)
                        this.client.Character.MP = this.client.Character.MaxMP;
                    this.client.Character.SP += (uint)(0.1f * this.client.Character.MaxSP);
                    if (this.client.Character.SP > this.client.Character.MaxSP)
                        this.client.Character.SP = this.client.Character.MaxSP;
                    this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.client.Character, true);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
                this.client.Character.Tasks.Remove("Regeneration");
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
