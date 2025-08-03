using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FF_UNIT_APPEAR : Packet
    {
        public SSMG_FF_UNIT_APPEAR()
        {
            this.data = new byte[29];
            this.offset = 2;
                this.ID = 0x20D1;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint PictID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
        public short unknown
        {
            set
            {
                this.PutShort(8, 14);
            }
        }
        public short X
        {
            set
            {
                this.PutShort(value, 16);
            }
        }
        public short Z
        {
            set
            {
                this.PutShort(value, 18);
            }
        }
        public short Yaxis
        {
            set
            {
                this.PutShort(value, 20);
            }
        }
        public short unknown2
        {
            set
            {
                this.PutShort(186, 22);
            }
        }
        public uint unknown3
        {
            set
            {
                this.PutUInt(0, 24);
            }
        }


        public byte unknown4
        {
            set
            {
                this.PutByte(2, 28);
            }
        }
    }
}

