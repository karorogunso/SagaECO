using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_DETAIL_UPDATE : Packet
    {
        public SSMG_FRIEND_DETAIL_UPDATE()
        {
            this.data = new byte[12];
            this.ID = 0x00E3;
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public PC_JOB Job
        {
            set
            {
                this.PutUShort((ushort)value, 6);
            }
        }

        public byte Level
        {
            set
            {
                this.PutUShort(value, 8);
            }
        }

        public byte JobLevel
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }
    }
}

