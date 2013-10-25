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
    public partial class MapClient : SagaLib.Client
    {
        private string client_Version;

        private uint frontWord, backWord;

        private Account account;
        private ActorPC chara;

        public Map map;
        public enum SESSION_STATE
        {
            LOGIN, AUTHENTIFICATED, REDIRECTING, DISCONNECTED
        }
        public SESSION_STATE state;

        public ActorPC Character { get { return this.chara; } set { this.chara = value; } }
        public Map Map { get { return this.map; } set { this.map = value; } }

        public MapClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, MapClientManager.Instance);
            this.netIO.SetMode(NetIO.Mode.Server);
            this.netIO.FirstLevelLength = 2;
            if (this.netIO.sock.Connected) this.OnConnect();
        }

        public override string ToString()
        {
            try
            {
                if (this.netIO != null) return this.netIO.sock.RemoteEndPoint.ToString();
                else
                    return "MapClient";
            }
            catch (Exception)
            {
                return "MapClient";
            }
        }

        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {
            this.Map.DeleteActor(this.Character);
            MapServer.charDB.SaveChar(this.Character);
        }
    }
}
