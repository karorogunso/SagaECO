using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Manager
{
    public class MapServer
    {
        public LoginClient Client;
        public List<uint> HostedMaps = new List<uint>();
        public string Password;
        public string IP;
        public int port;
    }

    public class MapServerManager : Singleton<MapServerManager>
    {
        Dictionary<uint, MapServer> servers = new Dictionary<uint, MapServer>();

        public Dictionary<uint, MapServer> MapServers
        {
            get
            {
                return this.servers;
            }
        }
        public MapServerManager()
        {

        }
    }
}
