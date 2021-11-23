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
