using SagaLib;
using System;

namespace SagaMap.Packets.Server
{
    public class SSMG_INFINITECORRIDOR_WARP_SET : Packet
    {
        public SSMG_INFINITECORRIDOR_WARP_SET()
        {
            this.data = new byte[12];
            this.ID = 0x2292;
            this.offset = 2;
        }
    }
}
