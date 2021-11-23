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
        public bool bbsClose;
        public int bbsCurrentPage;
        public uint bbsID;
        public uint bbsCost;

        public void OnBBSRequestPage(Packets.Client.CSMG_COMMUNITY_BBS_REQUEST_PAGE p)
        {
            Packets.Server.SSMG_COMMUNITY_BBS_PAGE_INFO p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_PAGE_INFO();
            this.bbsCurrentPage = p.Page;
            p1.Posts = MapServer.charDB.GetBBSPage(this.bbsID, this.bbsCurrentPage);
            this.netIO.SendPacket(p1);
        }

        public void OnBBSPost(Packets.Client.CSMG_COMMUNITY_BBS_POST p)
        {
            Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT();
            if (this.Character.Gold >= this.bbsCost)
            {
                if (MapServer.charDB.BBSNewPost(this.Character, this.bbsID, p.Title, p.Content))
                {
                    p1.Result = SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT.Results.SUCCEED;
                    this.Character.Gold -= (int)this.bbsCost;                    
                }
                else
                    p1.Result = SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT.Results.FAILED;
            }
            else
                p1.Result = SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT.Results.NOT_ENOUGH_MONEY;
            this.netIO.SendPacket(p1);

            Packets.Server.SSMG_COMMUNITY_BBS_PAGE_INFO p2 = new SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_PAGE_INFO();
            p2.Posts = MapServer.charDB.GetBBSPage(this.bbsID, this.bbsCurrentPage);
            this.netIO.SendPacket(p2);
        }

        public void OnBBSClose(Packets.Client.CSMG_COMMUNITY_BBS_CLOSE p)
        {
            this.bbsClose = true;
        }

        public void OnRecruit(Packets.Client.CSMG_COMMUNITY_RECRUIT p)
        {
            int maxPage;
            int page = p.Page;
            List<Recruitment> res = RecruitmentManager.Instance.GetRecruitments(p.Type, page, out maxPage);
            Packets.Server.SSMG_COMMUNITY_RECRUIT p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT();
            p1.Type = p.Type;
            p1.Page = page;
            p1.MaxPage = maxPage;
            p1.Entries = res;
            this.netIO.SendPacket(p1);
        }

        public void OnRecruitRequestAns(Packets.Client.CSMG_COMMUNITY_RECRUIT_REQUEST_ANS p)
        {
            MapClient target = MapClientManager.Instance.FindClient(p.CharID);
            if (target == null || this.partyPartner == null)
                return;
            if (target.chara.CharID == this.partyPartner.CharID)
            {
                if (p.Accept)
                {
                    //Logger.ShowError("test2");
                    Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                    p1.Result = SagaMap.Packets.Server.JoinRes.OK;
                    p1.CharID = this.chara.CharID;
                    target.netIO.SendPacket(p1);
                    if ((this.Character.Mode == PlayerMode.KNIGHT_EAST || this.Character.Mode == PlayerMode.KNIGHT_FLOWER || this.Character.Mode == PlayerMode.KNIGHT_NORTH
                       || this.Character.Mode == PlayerMode.KNIGHT_ROCK || this.Character.Mode == PlayerMode.KNIGHT_SOUTH || this.Character.Mode == PlayerMode.KNIGHT_WEST)
                        && (target.Character.Mode == PlayerMode.KNIGHT_EAST || target.Character.Mode == PlayerMode.KNIGHT_FLOWER || target.Character.Mode == PlayerMode.KNIGHT_NORTH
                        || target.Character.Mode == PlayerMode.KNIGHT_ROCK || target.Character.Mode == PlayerMode.KNIGHT_SOUTH || target.Character.Mode == PlayerMode.KNIGHT_WEST)
                                )
                    {
                        if (this.Character.Mode != target.Character.Mode)
                        {
                            //Logger.ShowError("test");
                            return;
                        }
                    }
                    if (this.Character.Party != null)
                    {
                        if (this.Character.Party.MemberCount >= 8)
                            return;
                        PartyManager.Instance.AddMember(this.Character.Party, partyPartner);
                    }
                    else
                    {
                        Party party = PartyManager.Instance.CreateParty(this.chara);
                        PartyManager.Instance.AddMember(party, partyPartner);
                    }
                }
                else
                {
                    Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                    p1.Result = SagaMap.Packets.Server.JoinRes.REJECTED;
                    p1.CharID = this.chara.CharID;
                    target.netIO.SendPacket(p1);
                }
            }
        }
        public void OnRecruitJoin(Packets.Client.CSMG_COMMUNITY_RECRUIT_JOIN p)
        {
            MapClient target = MapClientManager.Instance.FindClient(p.CharID);
            if (this.Character.Party != null)
            {
                Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                p1.Result = SagaMap.Packets.Server.JoinRes.ALREADY_IN_PARTY;
                p1.CharID = p.CharID;
                this.netIO.SendPacket(p1);
                return;
            }
            
            if (target != null)
            {
                if (target.Character.CharID == this.Character.CharID)
                {
                    Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                    p1.Result = SagaMap.Packets.Server.JoinRes.SELF;
                    p1.CharID = p.CharID;
                    this.netIO.SendPacket(p1);
                    return;
                }
                if (target.Character.Party != null)
                {
                    if (target.Character.Party.MemberCount == 8)
                    {
                        Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                        p1.Result = SagaMap.Packets.Server.JoinRes.PARTY_FULL;
                        p1.CharID = p.CharID;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                }
                this.partyPartner = target.Character;
                target.partyPartner = this.chara;
                Packets.Server.SSMG_COMMUNITY_RECRUIT_REQUEST p2 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_REQUEST();
                p2.CharID = this.chara.CharID;
                p2.CharName = this.chara.Name;
                target.netIO.SendPacket(p2);
            }
            else
            {
                Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                p1.Result = SagaMap.Packets.Server.JoinRes.TARGET_OFFLINE;
                p1.CharID = p.CharID;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnRecruitCreate(Packets.Client.CSMG_COMMUNITY_RECRUIT_CREATE p)
        {
            Recruitment rec = new Recruitment();
            rec.Creator = this.Character;
            rec.Type = p.Type;
            rec.Title = p.Title;
            rec.Content = p.Content;
            Manager.RecruitmentManager.Instance.CreateRecruiment(rec);

            Packets.Server.SSMG_COMMUNITY_RECRUIT_CREATE p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_CREATE();
            this.netIO.SendPacket(p1);
        }

        public void OnRecruitDelete(Packets.Client.CSMG_COMMUNITY_RECRUIT_DELETE p)
        {
            RecruitmentManager.Instance.DeleteRecruitment(this.Character);
            Packets.Server.SSMG_COMMUNITY_RECRUIT_DELETE p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_DELETE();
            this.netIO.SendPacket(p1);
        }
    }
}
