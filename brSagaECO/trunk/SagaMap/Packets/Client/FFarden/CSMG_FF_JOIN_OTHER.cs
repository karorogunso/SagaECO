using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FFGARDEN_JOIN_OTHER : Packet
    {
        public CSMG_FFGARDEN_JOIN_OTHER()
        {
            this.offset = 2;
        }
        public uint ff_id
        {
            get
            {
                return this.GetUInt(2);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FFGARDEN_JOIN_OTHER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFFardenOtherJoin(this);
        }

    }
}