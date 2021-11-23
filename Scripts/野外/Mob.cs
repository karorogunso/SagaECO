
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap;
using SagaMap.Manager;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public class WeeklySpawn
    {
        static WeeklySpawn instance = new WeeklySpawn();
        public static WeeklySpawn Instance { get { return instance; } }
        public List<ActorMob> SpawnMob(uint MobID, uint MapID, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai)
        {
            return SpawnMob(MobID, MapID, x, y, range, count, delay, mobinfo, Ai, null, 0);
        }
        public List<ActorMob> SpawnMob(uint MobID, uint MapID, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, MobCallback Event, byte Callbacktype)
        {
            return SpawnMob(MobID, MapID, 0,0,0, x, y, range, count, delay, mobinfo, Ai, null, 0);
        }
        public List<ActorMob> SpawnMob(uint MobID, uint MapID, uint PictID, uint AnotherID,byte AnotherCamp, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, MobCallback Event, byte Callbacktype)
        {
            Map map = MapManager.Instance.GetMap(MapID);
            List<ActorMob> mobs = new List<ActorMob>();
            for (int i = 0; i < count; i++)
            {
                ActorMob mob = new ActorMob(MobID, mobinfo);
                mob.PictID = PictID;
                mob.MapID = MapID;
                mob.Camp = AnotherCamp;
                mob.AnotherID = AnotherID;
                if (map == null) continue;
                int min_x, max_x, min_y, max_y;
                min_x = x - range;
                max_x = x + range;
                min_y = y - range;
                max_y = y + range;
                if (min_x < 0) min_x = 0;
                if (max_x >= map.Width)
                    max_x = map.Width - 1;
                if (min_y < 0) min_y = 0;
                if (max_y >= map.Height)
                    max_y = map.Height -1;
                int x_new, y_new;
                x_new = (byte)Global.Random.Next(min_x, max_x);
                y_new = (byte)Global.Random.Next(min_y, max_y);

                int counter = 0;
                try
                {
                    while(map.Info.walkable[x_new,y_new] != 2)
                    {
                        if (counter > 1000 || range == 0) break;
                        x_new = (byte)Global.Random.Next(min_x, max_x);
                        y_new = (byte)Global.Random.Next(min_y, max_y);
                        counter++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                if (counter > 1000)
                    continue;
                mob.X = Global.PosX8to16((byte)x_new, map.Width);
                mob.Y = Global.PosY8to16((byte)y_new, map.Height);
                mob.Dir = (ushort)Global.Random.Next(0, 7);
                MobEventHandler eh = new MobEventHandler(mob);
                mob.e = eh;
                if (Ai != null)
                    eh.AI.Mode = Ai;
                else eh.AI.Mode = new SagaMap.Mob.AIMode(0);
                eh.AI.X_Ori = Global.PosX8to16(x, map.Width);
                eh.AI.Y_Ori = Global.PosY8to16(y, map.Height);
                eh.AI.X_Spawn = mob.X;
                eh.AI.Y_Spawn = mob.Y;
                eh.AI.MoveRange = 10000;
                eh.AI.SpawnDelay = delay * 1000;
                map.RegisterActor(mob);
                mob.invisble = false;
                mob.sightRange = 2500;
                map.OnActorVisibilityChange(mob);

                if (Event != null)
                {
                    switch (Callbacktype)
                    {
                        case 1:
                            eh.Dying += Event;
                            break;
                        case 2:
                            eh.SkillUsing += Event;
                            break;
                        case 3:
                            eh.Moving += Event;
                            break;
                        case 4:
                            eh.Attacking += Event;
                            break;
                        case 5:
                            eh.Defending += Event;
                            break;
                    }
                }
                eh.AI.Start();
                mobs.Add(mob);
            }
            return mobs;
        }
    }
}

