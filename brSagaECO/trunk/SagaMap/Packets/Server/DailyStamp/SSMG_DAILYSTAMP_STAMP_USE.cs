using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_DAILYSTAMP_STAMP_USE:Packet
    {
        //predicted
        public SSMG_DAILYSTAMP_STAMP_USE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x1F72;
        }

        public byte Slot
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}
