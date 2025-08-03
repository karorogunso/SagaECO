using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_INFO : Packet
    {
        public SSMG_STAMP_INFO()
        {
            this.data = new byte[29];
            this.offset = 2;
            this.ID = 0x1BBC;
        }

        int page;
        public int Page
        {
            get { return page; }
            set
            {
                this.PutInt(value, 2);
                page = value;
            }
        }

        public Stamp Stamp
        {
            set
            {
                this.PutByte(0x0B, 6);
                if (Page == 0)
                    for (int i=0;i<10; i++)
                        this.PutShort((short)value[(StampGenre)i].Value);
                else if (Page == 1)
                    for (int i = 11; i < 21; i++)
                        this.PutShort((short)value[(StampGenre)i].Value);
            }
        }
    }
}
