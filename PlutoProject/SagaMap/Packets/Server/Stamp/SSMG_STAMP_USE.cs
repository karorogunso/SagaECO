using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_USE : Packet
    {
        public SSMG_STAMP_USE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x1BC1;
        }

        public uint Page
        {
            set
            {
                this.PutUInt((byte)value, 2);
            }
        }
        public StampGenre Genre
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }

        public StampSlot slot
        {
            set
            {
                switch (value)
                {
                    case StampSlot.One:
                        this.PutByte((byte)0, 7);
                        break;
                    case StampSlot.Two:
                        this.PutByte((byte)1, 7);
                        break;
                    case StampSlot.Three:
                        this.PutByte((byte)2, 7);
                        break;
                    case StampSlot.Four:
                        this.PutByte((byte)3, 7);
                        break;
                    case StampSlot.Five:
                        this.PutByte((byte)4, 7);
                        break;
                    case StampSlot.Six:
                        this.PutByte((byte)5, 7);
                        break;
                    case StampSlot.Seven:
                        this.PutByte((byte)6, 7);
                        break;
                    case StampSlot.Eight:
                        this.PutByte((byte)7, 7);
                        break;
                    case StampSlot.Nine:
                        this.PutByte((byte)8, 7);
                        break;
                    case StampSlot.Ten:
                        this.PutByte((byte)9, 7);
                        break;
                }
            }
        }
    }
}