using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_DAILYDUNGEON_INFO : Packet
    {        
        public SSMG_DAILYDUNGEON_INFO()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1F77;
        }
        public uint RemainSecond
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public List<byte> IDs
        {
            set
            {
                PutByte((byte)value.Count, 6);
                for (int i = 0; i < value.Count; i++)
                {
                    PutByte(value[i], 7 + i);
                }
            }
        }
    }
}

