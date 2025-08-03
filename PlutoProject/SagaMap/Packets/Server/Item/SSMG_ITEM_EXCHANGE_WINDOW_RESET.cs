using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_EXCHANGE_WINDOW_RESET : Packet
    {
        public SSMG_ITEM_EXCHANGE_WINDOW_RESET()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0610;
        }
    }
}
