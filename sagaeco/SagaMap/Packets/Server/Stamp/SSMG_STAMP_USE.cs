
using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_USE : Packet
    {
        public SSMG_STAMP_USE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x1BC1;
        }

        public uint Page
        {
            set
            {
                this.PutUInt((byte)value, 2);
            }
        }
        public StampGenre Genre
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }

        public byte Slot
        {
            set
            {
                this.PutByte(value, 7);
            }
        }
    }
}