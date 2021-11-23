using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_INFO_BASIC : Packet
    {
        public SSMG_PARTNER_INFO_BASIC()
        {
            this.data = new byte[45];
            this.offset = 2;
            this.ID = 0x217A;
            PutByte(6, 30);
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte Level
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
        public uint EXPPercentage
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }

        public byte Rebirth
        {
            set
            {
                this.PutByte(value, 11);
            }
        }
        public byte Rank//1perbarslot 10per rank level at least 1
        {
            set
            {
                this.PutByte(value, 12);
            }
        }
        public byte ReliabilityColor//max is 9,more will collapse the client at least 0
        {
            set
            {
                this.PutByte(value, 13);
            }
        }
        public ushort ReliabilityUpRate
        {
            set
            {
                this.PutUShort(value, 14);
            }
        }
        /// <summary>
        /// seconds
        /// </summary>
        public uint NextFeedTime
        {
            set
            {
                this.PutUInt(value, 16);
            }
        }
        public byte AIMode
        {
            set
            {
                this.PutByte(value, 20);
            }
        }
        public uint MaxNextFeedTime//infinity feed time to show --:--
        {
            set
            {
                this.PutUInt(value, 21);
            }
        }
        /// <summary>
        /// 0 for 1 sheet, 1 for 2 sheet
        /// </summary>
        public byte CustomAISheet
        {
            set
            {
                this.PutByte(value, 25);
            }
        }
        public byte AICommandCount1
        {
            set
            {
                this.PutByte(value, 26);
            }
        }
        public byte AICommandCount2
        {
            set
            {
                this.PutByte(value, 27);
            }
        }
        public ushort PerkPoint
        {
            set
            {
                this.PutUShort(value, 28);
            }
        }
        public byte PerkListCount
        {
            set
            {
                this.PutByte(6, 30);
            }
        }
        public byte Perk0
        {
            set
            {
                this.PutByte(value, 31);
            }
        }
        public byte Perk1
        {
            set
            {
                this.PutByte(value, 32);
            }
        }
        public byte Perk2
        {
            set
            {
                this.PutByte(value, 33);
            }
        }
        public byte Perk3
        {
            set
            {
                this.PutByte(value, 34);
            }
        }
        public byte Perk4
        {
            set
            {
                this.PutByte(value, 35);
            }
        }
        public byte Perk5
        {
            set
            {
                this.PutByte(value, 36);
            }
        }
        public uint WeaponID
        {
            set
            {
                this.PutUInt(value, 37);
            }
        }
        public uint ArmorID
        {
            set
            {
                this.PutUInt(value, 41);
            }
        }
    }
}
        
