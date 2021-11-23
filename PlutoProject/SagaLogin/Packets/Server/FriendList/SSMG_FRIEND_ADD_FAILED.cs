using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_ADD_FAILED : Packet
    {
        public enum Result
        {
            CANNOT_FIND_TARGET = -5,
            TARGET_REFUSED = -3,
            NO_FREE_SPACE = -2,
            TARGET_NO_FREE_SPACE = -1,
        }
        public SSMG_FRIEND_ADD_FAILED()
        {
            this.data = new byte[6];
            this.ID = 0x00D6;
        }

        public Result AddResult
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
    }
}

