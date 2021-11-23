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
using SagaDB.Team;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        ActorPC teamPartner;
        public void OnAbyssTeamListRequest(Packets.Client.CSMG_ABYSSTEAM_LIST_REQUEST p)
        {
        }
        public void OnAbyssTeamListClose(Packets.Client.CSMG_ABYSSTEAM_LIST_CLOSE p)
        {

        }
        public void OnAbyssTeamBreakRequest(Packets.Client.CSMG_ABYSSTEAM_BREAK_REQUEST p)
        {
            Team team = this.Character.Team;
            List<ActorPC> members = new List<ActorPC>();
            string teamName="";
            if (team != null)
            {
                teamName = team.Name;
                members = team.Members.Values.ToList();
            }

            byte Result = unchecked((byte)(CheckAbyssTeamBreakRequest(team)));

            if (Result==0)
            {
                Packets.Server.SSMG_ABYSSTEAM_BREAK p1 = new Packets.Server.SSMG_ABYSSTEAM_BREAK();
                p1.TeamName = teamName;
                MapClient member;
                for (int i =0;i<members.Count;i++)
                {
                    if (members[i].Online)
                    {
                        member = MapClient.FromActorPC(members[i]);
                        if (member != null)
                        {
                            member.netIO.SendPacket(p1);
                        }
                    }
                }
            }
            else
            {
                Packets.Server.SSMG_ABYSSTEAM_BREAK p1 = new Packets.Server.SSMG_ABYSSTEAM_BREAK();
                p1.Result = Result;
                this.netIO.SendPacket(p1);
            }
        }
        int CheckAbyssTeamBreakRequest(Team team)
        {
            if (team == null)
                return -3; //既にチームが解散されています
            try
            {
                AbyssTeamManager.Instance.TeamDismiss(team);
            }
            catch
            {

            }
            return 0; //チーム「%s」を解散しました
        }
        public void OnAbyssTeamLeaveRequest(Packets.Client.CSMG_ABYSSTEAM_LEAVE_REQUEST p)
        {
            Team team = this.Character.Team;
            string teamName = "";
            if (team != null)
                teamName = team.Name;

            byte Result = unchecked((byte)(CheckAbyssTeamLeaveRequest(team)));
            Packets.Server.SSMG_ABYSSTEAM_LEAVE p1 = new Packets.Server.SSMG_ABYSSTEAM_LEAVE();
            this.netIO.SendPacket(p1);
        }
        int CheckAbyssTeamLeaveRequest(Team team)
        {
            if (team == null)
                return -3; //既にチームが解散されています
            try
            {
                AbyssTeamManager.Instance.DeleteMember(team,this.Character.CharID);
            }
            catch
            {
            }
            return 0; //チーム「%s」を脱退しました
        }
        public void OnAbyssTeamRegistRequest(Packets.Client.CSMG_ABYSSTEAM_REGIST_REQUEST p)
        {
            byte Result = unchecked((byte)CheckAbyssTeamRegistRequest(p.LeaderID, p.Password));
            if (Result ==2)
            {
                MapClient target = MapClientManager.Instance.FindClient(p.LeaderID);
                this.teamPartner = target.Character;
                Packets.Server.SSMG_ABYSSTEAM_REGIST_APPROVAL p1 = new Packets.Server.SSMG_ABYSSTEAM_REGIST_APPROVAL();
                p1.CharID = this.Character.CharID;
                p1.Name = this.Character.Name;
                p1.Level = this.Character.Level;
                p1.Job = this.Character.Job;
                target.netIO.SendPacket(p1);
            }
            Packets.Server.SSMG_ABYSSTEAM_REGIST_APPLY p2 = new Packets.Server.SSMG_ABYSSTEAM_REGIST_APPLY();
            p2.Result = Result;
            this.netIO.SendPacket(p2);
        }
        int CheckAbyssTeamRegistRequest(uint leaderID, string pass)
        {
            Team team = AbyssTeamManager.Instance.GetTeam(leaderID);
            if (this.teamPartner != null)
                return -9; //参加申し込みの上限に達しているため、申請を行えませんでした
            if (this.Character.Team != null)
                return -8; //既にチームに参加しています
            if (team == null)
                return -7; //チームが解散されているため入れませんでした
            if (!team.Leader.Online)
                return -6; //リーダーがいないため入れませんでした
            if (!team.JobRequirements.Contains(this.Character.Job))
                return -4; //条件に合わなかったため入れませんでした
            if (pass != team.Pass)
                return -3; //パスワードが違います
            if (team.Members.Count >= team.MaxMember)
                return -2; //チームが満員のため入れませんでした
            MapClient leader = MapClientManager.Instance.FindClient(leaderID);
            if (leader.teamPartner != null)
                return -10; //申請許可を待っているユーザーは多数居たため、加入することは出来ませんでした
            return 2; //加入申請中です
        }
        public void OnAbyssTeamRegistApproval(Packets.Client.CSMG_ABYSSTEAM_REGIST_APPROVAL p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            Packets.Server.SSMG_ABYSSTEAM_REGIST_APPLY p1 = new Packets.Server.SSMG_ABYSSTEAM_REGIST_APPLY();
            bool approved = false;
            if (p.Result == 0)
                approved = true;
            byte Result = unchecked((byte)CheckAbyssTeamRegistApproval(approved));
            p1.Result = Result;
            if (Result == 1)
                p1.TeamName = this.Character.Team.Name;
            client.netIO.SendPacket(p1);
        }
        int CheckAbyssTeamRegistApproval(bool approved)
        {
            if (!approved)
                return -5; //参加できませんでした
            Team team = this.Character.Team;
            try
            {
                AbyssTeamManager.Instance.AddMember(team, this.teamPartner);
                return 1; //%sに参加しました
            }
            catch
            {
                return -1; //何らかの原因で失敗しました
            }
            //何らかの原因で失敗しました
            //参加申請を出したユーザーがロビー内に居なかったため、参加申請はキャンセルされました
            //既に他のチームに加入しているため、加入することが出来ませんでした。
        }
        public void OnAbyssTeamSetCreateRequest(Packets.Client.CSMG_ABYSSTEAM_SET_CREATE_REQUEST p)
        {
            byte Result = unchecked((byte)CheckAbyssTeamSetCreate(p));
            Packets.Server.SSMG_ABYSSTEAM_SET_CREATE p1 = new Packets.Server.SSMG_ABYSSTEAM_SET_CREATE();
            p1.Result = Result;
            this.netIO.SendPacket(p1);
        }
        int CheckAbyssTeamSetCreate(Packets.Client.CSMG_ABYSSTEAM_SET_CREATE_REQUEST p)
        {
            if (!AbyssTeamManager.Instance.CheckRegistLimit())
                return -2; //チーム登録数の上限に達しているため、チームを登録することが出来ませんでした
            try
            {
                List<PC_JOB> job = new List<PC_JOB>();
                AbyssTeamManager.Instance.CreateTeam(this.Character,p.TeamName,p.Comment,p.Password,p.IsFromSave,p.MinLV,p.MaxLV, job);
            }
            catch
            {
                return -1;//何らかの原因で失敗しました
            }
            return 0; //チームを登録しました
        }
        public void OnAbyssTeamSetOpenRequest(Packets.Client.CSMG_ABYSSTEAM_SET_OPEN_REQUEST p)
        {
            byte Result = unchecked((byte)CheckAbyssTeamSetOpen());
            Packets.Server.SSMG_ABYSSTEAM_SET_OPEN p1 = new Packets.Server.SSMG_ABYSSTEAM_SET_OPEN();
            p1.Result = Result;
            if (Result == 0)
                p1.Floor = 100;
            this.netIO.SendPacket(p1);
        }
        int CheckAbyssTeamSetOpen()
        {
            if (this.Character.Team != null)
            {
                if (this.Character.Team.Leader == this.Character)
                    return -2; //既にチームを登録しているため、チーム登録が行えませんでした
                else
                    return -3; //既に他のチームに加入しているため、チーム登録が行えませんでした
            }
            if (!AbyssTeamManager.Instance.CheckRegistLimit())
                return -4; //チーム登録数の上限に達しているため、チームを登録することが出来ませんでした
            if (this.Character.AbyssFloor==0)
                return -1; //何らかの原因で失敗しました
            return 0;
        }
    }
}