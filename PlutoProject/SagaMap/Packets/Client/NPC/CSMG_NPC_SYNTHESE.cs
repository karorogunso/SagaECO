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

        public Dictionary<uint,ushort> SynIDs
        {
            get
            {
                byte count = GetByte(2);
                Dictionary<uint, ushort> ids = new Dictionary<uint, ushort>();
                for (int i = 0; i < count; i++)
                {
                    uint id = GetUInt((ushort)(3 + i * 4));
                    ushort c = GetUShort((ushort)(5 + count * 8 + i * 2));
                    ids.Add(id,c);
                }
                return ids;
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