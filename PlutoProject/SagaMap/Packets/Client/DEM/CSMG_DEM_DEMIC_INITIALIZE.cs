using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DEM_DEMIC_INITIALIZE : Packet
    {
        public CSMG_DEM_DEMIC_INITIALIZE()
        {
            this.offset = 2;
        }

        public byte Page
        {
            get
            {
                return GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_DEMIC_INITIALIZE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMDemicInitialize(this);
        }

    }
}