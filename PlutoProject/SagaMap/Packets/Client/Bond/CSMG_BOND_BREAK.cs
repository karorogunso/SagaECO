using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_BOND_CANCEL : Packet
    {
        public CSMG_BOND_CANCEL()
        {
            this.offset = 2;
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_BOND_CANCEL();
        }
        public uint TargetCharID
        {
            get
            {
                return this.GetUInt(2);
            }
        }
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnBondBreak(this);
        }
    }
}