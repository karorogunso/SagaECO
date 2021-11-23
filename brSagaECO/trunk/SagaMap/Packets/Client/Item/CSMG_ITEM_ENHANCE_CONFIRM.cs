using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_ENHANCE_CONFIRM : Packet
    {
        public CSMG_ITEM_ENHANCE_CONFIRM()
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

        public uint ItemID
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public ushort Amount
        {
            get
            {
                return this.GetUShort(10);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_ENHANCE_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            //((MapClient)(client)).OnItemEnhanceConfirm(this);
        }

    }
}