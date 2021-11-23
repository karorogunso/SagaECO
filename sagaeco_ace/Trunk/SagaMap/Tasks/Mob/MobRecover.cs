using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.Mob
{
    public class MobRecover : MultiRunTask
    {
        private ActorMob mob;
        byte count;
        public MobRecover(ActorMob mob)
        {
            this.dueTime = 1000;
            this.period = 1000;
            this.mob = mob;
        }

        public override void CallBack()
        {
            try
            {
                ClientManager.EnterCriticalArea();
                int hpadd = 0;
                hpadd = mob.BaseData.resilience;
                mob.HP += (uint)hpadd;
                if (mob.HP > mob.MaxHP)
                {
                    mob.HP = mob.MaxHP;
                    this.count = 0;
                }
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(mob.MapID);
                //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, mob, false);

                if (mob.HP == mob.MaxHP)
                    count++;
                if (count >= 100)
                    this.Deactivate();
                ClientManager.LeaveCriticalArea();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                mob.Tasks.Remove("MobRecover");
                this.Deactivate();
            }
        }
    }
}
