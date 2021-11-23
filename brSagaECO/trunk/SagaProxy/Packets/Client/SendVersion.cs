using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;

namespace SagaProxy.Packets.Client
{
    [Serializable]
    public class SendVersion : Packet
    {
        public SendVersion()
        {
            this.size = 10;
            this.offset = 2;
        }

        public string GetVersion()
        {
            byte[] buf = this.GetBytes(6, 4);
            return Conversions.bytes2HexString(buf);
        }
        public override Packet New()
        {
            return (SagaLib.Packet)new SagaProxy.Packets.Client.SendVersion();
        }
        public override void Parse(SagaLib.Client client)
        {
            ((ProxyClient)(client)).OnRedirectVersion(this);
        }
    }
}
