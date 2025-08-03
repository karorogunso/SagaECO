using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_EXCHANGE_WINDOW_OPEN : Packet
    {
        public SSMG_ITEM_EXCHANGE_WINDOW_OPEN()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x060E;
        }

        public int SetWindowType
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}
