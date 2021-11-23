using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_KICK : Packet
    {
        public SSMG_PARTY_KICK()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x19D3;
        }
        /// <summary>
        /// -1:GAME_SMSG_PARTY_KICKERR1,"指定プレイヤーが存在しません"
        /// </summary>
        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}

