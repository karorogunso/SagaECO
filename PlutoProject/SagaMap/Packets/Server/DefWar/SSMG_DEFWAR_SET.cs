using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.DefWar;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEFWAR_SET : Packet
    {

        public SSMG_DEFWAR_SET()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x1BD1;
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public DefWar Data
        {
            set
            {
                this.PutByte(value.Number, 2);
                this.PutUInt(value.ID, 3);
                this.PutByte(value.Result1, 7);
                this.PutByte(value.Result2, 8);
                this.PutByte(value.unknown1, 9);
            }
        }
    }
}
