using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_CHANGE_BATTLE_STATUS : Packet
    {
        public SSMG_SKILL_CHANGE_BATTLE_STATUS()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x0FA6;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Status
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
    }
}

