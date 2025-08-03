using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Npc;
using SagaDB.Quests;
using SagaDB.Party;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        ActorPC partyPartner;

        public void OnPartyRoll(Packets.Client.CSMG_PARTY_ROLL p)
        {
            if (Character.Party == null) return;
            if (Character.Party.Leader != this.Character) return;

            if(p.status == 1)
            {
                Character.Party.Roll = 0;
                foreach (var item in Character.Party.Members.Values)
                {
                    if (item.Online)
                        MapClient.FromActorPC(item).SendRollInfo(item);
                }
            }
            if (p.status == 0)
            {
                Character.Party.Roll = 1;
                foreach (var item in Character.Party.Members.Values)
                {
                    if (item.Online)
                        MapClient.FromActorPC(item).SendRollInfo(item);
                }
            }
        }
        public void SendRollInfo(ActorPC pc)
        {
            if (this.Character.Party == null) return;
            if (this.Character.Party.IsMember(pc))
            {
                if (pc.Online)
                {
                    Packets.Server.SSMG_PARTY_ROLL p2 = new SagaMap.Packets.Server.SSMG_PARTY_ROLL();
                    byte roll = 0;
                    if (pc.Party.Roll == 0) roll = 1;
                    p2.status = roll;
                    netIO.SendPacket(p2);
                }
            }
        }
        public void OnPartyName(Packets.Client.CSMG_PARTY_NAME p)
        {
            if (this.Character.Party == null) return;
            if (p.Name == "") return;
            if (this.Character.Party.Leader != this.Character) return;
            this.Character.Party.Name = p.Name;
            PartyManager.Instance.UpdatePartyName(this.Character.Party);
        }

        public void OnPartyKick(Packets.Client.CSMG_PARTY_KICK p)
        {
            if (this.Character.Party == null)
                return;
            if (this.Character.Party.Leader != this.Character)
                return;
            Packets.Server.SSMG_PARTY_KICK p1 = new SagaMap.Packets.Server.SSMG_PARTY_KICK();
            if (this.Character.Party.IsMember(p.CharID))
            {
                PartyManager.Instance.DeleteMember(Character.Party, p.CharID, Packets.Server.SSMG_PARTY_DELETE.Result.KICKED);
                p1.Result = 0;
            }
            else
                p1.Result = -1; //指定プレイヤーが存在しません
            this.netIO.SendPacket(p1);
        }

        public void OnPartyQuit(Packets.Client.CSMG_PARTY_QUIT p)
        {
            Packets.Server.SSMG_PARTY_QUIT p1 = new SagaMap.Packets.Server.SSMG_PARTY_QUIT();
            if (this.Character.Party == null)
                p1.Result = -1; //パーティーに所属していません
            else
            {
                if (this.Character != this.Character.Party.Leader)
                    PartyManager.Instance.DeleteMember(this.Character.Party, this.Character.CharID, SagaMap.Packets.Server.SSMG_PARTY_DELETE.Result.QUIT);
                else
                    PartyManager.Instance.PartyDismiss(this.Character.Party);
            }
            this.netIO.SendPacket(p1);
           
        }

        public void OnPartyInviteAnswer(Packets.Client.CSMG_PARTY_INVITE_ANSWER p)
        {
            if (partyPartner == null) return;
            if (partyPartner.CharID != p.CharID) return;
            MapClient client = MapClient.FromActorPC(partyPartner);
            if ((client.Character.Mode == PlayerMode.KNIGHT_EAST || client.Character.Mode == PlayerMode.KNIGHT_FLOWER || client.Character.Mode == PlayerMode.KNIGHT_NORTH
   || client.Character.Mode == PlayerMode.KNIGHT_ROCK || client.Character.Mode == PlayerMode.KNIGHT_SOUTH || client.Character.Mode == PlayerMode.KNIGHT_WEST)
    && (this.Character.Mode == PlayerMode.KNIGHT_EAST || this.Character.Mode == PlayerMode.KNIGHT_FLOWER || this.Character.Mode == PlayerMode.KNIGHT_NORTH
    || this.Character.Mode == PlayerMode.KNIGHT_ROCK || this.Character.Mode == PlayerMode.KNIGHT_SOUTH || this.Character.Mode == PlayerMode.KNIGHT_WEST)
            )
            {
                if (client.Character.Mode != this.Character.Mode)
                {
                    return;
                }
            }
            if (client.Character.Party != null)
            {
                if (client.Character.Party.MemberCount >= 8)
                    return;
                PartyManager.Instance.AddMember(client.Character.Party, this.Character);
                PartnerTalking(Character.Partner, TALK_EVENT.JOINPARTY, 100, 0);
            }
            else
            {
                Party party = PartyManager.Instance.CreateParty(partyPartner);
                PartyManager.Instance.AddMember(party, this.Character);
                PartnerTalking(partyPartner.Partner, TALK_EVENT.JOINPARTY, 100, 0);
                PartnerTalking(client.Character.Partner, TALK_EVENT.JOINPARTY, 100, 0);
            }
        }

        public void OnPartyInvite(Packets.Client.CSMG_PARTY_INVITE p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            int result = CheckPartyInvite(client);
            Packets.Server.SSMG_PARTY_INVITE_RESULT p1 = new SagaMap.Packets.Server.SSMG_PARTY_INVITE_RESULT();
            p1.InviteResult = result;
            if (result >= 0)
            {
                Packets.Server.SSMG_PARTY_INVITE p2 = new SagaMap.Packets.Server.SSMG_PARTY_INVITE();
                p2.CharID = this.Character.CharID;
                p2.Name = this.Character.Name;
                client.partyPartner = this.Character;
                client.netIO.SendPacket(p2);
            }
            this.netIO.SendPacket(p1);
        }

        private int CheckPartyInvite(MapClient client)
        {
            if (client == null)
                return -2;  //プレイヤーが存在しません
            if (client.scriptThread != null || client.trading)
                return -3; //相手がパーティに誘えない状態になりました
            if (client.Character.Party != null)
            {
                if (this.Character.Party != null)
                {
                    if (this.Character.Party.IsMember(client.Character.CharID))
                        return -11; //既にパーティーが存在しています
                }
                return -10; //既にパーティーに所属しています
            }
            if (this.Character.Party != null)
            {
                if (this.Character.Party.MemberCount == 8)
                    return -12; //パーティー人数が限界を超えてます
            }
            return 0;
        }

        public void SendPartyInfo()
        {
            if (this.Character.Party == null)
                return;
            Packets.Server.SSMG_PARTY_INFO p = new SagaMap.Packets.Server.SSMG_PARTY_INFO();
            p.Party(this.Character.Party, this.Character);
            Packets.Server.SSMG_PARTY_NAME p1 = new SagaMap.Packets.Server.SSMG_PARTY_NAME();
            p1.Party(this.Character.Party, this.Character);
            this.netIO.SendPacket(p);
            this.netIO.SendPacket(p1);
            SendPartyMember();
        }

        public void SendPartyMeDelete(Packets.Server.SSMG_PARTY_DELETE.Result reason)
        {
            Packets.Server.SSMG_PARTY_DELETE p = new SagaMap.Packets.Server.SSMG_PARTY_DELETE();
            p.PartyID = this.Character.Party.ID;
            p.PartyName = this.Character.Party.Name;
            p.Reason = reason;
            this.netIO.SendPacket(p);
        }

        public void SendPartyMemberDelete(uint pc)
        {
            Packets.Server.SSMG_PARTY_MEMBER p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER();
            p.PartyIndex = -1;
            p.CharID = pc;
            p.CharName = "";
            this.netIO.SendPacket(p);
        }

        public void SendPartyMemberPosition(ActorPC pc)
        {
            if (this.Character.Party == null) return;
            if (this.Character.Party.IsMember(pc))
            {
                if (pc.Online)
                {
                    Packets.Server.SSMG_PARTY_MEMBER_POSITION p1 = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_POSITION();
                    p1.PartyIndex = (byte)this.Character.Party.IndexOf(pc);
                    p1.CharID = pc.CharID;
                    uint mapid = pc.MapID;
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                    if (map.returnori)
                        mapid = map.OriID;
                    p1.MapID = mapid;
                    p1.X = Global.PosX16to8(pc.X, Manager.MapManager.Instance.GetMap(pc.MapID).Width);
                    p1.Y = Global.PosY16to8(pc.Y, Manager.MapManager.Instance.GetMap(pc.MapID).Height);
                    this.netIO.SendPacket(p1);
                }
            }
        }

        public void SendPartyMemberDeungeonPosition(ActorPC pc)
        {
            if (this.Character.Party == null) return;
            if (this.Character.Party.IsMember(pc))
            {
                if (this.map.IsDungeon)
                {
                    SagaMap.Map map = MapManager.Instance.GetMap(pc.MapID);
                    if (map.IsDungeon)
                    {
                        Packets.Server.SSMG_PARTY_MEMBER_DUNGEON_POSITION p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_DUNGEON_POSITION();
                        p.CharID = pc.CharID;
                        p.MapID = map.ID;
                        p.X = map.DungeonMap.X;
                        p.Y = map.DungeonMap.Y;
                        p.Dir = map.DungeonMap.Dir;
                        this.netIO.SendPacket(p);
                    }
                }
            }
        }

        public void SendPartyMemberDetail(ActorPC pc)
        {
            if (this.Character.Party == null) return;
            if (this.Character.Party.IsMember(pc))
            {
                if (pc.Online)
                {
                    Packets.Server.SSMG_PARTY_MEMBER_DETAIL p2 = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_DETAIL();
                    p2.PartyIndex = (byte)this.Character.Party.IndexOf(pc);
                    p2.CharID = pc.CharID;
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        p2.Form = 0;
                    }
                    p2.Job = pc.Job;
                    p2.Level = pc.Level;
                    p2.JobLevel = pc.CurrentJobLevel;
                    this.netIO.SendPacket(p2);
                }
            }
        }

        public void SendPartyMemberState(ActorPC pc)
        {
            if (this.Character.Party == null) return;
            if (this.Character.Party.IsMember(pc))
            {
                byte i = (byte)this.Character.Party.IndexOf(pc);
                Packets.Server.SSMG_PARTY_MEMBER_STATE p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_STATE();
                p.PartyIndex = i;
                p.CharID = pc.CharID;
                p.Online = pc.Online;
                this.netIO.SendPacket(p);
            }
        }

        public void SendPartyMemberHPMPSP(ActorPC pc)
        {
            if (this.Character.Party == null) return;
            if (this.Character.Party.IsMember(pc))
            {
                byte i = (byte)this.Character.Party.IndexOf(pc);
                Packets.Server.SSMG_PARTY_MEMBER_HPMPSP p3 = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_HPMPSP();
                p3.PartyIndex = i;
                p3.CharID = pc.CharID;
                p3.HP = pc.HP;
                p3.MaxHP = pc.MaxHP;
                p3.MP = pc.MP;
                p3.MaxMP = pc.MaxMP;
                p3.SP = pc.SP;
                p3.MaxSP = pc.MaxSP;
                this.netIO.SendPacket(p3);
            }
        }

        public void SendPartyMemberInfo(ActorPC pc)
        {
            if (this.Character.Party == null) return;            
            if (this.Character.Party.IsMember(pc))
            {
                if (pc.Online)
                {
                    try
                    {
                        byte i = (byte)this.Character.Party.IndexOf(pc);
                        Packets.Server.SSMG_PARTY_MEMBER_STATE p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_STATE();
                        p.PartyIndex = i;
                        p.CharID = pc.CharID;
                        p.Online = pc.Online;
                        this.netIO.SendPacket(p);
                        Packets.Server.SSMG_PARTY_MEMBER_POSITION p1 = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_POSITION();
                        p1.PartyIndex = i;
                        p1.CharID = pc.CharID;
                        p1.MapID = pc.MapID;
                        p1.X = Global.PosX16to8(pc.X, Manager.MapManager.Instance.GetMap(pc.MapID).Width);
                        p1.Y = Global.PosY16to8(pc.Y, Manager.MapManager.Instance.GetMap(pc.MapID).Height);
                        this.netIO.SendPacket(p1);
                        Packets.Server.SSMG_PARTY_MEMBER_DETAIL p2 = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_DETAIL();
                        p2.PartyIndex = i;
                        p2.CharID = pc.CharID;
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            p2.Form = 0;
                        }
                        p2.Job = pc.Job;
                        p2.Level = pc.Level;
                        p2.JobLevel = pc.CurrentJobLevel;
                        this.netIO.SendPacket(p2);
                        Packets.Server.SSMG_PARTY_MEMBER_HPMPSP p3 = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_HPMPSP();
                        p3.PartyIndex = i;
                        p3.CharID = pc.CharID;
                        p3.HP = pc.HP;
                        p3.MaxHP = pc.MaxHP;
                        p3.MP = pc.MP;
                        p3.MaxMP = pc.MaxMP;
                        p3.SP = pc.SP;
                        p3.MaxSP = pc.MaxSP;
                        this.netIO.SendPacket(p3);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
            }
        }

        void SendPartyMember()
        {
            if (this.Character.Party == null)
                return;
            foreach (byte i in this.Character.Party.Members.Keys)
            {
                Packets.Server.SSMG_PARTY_MEMBER p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER();
                p.PartyIndex = i;
                p.CharID = this.Character.Party[i].CharID;
                p.CharName = this.Character.Party[i].Name;
                p.Leader = (this.Character.Party.Leader == this.Character.Party[i]);
                this.netIO.SendPacket(p);
            }
            SagaDB.Party.Party party = this.Character.Party;
            foreach (byte i in party.Members.Keys)
            {
                SendPartyMemberInfo(party[i]);
            }
        }
    }
}
