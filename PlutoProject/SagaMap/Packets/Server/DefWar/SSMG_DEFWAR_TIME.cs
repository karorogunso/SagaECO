using SagaLib;
using SagaDB.DefWar;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_DEFWAR_TIME : Packet
    {
        public SSMG_DEFWAR_TIME()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1BCD;
        }


        public uint Time
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}
