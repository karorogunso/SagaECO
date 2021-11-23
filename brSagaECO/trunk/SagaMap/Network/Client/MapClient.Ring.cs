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
        public ActorPC ringPartner;

        public void OnRingEmblemUpload(Packets.Client.CSMG_RING_EMBLEM_UPLOAD p)
        {
            Packets.Server.SSMG_RING_EMBLEM_UPLOAD_RESULT p1 = new SagaMap.Packets.Server.SSMG_RING_EMBLEM_UPLOAD_RESULT();
            if (this.Character.Ring == null)
                return;

            if (this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(SagaDB.Ring.RingRight.RingMaster) ||
                this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(SagaDB.Ring.RingRight.Ring2ndMaster))
            {
                byte[] data = p.Data;
                if (data[0] == 0x89)
                {
                    if (this.Character.Ring.Fame >= Configuration.Instance.RingFameNeededForEmblem)
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_RING_EMBLEM_UPLOAD_RESULT.Results.OK;
                        MapServer.charDB.RingEmblemUpdate(this.Character.Ring, p.Data);
                    }
                    else
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_RING_EMBLEM_UPLOAD_RESULT.Results.FAME_NOT_ENOUGH;
                    }
                }
                else
                    p1.Result = SagaMap.Packets.Server.SSMG_RING_EMBLEM_UPLOAD_RESULT.Results.WRONG_FORMAT;
            }
            this.netIO.SendPacket(p1);
        }

        public void OnChatRing(Packets.Client.CSMG_CHAT_RING p)
        {
            if (this.Character.Ring == null)
                return;
            RingManager.Instance.RingChat(this.Character.Ring, this.Character, p.Content);

            //TODO:ECOE用
            //Logger.ShowChat("[R]" + this.Character.Name + " :" + p.Content, null);
        }

        public void OnRingRightSet(Packets.Client.CSMG_RING_RIGHT_SET p)
        {
            if (this.Character.Ring == null)
                return;
            if (this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(SagaDB.Ring.RingRight.RingMaster) ||
                this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(SagaDB.Ring.RingRight.Ring2ndMaster))
            {
                RingManager.Instance.SetMemberRight(this.Character.Ring, p.CharID, p.Right);
            }
        }

        public void OnRingKick(Packets.Client.CSMG_RING_KICK p)
        {
            if (this.Character.Ring == null)
                return;
            if (this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(SagaDB.Ring.RingRight.KickRight))
            {
                RingManager.Instance.DeleteMember(this.Character.Ring, this.Character.Ring.GetMember(p.CharID), SagaMap.Packets.Server.SSMG_RING_QUIT.Reasons.KICK);
            }
        }

        public void OnRingQuit(Packets.Client.CSMG_RING_QUIT p)
        {
            Packets.Server.SSMG_RING_QUIT_RESULT p1 = new SagaMap.Packets.Server.SSMG_RING_QUIT_RESULT();
            if (this.Character.Ring == null)
                p1.Result = -1;
            else
            {
                if (this.Character != this.Character.Ring.Leader)
                    RingManager.Instance.DeleteMember(this.Character.Ring, this.Character, SagaMap.Packets.Server.SSMG_RING_QUIT.Reasons.LEAVE);
                else
                    RingManager.Instance.RingDismiss(this.Character.Ring);
            }
            this.netIO.SendPacket(p1);
        }

        public void OnRingInviteAnswer(Packets.Client.CSMG_RING_INVITE_ANSWER p, bool accepted)
        {
            if (accepted)
            {
                Packets.Server.SSMG_RING_INVITE_ANSWER_RESULT p1 = new SagaMap.Packets.Server.SSMG_RING_INVITE_ANSWER_RESULT();
                int result = CheckRingInvitationReply();
                if (result==0)
                    RingManager.Instance.AddMember(this.ringPartner.Ring, this.Character);
                p1.Result = result;
                this.netIO.SendPacket(p1);
            }
            this.ringPartner = null;            
        }

        int CheckRingInvitationReply()
        {
            if (ringPartner == null)
                return -7; //誘った相手が存在していません
            if (this.ringPartner.Ring == null)
                return -11; //RingErr
            else
            {
                int index = this.ringPartner.Ring.IndexOf(this.ringPartner);
                if (!this.ringPartner.Ring.Rights[index].Test(SagaDB.Ring.RingRight.AddRight))
                    return -6; //誘った相手に招待権限がありません
            }
            if (this.Character.Ring != null)
                return -9; //既にリングに入っています
            if (this.ringPartner.Ring.MemberCount >= this.ringPartner.Ring.MaxMemberCount)
                return -10; // 満員で入れません
            return 0;
        }

        public void OnRingInvite(Packets.Client.CSMG_RING_INVITE p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            int result = CheckRingInvitation(client);
            Packets.Server.SSMG_RING_INVITE_RESULT p1 = new SagaMap.Packets.Server.SSMG_RING_INVITE_RESULT();
            p1.Result = result;
            this.netIO.SendPacket(p1);
            if (result == 0)
            {
                client.ringPartner = this.Character;
                Packets.Server.SSMG_RING_INVITE p2 = new SagaMap.Packets.Server.SSMG_RING_INVITE();
                p2.CharID = this.Character.CharID;
                p2.CharName = this.Character.Name;
                p2.RingName = this.Character.Ring.Name;
                client.netIO.SendPacket(p2);
            }
        }

        int CheckRingInvitation (MapClient client)
        {
            if (client == null)
                return -1; //相手が見つかりません
            if (client.Character.Ring != null)
                return -4;//相手はリングに加入済みです
            if (this.Character.Ring == null)
                return -5; //リングを組んでいないので誘えません 
            else
            {
                int index = this.Character.Ring.IndexOf(this.Character);
                if (!this.Character.Ring.Rights[index].Test(SagaDB.Ring.RingRight.AddRight))
                    return -6; //招待権限を持っていません
            }
            return 0;
        }

        public void SendRingMember()
        {
            if (this.Character.Ring == null)
                return;
            foreach (ActorPC i in this.Character.Ring.Members.Values)
            {
                Packets.Server.SSMG_RING_MEMBER_INFO p = new SagaMap.Packets.Server.SSMG_RING_MEMBER_INFO();
                p.Member(i, this.Character.Ring);
                this.netIO.SendPacket(p);
                SendRingMemberInfo(i);
            }
        }

        public void SendRingInfo(SagaMap.Packets.Server.SSMG_RING_INFO.Reason reason)
        {
            if (this.Character.Ring == null)
                return;
            if (reason != SagaMap.Packets.Server.SSMG_RING_INFO.Reason.UPDATED)
            {
                Packets.Server.SSMG_RING_INFO p = new SagaMap.Packets.Server.SSMG_RING_INFO();
                Packets.Server.SSMG_RING_NAME p1 = new SagaMap.Packets.Server.SSMG_RING_NAME();
                p.Ring(this.Character.Ring, reason);
                p1.Player = this.Character;
                this.netIO.SendPacket(p);
                this.netIO.SendPacket(p1);
                SendRingMember();
            }
            else
            {
                Packets.Server.SSMG_RING_INFO_UPDATE p = new SagaMap.Packets.Server.SSMG_RING_INFO_UPDATE();
                p.RingID = this.Character.Ring.ID;
                p.Fame = this.Character.Ring.Fame;
                p.CurrentMember = (byte)this.Character.Ring.MemberCount;
                p.MaxMember = (byte)this.Character.Ring.MaxMemberCount;
                this.netIO.SendPacket(p);
            }
        }

        public void SendRingMemberInfo(ActorPC pc)
        {
            if (this.Character.Ring == null) 
                return;
            if (this.Character.Ring.IsMember(pc))
            {
                if (pc.Online)
                {
                    uint i = (uint)this.Character.Ring.IndexOf(pc);
                    Packets.Server.SSMG_PARTY_MEMBER_STATE p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_STATE();
                    p.PartyIndex = i;
                    p.CharID = pc.CharID;
                    p.Online = pc.Online;
                    this.netIO.SendPacket(p);
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
                }
            }
        }

        public void SendRingMemberState(ActorPC pc)
        {
            if (this.Character.Ring == null) return;
            if (this.Character.Ring.IsMember(pc))
            {
                int i = this.Character.Ring.IndexOf(pc);
                Packets.Server.SSMG_PARTY_MEMBER_STATE p = new SagaMap.Packets.Server.SSMG_PARTY_MEMBER_STATE();
                p.PartyIndex = (uint)i;
                p.CharID = pc.CharID;
                p.Online = pc.Online;
                this.netIO.SendPacket(p);
            }
        }

        public void SendChatRing(string name, string content)
        {
            Packets.Server.SSMG_CHAT_RING p = new SagaMap.Packets.Server.SSMG_CHAT_RING();
            p.Sender = name;
            p.Content = content;
            this.netIO.SendPacket(p);
        }

        public void SendRingMeDelete(Packets.Server.SSMG_RING_QUIT.Reasons reason)
        {
            Packets.Server.SSMG_RING_QUIT p = new SagaMap.Packets.Server.SSMG_RING_QUIT();
            p.RingID = this.Character.Ring.ID;
            p.Reason = reason;
            this.netIO.SendPacket(p);
        }

        public void SendRingMemberDelete(ActorPC pc)
        {
            Packets.Server.SSMG_RING_MEMBER_INFO p = new SagaMap.Packets.Server.SSMG_RING_MEMBER_INFO();
            p.Member(pc, null);
            this.netIO.SendPacket(p);
        }
    }
}
