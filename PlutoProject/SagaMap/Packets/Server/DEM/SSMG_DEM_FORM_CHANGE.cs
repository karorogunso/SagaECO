using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_FORM_CHANGE : Packet
    {
        public SSMG_DEM_FORM_CHANGE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x1E7E;
        }

        public DEM_FORM Form
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}

