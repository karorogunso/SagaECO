using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ANO_PAPER_USE : Packet
    {
        public CSMG_ANO_PAPER_USE()
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
        public byte index
        {
            get
            {
                return GetByte(4);
            }
        }
        public uint slotID
        {
            get
            {
                return GetUInt(5);
            }
        }
        public override SagaLib.Packet New()
        {
            return new CSMG_ANO_PAPER_USE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAnoPaperUse(this);
        }

    }
}