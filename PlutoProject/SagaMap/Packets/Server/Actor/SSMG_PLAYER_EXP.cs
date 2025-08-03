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
            this.data = new byte[18+8];//8bytes unknowns
            this.offset = 2;
            this.ID = 0x0235;
            this.PutByte(1, 10);
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

        public int WRP
        {
            set
            {
                this.PutInt(value, 11);
            }
        }

        public uint ECoin
        {
            set
            {
                //this.PutUInt(value, 14);
            }
        }

        public long Exp
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                {
                    //this.PutLong(value, 18);
                }
            }
        }

        public long JExp
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                {
                   // this.PutLong(value, 26);
                }
            }
        }
    }
}
        
