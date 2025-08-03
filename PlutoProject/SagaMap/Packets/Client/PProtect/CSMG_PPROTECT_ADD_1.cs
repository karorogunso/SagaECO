using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PPROTECT_ADD_1 : Packet
    {
        public CSMG_PPROTECT_ADD_1()
        {
            this.offset = 2;
        }

        public uint PPID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PPROTECT_ADD_1();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPProtectADD1(this);
        }

    }
}