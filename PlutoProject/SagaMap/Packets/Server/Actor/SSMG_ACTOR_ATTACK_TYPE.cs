using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_ATTACK_TYPE : Packet
    {
        public SSMG_ACTOR_ATTACK_TYPE()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x0FBF;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ATTACK_TYPE AttackType
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }
    }
}

