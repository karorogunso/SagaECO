using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE_PROGRESS : Packet
    {
        public SSMG_PLAYER_TITLE_PROGRESS()
        {
            this.data = new byte[15];
            this.offset = 2;
            this.ID = 0x241C;
        }

        public uint Title
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        List<ulong> prerequisite = new List<ulong>();

        public List<ulong> Prerequisite
        {
            set
            {
                this.prerequisite = value;
            }
            get
            {
                return prerequisite;
            }
        }

        public void PutPrerequisite()
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

