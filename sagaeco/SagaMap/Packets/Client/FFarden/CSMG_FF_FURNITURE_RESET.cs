using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FF_FURNITURE_RESET : Packet
    {
        public CSMG_FF_FURNITURE_RESET()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(2);
            }
        }
        public byte Type
        {
            get
            {
                return this.GetByte(6);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FF_FURNITURE_RESET();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFFFurnitureReset(this);
        }

    }
}