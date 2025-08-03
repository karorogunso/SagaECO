using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ENHANCE_RESULT : Packet
    {
        public SSMG_ITEM_ENHANCE_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13C6;
        }

        /// <summary>
        /// 01 SUCCESS
        /// 00 FAILD
        /// FF NO GOLD
        /// FE ITEM IS NOT EXISTS
        /// FD NO MERIATIAL
        /// FC ????
        /// FB EXCEPTION CAN NOT GET EXP
        /// </summary>

        public int Result
        {
            set
            {
                this.PutByte((byte)value, 2);
                this.PutUShort(0x64, 3);
            }
        }

        public ushort OrignalRefine
        {
            set { this.PutUShort(value, 5); }
        }

        public ushort ExpectedRefine
        {
            set { this.PutUShort(value, 7); }
        }
    }
}

