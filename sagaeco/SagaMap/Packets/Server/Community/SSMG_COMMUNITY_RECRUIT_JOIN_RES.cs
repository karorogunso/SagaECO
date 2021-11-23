using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public enum JoinRes
    {
        OK = 0,
        DB_ERROR = -1,
        REJECTED = -2,
        PARTY_FULL = -3,
        TARGET_OFFLINE = -4,
        ALREADY_IN_PARTY = -5,
        SELF = -6,
        RECRUIT_DELETED = -7
    }

    public class SSMG_COMMUNITY_RECRUIT_JOIN_RES : Packet
    {
       
        public SSMG_COMMUNITY_RECRUIT_JOIN_RES()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1BA9;
        }

        public JoinRes Result
        {
            set
            {
                PutInt((int)value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                PutUInt(value, 6);
            }
        }
        
    }
}

