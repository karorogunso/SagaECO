using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaDB.DEMIC;

namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_CHIP_SHOP_DATA : Packet
    {
        public SSMG_DEM_CHIP_SHOP_DATA()
        {
            this.data = new byte[15];
            this.offset = 2;
            this.ID = 0x063A;            
        }

        public uint EXP
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint JEXP
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public string Description
        {
            set
            {
                byte[] comment = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[15 + comment.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)comment.Length, 14);
                this.PutBytes(comment, 15);
            }
        }
    }
}

