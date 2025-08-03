using SagaLib;
using System;

namespace SagaMap.Packets.Server
{
    public class SSMG_INFINITECORRIDOR_WARP_RESULT : Packet
    {
        public SSMG_INFINITECORRIDOR_WARP_RESULT()
        {
            this.data = new byte[4];
            this.ID = 0x2295;
            this.offset = 2;
        }
    }
}
