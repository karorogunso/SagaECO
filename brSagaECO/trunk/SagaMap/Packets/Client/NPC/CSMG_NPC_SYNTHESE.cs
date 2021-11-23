using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_SYNTHESE : Packet
    {
        public CSMG_NPC_SYNTHESE()
        {
            this.offset = 2;
        }

        public uint SynID
        {
            get
            {
                return this.GetUInt(3);
            }
        }

        public ushort Count
        {
            get
            {
                return this.GetUShort(13);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_SYNTHESE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCSynthese(this);
        }

    }
}