using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_CAPACITY : Packet
    {
        public SSMG_PLAYER_CAPACITY()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                this.data = new byte[18];
            else
            this.data = new byte[36];
            this.offset = 2;
            this.ID = 0x0230;
        }

        public uint Capacity
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                {
                    this.PutUInt(value, 2);
                }
            }
        }

        public uint Payload
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                {
                    this.PutUInt(value, 6);
                }
            }
        }

        public uint CapacityBody
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutByte(4, 2);
                    this.PutUInt(value, 3);
            }
        }

        public uint CapacityRight
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutUInt(value, 7);
            }
        }

        public uint CapacityLeft
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutUInt(value, 11);
            }
        }

        public uint CapacityBack
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutUInt(value, 15);
            }
        }

        public uint PayloadBody
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutByte(4, 19);
                    this.PutUInt(value, 20);
            }
        }

        public uint PayloadRight
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutUInt(value, 24);
            }
        }

        public uint PayloadLeft
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                this.PutUInt(value, 28);
            }
        }

        public uint PayloadBack
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    return;
                 this.PutUInt(value, 32);
            }
        }

        public uint MaxCapacity
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 10);
                else return;
            }
        }

        public uint MaxPayload
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 14);
                else return;
            }
        }
    }
}
        
