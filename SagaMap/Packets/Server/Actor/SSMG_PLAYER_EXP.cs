using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_EXP : Packet
    {
        public SSMG_PLAYER_EXP()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0235;
        }

        /// <summary>
        /// 345 means 34.5%
        /// </summary>
        public uint EXPPercentage
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint JEXPPercentage
        {
            set
            {
                this.PutUInt(value, 6);
            }
        } 
    }
}
        
