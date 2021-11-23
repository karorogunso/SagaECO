using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaLib;
using SagaLogin;
using SagaLogin.Manager;

namespace SagaLogin.Network.Client
{
    public enum CharStatus
    {
        OFFLINE,
        ONLINE,
        募集中,
        取り込み中,
        お話し中,
        休憩中,
        退席中,
        戦闘中,        
        商売中,
        憑依中,
        クエスト中,
        お祭り中,
        連絡求む
    }

    public partial class LoginClient : SagaLib.Client
    {
        public uint currentMap = 0;
        public CharStatus currentStatus = CharStatus.ONLINE;
        public byte lv, joblv;
        public PC_JOB job;
        LoginClient friendTarget = null;

        public void OnFriendDelete(Packets.Client.CSMG_FRIEND_DELETE p)
        {
            if (this.selectedChar == null)
                return;
            LoginServer.charDB.DeleteFriend(this.selectedChar.CharID, p.CharID);
            LoginServer.charDB.DeleteFriend(p.CharID, this.selectedChar.CharID);
            Packets.Server.SSMG_FRIEND_DELETE p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_DELETE();
            p1.CharID = p.CharID;
            this.netIO.SendPacket(p1);
            LoginClient client = LoginClientManager.Instance.FindClient(p.CharID);
            if (client != null)
            {
                p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_DELETE();
                p1.CharID = this.selectedChar.CharID;
                client.netIO.SendPacket(p1);
            }
        }

        public void OnFriendAdd(Packets.Client.CSMG_FRIEND_ADD p)
        {
            LoginClient client = LoginClientManager.Instance.FindClient(p.CharID);
            int result = CheckFriendRegister(client);
            if (result==0)
            {
                    friendTarget = client;
                    client.friendTarget = this;
                    Packets.Server.SSMG_FRIEND_ADD p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_ADD();
                    p1.CharID = this.selectedChar.CharID;
                    p1.Name = this.selectedChar.Name;
                    client.netIO.SendPacket(p1);
            }
            else
            {
                Packets.Server.SSMG_FRIEND_ADD_FAILED p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_ADD_FAILED();
                p1.AddResult = unchecked((uint)result);
                this.netIO.SendPacket(p1);                
            }
        }

        int CheckFriendRegister (LoginClient client)
        {
            if (client == null)
                return -1; //相手が見付かりません
            if (LoginServer.charDB.IsFriend(this.selectedChar.CharID, client.selectedChar.CharID))
                return -2; //既に登録しています
            if (LoginServer.charDB.GetFriendList(this.selectedChar).Count >= 200)
                return -4; //フレンドリストに空きがありません
            if (LoginServer.charDB.GetFriendList(client.selectedChar).Count >= 200)
                return -5; //相手のフレンドリストに空きがありません
            return 0;
        }

        public void OnFriendAddReply(Packets.Client.CSMG_FRIEND_ADD_REPLY p)
        {
            int result = CheckFriendRegisterReply(p.Reply);
            if (result == 0)
            {
                Packets.Server.SSMG_FRIEND_ADD_OK p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_ADD_OK();
                p1.CharID = this.friendTarget.selectedChar.CharID;
                this.netIO.SendPacket(p1);
                this.SendFriendAdd(this.friendTarget);
                LoginServer.charDB.AddFriend(this.selectedChar, this.friendTarget.selectedChar.CharID);
                p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_ADD_OK();
                p1.CharID = this.selectedChar.CharID;
                this.friendTarget.netIO.SendPacket(p1);
                this.friendTarget.SendFriendAdd(this);
                LoginServer.charDB.AddFriend(this.friendTarget.selectedChar, this.selectedChar.CharID);
            }
            else
            {
                Packets.Server.SSMG_FRIEND_ADD_FAILED p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_ADD_FAILED();
                p1.AddResult = unchecked((uint)result);
                this.netIO.SendPacket(p1);
                this.friendTarget.netIO.SendPacket(p1);                
            }
            this.friendTarget = null;
        }

        int CheckFriendRegisterReply(uint reply)
        {
            if (this.friendTarget == null)
                return -1; //相手が見付かりません
            if (LoginServer.charDB.IsFriend(this.selectedChar.CharID, friendTarget.selectedChar.CharID))
                return -2; //既に登録しています
            if (reply != 1)
                return -3; //相手に拒否されました
            if (LoginServer.charDB.GetFriendList(this.selectedChar).Count >= 200)
                return -4; //フレンドリストに空きがありません
            if (LoginServer.charDB.GetFriendList(friendTarget.selectedChar).Count >= 200)
                return -5; //相手のフレンドリストに空きがありません
            return 0;
        }

        public void OnFriendMapUpdate(Packets.Client.CSMG_FRIEND_MAP_UPDATE p)
        {
            if (this.selectedChar == null) return;
            List<ActorPC> friendlist = LoginServer.charDB.GetFriendList2(this.selectedChar);
            this.currentMap = p.MapID;
            foreach (ActorPC i in friendlist)
            {
                LoginClient client = LoginClientManager.Instance.FindClient(i);
                Packets.Server.SSMG_FRIEND_MAP_UPDATE p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_MAP_UPDATE();
                p1.CharID = this.selectedChar.CharID;
                if (client != null)
                {
                    p1.MapID = this.currentMap;
                    client.netIO.SendPacket(p1);
                }
            }
        }

        public void OnFriendDetailUpdate(Packets.Client.CSMG_FRIEND_DETAIL_UPDATE p)
        {
            if (this.selectedChar == null) return;
            List<ActorPC> friendlist = LoginServer.charDB.GetFriendList2(this.selectedChar);
            this.job = p.Job;
            this.lv = p.Level;
            this.joblv = p.Level;
            foreach (ActorPC i in friendlist)
            {
                LoginClient client = LoginClientManager.Instance.FindClient(i);
                Packets.Server.SSMG_FRIEND_DETAIL_UPDATE p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_DETAIL_UPDATE();
                p1.CharID = this.selectedChar.CharID;
                if (client != null)
                {
                    p1.Job = this.job;
                    p1.Level = this.lv;
                    p1.JobLevel = this.joblv;
                    client.netIO.SendPacket(p1);
                }
            }
        }

        public void SendFriendAdd(LoginClient client)
        {
            Packets.Server.SSMG_FRIEND_CHAR_INFO p = new SagaLogin.Packets.Server.SSMG_FRIEND_CHAR_INFO();
            p.ActorPC = client.selectedChar;
            p.MapID = client.currentMap;
            p.Status = client.currentStatus;
            p.Comment = "";
            this.netIO.SendPacket(p);
        }

        public void SendFriendList()
        {
            if (this.selectedChar == null) return;
            List<ActorPC> friendlist = LoginServer.charDB.GetFriendList(this.selectedChar);
            foreach (ActorPC i in friendlist)
            {
                LoginClient client = LoginClientManager.Instance.FindClient(i);
                Packets.Server.SSMG_FRIEND_CHAR_INFO p = new SagaLogin.Packets.Server.SSMG_FRIEND_CHAR_INFO();
                p.ActorPC = i;
                if (client != null)
                {
                    p.MapID = client.currentMap;
                    p.Status = client.currentStatus;
                }
                p.Comment = "";
                this.netIO.SendPacket(p);
            }
        }

        public void SendStatusToFriends()
        {
            if (this.selectedChar == null) return;
            List<ActorPC> friendlist = LoginServer.charDB.GetFriendList2(this.selectedChar);
            foreach (ActorPC i in friendlist)
            {
                LoginClient client = LoginClientManager.Instance.FindClient(i);
                Packets.Server.SSMG_FRIEND_STATUS_UPDATE p1 = new SagaLogin.Packets.Server.SSMG_FRIEND_STATUS_UPDATE();
                Packets.Server.SSMG_FRIEND_DETAIL_UPDATE p2 = new SagaLogin.Packets.Server.SSMG_FRIEND_DETAIL_UPDATE();
                p1.CharID = this.selectedChar.CharID;
                p2.CharID = this.selectedChar.CharID;
                if (client != null)
                {
                    p1.Status = this.currentStatus;
                    p2.Job = this.selectedChar.Job;
                    p2.Level = this.selectedChar.Level;
                    p2.JobLevel = this.selectedChar.CurrentJobLevel;
                    client.netIO.SendPacket(p1);
                    client.netIO.SendPacket(p2);
                }
            }
        }
    }
}
