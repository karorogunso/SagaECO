using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Tamaire;
using SagaLib;
using SagaDB.ECOShop;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_TAMAIRE_RENTAL : Packet
    {
        public SSMG_TAMAIRE_RENTAL()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x22B3;
        }

        public TimeSpan RentalDue
        {
            set
            {
                this.PutInt((int)(value.TotalSeconds),3);
            }
        }

        public short Factor
        {
            set
            {
                this.PutShort(value, 7);
            }
        }

        public byte JobType
        {
            set
            {
                this.PutByte(value,9);
            }
        }
    }
}
