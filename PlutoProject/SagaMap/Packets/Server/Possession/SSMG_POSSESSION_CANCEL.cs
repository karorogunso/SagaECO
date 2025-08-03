using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_CANCEL : Packet
    {
        public SSMG_POSSESSION_CANCEL()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x1780;   
        }

        public uint FromID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ToID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public PossessionPosition Position
        {
            set
            {
                this.PutByte((byte)value, 10);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 11);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 12);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 13);
            }
        }
    }
}

