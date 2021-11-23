using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_BOND_REQUEST_FROM_MASTER : Packet
    {
        public CSMG_BOND_REQUEST_FROM_MASTER()
        {
            this.offset = 2;
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_BOND_REQUEST_FROM_MASTER();
        }
        public uint CharID
        {
            get
            {
                return this.GetUInt(2);
            }
        }
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnBondRequestFromMaster(this);
        }

    }
}