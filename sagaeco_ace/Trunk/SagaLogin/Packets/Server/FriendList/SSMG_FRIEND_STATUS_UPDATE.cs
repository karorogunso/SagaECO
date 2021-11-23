using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_STATUS_UPDATE : Packet
    {
        public SSMG_FRIEND_STATUS_UPDATE()
        {
            this.data = new byte[7];
            this.ID = 0x00ED;
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public SagaLogin.Network.Client.CharStatus Status
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }
    }
}

