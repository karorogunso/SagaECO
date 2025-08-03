using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_IRIS_ADD_SLOT_CONFIRM : Packet
    {
        public CSMG_IRIS_ADD_SLOT_CONFIRM()
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

        public uint Material
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public uint SupportItem
        {
            get
            {
                return this.GetUInt(10);
            }
        }

        public uint ProtectItem
        {
            get
            {
                return this.GetUInt(14);
            }
        }

        public byte BaseLevel
        {
            get
            {
                return this.GetByte(18);
            }
        }

        public byte JobLevel
        {
            get
            {
                return this.GetByte(19);
            }
        }

        public ushort ExpRate
        {
            get
            {
                return this.GetUShort(20);
            }
        }

        public ushort JExpRate
        {
            get
            {
                return this.GetUShort(22);
            }
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_IRIS_ADD_SLOT_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnIrisAddSlotConfirm(this);
        }

    }
}