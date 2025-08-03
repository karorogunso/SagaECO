using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PPROTECT_LIST_OPEN : Packet
    {
        public CSMG_PPROTECT_LIST_OPEN()
        {
            this.offset = 2;
        }

        public ushort Page
        {
            get
            {
                return this.GetUShort(2);
            }
        }

        public int Search
        {
            get
            {
                return this.GetInt(4);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PPROTECT_LIST_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPProtectListOpen(this);
        }

    }
}