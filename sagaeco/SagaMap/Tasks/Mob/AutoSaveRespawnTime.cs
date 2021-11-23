using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Mob;
using SagaMap.Manager;
using SagaMap.Skill;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Tasks.Mob
{
    public class AutoSaveRespawnTime : MultiRunTask
    {
        int count = 0;
        public AutoSaveRespawnTime()
        {
            Period = 30000;
        }

        static AutoSaveRespawnTime instance;

        public static AutoSaveRespawnTime Instance
        {
            get
            {
                if (instance == null)
                    instance = new AutoSaveRespawnTime();
                return instance;
            }
        }

        public override void CallBack()
        {
            count++;
            ActorPC SerPC = ScriptManager.Instance.VariableHolder;
            foreach (ActorMob item in MobFactory.Instance.BossList)
            {
                if (count == 1)
                    item.TInt["MaxHP"] = (int)item.MaxHP;
                if (item.Tasks.ContainsKey("Respawn"))
                {
                    TimeSpan duration = item.Tasks["Respawn"].NextUpdateTime - DateTime.Now;
                    int seconds = (int)duration.TotalSeconds;
                    SerPC.Adict["BOSS重生时间"][item.Name] = seconds;
                }
                else  if (!item.Buff.Dead && SerPC.Adict["BOSS重生时间"][item.Name] > 600)
                {
                    MobEventHandler eh = (MobEventHandler)item.e;
                    eh.OnDie();
                    if (item.Tasks.ContainsKey("Respawn"))
                        item.Tasks["Respawn"].NextUpdateTime = DateTime.Now + new TimeSpan(0, 0, 0, SerPC.Adict["BOSS重生时间"][item.Name]);
                }
            }
         }
    }
}
