using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_QUIT : Packet
    {
        public SSMG_PARTY_QUIT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x19CE;
        }
        /// <summary>
        /// -1 : GAME_SMSG_PARTY_LEAVEERR1,"パーティーに所属していません" 
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

