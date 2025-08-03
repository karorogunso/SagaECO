using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SET_EVENT_AREA : Packet
    {
        public SSMG_NPC_SET_EVENT_AREA()
        {
            this.data = new byte[27];
            this.offset = 2;
            this.ID = 0x0C80;
        }

        public uint EventID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint StartX
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint StartY
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public uint EndX
        {
            set
            {
                this.PutUInt(value, 14);
            }
        }

        public uint EndY
        {
            set
            {
                this.PutUInt(value, 18);
            }
        }

        public uint EffectID
        {
            set
            {
                this.PutUInt(value, 22);
            }
        }
    }
}

