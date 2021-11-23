using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;
using System.Net;
using System.Threading;

namespace SagaProxy
{
    public class ProxyServer : Singleton<ProxyServer>
    {
        static Thread MainLoop = new Thread(loop);
        public bool VersionRecorded;
        public Packets.Client.SendVersion VersionPacket;
        public enum ServerType
        {
            Validation,
            Login,
            Map
        }
        public int Launch(string ip, int port, int localport, ServerType type)
        {
            if (type == ServerType.Map)
            {
                if (ProxyClientManager.MapInstance.ready)
                    return ((IPEndPoint)ProxyClientManager.MapInstance.listener.LocalEndpoint).Port;
                ProxyClientManager.MapInstance.type = ServerType.Map;
                ProxyClientManager.MapInstance.IP = ip;
                ProxyClientManager.MapInstance.port = port;
                ProxyClientManager.MapInstance.packetContainerClient = PacketContainer.Instance.packetsClientMap;
                ProxyClientManager.MapInstance.packetContainerServer = PacketContainer.Instance.packetsMap;
                ProxyClientManager.MapInstance.packets = PacketContainer.Instance.packets2;
                ProxyClientManager.MapInstance.Start();
                while (!ProxyClientManager.MapInstance.StartNetwork(localport))
                    localport++;
                ProxyClientManager.MapInstance.ready = true;
            }
            else if(type == ServerType.Login)
            {
                if (ProxyClientManager.LoginInstance.ready)
                    return ((IPEndPoint)ProxyClientManager.LoginInstance.listener.LocalEndpoint).Port;
                ProxyClientManager.LoginInstance.type = ServerType.Login;
                ProxyClientManager.LoginInstance.IP = ip;
                ProxyClientManager.LoginInstance.port = port;
                ProxyClientManager.LoginInstance.packetContainerClient = PacketContainer.Instance.packetsClientLogin;
                ProxyClientManager.LoginInstance.packetContainerServer = PacketContainer.Instance.packetsLogin;
                ProxyClientManager.LoginInstance.packets = PacketContainer.Instance.packets;
                ProxyClientManager.LoginInstance.Start();
                while (!ProxyClientManager.LoginInstance.StartNetwork(localport))
                    localport++;
                ProxyClientManager.LoginInstance.ready = true;
                //Global.clientMananger = (ClientManager)ProxyClientManager.LoginInstance;
            }
            else if (type == ServerType.Validation)
            {
                if (ProxyClientManager.ValidationInstance.ready)
                    return ((IPEndPoint)ProxyClientManager.ValidationInstance.listener.LocalEndpoint).Port;
                ProxyClientManager.ValidationInstance.type = ServerType.Validation;
                ProxyClientManager.ValidationInstance.IP = ip;
                ProxyClientManager.ValidationInstance.port = port;
                ProxyClientManager.ValidationInstance.packetContainerClient = PacketContainer.Instance.packetsClientValidation;
                ProxyClientManager.ValidationInstance.packetContainerServer = PacketContainer.Instance.packetsValidation;
                ProxyClientManager.ValidationInstance.packets = PacketContainer.Instance.packets3;
                ProxyClientManager.ValidationInstance.Start();
                while (!ProxyClientManager.ValidationInstance.StartNetwork(localport))
                    localport++;
                ProxyClientManager.ValidationInstance.ready = true;
                Global.clientMananger = (ClientManager)ProxyClientManager.LoginInstance;
            }
          return localport;
        }
        public void StartLoop()=>MainLoop.Start();
        public void AbortLoop() => MainLoop.Abort();
        static void loop()
        {
            while (true)
            {
                if (ProxyClientManager.ValidationInstance.ready)
                    ProxyClientManager.ValidationInstance.NetworkLoop(1);
                if (ProxyClientManager.LoginInstance.ready)
                    ProxyClientManager.LoginInstance.NetworkLoop(1);
                if (ProxyClientManager.MapInstance.ready)
                    ProxyClientManager.MapInstance.NetworkLoop(1);
                Thread.Sleep(1);
            }
        }
    }
}
