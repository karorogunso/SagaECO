using SagaLib;
using System;

namespace SagaMap.Packets.Server
{
    public class SSMG_MOSTERGUIDE_NEW_RECORD : Packet
    {
        public SSMG_MOSTERGUIDE_NEW_RECORD()
        {
            this.data = new byte[6];
            this.ID = 0x2289;
            this.offset = 2;
        }
        public short guideID
        {
            set
            {
                this.PutShort(value, 4);
            }
        }
    }
}
