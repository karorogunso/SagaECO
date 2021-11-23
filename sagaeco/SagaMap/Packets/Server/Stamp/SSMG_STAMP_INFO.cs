
using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_INFO : Packet
    {
        public SSMG_STAMP_INFO()
        {
            this.data = new byte[29];
            this.offset = 2;
            this.ID = 0x1BBC;
            this.PutByte(0x0b, 2+4);
        }
        public uint Page
        {
            set
            {
                this.PutUInt((byte)value, 2);
            }
        }
        public Stamp Stamp
        {
            set
            {
                this.PutShort((short)value[StampGenre.Special].Value, 3+4);
                this.PutShort((short)value[StampGenre.Pururu].Value, 5+4);
                this.PutShort((short)value[StampGenre.Field].Value, 7+4);
                this.PutShort((short)value[StampGenre.Coast].Value, 9+4);
                this.PutShort((short)value[StampGenre.Wild].Value, 11+4);
                this.PutShort((short)value[StampGenre.Cave].Value, 13+4);
                this.PutShort((short)value[StampGenre.Snow].Value, 15+4);
                this.PutShort((short)value[StampGenre.Colliery].Value, 17+4);
                this.PutShort((short)value[StampGenre.Northan].Value, 19+4);
                this.PutShort((short)value[StampGenre.IronSouth].Value, 21+4);
                this.PutShort((short)value[StampGenre.SouthDungeon].Value, 23+4);
            }
        }
    }
}
