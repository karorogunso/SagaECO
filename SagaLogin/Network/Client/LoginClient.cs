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
    public class LoginClient : SagaLib.Client
    {
        private string client_Version;

        private uint frontWord, backWord;

        private Account account;

        public enum SESSION_STATE
        {
            LOGIN, MAP, REDIRECTING, DISCONNECTED
        }
        public SESSION_STATE state;

        public LoginClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, LoginClientManager.Instance);
            this.netIO.SetMode(NetIO.Mode.Server);
            if (this.netIO.sock.Connected) this.OnConnect();
        }

        public override string ToString()
        {
            try
            {
                if (this.netIO != null) return this.netIO.sock.RemoteEndPoint.ToString();
                else
                    return "LoginClient";
            }
            catch (Exception)
            {
                return "LoginClient";
            }
        }

        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {

        }

        public void OnSendVersion(Packets.Client.CSMG_SEND_VERSION p)
        {
            Logger.ShowInfo("Client(Version:" + p.GetVersion() + ") is trying to connect...");
            this.client_Version = p.GetVersion();

            Packets.Server.SSMG_VERSION_ACK p1 = new SagaLogin.Packets.Server.SSMG_VERSION_ACK();
            p1.SetResult(SagaLogin.Packets.Server.SSMG_VERSION_ACK.Result.OK);
            p1.SetVersion(this.client_Version);
            this.netIO.SendPacket(p1);
            //Official HK server will now request for Hackshield GUID check , we don't know its algorithms, so not implemented
            Packets.Server.SSMG_LOGIN_ALLOWED p2 = new SagaLogin.Packets.Server.SSMG_LOGIN_ALLOWED();
            this.frontWord = (uint)Global.Random.Next();
            this.backWord = (uint)Global.Random.Next();
            p2.FrontWord = this.frontWord;
            p2.BackWord = this.backWord;
            this.netIO.SendPacket(p2);
        }

        public void OnSendGUID(Packets.Client.CSMG_SEND_GUID p)
        {            
            Packets.Server.SSMG_LOGIN_ALLOWED p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ALLOWED();
            this.netIO.SendPacket(p1);
        }

        public void OnPing(Packets.Client.CSMG_PING p)
        {
            Packets.Server.SSMG_PONG p1 = new SagaLogin.Packets.Server.SSMG_PONG();
            this.netIO.SendPacket(p1);
        }

        public void OnLogin(Packets.Client.CSMG_LOGIN p)
        {
            p.GetContent();
            if (LoginServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.OK;
                this.netIO.SendPacket(p1);

                account = LoginServer.accountDB.GetUser(p.UserName);

                uint[] charIDs = LoginServer.charDB.GetCharIDs(account.AccountID);

                account.Characters = new List<ActorPC>();
                for (int i = 0; i < charIDs.Length; i++)
                {
                    account.Characters.Add(LoginServer.charDB.GetChar(charIDs[i]));
                }

                this.SendWorldAddr();
            }
            else
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS;
                this.netIO.SendPacket(p1);                
            }
            
        }

        public void OnRequestMapServer(Packets.Client.CSMG_REQUEST_MAP_SERVER p)
        {
            Packets.Server.SSMG_SEND_TO_MAP_SERVER p1 = new SagaLogin.Packets.Server.SSMG_SEND_TO_MAP_SERVER();
            p1.ServerID = 1;
            p1.IP = Configuration.Instance.MapHost;
            p1.Port = Configuration.Instance.MapPort;
            this.netIO.SendPacket(p1);
        }

        private void SendWorldAddr()
        {
            //Packets.Server.SSMG_CHAR_DATA p2 = new SagaLogin.Packets.Server.SSMG_CHAR_DATA();
            //p2.Chars = account.Characters;
            //this.netIO.SendPacket(p2);
            //Packets.Server.SSMG_CHAR_EQUIP p3 = new SagaLogin.Packets.Server.SSMG_CHAR_EQUIP();
            //p3.Characters = account.Characters;
            //this.netIO.SendPacket(p3);
        }
    }
}
