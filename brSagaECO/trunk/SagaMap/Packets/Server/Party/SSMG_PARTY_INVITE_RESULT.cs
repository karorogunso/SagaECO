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
            PARRTY_ERR=-1,
            PLAYER_NOT_EXIST = -2,
            PLAYER_IN_COULD_NOT_INVITE_STATUS =-3,
            PLAYER_ALREADY_IN_PARTY = -10,
            PLAYER_ALREADY_IN_THAT_PARTY=-11,
            PARTY_MEMBER_EXCEED = -12,
        }

        public SSMG_PARTY_INVITE_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x19CC;
        }

        /*
        public Result InviteResult
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
        */

        public uint InviteResult
        {
            set
            {
                this.PutUInt((value), 2);
            }
        }
    }
}

