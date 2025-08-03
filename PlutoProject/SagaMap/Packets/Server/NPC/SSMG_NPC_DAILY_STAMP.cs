using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_DAILY_STAMP : Packet
    {
        public SSMG_NPC_DAILY_STAMP()
        {
            this.data = new byte[9];//470以后为9  1F 75 00 00 00 00 00 0A 02
            this.offset = 2;
            this.ID = 0x1F75;
        }

        /*public uint StampCount
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }*/

        public byte StampCount
        {
            set
            {
                this.PutByte(value,7);
            }
        }

        public byte Type
        {
            set
            {
                this.PutByte(value, 8);//470以后偏移 10
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

