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
using SagaLogin.Packets.Client;

namespace SagaLogin.Network.Client
{
    public partial class LoginClient : SagaLib.Client
    {
        MapServer server;
        public void OnInternMapRequestConfig(Packets.Map.INTERN_LOGIN_REQUEST_CONFIG p)
        {
            Configuration.Instance.Version = p.Version;
            Packets.Map.INTERN_LOGIN_REQUEST_CONFIG_ANSWER p1 = new SagaLogin.Packets.Map.INTERN_LOGIN_REQUEST_CONFIG_ANSWER();
            p1.AuthOK = (server.Password == Configuration.Instance.Password);
            p1.StartupSetting = Configuration.Instance.StartupSetting;
            this.netIO.SendPacket(p1);

            Logger.ShowInfo(string.Format("Mapserver:{0}:{1} is requesting configuration...", server.IP, server.port));
        }

        public void OnInternMapRegister(Packets.Map.INTERN_LOGIN_REGISTER p)
        {
            MapServer server = p.MapServer;
            this.IsMapServer = true;
            if (this.server == null)
            {
                this.server = server;
                if (server.Password != Configuration.Instance.Password)
                {
                    Logger.ShowWarning(string.Format("Mapserver:{0}:{1} is trying to register maps with wrong password:{2}", server.IP, server.port, server.Password));
                    return;
                }
            }
            else
            {
                if (server.Password != Configuration.Instance.Password)
                {
                    Logger.ShowWarning(string.Format("Mapserver:{0}:{1} is trying to register maps with wrong password:{2}", server.IP, server.port, server.Password));
                    return;
                }
                foreach (uint i in server.HostedMaps)
                {
                    if (!this.server.HostedMaps.Contains(i))
                        this.server.HostedMaps.Add(i);
                }
            }
            int count = 0;
            foreach(uint i in server.HostedMaps)
            {
                if (!MapServerManager.Instance.MapServers.ContainsKey(i))
                {
                    MapServerManager.Instance.MapServers.Add(i, this.server);
                    count++;
                }
                else
                {
                    MapServer oldserver = MapServerManager.Instance.MapServers[i];
//Logger.ShowWarning(string.Format("MapID:{0} was already hosted by Mapserver:{1}:{2}, skiping...", i, oldserver.IP, oldserver.port));
                }
            }
            Logger.ShowInfo(string.Format("{0} maps registered for MapServer:{1}:{2}...", count, server.IP, server.port));
        }
    }
}
