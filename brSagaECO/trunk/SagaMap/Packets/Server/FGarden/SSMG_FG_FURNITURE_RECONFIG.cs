using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_FURNITURE_RECONFIG : Packet
    {
        public SSMG_FG_FURNITURE_RECONFIG()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                this.data = new byte[14];
            else
                this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x1C12;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public short X
        {
            set
            {
                this.PutShort(value, 6);
            }
        }

        public short Y
        {
            set
            {
                this.PutShort(value, 8);
            }
        }

        public short Z
        {
            set
            {
                this.PutShort(value, 10);
            }
        }

        public ushort Dir
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                    this.PutUShort(value, 12);
                else
                    this.PutUShort(value, 14);
            }
        }
    }
}

