using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_EQUIPT : Packet
    {
        public CSMG_ITEM_EQUIPT()
        {
            this.offset = 2;
            this.data = new byte[8];
        }

        public uint InventoryID
        {
            set
            {
                this.PutUInt(value, 2);
            }
            get
            {
                return this.GetUInt(2);
            }
        }
        public byte EquipSlot
        {
            set
            {
                this.PutByte(value, 6);
            }
            get
            {
                return this.GetByte(6);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_EQUIPT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemEquipt(this);
        }

    }
}