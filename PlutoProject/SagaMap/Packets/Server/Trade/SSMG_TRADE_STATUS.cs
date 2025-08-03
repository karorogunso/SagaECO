using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_TRADE_STATUS : Packet
    {        
        public SSMG_TRADE_STATUS()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0A19;
            this.PutByte(1, 2);
            this.PutByte(1, 4);
        }

        public bool Confirm
        {
            set
            {
                if (value == true)
                {
                    this.PutByte(0, 3);
                }
                else
                {
                    this.PutByte(0xFF, 3);
                }
            }
        }

        public bool Perform
        {
            set
            {
                if (value == true)
                {
                    this.PutByte(0, 5);
                }
                else
                {
                    this.PutByte(0xFF, 5);
                }
            }
        }
    }
}

