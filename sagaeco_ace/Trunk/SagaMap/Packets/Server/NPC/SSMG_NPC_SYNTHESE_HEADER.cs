using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SYNTHESE_NEWINFO : Packet
    {
        public SSMG_NPC_SYNTHESE_NEWINFO()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x13B5;
        }

        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }

        public byte SkillLevel
        {
            set
            {
                this.PutByte(value, 4);
            }
        }
        public byte Unknown1
        {
            set
            {
                this.PutByte(value, 5);
            }
        }
        public byte Unknown2
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
    }
}

