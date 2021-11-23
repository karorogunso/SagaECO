using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_LOCK_VIEWANGLE : Packet
    {
        public SSMG_NPC_LOCK_VIEWANGLE()
        {
            this.data = new byte[23];
            this.offset = 2;
            this.ID = 0x0619;
        }

        public short X
        {
            set
            {
                this.PutShort(value, 2);
            }
        }

        public short Y
        {
            set
            {
                this.PutShort(value, 4);
            }
        }
        public short Z
        {
            set
            {
                this.PutShort(value, 6);
            }
        }
        public short Xdir
        {
            set
            {
                this.PutShort(value, 8);
            }
        }
        public short Ydir
        {
            set
            {
                this.PutShort(value, 10);
            }
        }
        public ushort CameraMoveSpeed
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }
        public short Param
        {
            set
            {
                this.PutShort(1, 13);
            }
        }
    }
}

