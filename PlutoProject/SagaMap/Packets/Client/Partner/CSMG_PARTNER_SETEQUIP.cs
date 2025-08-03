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
    public class CSMG_PARTNER_SETEQUIP : Packet
    {
        public CSMG_PARTNER_SETEQUIP()
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
        /// <summary>
        /// postivie slot id to equip, -1 to unequip
        /// </summary>
        public uint EquipItemInventorySlot
        {
            get
            {
                return this.GetUInt(6);
            }
            set
            {
                this.PutUInt(value, 6);
            }
        }
        /// <summary>
        /// 0 for weapon, 1 for costume
        /// </summary>
        public EnumPartnerEquipSlot PartnerEquipSlot
        {
            get
            {
                byte peqslot=this.GetByte(10);
                if (peqslot==0)
                {
                    return EnumPartnerEquipSlot.WEAPON;
                }
                else
                {
                    return EnumPartnerEquipSlot.COSTUME;
                }
                
            }
            set
            {
                if(value == EnumPartnerEquipSlot.WEAPON)
                {
                    this.PutByte(0, 10);
                }
                else
                {
                    this.PutByte(1, 10);
                }
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTNER_SETEQUIP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerItemEquipt(this);
        }

    }
}