using SagaLib;
using System;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_OPEN_REBIRTHREWARD_WINDOW : Packet
    {
        public SSMG_PLAYER_OPEN_REBIRTHREWARD_WINDOW()
        {
            this.data = new byte[0x0e];
            this.ID = 0x1edd;
            this.offset = 2;
        }

        public byte SetOpen
        {
            set { this.PutByte(value, 3); }
        }
    }
}
