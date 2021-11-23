using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_AAA_GROUP_IN : Packet
    {
        public SSMG_AAA_GROUP_IN()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x23E4;
        }

        public int GroupID
        {
            set
            {
            }
        }

        public byte Position
        {
            set
            {
            }
        }

        public string Name
        {
            set
            {
            }
        }

        public int CharID
        {
            set
            {
            }
        }

    }
}

