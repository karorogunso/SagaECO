using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_DUALJOB_SET_DUALJOB_INFO : Packet
    {
        public SSMG_DUALJOB_SET_DUALJOB_INFO()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x22D1;
        }

        public bool Result
        {
            set
            {
                if (value)
                    this.PutByte(0x00);
                else
                    this.PutByte(0x01);
            }
        }
        public byte RetType
        {
            set
            {
                this.PutByte(value);
            }
        }
    }
}
