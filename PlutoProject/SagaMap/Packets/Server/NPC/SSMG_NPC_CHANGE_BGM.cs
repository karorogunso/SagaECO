using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_CHANGE_BGM : Packet
    {
        public SSMG_NPC_CHANGE_BGM()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x05EC;
        }

        public uint SoundID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Loop
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public uint Volume
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }

        public byte Balance
        {
            set
            {
                //this.PutByte(value, 11);
            }
        }
    }
}

