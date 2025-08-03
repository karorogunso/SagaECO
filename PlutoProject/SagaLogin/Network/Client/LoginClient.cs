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
    public partial class LoginClient : SagaLib.Client
    {
        private string client_Version;

        private uint frontWord, backWord;

        public bool IsMapServer = false;

        public Account account;

        public enum SESSION_STATE
        {
            LOGIN, MAP, REDIRECTING, DISCONNECTED
        }
        public SESSION_STATE state;

        public LoginClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this);
            if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.netIO.FirstLevelLength = 2;
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
            if (this.currentStatus != CharStatus.OFFLINE)
            {
                if (this.IsMapServer)
                {
                    Logger.ShowWarning("A map server has just disconnected...");
                    foreach (uint i in this.server.HostedMaps)
                    {
                        if (MapServerManager.Instance.MapServers.ContainsKey(i))
                            MapServerManager.Instance.MapServers.Remove(i);
                    }
                }
                else
                {
                    this.currentStatus = CharStatus.OFFLINE;
                    this.currentMap = 0;
                    try
                    {
                        this.SendStatusToFriends();
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    if (this.account != null)
                        Logger.ShowInfo(this.account.Name + " logged out.");
                }
            }
            if (LoginClientManager.Instance.Clients.Contains(this))
                LoginClientManager.Instance.Clients.Remove(this);
        }

        public void OnWRPRequest(Packets.Client.CSMG_WRP_REQUEST p)
        {
            Packets.Server.SSMG_WRP_LIST p1 = new SagaLogin.Packets.Server.SSMG_WRP_LIST();
            p1.RankingList = LoginServer.charDB.GetWRPRanking();
            this.netIO.SendPacket(p1);
        }
    }
}
