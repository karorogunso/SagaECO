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
        public uint bbsMinContent;

        public void OnBBSRequestPage(Packets.Client.CSMG_COMMUNITY_BBS_REQUEST_PAGE p)
        {
            this.bbsCurrentPage = p.Page;
            SendBBSPage();
        }

        public void SendBBSPage()
        {
            Packets.Server.SSMG_COMMUNITY_BBS_PAGE_INFO p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_PAGE_INFO();
            p1.Posts = MapServer.charDB.GetBBSPage(this.bbsID, this.bbsCurrentPage);
            this.netIO.SendPacket(p1);
        }

        public void OnBBSPost(Packets.Client.CSMG_COMMUNITY_BBS_POST p)
        {
            Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_POST_RESULT();
            int result = CheckBBSPost(p.Title, p.Content);
            if (result>=0)
                this.Character.Gold -= (int)this.bbsCost;
            this.netIO.SendPacket(p1);
            SendBBSPage();
        }

        private int CheckBBSPost(string title, string content)
        {
            if (this.Character.Gold <= this.bbsCost)
                return -2; //お金が足りません
            if (content.Length < this.bbsMinContent)
                return -3; //投稿内容の文字数が足りません
            if (!MapServer.charDB.BBSNewPost(this.Character, this.bbsID, title, content))
                return -1; //投稿に失敗しました
            return 0; //投稿しました
            // return -4; //投稿回数が制限に達したため失敗しました
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
                int result=0;
                Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
                if (this.Character.Mode != target.Character.Mode)
                    return;
                if (!p.Accept)
                    result = -2; //パーティー参加要請を断られました
                if (result >= 0)
                {
                    if (this.Character.Party == null)
                    {
                        Party party = PartyManager.Instance.CreateParty(this.chara);
                        PartyManager.Instance.AddMember(party, partyPartner);
                    }
                    else if (this.Character.Party.MemberCount >= 8)
                        result = -3; //要請をしたパーティーは満員です
                }
                if (result>=0)
                    PartyManager.Instance.AddMember(this.Character.Party, partyPartner);
                p1.Result = result;
                p1.CharID = this.chara.CharID;
                target.netIO.SendPacket(p1);
            }
        }

        public void OnRecruitJoin(Packets.Client.CSMG_COMMUNITY_RECRUIT_JOIN p)
        {
            MapClient target = MapClientManager.Instance.FindClient(p.CharID);
            int result = CheckRecuitJoin(target);
            Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES p1 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_JOIN_RES();
            p1.Result = result;
            p1.CharID = p.CharID;
            this.netIO.SendPacket(p1);
            if (result>=0)
            {
                this.partyPartner = target.Character;
                target.partyPartner = this.chara;
                Packets.Server.SSMG_COMMUNITY_RECRUIT_REQUEST p2 = new SagaMap.Packets.Server.SSMG_COMMUNITY_RECRUIT_REQUEST();
                p2.CharID = this.chara.CharID;
                p2.CharName = this.chara.Name;
                target.netIO.SendPacket(p2);
            }
        }

        private int CheckRecuitJoin(MapClient target)
        {
            // return -1; //パーティーサーバーとの接続に失敗しました
            // return -2; //パーティー参加要請を断られました
            if (target == null)
                return -4; //パーティー要請をした相手がオフライン中です
            if (target.Character.Party != null)
                if (target.Character.Party.MemberCount >= 8)
                return -3; //要請をしたパーティーは満員です
            if (this.Character.Party != null)
                return -5; //パーティー参加中に要請はできません
            if (target.Character.CharID == this.Character.CharID)
                return -6; //自分にパーティー申請はできません
            bool exist = false;
            var recruitlist = RecruitmentManager.Instance.GetRecruitments(RecruitmentType.Party);
            for (int i = 0; i < recruitlist.Count; i++)
                if (recruitlist[i].Creator.Name == target.chara.Name)
                    exist = true;
            if (!exist)
                return -7; //募集コメントの登録が消えています
            return 0; //参加要請中です
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
