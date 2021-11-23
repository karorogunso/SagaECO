using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_EVENT_START : Packet
    {
        public CSMG_NPC_EVENT_START()
        {
            this.offset = 2;
        }

        public uint EventID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public byte X
        {
            get
            {
                return this.GetByte(6);
            }
        }

        public byte Y
        {
            get
            {
                return this.GetByte(7);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_EVENT_START();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCEventStart(this);
        }

    }
}