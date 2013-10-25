using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_JOB : Packet
    {
        public SSMG_PLAYER_JOB()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x023A;
        }

        public PC_JOB Job
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }

    }
}
        
