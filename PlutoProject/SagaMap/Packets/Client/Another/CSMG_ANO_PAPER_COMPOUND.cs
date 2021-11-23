using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ANO_PAPER_COMPOUND : Packet
    {
        public CSMG_ANO_PAPER_COMPOUND()
        {
            this.offset = 2;
        }
        public byte paperID
        {
            get
            {
                return GetByte(3);
            }
        }
        public uint SlotID
        {
            get
            {
                return GetUInt(4);
            }
        }
        public override SagaLib.Packet New()
        {
            return new CSMG_ANO_PAPER_COMPOUND();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAnoPaperCompound(this);
        }

    }
}