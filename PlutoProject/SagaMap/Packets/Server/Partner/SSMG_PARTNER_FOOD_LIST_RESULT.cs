using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_FOOD_LIST_RESULT : Packet
    {
        public SSMG_PARTNER_FOOD_LIST_RESULT()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x219A;
        }
        /// <summary>
        /// Careful!ClientShownList not equals the real List!
        /// </summary>
        public byte FoodSlot
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        /// <summary>
        /// 1 for in, 0 for out
        /// </summary>
        public byte MoveType
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

        public uint FoodItemID
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }

    }
}
        
