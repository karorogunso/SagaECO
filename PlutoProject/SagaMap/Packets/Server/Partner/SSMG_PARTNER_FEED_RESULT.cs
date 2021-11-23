using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_FEED_RESULT : Packet
    {
        //should be sent when the next feed is available.
        public SSMG_PARTNER_FEED_RESULT()
        {
            this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x219C;
        }
        /// <summary>
        /// 0 for no message shown, 1 for message shown
        /// </summary>
        public byte MessageSwitch
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }
        public uint FoodItemID
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
        /// <summary>
        /// 100=1.00
        /// </summary>
        public ushort ReliabilityUpRate
        {
            set
            {
                this.PutUShort(value, 11);
            }
        }
        /// <summary>
        /// seconds
        /// </summary>
        public uint NextFeedTime
        {
            set
            {
                this.PutUInt(value, 13);
            }
        }
        public byte PartnerRank
        {
            set
            {
                this.PutByte(value, 17);
            }
        }
    }
}
        
