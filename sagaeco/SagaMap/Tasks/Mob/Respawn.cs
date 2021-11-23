using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Mob;
using SagaMap.Manager;

namespace SagaMap.Tasks.Mob
{
    public class Respawn : MultiRunTask
    {
        private ActorMob mob;
        public Respawn(ActorMob mob, int delay)
        {
            this.dueTime = delay;
            this.period = delay;
            this.mob = mob;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                Map map_ = Manager.MapManager.Instance.GetMap(mob.MapID);
                int x_new, y_new;
                ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)mob.e;
                int min_x, max_x, min_y, max_y, ori_x, ori_y;
                ori_x = Global.PosX16to8((short)(eh.AI.X_Ori), map_.Width);
                ori_y = Global.PosY16to8((short)(eh.AI.Y_Ori), map_.Height);

                min_x = ori_x - eh.AI.MoveRange / 100;
                max_x = ori_x + eh.AI.MoveRange / 100;
                min_y = ori_y - eh.AI.MoveRange / 100;
                max_y = ori_y + eh.AI.MoveRange / 100;

                if (min_x < 0) min_x = 0;
                if (max_x >= map_.Width)
                    max_x = map_.Width - 1;
                if (min_y < 0) min_y = 0;
                if (max_y >= map_.Height)
                    max_y = map_.Height - 1;
                
                x_new = (byte)Global.Random.Next(min_x ,max_x);
                while (x_new < min_x || x_new > max_x)
                {
                    x_new = (byte)Global.Random.Next(min_x, max_x);
                    Thread.Sleep(1);
                }
                y_new = (byte)Global.Random.Next(min_y, max_y);
                while (y_new < min_y || y_new > max_y)
                {
                    y_new = (byte)Global.Random.Next(min_y, max_y);
                    Thread.Sleep(1);
                }
                int counter = 0;
                while (map_.Info.walkable[x_new, y_new] != 2)
                {
                    if (counter > 1000)
                    {
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    x_new = (byte)Global.Random.Next(min_x, max_x);
                    while (x_new < min_x || x_new > max_x)
                    {
                        x_new = (byte)Global.Random.Next(min_x, max_x);
                        Thread.Sleep(1);
                    }
                    y_new = (byte)Global.Random.Next(min_y, max_y);
                    while (y_new < min_y || y_new > max_y)
                    {
                        y_new = (byte)Global.Random.Next(min_y, max_y);
                        Thread.Sleep(1);
                    }
                    counter++;
                }

                mob.Buff.Clear();
                mob.Dir = (ushort)Global.Random.Next(0, 7);
                mob.X = Global.PosX8to16((byte)x_new, map_.Width);
                mob.Y = Global.PosY8to16((byte)y_new, map_.Height);
                eh.AI.X_Spawn = mob.X;
                eh.AI.Y_Spawn = mob.Y;
                map_.RegisterActor(mob);
                if(MobFactory.Instance.BossList.Contains(mob))
                {
                    int onlineCount = MapClientManager.Instance.OnlinePlayerOnlyIP.Count;
                    if (onlineCount < 10)
                        mob.MaxHP = (uint)(mob.TInt["MaxHP"] * 0.6f);
                    else if (onlineCount < 20)
                        mob.MaxHP = (uint)(mob.TInt["MaxHP"] * 0.8f);
                    else if (onlineCount < 30)
                        mob.MaxHP = (uint)mob.TInt["MaxHP"];
                    else if (onlineCount < 40)
                        mob.MaxHP = (uint)(mob.TInt["MaxHP"] * 1.4f);
                    else if (onlineCount < 50)
                        mob.MaxHP = (uint)(mob.TInt["MaxHP"] * 1.8f);
                    else
                        mob.MaxHP = (uint)(mob.TInt["MaxHP"] * 2f);
                }
                mob.HP = mob.MaxHP;
                mob.MP = mob.MaxMP;
                mob.SP = mob.MaxSP;
               
                mob.invisble = false;
                map_.OnActorVisibilityChange(mob);
                map_.SendVisibleActorsToActor(mob);

                mob.Tasks.Remove("Respawn");
                ((ActorEventHandlers.MobEventHandler)(mob.e)).AI.Start();
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
