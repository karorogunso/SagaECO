using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAR_SLOT : Packet
    {
        public CSMG_CHAR_SLOT()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAR_SLOT();
        }

        public byte Slot
        {
            get
            {
                return this.GetByte(6);
            }
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnCharSlot(this);
        }

    }
}