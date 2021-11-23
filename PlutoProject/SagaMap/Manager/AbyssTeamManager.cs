
using SagaLib;
using SagaDB.Party;
using SagaDB.Actor;
using SagaDB.Tamaire;
using SagaMap.Network.Client;
using System.Xml;
using SagaLib.VirtualFileSystem;
using SagaMap.Scripting;
using System.Collections.Generic;
using SagaDB.Team;

namespace SagaMap.Manager
{
    public class AbyssTeamManager : Singleton<AbyssTeamManager>
    {
        Dictionary<uint, Team> teams = new Dictionary<uint, Team>();
        int RegistLimit=10;
        public AbyssTeamManager()
        {

        }
        public bool CheckRegistLimit()
        {
            return (teams.Count <= RegistLimit);
        }
        public Team GetTeam(uint leaderID)
        {
            if (leaderID == 0)
                return null;
            if (teams.ContainsKey(leaderID))
                return teams[leaderID];
            return null;
        }
        public void PlayerOnline(Team team, ActorPC pc)
        {
            if (team == null)
                return;
            if (!team.IsMember(pc))
            {
                pc.Team = null;
                return;
            }
            team.MemberOnline(pc);
            foreach (ActorPC i in team.Members.Values)
            {
                if (i == pc || !i.Online) continue;
                //MapClient.FromActorPC(i).SendPartyMemberInfo(pc);
            }
        }
        public void CreateTeam(ActorPC pc, string name, string comment,string pass, bool isFromSave, byte minlv,byte maxlv,List<PC_JOB> job)
        {
            Team team = new Team();
            team.Name = name;
            team.Comment = comment;
            team.Pass = pass;
            if (isFromSave)
                team.StartingFloor = 1;
            else
                team.StartingFloor = 102;
            team.MinLevel = minlv;
            team.MaxLevel = maxlv;
            team.JobRequirements = job;

            teams.Add(pc.CharID, team);
            team.Leader = pc;
            AddMember(team, pc);
            pc.Team = team;
        }

        public void AddMember(Team team, ActorPC pc)
        {
            if (team == null)
                return;
            if (team.IsMember(pc))
                return;
            if (team.MemberCount >= team.MaxMember)
                return;
            team.NewMember(pc);
            pc.Team = team;
        }

        public void DeleteMember(Team team, uint pc)
        {
            if (team == null)
                return;
            if (!team.IsMember(pc))
                return;
            team.DeleteMemeber(pc);
            if (team.Members.Count == 1 || pc==team.Leader.CharID)
            {
                TeamDismiss(team);
            }
        }

        public void TeamDismiss(Team team)
        {
            if (team == null)
                return;
            for (byte i = 0; i < team.Members.Count; i++)
                team.Members[i].Team = null;
            if (teams.ContainsKey(team.ID))
                teams.Remove(team.ID);
        }
    }
}