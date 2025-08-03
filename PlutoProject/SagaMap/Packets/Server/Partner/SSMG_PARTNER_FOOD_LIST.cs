using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_FOOD_LIST : Packet
    {
        private byte foodlistlen = 20;
        public SSMG_PARTNER_FOOD_LIST()
        {
            this.data = new byte[3 + 4 * foodlistlen];
            this.offset = 2;
            this.ID = 0x2198;
            this.PutByte(foodlistlen, 2);
        }
        public List<uint> FoodItemList
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i], 3 + i * 4);
                }
            }
        }
    }
}
        
