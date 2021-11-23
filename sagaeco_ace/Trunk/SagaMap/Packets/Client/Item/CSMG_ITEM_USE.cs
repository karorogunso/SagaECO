using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_USE : Packet
    {
        public CSMG_ITEM_USE()
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

        public uint ActorID
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public byte X
        {
            get
            {
                return this.GetByte(10);
            }
        }

        public byte Y
        {
            get
            {
                return this.GetByte(11);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_USE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemUse(this);
        }

    }
}