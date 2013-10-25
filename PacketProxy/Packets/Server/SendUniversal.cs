using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace PacketProxy.Packets.Server
{
    [Serializable]
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class SendUniversal : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public SendUniversal()
        {
            this.size = 8;
            this.offset = 8;
        }

        public override bool isStaticSize()
        {
            return false;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new PacketProxy.Packets.Server.SendUniversal();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ServerSession)(client)).OnSendUniversal(this);
        }

    }
}
