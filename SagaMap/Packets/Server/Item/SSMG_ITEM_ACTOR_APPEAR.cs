using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ACTOR_APPEAR : Packet
    {
        public SSMG_ITEM_ACTOR_APPEAR()
        {
            this.data = new byte[26];
            this.offset = 2;
            this.ID = 0x07D5;   
        }

        public ActorItem Item
        {
            set
            {
                this.PutUInt(value.ActorID, 2);
                this.PutUInt(value.Item.ItemID, 6);
                this.PutByte(Global.PosX16to8(value.X), 10);
                this.PutByte(Global.PosY16to8(value.Y), 11);
                this.PutUShort(value.Item.stack, 12);
                this.PutUInt(10, 14);//Unknown
                this.PutByte(0, 22);//type, possession item is 1, otherwise 0
                this.PutByte(0, 23);//possession comment
                this.PutByte(value.Item.identified, 24);
            }
        }
    }
}

