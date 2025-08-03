using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOWPICT_LOCATION : Packet
    {
        public SSMG_NPC_SHOWPICT_LOCATION()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x05E0;
        }

        public uint NPCID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        
        public byte X
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 7);
            }
        }
        public byte Dir
        {
            set
            {
                this.PutByte(value, 8);
            }
        }
    }
}

