using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaWorld;
using SagaWorld.Network.Client;

using SagaDB.Actor;

namespace SagaWorld.Packets.Client
{
    public class CSMG_CHAR_STATUS : Packet
    {
        public CSMG_CHAR_STATUS()
        {
            this.offset = 2;
        }

        public byte Slot
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaWorld.Packets.Client.CSMG_CHAR_STATUS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((WorldClient)(client)).OnCharStatus(this);
        }

    }
}