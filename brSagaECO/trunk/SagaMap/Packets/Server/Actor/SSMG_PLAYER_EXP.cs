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
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                this.data = new byte[23];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[34];
            else
                this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x0235;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.PutByte(0x01, 10);
            }
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
                if (Configuration.Instance.Version < SagaLib.Version.Saga18)
                    this.PutInt(value, 10);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutInt(value, 11);
            }
        }

        public uint ECoin
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga18)
                    this.PutUInt(value, 14);
                //Predicted
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 15);
            }
        }

        public uint Unknown
        {
            set
            {
                //Predicted
               if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 19);
            }
        }

        public long Exp
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                { }
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga10 && Configuration.Instance.Version <= SagaLib.Version.Saga18)
                    this.PutLong(value, 18);
            }
        }

        public long JExp
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                { }
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga10 && Configuration.Instance.Version <= SagaLib.Version.Saga18)
                    this.PutLong(value, 26);
            }
        }
    }
}
        
