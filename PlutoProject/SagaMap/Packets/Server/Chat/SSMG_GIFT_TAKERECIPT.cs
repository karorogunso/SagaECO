using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;
using SagaDB.BBS;

namespace SagaMap.Packets.Server
{
    public class SSMG_GIFT_TAKERECIPT : Packet
    {
        public SSMG_GIFT_TAKERECIPT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0691;   
        }
        public byte type
        {
            set
            {
                PutByte(value, 5);
            }
        }
        public uint MailID
        {
            set
            {
                PutUInt(value, 6);
            }
        }
    }
}

