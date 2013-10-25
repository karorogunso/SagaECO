using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnPing(Packets.Client.CSMG_PING p)
        {
            Packets.Server.SSMG_PONG p2 = new SagaMap.Packets.Server.SSMG_PONG();
            this.netIO.SendPacket(p);
        }

        public void OnSendVersion(Packets.Client.CSMG_SEND_VERSION p)
        {
            Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.CLIENT_CONNECTING, p.GetVersion()));
            this.client_Version = p.GetVersion();

            Packets.Server.SSMG_VERSION_ACK p1 = new SagaMap.Packets.Server.SSMG_VERSION_ACK();
            p1.SetResult(SagaMap.Packets.Server.SSMG_VERSION_ACK.Result.OK);
            p1.SetVersion(this.client_Version);
            this.netIO.SendPacket(p1);
            //Official HK server will now request for Hackshield GUID check , we don't know its algorithms, so not implemented
            Packets.Server.SSMG_LOGIN_ALLOWED p2 = new SagaMap.Packets.Server.SSMG_LOGIN_ALLOWED();
            this.frontWord = (uint)Global.Random.Next();
            this.backWord = (uint)Global.Random.Next();
            p2.FrontWord = this.frontWord;
            p2.BackWord = this.backWord;
            this.netIO.SendPacket(p2);
        }

        public void OnLogin(Packets.Client.CSMG_LOGIN p)
        {
            p.GetContent();
            if (MapServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.OK;
                p1.Unknown1 = 0x100;
                p1.Unknown2 = 0x486EB420;

                this.netIO.SendPacket(p1);

                account = MapServer.accountDB.GetUser(p.UserName);

                uint[] charIDs = MapServer.charDB.GetCharIDs(account.AccountID);

                account.Characters = new List<ActorPC>();
                for (int i = 0; i < charIDs.Length; i++)
                {
                    account.Characters.Add(MapServer.charDB.GetChar(charIDs[i]));
                }
                this.state = SESSION_STATE.AUTHENTIFICATED;
            }
            else
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnCharSlot(Packets.Client.CSMG_CHAR_SLOT p)
        {
            if (this.state == SESSION_STATE.AUTHENTIFICATED)
            {
                var chr =
                    from c in account.Characters
                    where c.Slot == p.Slot
                    select c;                    
                this.Character = chr.First();
                this.Character.e = new ActorEventHandlers.PCEventHandler(this);
                this.Character.Account = account;

                Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.PLAYER_LOG_IN, this.Character.Name));
                this.Map = MapManager.Instance.GetMap(0);
                this.Map.RegisterActor(this.Character);     
            }
        }

        public void OnMapLoaded(Packets.Client.CSMG_PLAYER_MAP_LOADED p)
        {
            this.Character.invisble = false;
            this.Map.OnActorVisibilityChange(this.Character);
            this.Map.SendVisibleActorsToActor(this.Character);
            Packets.Server.SSMG_LOGIN_FINISHED p1 = new SagaMap.Packets.Server.SSMG_LOGIN_FINISHED();
            this.netIO.SendPacket(p1);
        }

        public void OnLogout(Packets.Client.CSMG_LOGOUT p)
        {
            Packets.Server.SSMG_LOGOUT p1 = new SagaMap.Packets.Server.SSMG_LOGOUT();
            p1.Result = SagaMap.Packets.Server.SSMG_LOGOUT.Results.START;
            this.netIO.SendPacket(p1);
        }
       
    }
}
