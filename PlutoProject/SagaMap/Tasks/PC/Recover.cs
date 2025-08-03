using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public partial class Recover : MultiRunTask
    {
        MapClient client;
        public Recover(MapClient client)
        {
            this.dueTime = 3000;
            this.period = 3000;
            this.client = client;
        }

        public override void CallBack()
        {
            //ClientManager.EnterCriticalArea();
            try
            {
                if (client != null)
                {
                    ActorPC pc = client.Character;
                    if (pc == null) return;
                    if ((DateTime.Now - pc.TTime["上次自然回复时间"]).TotalSeconds < 3)
                    {
                        Deactivate();
                        client.Character.Tasks.Remove("Recover");
                        return;
                    }
                    if (client.Character.Tasks.ContainsKey("Recover"))
                    {
                        if (client.Character.Tasks["Recover"] != this)
                        {
                            Deactivate();
                            return;
                        }
                    }
                    if (!client.Character.Tasks.ContainsKey("Recover"))
                    {
                        client.Character.Tasks.Add("Recover", this);
                    }
                    pc.TTime["上次自然回复时间"] = DateTime.Now;
                    DateTime s = DateTime.Now;
                    BuffChecker(pc);
                    if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Custom) == Logger.defaultlogger.LogLevel)
                        Logger.ShowError("玩家" + client.Character.Name + "BUFF检测耗时：" + (DateTime.Now - s).TotalMilliseconds.ToString());
                    if (pc.MapID == 91000999 && pc.FurnitureID != 0 && pc.FurnitureID != 255)
                    {
                        this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.FURNITURE_SIT, null, pc, true);
                    }
                    if (pc.Mode == PlayerMode.KNIGHT_EAST)//除夕活动
                    {
                        this.Deactivate();
                        this.client.Character.Tasks.Remove("Recover");
                    }
                    if (pc.HP > 0 && pc.Buff.Dead)
                    {
                        pc.Buff.Dead = false;
                        pc.Buff.TurningPurple = false;
                    }
                    if (pc.HP > 0 && !pc.Buff.TurningPurple && !pc.Buff.Dead)
                    {

                    }
                    else
                    {
                        this.Deactivate();
                        this.client.Character.Tasks.Remove("Recover");
                    }
                    if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Custom) == Logger.defaultlogger.LogLevel)
                        Logger.ShowError("玩家" + client.Character.Name + "自然恢复总耗时：" + (DateTime.Now - s).TotalMilliseconds.ToString());
                }

                else
                {
                    this.Deactivate();
                    this.client.Character.Tasks.Remove("Recover");
                }

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
                this.client.Character.Tasks.Remove("Recover");
            }
            //ClientManager.LeaveCriticalArea();
        }
    }
}
