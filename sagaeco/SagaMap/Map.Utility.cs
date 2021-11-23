using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;

using SagaDB.Actor;
using SagaDB.DefWar;
using SagaDB.Item;
using SagaDB.Map;
using SagaLib;
using SagaMap.Manager;

using SagaMap.Mob;

namespace SagaMap
{
    public partial class Map
    {
        public static short Distance(Actor sActor, Actor dActor)
        {
            return (short)Math.Sqrt((dActor.X - sActor.X) * (dActor.X - sActor.X) + (dActor.Y - sActor.Y) * (dActor.Y - sActor.Y));
        }

        public void Announce(string text)
        {
            List<Actor> list = actorsByID.Values.ToList();
            foreach (Actor i in list)
            {
                if (i.type == ActorType.PC)
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendAnnounce(text);
                }
            }
        }

        public void FindFreeCoord(short x, short y, out short x2, out short y2, params Actor[] excludes)
        {
            if (GetActorsArea(x, y, 8, excludes).Count == 0)
            {
                x2 = x;
                y2 = y;
                return;
            }
            short X = (short)Global.Random.Next(-100, 100);
            short Y = (short)Global.Random.Next(-100, 100);
            for (short i = X; i < 200; i += 100)
            {
                for (short j = Y; j < 200; j += 100)
                {
                    if (GetActorsArea((short)(x + i), (short)(y + j), 8, excludes).Count == 0)
                    {
                        x2 = (short)(x + i);
                        y2 = (short)(y + j);
                        return;
                    }
                }
            }
            x2 = x;
            y2 = y;
        }

        /// <summary>
        /// 计算2点之间的夹角
        /// </summary>
        /// <param name="x">原点X</param>
        /// <param name="y">原点Y</param>
        /// <param name="x2">目标点X</param>
        /// <param name="y2">目标点Y</param>
        /// 都是actor坐标 不是地图坐标！！！
        /// <returns></returns>
        public ushort CalcDir(short x, short y, short x2, short y2)
        {
            short vecX = (short)(x2 - x);
            short vecY = (short)(y2 - y);
            //注意注意：actor坐标y和地图方向是反的！反的！反的！
            if (vecX < 0)
            {
                return (ushort)(Math.Acos(-vecY / (Math.Sqrt(vecX * vecX + vecY * vecY))) / Math.PI * 180);
            }
            else
                return (ushort)(360 - (Math.Acos(-vecY / (Math.Sqrt(vecX * vecX + vecY * vecY))) / Math.PI * 180));
        }
        //换算成平面直角坐标系的角度..
        public ushort DirChange(ushort dir)
        {
            int d = 270 - dir;
            if (d < 0)
                d += 360;
            return (ushort)d;
        }

        /// <summary>
        /// 取得指定Actor周围指定距离内的所有玩家
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="range"></param>
        /// <param name="includeSourceActor"></param>
        /// <returns></returns>
        public List<Actor> GetPlayersArea(Actor sActor, short range, bool includeSourceActor)
        {
            if (sActor.type != ActorType.PC) return null;
            ActorPC caster = sActor as ActorPC;
            List<Actor> actors = GetActorsArea(sActor, range, includeSourceActor);
            List<Actor> result = new List<Actor>();
            foreach (var item in actors)
            {
                if (item.type == ActorType.PC)
                {
                    ActorPC pc = item as ActorPC;
                    if (pc.Mode == caster.Mode && pc.HP > 0)
                        result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 取得指定Actor周围指定距离内的所有Actor
        /// </summary>
        /// <param name="sActor">源Actor</param>
        /// <param name="range">范围</param>
        /// <param name="includeSourceActor">是否包含源Actor</param>
        /// <returns>范围内的Actor列表</returns>
        public List<Actor> GetActorsArea(Actor sActor, short range, bool includeSourceActor)
        {
            return GetActorsArea(sActor, range, includeSourceActor, true);
        }

        /// <summary>
        /// 取得指定坐标周围指定距离内的所有Actor
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="range">范围</param>
        /// <returns>范围内的Actor列表</returns>
        public List<Actor> GetActorsArea(Actor sActor, short range, bool includeSourceActor, bool includeInvisibleActor)
        {
            List<Actor> actors = new List<Actor>();
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(this.GetRegion(sActor.X, sActor.Y) + (deltaX * 1000000) + deltaY);
                    ConcurrentDictionary<ulong, Actor> actorsC;
                    if (!actorsByRegion.TryGetValue(region, out actorsC)) continue;
                    foreach (KeyValuePair<ulong, Actor> pair in actorsC)
                    {
                        Actor actor = pair.Value;
                        try
                        {
                            if (!includeSourceActor && (actor.ActorID == sActor.ActorID)) continue;
                            if (!includeInvisibleActor && actor.Buff.Transparent) continue;

                            if (this.ACanSeeB(actor, sActor, range))
                            {
                                actors.Add(actor);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
            }
            return actors;
        }
        /// <summary>
        /// 取得Actor周围随机一个Actor
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="range">范围</param>
        /// <returns></returns>
        public Actor GetRandomAreaActor(Actor sActor,short range)
        {
            List<Actor> actors = GetActorsArea(sActor, range, false, false);
            if (actors.Count == 0) return null;
            int num = SagaLib.Global.Random.Next(0, actors.Count - 1);
            return actors[num];
        }
        //获得路程长度
        public double GetLengthD(short x, short y, short x2, short y2)
        {
            return Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y));
        }
        //计算三角形面积，其他函数用
        public double TriangleArea(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * Math.Abs(p - a) * Math.Abs(p - b) * Math.Abs(p - c));
        }
        //获取任意矩形内角色列表
        public List<Actor> GetRectAreaActors(short x1, short y1, short x2, short y2, short x3, short y3, short x4, short y4, bool includeInvisibleActor = false)
        {
            List<Actor> actors = new List<Actor>();
            Actor[] list = this.Actors.Values.ToArray();
            double a = GetLengthD(x1, y1, x2, y2);
            double b = GetLengthD(x2, y2, x3, y3);
            double ab = GetLengthD(x1, y1, x3, y3);
            double c = GetLengthD(x3, y3, x4, y4);
            double d = GetLengthD(x4, y4, x1, y1);

            double area = TriangleArea(a, b, ab) + TriangleArea(c, d, ab);
            double e, f, g, h;

            foreach (Actor actor in list)
            {
                if (actor == null)
                    continue;
                if (!includeInvisibleActor && actor.Buff.Transparent) continue;
                e = GetLengthD(actor.X, actor.Y, x1, y1);
                f = GetLengthD(actor.X, actor.Y, x2, y2);
                g = GetLengthD(actor.X, actor.Y, x3, y3);
                h = GetLengthD(actor.X, actor.Y, x4, y4);
                double dd = TriangleArea(a, e, f) + TriangleArea(b, f, g) + TriangleArea(c, g, h) + TriangleArea(d, h, e);
                if (TriangleArea(a, e, f) + TriangleArea(b, f, g) + TriangleArea(c, g, h) + TriangleArea(d, h, e)
                    <= area + 1)
                {
                    actors.Add(actor);
                }
            }
            return actors;
        }
        //真·圆形判定
        public List<Actor> GetRoundAreaActors(short x, short y, short range, bool includeInvisibleActor = false)
        {
            List<Actor> actors = new List<Actor>();
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(this.GetRegion(x, y) + (deltaX * 1000000) + deltaY);
                    if (!this.actorsByRegion.ContainsKey(region)) continue;

                    ConcurrentDictionary<ulong, Actor> actorsC;
                    if (!this.actorsByRegion.TryGetValue(region, out actorsC)) continue;
                    foreach (KeyValuePair<ulong, Actor> pair in actorsC)
                    {
                        Actor actor = pair.Value;
                        try
                        {
                            if (actor == null)
                                continue;
                            if (!includeInvisibleActor && actor.Buff.Transparent) continue;

                            if ((actor.X - x) * (actor.X - x) + (actor.Y - y) * (actor.Y - y) <= range * range)
                            {
                                actors.Add(actor);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
            }
            return actors;
        }
        public List<Actor> GetActorsArea(short x, short y, short range, params Actor[] excludes)
        {
            return GetActorsArea(x, y, range, true, excludes);
        }
        public List<Actor> GetActorsArea(short x, short y, short range, bool includeInvisibleActor, params Actor[] excludes)
        {
            List<Actor> actors = new List<Actor>();
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(this.GetRegion(x, y) + (deltaX * 1000000) + deltaY);
                    if (!this.actorsByRegion.ContainsKey(region)) continue;

                    ConcurrentDictionary<ulong, Actor> actorsC;
                    if (!this.actorsByRegion.TryGetValue(region, out actorsC)) continue;
                    foreach (KeyValuePair<ulong, Actor> pair in actorsC)
                    {
                        Actor actor = pair.Value;
                        try
                        {
                            bool skip = false;
                            if (excludes != null)
                            {
                                foreach (Actor j in excludes)
                                {
                                    if (actor == j)
                                        skip = true;
                                }
                            }
                            if (actor == null)
                                continue;
                            if (skip) continue;
                            if (!includeInvisibleActor && actor.Buff.Transparent) continue;

                            if (actor.X >= x - range && actor.X <= x + range && actor.Y >= y - range && actor.Y <= y + range)
                            {
                                actors.Add(actor);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
            }
            return actors;
        }
        public List<ActorMob> SpawnCustomMob(uint MobID, uint MapID, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai)
        {
            return SpawnCustomMob(MobID, MapID,0,0,0, 0,x, y, range, count, delay, mobinfo, Ai, null, 0);
        }
        public List<ActorMob> SpawnCustomMob(uint MobID, uint MapID, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, SagaMap.Scripting.MobCallback Event, byte Callbacktype)
        {
            return SpawnCustomMob(MobID, MapID, 0, 0,0, 0, x, y, range, count, delay, mobinfo, Ai, Event, Callbacktype);
        }
        public List<ActorMob> SpawnCustomMob(uint MobID, uint MapID, uint PictID, uint AnotherID, byte AnotherCamp, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, SagaMap.Scripting.MobCallback Event, byte Callbacktype)
        {
            return SpawnCustomMob(MobID, MapID, PictID, 0,AnotherID, AnotherCamp, x, y, range, count, delay, mobinfo, Ai, Event, Callbacktype);
        }
        public List<ActorMob> SpawnCustomMob(uint MobID, uint MapID, uint PictID, uint RideID, uint AnotherID, byte AnotherCamp, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, SagaMap.Scripting.MobCallback Event, byte Callbacktype)
        {
            return SpawnCustomMob(MobID, MapID, PictID, RideID, AnotherID, AnotherCamp, x, y, range, count, delay, mobinfo, Ai, Event, Callbacktype,false);
        }
        public List<ActorMob> SpawnCustomMob(uint MobID, uint MapID, uint PictID, uint RideID, uint AnotherID, byte AnotherCamp, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, SagaMap.Scripting.MobCallback Event, byte Callbacktype, bool noreturn)
        {
            return SpawnCustomMob(MobID, MapID, PictID, RideID, AnotherID, AnotherCamp, x, y, range, count, delay, mobinfo, Ai, Event, Callbacktype, false,false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MobID"></param>
        /// <param name="MapID"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="range"></param>
        /// <param name="count"></param>
        /// <param name="delay"></param>
        /// <param name="mobinfo"></param>
        /// <param name="Ai"></param>
        /// <param name="Event"></param>
        /// <param name="Callbacktype">1=死亡事件 2=怪物技能使用时事件 3=怪物移动时事件 4=怪物攻击时事件 5=被攻击时事件</param>
        /// <returns></returns>
        public List<ActorMob> SpawnCustomMob(uint MobID, uint MapID, uint PictID, uint RideID, uint AnotherID, byte AnotherCamp, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai, SagaMap.Scripting.MobCallback Event, byte Callbacktype,bool noreturn,bool NoAIForNPC)
        {
            try
            {
                //Map map = MapManager.Instance.GetMap(MapID);
                List<ActorMob> mobs = new List<ActorMob>();
                for (int i = 0; i < count; i++)
                {
                    if (ID == 10054001)
                        AnotherID = 0;
                    ActorMob mob = new ActorMob(MobID, mobinfo);
                    if (mobinfo.level != 0)
                        mob.Level = mobinfo.level;
                    mob.MapID = MapID;
                    mob.PictID = PictID;
                    mob.Camp = AnotherCamp;
                    mob.AnotherID = AnotherID;
                    mob.RideID = RideID;
                    //mob.AnotherMark = 1;
                    //if (map == null) continue;
                    int min_x, max_x, min_y, max_y;
                    min_x = x - range;
                    max_x = x + range;
                    min_y = y - range;
                    max_y = y + range;
                    if (min_x < 0) min_x = 0;
                    if (max_x >= this.Width)
                        max_x = this.Width - 1;
                    if (min_y < 0) min_y = 0;
                    if (max_y >= this.Height)
                        max_y = this.Height - 1;
                    int x_new, y_new;
                    x_new = (byte)Global.Random.Next(min_x, max_x);
                    y_new = (byte)Global.Random.Next(min_y, max_y);

                    int counter = 0;
                    try
                    {
                        while (this.Info.walkable[x_new, y_new] != 2)
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
                    mob.X = Global.PosX8to16((byte)x_new, this.Width);
                    mob.Y = Global.PosY8to16((byte)y_new, this.Height);
                    mob.Dir = (ushort)Global.Random.Next(0, 7);
                    ActorEventHandlers.MobEventHandler eh = new ActorEventHandlers.MobEventHandler(mob);
                    mob.e = eh;
                    if (Ai != null)
                        eh.AI.Mode = Ai;
                    else eh.AI.Mode = new SagaMap.Mob.AIMode(0);
                    eh.AI.X_Ori = Global.PosX8to16(x, this.Width);
                    eh.AI.Y_Ori = Global.PosY8to16(y, this.Height);
                    eh.AI.X_Spawn = mob.X;
                    eh.AI.Y_Spawn = mob.Y;
                    eh.AI.MoveRange = (short)(range * 100);
                    eh.AI.SpawnDelay = delay * 1000;
                    this.RegisterActor(mob);
                    mob.invisble = false;
                    mob.sightRange = 2500;
                    this.SendVisibleActorsToActor(mob);
                    this.OnActorVisibilityChange(mob);

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
                            case 6:
                                eh.Returning += Event;
                                break;
                            case 7:
                                eh.FirstTimeDefending += Event;
                                break;
                        }
                    }
                    mob.YggMob = true;
                    mobs.Add(mob);
                    eh.AI.noreturn = noreturn;
                    if (!NoAIForNPC)
                        eh.AI.Start();
                    else eh.AI.Pause();

                    if (delay > 1800)
                        SagaDB.Mob.MobFactory.Instance.BossList.Add(mob);
                }
                return mobs;
            }
            catch(Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return null;
            }

        }
        public ActorMob SpawnMob(uint mobID, short x, short y, short moveRange, Actor master)
        {
            ActorMob mob = new ActorMob(mobID);
            mob.MapID = this.ID;
            mob.X = x;
            mob.Y = y;
            ActorEventHandlers.MobEventHandler eh = new ActorEventHandlers.MobEventHandler(mob);
            mob.e = eh;
            eh.AI.MoveRange = moveRange;
            if (Mob.MobAIFactory.Instance.Items.ContainsKey(mob.MobID))
                eh.AI.Mode = Mob.MobAIFactory.Instance.Items[mob.MobID];
            else
                eh.AI.Mode = new Mob.AIMode(0);
            eh.AI.Master = master;
            eh.AI.X_Ori = x;
            eh.AI.Y_Ori = y;
            eh.AI.X_Spawn = x;
            eh.AI.Y_Spawn = y;
            if (eh.AI.Master != null)
            {
                eh.AI.OnAttacked(master, 1);
            }
            this.RegisterActor(mob);
            mob.invisble = false;
            mob.sightRange = 2000;
            this.SendVisibleActorsToActor(mob);
            this.OnActorVisibilityChange(mob);
            eh.AI.Start();
            return mob;
        }


        public bool CheckActorSkillInRange(short x, short y, short range)
        {
            List<Actor> actors = GetActorsArea(x, y, range);
            foreach (Actor i in actors)
            {
                if (i.type != ActorType.SKILL)
                    continue;
                ActorSkill skill = (ActorSkill)i;
                if (!skill.Stackable)
                    return true;
            }
            return false;
        }

        public int CountActorType(ActorType type)
        {
            List<Actor> actors = actorsByID.Values.ToList();
            int count = 0;
            foreach (Actor i in actors)
            {
                if (i.type == type)
                    count++;
            }
            return count;
        }

        public void SendEffect(Actor actor, uint effect)
        {
            EffectArg arg = new EffectArg();
            arg.actorID = actor.ActorID;
            arg.effectID = effect;
            this.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
        }
        public void SendEffect(Actor actor, byte x, byte y, uint effectID)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = 0xFFFFFFFF;
            arg.x = x;
            arg.y = y;
            this.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
        }


        public void DefWarChange(DefWar text)
        {
            List<Actor> list = actorsByID.Values.ToList();
            foreach (Actor i in list)
            {
                if (i.type == ActorType.PC && ((ActorPC)i).DefWarShow)
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendDefWarChange(text);
                }
            }
        }


        public void DefWarResult(byte r1, byte r2, int exp, int jobexp, int cp, byte u = 0)
        {
            //*
            List<Actor> list = actorsByID.Values.ToList();
            foreach (Actor i in list)
            {
                if (i.type == ActorType.PC)
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendDefWarResult(r1, r2, exp, jobexp, cp, u);
                }
            }//*/
        }


        public void DefWarState(byte rate)
        {
            List<Actor> list = actorsByID.Values.ToList();
            foreach (Actor i in list)
            {
                if (i.type == ActorType.PC)
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendDefWarState(rate);
                }
            }
        }

        public void DefWarStates(Dictionary<uint, byte> l)
        {
            List<Actor> list = actorsByID.Values.ToList();
            foreach (Actor i in list)
            {
                if (i.type == ActorType.PC)
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendDefWarStates(l);
                }
            }
        }
    }
}
