using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaWorld;
using SagaWorld.Network.Client;

using SagaDB.Actor;

namespace SagaWorld.Packets.Client
{
    public class CSMG_CHAR_SELECT : Packet
    {
        public CSMG_CHAR_SELECT()
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
            return (SagaLib.Packet)new SagaWorld.Packets.Client.CSMG_CHAR_SELECT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((WorldClient)(client)).OnCharSelect(this);
        }

    }
}