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

                this.SendCharData();
            }
            else
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS;
                this.netIO.SendPacket(p1);                
            }
            
        }

        public void OnCharCreate(Packets.Client.CSMG_CHAR_CREATE p)
        {
            Packets.Server.SSMG_CHAR_CREATE_ACK p1 = new SagaLogin.Packets.Server.SSMG_CHAR_CREATE_ACK();
            if (LoginServer.charDB.CharExists(p.Name))
            {
                p1.CreateResult = SagaLogin.Packets.Server.SSMG_CHAR_CREATE_ACK.Result.GAME_SMSG_CHRCREATE_E_NAME_CONFLICT;
            }
            else
            {
                var slot =
                    from a in account.Characters
                    where a.Slot == p.Slot
                    select a;
                if (slot.Count() != 0)
                {
                    p1.CreateResult = SagaLogin.Packets.Server.SSMG_CHAR_CREATE_ACK.Result.GAME_SMSG_CHRCREATE_E_ALREADY_SLOT;
                }
                else
                {
                    ActorPC pc = new ActorPC();
                    pc.Name = p.Name;
                    pc.Face = p.Face;
                    pc.Gender = p.Gender;
                    pc.HairColor = p.HairColor;
                    pc.HairStyle = p.HairStyle;
                    pc.Race = p.Race;
                    pc.Slot = p.Slot;
                    pc.Wig = 0xFF;
                    pc.Level = 1;
                    pc.JobLevel1 = 1;
                    pc.JobLevel2T = 1;
                    pc.JobLevel2X = 1;
                    pc.QuestRemaining = 3;
                    pc.MapID = 10024000;
                    pc.X = Global.PosX8to16(207);
                    pc.Y = Global.PosY8to16(114);
                    pc.Dir = 2;
                    pc.HP = 100;
                    pc.MaxHP = 120;
                    pc.MP = 200;
                    pc.MaxMP = 220;
                    pc.SP = 50;
                    pc.MaxSP = 60;
                    pc.Str = 2;
                    pc.Dex = 3;
                    pc.Int = 4;
                    pc.Vit = 5;
                    pc.Agi = 6;
                    pc.Mag = 7;
                    pc.Gold = 123456;

                    pc.Inventory.AddItem(ContainerType.UPPER_BODY, ItemFactory.Instance.GetItem(50001381));
                    pc.Inventory.AddItem(ContainerType.LOWER_BODY, ItemFactory.Instance.GetItem(50012360));
                    pc.Inventory.AddItem(ContainerType.SHOES, ItemFactory.Instance.GetItem(50060100));
                    pc.Inventory.AddItem(ContainerType.BODY, ItemFactory.Instance.GetItem(10020114));
                    pc.Inventory.AddItem(ContainerType.BODY, ItemFactory.Instance.GetItem(60010082));

                    LoginServer.charDB.CreateChar(pc, account.AccountID);
                    account.Characters.Add(pc);
                    p1.CreateResult = SagaLogin.Packets.Server.SSMG_CHAR_CREATE_ACK.Result.OK;
                }
            }
            this.netIO.SendPacket(p1);
            this.SendCharData();
        }

        public void OnCharDelete(Packets.Client.CSMG_CHAR_DELETE p)
        {
            Packets.Server.SSMG_CHAR_DELETE_ACK p1 = new SagaLogin.Packets.Server.SSMG_CHAR_DELETE_ACK();
            var chr =
                from c in account.Characters
                where c.Slot == p.Slot
                select c;
            ActorPC pc = chr.First();
            if (account.DeletePassword.ToLower() == p.DeletePassword.ToLower())
            {
                LoginServer.charDB.DeleteChar(pc);
                account.Characters.Remove(pc);
                p1.DeleteResult = SagaLogin.Packets.Server.SSMG_CHAR_DELETE_ACK.Result.OK;
            }
            else
            {
                p1.DeleteResult = SagaLogin.Packets.Server.SSMG_CHAR_DELETE_ACK.Result.WRONG_DELETE_PASSWORD;
            }
            this.netIO.SendPacket(p1);
            this.SendCharData();
        }

        public void OnCharSelect(Packets.Client.CSMG_CHAR_SELECT p)
        {
            Packets.Server.SSMG_CHAR_SELECT_ACK p1 = new SagaLogin.Packets.Server.SSMG_CHAR_SELECT_ACK();
            var chr =
                from c in account.Characters
                where c.Slot == p.Slot
                select c;
            ActorPC pc = chr.First();
            p1.MapID = pc.MapID;
            this.netIO.SendPacket(p1);
        }

        public void OnRequestMapServer(Packets.Client.CSMG_REQUEST_MAP_SERVER p)
        {
            Packets.Server.SSMG_SEND_TO_MAP_SERVER p1 = new SagaLogin.Packets.Server.SSMG_SEND_TO_MAP_SERVER();
            p1.ServerID = 1;
            p1.IP = Configuration.Instance.MapHost;
            p1.Port = Configuration.Instance.MapPort;
            this.netIO.SendPacket(p1);
        }

        public void OnCharStatus(Packets.Client.CSMG_CHAR_STATUS p)
        {
            Packets.Server.SSMG_CHAR_STATUS p1 = new SagaLogin.Packets.Server.SSMG_CHAR_STATUS();
            this.netIO.SendPacket(p1);
        }

        private void SendCharData()
        {
            Packets.Server.SSMG_CHAR_DATA p2 = new SagaLogin.Packets.Server.SSMG_CHAR_DATA();
            p2.Chars = account.Characters;
            this.netIO.SendPacket(p2);
            Packets.Server.SSMG_CHAR_EQUIP p3 = new SagaLogin.Packets.Server.SSMG_CHAR_EQUIP();
            p3.Characters = account.Characters;
            this.netIO.SendPacket(p3);
        }
    }
}
