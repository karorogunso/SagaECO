using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_LOGIN_FINISHED : Packet
    {
       
        public SSMG_LOGIN_FINISHED()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1B67;

        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

    }
}

