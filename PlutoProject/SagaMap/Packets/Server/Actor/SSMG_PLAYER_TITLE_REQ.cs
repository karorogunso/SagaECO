using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE_REQ : Packet
    {
        public SSMG_PLAYER_TITLE_REQ()
        {
            this.data = new byte[6];//8bytes unknowns
            this.offset = 2;
            this.ID = 0x241C;
        }

        public uint tID
        {
            set
            {
                PutUInt(value, 2);
            }
        }

        public void PutPrerequisite(List<ulong> prerequisite)
        {
            byte[] buf = new byte[15 + (prerequisite.Count) * 8];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.PutByte((byte)(prerequisite.Count), 6);
            this.offset = 7;
            foreach (ulong progress in prerequisite)
                this.PutULong(progress);
        }
    }
}
        
