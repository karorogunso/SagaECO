using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Partner;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTNER_PERK_CONFIRM : Packet
    {
        public CSMG_PARTNER_PERK_CONFIRM()
        {
            this.offset = 2;
        }

        public uint PartnerInventorySlot
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
        public byte unknown0
        {
            get
            {
                return this.GetByte(6);
            }
        }

        public byte PerkListLength
        {
            get
            {
                return this.GetByte(7);
            }
            set
            {
                this.PutByte(value, 7);
            }
        }
        public byte Perk0
        {
            get
            {
                return this.GetByte(8);
            }
            set
            {
                this.PutByte(value, 8);
            }
        }
        public byte Perk1
        {
            get
            {
                return this.GetByte(9);
            }
            set
            {
                this.PutByte(value, 9);
            }
        }
        public byte Perk2
        {
            get
            {
                return this.GetByte(10);
            }
            set
            {
                this.PutByte(value, 10);
            }
        }
        public byte Perk3
        {
            get
            {
                return this.GetByte(11);
            }
            set
            {
                this.PutByte(value, 11);
            }
        }
        public byte Perk4
        {
            get
            {
                return this.GetByte(12);
            }
            set
            {
                this.PutByte(value, 12);
            }
        }
        public byte Perk5
        {
            get
            {
                return this.GetByte(13);
            }
            set
            {
                this.PutByte(value, 13);
            }
        }

        public override Packet New()
        {
            return (Packet)new CSMG_PARTNER_PERK_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerPerkConfirm(this);
        }

    }
}