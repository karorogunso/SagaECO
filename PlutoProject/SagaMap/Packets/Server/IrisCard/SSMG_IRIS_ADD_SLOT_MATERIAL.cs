using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_ADD_SLOT_MATERIAL : Packet
    {
        public SSMG_IRIS_ADD_SLOT_MATERIAL()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x13E4;
        }

        public byte Slot
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public uint Material
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public int Gold
        {
            set
            {
                this.PutInt(value, 7);
            }
        }
    }
}

