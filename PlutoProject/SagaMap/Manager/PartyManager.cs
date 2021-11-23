using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Party;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.Item;
using SagaDB.Skill;
using SagaDB.Quests;
using SagaDB.Npc;

using SagaMap;
using SagaMap.Network.Client;
using SagaMap.Manager;
using SagaMap.Skill;

namespace SagaMap.Manager
{
    public class PartyManager:Singleton<PartyManager>
    {
        Dictionary<uint, Party> partys = new Dictionary<uint, Party>();
        public PartyManager()
        {

        }

        public Party GetParty(uint pattern)
        {
            Party res;
            if (pattern == 0) return null;
            if (partys.ContainsKey(pattern))
                res = partys[pattern];
            else
            {
                res = new Party();
                res.ID = pattern;
                partys.Add(pattern, res);
                if (res.Name == null)
                {
                    res = MapServer.charDB.GetParty(res.ID);
                    if (res == null)
                    {
                        Logger.ShowDebug("Party with ID:" + pattern + " not found!", Logger.defaultlogger);
                        partys.Remove(pattern);
                        return null;
                    }
                    partys[res.ID] = res;
                }
                byte[] index = res.Members.Keys.ToArray();
                foreach (byte i in index)
                {
                    MapClient client = MapClientManager.Instance.FindClient(res.Members[i]);
                    if (client != null)
                    {
                        res.Members[i] = client.Character;
                        if (res.Leader.CharID == client.Character.CharID)
                            res.Leader = client.Character;
                    }
                }
            }
            return res;
        }

        public Party GetParty(Party pattern)
        {
            Party res;
            if (pattern == null) return null;
            if (pattern.ID == 0) return null;
            if (partys.ContainsKey(pattern.ID))
                res = partys[pattern.ID];
            else
            {
                res = pattern;
                partys.Add(pattern.ID, pattern);
                if (res.Name == null)
                {
                    res = MapServer.charDB.GetParty(res.ID);
                    if (res == null)
                    {
                        Logger.ShowDebug("Party with ID:" + pattern.ID + " not found!", Logger.defaultlogger);
                        partys.Remove(pattern.ID);
                        return null;
                    }
                    partys[res.ID] = res;
                }
                else
                {
                    res = pattern;
                }
                byte[] index = res.Members.Keys.ToArray();
                foreach (byte i in index)
                {
                    MapClient client = MapClientManager.Instance.FindClient(res.Members[i]);
                    if (client != null)
                    {
                        res.Members[i] = client.Character;
                        if (res.Leader.CharID == client.Character.CharID)
                            res.Leader = client.Character;
                    }
                }
            }
            return res;
        }

        public void PlayerOnline(Party party, ActorPC pc)
        {
            if (party == null)
                return;
            if (!party.IsMember(pc))
            {
                pc.Party = null;
                return;
            }
            party.MemberOnline(pc);
            foreach (ActorPC i in party.Members.Values)
            {
                if (i == pc || !i.Online) continue;
                MapClient.FromActorPC(i).SendPartyMemberInfo(pc);
            }
        }

        public void PlayerOffline(Party party, ActorPC pc)
        {
            if (party == null)
                return;
            pc.Online = false;
            foreach (ActorPC i in party.Members.Values)
            {
                if (i == pc || !i.Online) continue;
                MapClient.FromActorPC(i).SendPartyMemberState(pc);
            }
        }

        public void UpdatePartyInfo(Party party)
        {
            if (party == null)
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                if (!i.Online) continue;
                MapClient.FromActorPC(i).SendPartyInfo();
            }
        }

        public void UpdateMemberPosition(Party party, ActorPC pc)
        {
            if (party == null)
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                if (i == pc || !i.Online) continue;
                MapClient.FromActorPC(i).SendPartyMemberPosition(pc);
            }
        }

        public void UpdateMemberDungeonPosition(Party party, ActorPC pc)
        {
            if (party == null)
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                if (i == pc || !i.Online) continue;
                MapClient.FromActorPC(i).SendPartyMemberDeungeonPosition(pc);
                MapClient.FromActorPC(pc).SendPartyMemberDeungeonPosition(i);
            }
        }

        public void UpdateMemberHPMPSP(Party party, ActorPC pc)
        {
            if (party == null)
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                if (i == pc || !i.Online || i.MapID != pc.MapID) continue;
                MapClient.FromActorPC(i).SendPartyMemberHPMPSP(pc);
            }
        }

        public void UpdatePartyName(Party party)
        {
            if (party == null)
                return;
            MapServer.charDB.SaveParty(party);
            foreach (ActorPC i in party.Members.Values)
            {
                if (!i.Online) continue;
                MapClient.FromActorPC(i).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, null, i, true);
            }
        }

        public void PartyChat(Party party, ActorPC pc, string content)
        {
            if (party == null)
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                if (!i.Online) continue;
                MapClient.FromActorPC(i).SendChatParty(pc.Name, content);
            }
        }

        public Party CreateParty(ActorPC pc)
        {
            Party party = new Party();
            party.Name = LocalManager.Instance.Strings.PARTY_NEW_NAME;
            party.Leader = pc;
            pc.Party = party;
            
            MapServer.charDB.NewParty(party);
            AddMember(party, pc);
            partys.Add(party.ID, party);
            if (pc.DungeonID != 0)
            {
                Dungeon.DungeonFactory.Instance.GetDungeon(pc.DungeonID).Destory(SagaMap.Dungeon.DestroyType.PartyMemberChange);
            }
            return party;
        }

        public void AddMember(Party party, ActorPC pc)
        {
            if (party == null)
                return;
            if (party.IsMember(pc))
                return;
            if (party.MemberCount >= party.MaxMember)
                return;
            party.NewMember(pc);
            pc.Party = party;
            if (pc.Online)
                MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, null, pc, true);
            MapServer.charDB.SaveParty(party);
            UpdatePartyInfo(party);
            if (pc.DungeonID != 0)
            {
                Dungeon.DungeonFactory.Instance.GetDungeon(pc.DungeonID).Destory(SagaMap.Dungeon.DestroyType.PartyMemberChange);
            }
            foreach (ActorPC i in party.Members.Values)
            {
                PC.StatusFactory.Instance.CalcStatus(i);
                MapClient.FromActorPC(i).SendPlayerInfo();
                MapClient.FromActorPC(i).SendRollInfo(i);
            }
        }

        public void DeleteMember(Party party, uint pc, Packets.Server.SSMG_PARTY_DELETE.Result reason)
        {
            if (party == null)
                return;
            if (!party.IsMember(pc))
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                if (i.CharID == pc)
                {                    
                    if (i.Online)
                    {
                        MapClient.FromActorPC(i).SendPartyMeDelete(reason);
                        i.Party = null;                    
                        MapClient.FromActorPC(i).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, null, i, false);
                        MapClient.FromActorPC(i).PartnerTalking(i.Partner, MapClient.TALK_EVENT.LEAVEPARTY, 100, 0);
                        if (i.TInt["副本复活标记"] == 1)
                        {
                            MapInfo info = MapInfoFactory.Instance.MapInfo[10054000];
                            MapManager.Instance.GetMap(party.SearchMemeber(pc).MapID).SendActorToMap(i, 10054000, Global.PosX8to16(197, info.width),
                                Global.PosY8to16(165, info.height));
                        }
                        PC.StatusFactory.Instance.CalcStatus(i);
                        MapClient.FromActorPC(i).SendPlayerInfo();
                    }
                    i.Party = null;
                    if (i.DungeonID != 0)
                    {
                        Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Destory(SagaMap.Dungeon.DestroyType.PartyMemberChange);
                    }
                }
                else
                {
                    if (i.Online)
                        MapClient.FromActorPC(i).SendPartyMemberDelete(pc);
                    if (i.DungeonID != 0)
                    {
                        foreach (Dungeon.DungeonMap dmap in Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Maps)
                        {
                            if (dmap.Map.ID == party.SearchMemeber(pc).MapID)
                            {
                                MapManager.Instance.GetMap(party.SearchMemeber(pc).MapID).SendActorToMap(party.SearchMemeber(pc), Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Start.Map.ClientExitMap, Global.PosX8to16(Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Start.Map.ClientExitX, MapManager.Instance.GetMap(Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Start.Map.ClientExitMap).Width), Global.PosY8to16(Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Start.Map.ClientExitY, MapManager.Instance.GetMap(Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Start.Map.ClientExitMap).Height));
                            }
                        }
                    }
                }
            }
            party.DeleteMemeber(pc);
            MapServer.charDB.SaveParty(party);
            foreach (ActorPC i in party.Members.Values)
            {
                PC.StatusFactory.Instance.CalcStatus(i);
                MapClient.FromActorPC(i).SendPlayerInfo();
            }
            if (party.Members.Count == 1)
            {
                PartyDismiss(party);
            }                  
        }

        public void PartyDismiss(Party party)
        {
            if (party == null)
                return;
            foreach (ActorPC i in party.Members.Values)
            {
                try
                {
                    if (i.DungeonID != 0)
                    {
                        Dungeon.DungeonFactory.Instance.GetDungeon(i.DungeonID).Destory(SagaMap.Dungeon.DestroyType.PartyDismiss);
                    }
                    if (!i.Online) continue;
                    MapClient.FromActorPC(i).SendPartyMeDelete(SagaMap.Packets.Server.SSMG_PARTY_DELETE.Result.DISMISSED);
                    i.Party = null;
                    MapClient.FromActorPC(i).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, null, i, false);
                    if (i.TInt["副本复活标记"] == 1)
                    {
                        MapInfo info = MapInfoFactory.Instance.MapInfo[10054000];
                        MapManager.Instance.GetMap(i.MapID).SendActorToMap(i, 10054000, Global.PosX8to16(153, info.width),
                            Global.PosY8to16(149, info.height));
                    }
                    MapClient.FromActorPC(i).PartnerTalking(i.Partner, MapClient.TALK_EVENT.LEAVEPARTY, 100, 0);
                }
                catch
                {
                }
            }
            MapServer.charDB.DeleteParty(party);
            if (partys.ContainsKey(party.ID))
                partys.Remove(party.ID);
        }
    }
}
