using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_PET_APPEAR : Packet
    {
        public SSMG_ACTOR_PET_APPEAR()
        {
            if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                this.data = new byte[30];
            if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                this.data = new byte[36];
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.data = new byte[42];
            this.offset = 2;
            this.ID = 0x122F;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Union
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public uint OwnerActorID
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }

        public uint OwnerCharID
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }

        public byte OwnerLevel
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                {
                    this.PutByte(value, 15);
                }
            }
        }

        public uint OwnerWRP
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                {
                    this.PutUInt(value, 16);
                }
            }
        }


        public byte X
        {
            set
            {
                if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                    this.PutByte(value, 15);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                    this.PutByte(value, 21);
            }
        }

        public byte Y
        {
            set
            {
                if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                    this.PutByte(value, 16);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                    this.PutByte(value, 22);
            }
        }

        public ushort Speed
        {
            set
            {
                if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                    this.PutUShort(value, 17);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                    this.PutUShort(value, 23);
            }
        }

        public byte Dir
        {
            set
            {
                if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                    this.PutByte(value, 19);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                    this.PutByte(value, 25);
            }
        }
        
        public uint HP
        {
            set
            {
                if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                    this.PutUInt(value, 20);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                    this.PutUInt(value, 26);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    this.PutUInt(value, 26);///0???
                    this.PutUInt(value, 30);
                }
            }
        }

        public uint MaxHP
        {
            set
            {
                if (Configuration.Instance.Version == SagaLib.Version.Saga6)
                    this.PutUInt(value, 24);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                    this.PutUInt(value, 30);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    this.PutUInt(value, 34);//0???
                    this.PutUInt(value, 38);
                }
            }
        }
    }
}

