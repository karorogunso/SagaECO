using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_MOVE : Packet
    {
        public CSMG_ITEM_MOVE()
        {
            this.offset = 2;
        }

        public uint InventoryID
        {
            get
            {
                return this.GetUInt(2);
            }
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ContainerType Target
        {
            get
            {
                return (ContainerType)this.GetByte(6);
            }
            set
            {
                this.PutByte((byte)value, 6);
            }
        }

        public ushort Count
        {
            get
            {
                return this.GetUShort(7);
            }
            set
            {
                this.PutUShort(value, 7);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemMove(this);
        }

    }
}