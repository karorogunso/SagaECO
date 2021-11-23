using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.ODWar;
using SagaDB.Actor;

using SagaMap.Manager;
using SagaMap.Network.Client;

namespace SagaMap.Scripting
{
    public enum SymbolReviveResult
    {
        Success,
        NotDown,
        StillTrash,
        Faild,
    }
}

namespace SagaMap.Manager
{
    public class ODWarManager:Singleton<ODWarManager>
    {
        public ODWarManager()
        {
        }

        public void StartODWar(uint mapID)
        {
            if (IsDefence(mapID))
            {
                spawnSymbol(mapID);               
            }
            else
            {
                spawnSymbolTrash(mapID);
            }
        }

        public bool IsDefence(uint mapID)
        {
            return (ScriptManager.Instance.VariableHolder.AInt["ODWar" + mapID.ToString() + "Captured"] == 0);
        }

        void spawnSymbol(uint mapID)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            foreach(SagaDB.ODWar.ODWar.Symbol i in war.Symbols.Values)
            {
                short x = Global.PosX8to16(i.x, map.Width);
                short y = Global.PosY8to16(i.y, map.Height);

                i.actorID = map.SpawnMob(i.mobID, x, y, 2000, null).ActorID;
                i.broken = false;
            }
        }

        void spawnSymbolTrash(uint mapID)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol i in war.Symbols.Values)
            {
                short x = Global.PosX8to16(i.x, map.Width);
                short y = Global.PosY8to16(i.y, map.Height);
                i.actorID = map.SpawnMob(war.SymbolTrash, x, y, 2000, null).ActorID;
                i.broken = true;
            }
        }

        public void SymbolDown(uint mapID, ActorMob mob)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            bool alldown = true;
            foreach (int i in war.Symbols.Keys)
            {
                SagaDB.ODWar.ODWar.Symbol sym = war.Symbols[i];
                if (sym.actorID == mob.ActorID)
                {
                    if (mob.MobID == war.SymbolTrash)
                    {
                        sym.actorID = 0;
                    }
                    else
                    {
                        if (mob.MobID == sym.mobID)
                        {
                            sym.actorID = map.SpawnMob(war.SymbolTrash, mob.X, mob.Y, 10, null).ActorID;
                            sym.broken = true;
                            map.Announce(string.Format(LocalManager.Instance.Strings.ODWAR_SYMBOL_DOWN, i));
                        }
                    }
                }
                if (!sym.broken)
                    alldown = false;
            }
            if (IsDefence(mapID) && alldown)
            {
                EndODWar(mapID, false);   
            }
        }

        public void UpdateScore(uint mapID, uint actorID, int delta)
        {
            if (ODWarFactory.Instance.Items.ContainsKey(mapID))
            {
                SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
                if (!war.Score.ContainsKey(actorID))
                    war.Score.Add(actorID, 0);
                war.Score[actorID] += delta;
                if (war.Score[actorID] < 0)
                    war.Score[actorID] = 0;
            }
        }

        public SagaMap.Scripting.SymbolReviveResult ReviveSymbol(uint mapID, int number)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            if (war.Symbols.ContainsKey(number))
            {
                if (war.Symbols[number].broken)
                {
                    if (war.Symbols[number].actorID == 0)
                    {
                        short x = Global.PosX8to16(war.Symbols[number].x, map.Width);
                        short y = Global.PosY8to16(war.Symbols[number].y, map.Height);
                        Actor actor = map.SpawnMob(war.Symbols[number].mobID, x, y, 10, null);
                        actor.HP = actor.MaxHP / 2;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, false);
                        war.Symbols[number].actorID = actor.ActorID;
                        war.Symbols[number].broken = false;

                        map.Announce(string.Format(LocalManager.Instance.Strings.ODWAR_SYMBOL_ACTIVATE, number));
                        if (!IsDefence(mapID))
                        {
                            bool win = true;
                            foreach (SagaDB.ODWar.ODWar.Symbol i in war.Symbols.Values)
                            {
                                if (i.broken)
                                    win = false;
                            }
                            if (win)
                            {
                                EndODWar(mapID, true);
                            }
                        }
                        return SagaMap.Scripting.SymbolReviveResult.Success;
                    }
                    else
                        return SagaMap.Scripting.SymbolReviveResult.StillTrash;
                }
                else
                    return SagaMap.Scripting.SymbolReviveResult.NotDown;
            }
            else
                return SagaMap.Scripting.SymbolReviveResult.Faild;
        }

        public void SpawnBoss(uint mapID)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol i in war.Symbols.Values)
            {
                short x = Global.PosX8to16(i.x, map.Width);
                short y = Global.PosY8to16(i.y, map.Height);
                uint mobID = war.Boss[Global.Random.Next(0, war.Boss.Count - 1)];
                short[] pos = map.GetRandomPosAroundPos(x, y, 1500);
                map.SpawnMob(mobID, pos[0], pos[1], 2000, null);
            }
        }

        public void SpawnMob(uint mapID, bool strong)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol i in war.Symbols.Values)
            {
                short x = Global.PosX8to16(i.x, map.Width);
                short y = Global.PosY8to16(i.y, map.Height);
                if (strong)
                {
                    for (int j = 0; j < war.WaveStrong.DEMChamp; j++)
                    {
                        uint mobID = war.DEMChamp[Global.Random.Next(0, war.DEMChamp.Count - 1)];
                        short[] pos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, pos[0], pos[1], 2000, null);
                    }
                    for (int j = 0; j < war.WaveStrong.DEMNormal; j++)
                    {
                        uint mobID = war.DEMNormal[Global.Random.Next(0, war.DEMNormal.Count - 1)];
                        short[] pos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, pos[0], pos[1], 2000, null);
                    }
                }
                else
                {
                    for (int j = 0; j < war.WaveWeak.DEMChamp; j++)
                    {
                        uint mobID = war.DEMChamp[Global.Random.Next(0, war.DEMChamp.Count - 1)];
                        short[] pos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, pos[0], pos[1], 2000, null);
                    }
                    for (int j = 0; j < war.WaveWeak.DEMNormal; j++)
                    {
                        uint mobID = war.DEMNormal[Global.Random.Next(0, war.DEMNormal.Count - 1)];
                        short[] pos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, pos[0], pos[1], 2000, null);
                    }
                }
            }
        }

        /// <summary>
        /// 是否可以申请城市攻防战
        /// </summary>
        /// <param name="mapID"></param>
        /// <returns></returns>
        public bool CanApply(uint mapID)
        {
            if (!IsDefence(mapID))
                return false;
            ODWar war = ODWarFactory.Instance.Items[mapID];
            if (war.StartTime.ContainsKey((int)DateTime.Today.DayOfWeek))
            {
                if (DateTime.Now.Hour < war.StartTime[(int)DateTime.Today.DayOfWeek])
                    return true;
                else
                {
                    if (DateTime.Now.Minute < 15)
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 结束都市攻防战
        /// </summary>
        /// <param name="mapID"></param>
        /// <returns>是否胜利</returns>
        public void EndODWar(uint mapID, bool win)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);
            
            if (IsDefence(mapID))
            {
                if (!win)
                {
                    ScriptManager.Instance.VariableHolder.AInt["ODWar" + mapID.ToString() + "Captured"] = 1;
                    MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_LOSE);
                    List<Actor> actors = map.Actors.Values.ToList();
                    foreach (Actor i in actors)
                    {
                        if (i.type == ActorType.MOB)
                        {
                            ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)i.e;
                            if (!eh.AI.Mode.Symbol && !eh.AI.Mode.SymbolTrash)
                                eh.OnDie();
                        }
                    }
                }
                else
                {
                    MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_WIN);
                    MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_WIN2);
                    MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_WIN3);
                    MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_WIN4);
                    
                    List<Actor> actors = map.Actors.Values.ToList();
                    foreach (Actor i in actors)
                    {
                        if (i.type == ActorType.MOB)
                        {
                            ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)i.e;
                            if (!eh.AI.Mode.Symbol && !eh.AI.Mode.SymbolTrash)
                                eh.OnDie();
                        }
                    }
                }
                SendResult(mapID, win);
            }
            else
            {
                if (win)
                {
                    ScriptManager.Instance.VariableHolder.AInt["ODWar" + mapID.ToString() + "Captured"] = 0;
                    MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_CAPTURE);
                    List<Actor> actors = map.Actors.Values.ToList();
                    foreach (Actor i in actors)
                    {
                        if (i.type == ActorType.MOB)
                        {
                            ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)i.e;
                            if (!eh.AI.Mode.Symbol && !eh.AI.Mode.SymbolTrash)
                                eh.OnDie();
                        }
                    }
                }
            }
            List<Actor> actors2 = map.Actors.Values.ToList();
            foreach (Actor i in actors2)
            {
                if (i.type == ActorType.PC)
                {
                    if (((ActorPC)i).Online)
                    {
                        Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                        p1.StartX = 6;
                        p1.EndX = 6;
                        p1.StartY = 127;
                        p1.EndY = 127;
                        p1.EventID = 0xF1000000;
                        p1.EffectID = 9005;
                        MapClient.FromActorPC(((ActorPC)i)).netIO.SendPacket(p1);
                        p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                        p1.StartX = 245;
                        p1.EndX = 245;
                        p1.StartY = 127;
                        p1.EndY = 127;
                        p1.EventID = 0xF1000001;
                        p1.EffectID = 9005;
                        MapClient.FromActorPC(((ActorPC)i)).netIO.SendPacket(p1);                        
                    }
                }
            }
            war.Score.Clear();
            war.Started = false;
        }

        void SendResult(uint mapID, bool win)
        {
            SagaDB.ODWar.ODWar war = ODWarFactory.Instance.Items[mapID];
            Map map = MapManager.Instance.GetMap(mapID);

            foreach (uint i in war.Score.Keys)
            {
                Actor actor = map.GetActor(i);
                if (actor == null)
                    continue;
                if (actor.type != ActorType.PC)
                    continue;

                uint score = (uint)war.Score[i];
                ActorPC pc = (ActorPC)actor;
                if (!pc.Online)
                    continue;
                if (score > 3000)
                    score = 3000;

                if (pc.WRPRanking <= 10)
                    score = (uint)(score * 1.5f);
                if (!win)
                    score = (uint)(score * 0.75f);
                if (win)
                {
                    if (score < 200)
                        score = 200;
                }
                uint exp = (uint)(score * 0.6f);

                pc.CP += score;
                //ExperienceManager.Instance.ApplyExp(pc, exp, exp, 1f);

                Packets.Server.SSMG_ODWAR_RESULT p = new SagaMap.Packets.Server.SSMG_ODWAR_RESULT();
                p.Win = win;
                p.EXP = exp;
                p.JEXP = exp;
                p.CP = score;
                MapClient.FromActorPC(pc).netIO.SendPacket(p);
            }
        }
    }
}
