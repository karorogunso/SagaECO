using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_INFO : Packet
    {
        public SSMG_STAMP_INFO()
        {
            this.data = new byte[25];
            this.offset = 2;
            this.ID = 0x1BBC;
            this.PutByte(0x0b, 2);
        }

        public Stamp Stamp
        {
            set
            {
                this.PutShort((short)value[StampGenre.Special].Value, 3);
                this.PutShort((short)value[StampGenre.Pururu].Value, 5);
                this.PutShort((short)value[StampGenre.Field].Value, 7);
                this.PutShort((short)value[StampGenre.Coast].Value, 9);
                this.PutShort((short)value[StampGenre.Wild].Value, 11);
                this.PutShort((short)value[StampGenre.Cave].Value, 13);
                this.PutShort((short)value[StampGenre.Snow].Value, 15);
                this.PutShort((short)value[StampGenre.Colliery].Value, 17);
                this.PutShort((short)value[StampGenre.Northan].Value, 19);
                this.PutShort((short)value[StampGenre.IronSouth].Value, 21);
                this.PutShort((short)value[StampGenre.SouthDungeon].Value, 23);
            }
        }
    }
}
