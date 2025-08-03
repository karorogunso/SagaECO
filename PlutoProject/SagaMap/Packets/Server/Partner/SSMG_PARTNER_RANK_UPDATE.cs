using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_RANK_UPDATE : Packet
    {
        public SSMG_PARTNER_RANK_UPDATE()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x2192;
        }
        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte PartnerRank
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
        public ushort PerkPoint
        {
            set
            {
                this.PutUShort(value, 7);
            }
        }   
    }
}
        
