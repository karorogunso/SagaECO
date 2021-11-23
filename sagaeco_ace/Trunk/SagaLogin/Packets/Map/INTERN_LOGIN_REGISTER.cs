using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Map
{
    public class INTERN_LOGIN_REGISTER : Packet
    {
        public INTERN_LOGIN_REGISTER()
        {
            this.offset = 2;
        }

        public Manager.MapServer MapServer
        {
            get
            {
                Manager.MapServer server = new SagaLogin.Manager.MapServer();
                byte size = this.GetByte(2);
                byte[] buf = new byte[size];
                ushort offset;
                buf = this.GetBytes(size, 3);
                server.Password = Global.Unicode.GetString(buf);

                offset = (ushort)(3 + size);

                byte size2 = this.GetByte(offset);
                buf = new byte[size2];
                buf = this.GetBytes(size2, (ushort)(offset + 1));
                server.IP = Global.Unicode.GetString(buf);
                
                offset = (ushort)(4 + size +size2);

                server.port = this.GetInt(offset);
                size = this.GetByte((ushort)(offset + 4));
                for (int i = 0; i < size; i++)
                {
                    server.HostedMaps.Add(this.GetUInt((ushort)(offset + 5 + i * 4)));
                }
                return server;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Map.INTERN_LOGIN_REGISTER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnInternMapRegister(this);
        }

    }
}