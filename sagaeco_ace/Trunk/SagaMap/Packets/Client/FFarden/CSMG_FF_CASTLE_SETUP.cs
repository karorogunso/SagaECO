using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FF_CASTLE_SETUP : Packet
    {
        public CSMG_FF_CASTLE_SETUP()
        {
            this.offset = 2;
        }

        public uint InventorySlot
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public short X
        {
            get
            {
                return GetShort(6);
            }
        }

        public short Z
        {
            get
            {
                return GetShort(8);
            }
        }
        public short Yaxis
        {
            get
            {
                return GetShort(10);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FF_CASTLE_SETUP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFFurnitureCastleSetup(this);
        }

    }
}