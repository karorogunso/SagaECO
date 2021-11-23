using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;


namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_CHANGE : Packet
    {
        public CSMG_ITEM_CHANGE()
        {
            this.offset = 2;
        }
        public uint ChangeID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public uint InventorySlot
        {
            get
            {
                return this.GetUInt(7);
            }
        }
        /*
        public List<uint> SlotList()
        {
            List<uint> list = new List<uint>();
            byte count = GetByte(6);
            for (int i = 0; i < count; i++)
            {
                list.Add(GetUInt((ushort)(7 + (i * 4))));
            }
            return list;
        }
        */
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_CHANGE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemChange(this);
        }
    }
}
