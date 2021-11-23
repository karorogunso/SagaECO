using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Partner;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_EQUIP_RESULT : Packet
    {
        public SSMG_PARTNER_EQUIP_RESULT()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x218B;
        }
        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint EquipItemID
        {
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
            set
            {
                if (value== EnumPartnerEquipSlot.WEAPON)
                {
                    this.PutByte(0, 10);
                }
                else
                {
                    this.PutByte(1, 10);
                }
                
            }
        }
        /// <summary>
        /// 0 for in, 1 for out
        /// </summary>
        public byte MoveType
        {
            set
            {
                this.PutByte(value, 11);
            }
        }   
    }
}
        
