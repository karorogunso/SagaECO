using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_GACHA_RESULT : Packet
    {
        public SSMG_IRIS_GACHA_RESULT()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x1DDA;
        }
        public List<uint> ItemIDs
        {
            set
            {
                data = new byte[19 + value.Count * 4];
                ID = 0x1DDA;
                PutByte((byte)value.Count, 18);
                for (int i = 0; i < value.Count; i++)
                    PutUInt(value[i], 19 + i * 4);
            }
        }
    }
}

