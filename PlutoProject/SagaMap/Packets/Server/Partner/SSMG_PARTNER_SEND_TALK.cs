using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_SEND_TALK : Packet
    {
        public SSMG_PARTNER_SEND_TALK()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x219f;
        }

        public uint PartnerID
        {
            set { this.PutUInt(value, 2); }
        }

        public uint Parturn
        {
            set { this.PutUInt(value, 6); }
        }
    }
}
