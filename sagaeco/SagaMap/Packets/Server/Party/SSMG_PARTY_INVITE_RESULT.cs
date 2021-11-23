using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_INVITE_RESULT : Packet
    {
        public enum Result
        {
            OK = 0,
            PLAYER_NOT_EXIST = -2,
            PLAYER_ALREADY_IN_PARTY = -10,
            PARTY_MEMBER_EXCEED = -12,
        }

        public SSMG_PARTY_INVITE_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x19CC;
        }

        public Result InviteResult
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
    }
}

