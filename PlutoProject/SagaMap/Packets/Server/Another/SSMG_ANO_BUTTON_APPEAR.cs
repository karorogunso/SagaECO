using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_DIALOG_BOX : Packet
    {
        public SSMG_ANO_DIALOG_BOX()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1FB4;
        }

        public ushort DID
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }
    }
}

