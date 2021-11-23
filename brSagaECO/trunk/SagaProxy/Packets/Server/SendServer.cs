using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;
using System.Net;

namespace SagaProxy.Packets.Server
{
    public class SendServer: Packet
    {
        public SendServer()
        {
            this.offset = 2;
        }
        public override bool isStaticSize()
        {
            return false;
        }

        public override Packet New()
        {
            return (SagaLib.Packet)new SagaProxy.Packets.Server.SendServer();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((GameServerSession)(client)).OnRedirectServer(this);
        }
    }
}
