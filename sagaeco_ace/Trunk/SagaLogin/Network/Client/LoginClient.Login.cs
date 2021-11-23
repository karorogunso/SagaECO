using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Map;
using SagaLib;
using SagaLogin;
using SagaLogin.Manager;

namespace SagaLogin.Network.Client
{
    public partial class LoginClient : SagaLib.Client
    {
        public ActorPC selectedChar;
        public void OnSendVersion(Packets.Client.CSMG_SEND_VERSION p)
        {
            Logger.ShowInfo("Client(Version:" + p.GetVersion() + ") is trying to connect...");
            this.client_Version = p.GetVersion();

            string args = "FF FF E8 6A 6A CA DC E8 06 05 2B 29 F8 96 2F 86 7C AB 2A 57 AD 30";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            Packet p3 = new Packet();
            p3.data = buf;
            this.netIO.SendPacket(p3);



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
            if (MapServerManager.Instance.MapServers.Count == 0)
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_IPBLOCK;
                this.netIO.SendPacket(p1);
                return;
            }

            if (LoginServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                Account tmp = LoginServer.accountDB.GetUser(p.UserName.Replace("\\", "").Replace("'", "\\'"));

                //var DumplateIPCount = LoginClientManager.Instance.Clients.Count(x => x.netIO.sock.RemoteEndPoint.ToString().Split(':')[0] == this.netIO.sock.RemoteEndPoint.ToString().Split(':')[0] && !x.IsMapServer);
                //if (DumplateIPCount > 1 && tmp.GMLevel == 0 && !Configuration.Instance.AllowMultiConnection)
                //{
                //    Packets.Server.SSMG_LOGIN_ACK p2 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                //    p2.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_ALREADY;
                //    this.netIO.SendPacket(p2);
                //    return;
                //}

                if (LoginClientManager.Instance.FindClientAccount(p.UserName.Replace("\\", "").Replace("'", "\\'")) != null && tmp.GMLevel == 0)
                {
                    LoginClientManager.Instance.FindClientAccount(p.UserName.Replace("\\", "").Replace("'", "\\'")).netIO.Disconnect();
                    Packets.Server.SSMG_LOGIN_ACK p2 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                    p2.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_ALREADY;
                    this.netIO.SendPacket(p2);
                    return;
                }

                account = tmp;
                if (account.Banned)
                {
                    Packets.Server.SSMG_LOGIN_ACK p2 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                    p2.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BFALOCK;
                    this.netIO.SendPacket(p2);
                    return;
                }
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.OK;
                this.netIO.SendPacket(p1);

                account.LastIP = this.netIO.sock.RemoteEndPoint.ToString().Split(':')[0];



                uint[] charIDs = LoginServer.charDB.GetCharIDs(account.AccountID);

                account.Characters = new List<ActorPC>();
                for (int i = 0; i < charIDs.Length; i++)
                {
                    ActorPC pc = LoginServer.charDB.GetChar(charIDs[i], false);
                    if (pc.QuestNextResetTime < DateTime.Now)
                    {
                        if ((DateTime.Now - pc.QuestNextResetTime).TotalDays > 1000)
                        {
                            pc.QuestNextResetTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);
                        }
                        else
                        {
                            int days = (int)(DateTime.Now - pc.QuestNextResetTime).TotalDays;
                            pc.QuestRemaining += (ushort)((days + 1) * 5);
                            if (pc.QuestRemaining > 15)
                                pc.QuestRemaining = 15;
                        }
                    }
                    account.Characters.Add(pc);
                }
                LoginServer.accountDB.WriteUser(account);
                this.SendCharData();
            }
            else
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaLogin.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaLogin.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS;
                this.netIO.SendPacket(p1);
            }

        }

        bool checkHairStyle(Packets.Client.CSMG_CHAR_CREATE p)
        {
            if (p.Gender == PC_GENDER.FEMALE)
            {
                if (p.HairStyle > 9 && p.HairStyle != 14)
                    return false;
                return true;
            }
            if (p.HairStyle > 9)
                return false;
            else
                return true;
        }

        bool checkHairColor(Packets.Client.CSMG_CHAR_CREATE p)
        {
            if (p.Race == PC_RACE.DOMINION)
            {
                if (p.HairColor >= 70 && p.HairColor <= 72)
                    return true;
                else
                    return false;
            }
            if (p.Race == PC_RACE.EMIL)
            {
                if (p.HairColor >= 50 && p.HairColor <= 52)
                    return true;
                else
                    return false;
            }
            if (p.Race == PC_RACE.TITANIA)
            {
                if (p.HairColor == 7 || p.HairColor == 60 || p.HairColor == 61 || p.HairColor == 62)
                    return true;
                else
                    return false;
            }
            return true;
        }

        public void OnCharCreate(Packets.Client.CSMG_CHAR_CREATE p)
        {
            Packets.Server.SSMG_CHAR_CREATE_ACK p1 = new SagaLogin.Packets.Server.SSMG_CHAR_CREATE_ACK();
            if (p.Race != PC_RACE.DEM)
            {
                if (!checkHairColor(p) || !checkHairStyle(p))
                {
                    this.account.Banned = true;
                    this.netIO.Disconnect();
                    LoginServer.accountDB.WriteUser(this.account);
                    return;
                }
            }
            if (p.Name.Contains("\\") || p.Name.Contains("'"))
                p1.CreateResult = Packets.Server.SSMG_CHAR_CREATE_ACK.Result.GAME_SMSG_CHRCREATE_E_NAME_BADCHAR;
            else if (LoginServer.charDB.CharExists(p.Name))
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
                    pc.EP = 100;
                    pc.MapID = Configuration.Instance.StartupSetting[pc.Race].StartMap;
                    //MapInfo info = MapInfoFactory.Instance.MapInfo[pc.MapID];
                    pc.X2 = Configuration.Instance.StartupSetting[pc.Race].X;
                    pc.Y2 = Configuration.Instance.StartupSetting[pc.Race].Y;

                    pc.Dir = 2;
                    pc.HP = 900;
                    pc.MaxHP = 120;
                    pc.MP = 900;
                    pc.MaxMP = 220;
                    pc.SP = 100;
                    pc.MaxSP = 100;

                    pc.Str = Configuration.Instance.StartupSetting[pc.Race].Str;
                    pc.Dex = Configuration.Instance.StartupSetting[pc.Race].Dex;
                    pc.Int = Configuration.Instance.StartupSetting[pc.Race].Int;
                    pc.Vit = Configuration.Instance.StartupSetting[pc.Race].Vit;
                    pc.Agi = Configuration.Instance.StartupSetting[pc.Race].Agi;
                    pc.Mag = Configuration.Instance.StartupSetting[pc.Race].Mag;
                    pc.SkillPoint = 3;
                    pc.StatsPoint = 2;
                    pc.Gold = 0;

                    List<Configurations.StartItem> lists;
                    lists = Configuration.Instance.StartItem[pc.Race][pc.Gender];
                    foreach (Configurations.StartItem i in lists)
                    {
                        Item item = ItemFactory.Instance.GetItem(i.ItemID);
                        item.Stack = i.Count;
                        pc.Inventory.AddItem(i.Slot, item);
                    }

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
            selectedChar = pc;
            p1.MapID = pc.MapID;
            this.netIO.SendPacket(p1);
        }

        public void OnRequestMapServer(Packets.Client.CSMG_REQUEST_MAP_SERVER p)
        {
            Packets.Server.SSMG_SEND_TO_MAP_SERVER p1 = new SagaLogin.Packets.Server.SSMG_SEND_TO_MAP_SERVER();

            if (MapServerManager.Instance.MapServers.ContainsKey(selectedChar.MapID))
            {
                MapServer server = MapServerManager.Instance.MapServers[selectedChar.MapID];
                p1.ServerID = 1;
                p1.IP = server.IP;
                p1.Port = server.port;
            }
            else
            {
                if (MapServerManager.Instance.MapServers.ContainsKey((uint)(selectedChar.MapID / 1000 * 1000)))
                {
                    MapServer server = MapServerManager.Instance.MapServers[(uint)(selectedChar.MapID / 1000 * 1000)];
                    p1.ServerID = 1;
                    p1.IP = server.IP;
                    p1.Port = server.port;
                }
                else
                {
                    Logger.ShowWarning("No map server registered for mapID:" + selectedChar.MapID);
                    p1.ServerID = 255;
                    p1.IP = "127.0.0.001";
                    p1.Port = 10000;
                }
            }
            this.netIO.SendPacket(p1);
        }

        public void OnCharStatus(Packets.Client.CSMG_CHAR_STATUS p)
        {
            string args = "00 2b";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            Packet ps1 = new Packet();
            ps1.data = buf;
            this.netIO.SendPacket(ps1);

            Packets.Server.SSMG_CHAR_STATUS p1 = new SagaLogin.Packets.Server.SSMG_CHAR_STATUS();
            this.netIO.SendPacket(p1);
            SendFriendList();
            SendStatusToFriends();

            args = "00 de";
            buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            Packet ps = new Packet();
            ps.data = buf;
            this.netIO.SendPacket(ps);
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
