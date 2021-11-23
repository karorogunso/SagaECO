using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_STATS_PRE_CALC : Packet
    {
        public SSMG_PLAYER_STATS_PRE_CALC()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.data = new byte[62];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.data = new byte[58];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[80];
            else
                this.data = new byte[74];
            this.offset = 2;
            this.ID = 0x0259;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
            {
                this.PutByte(0x13, 2);
                this.PutByte(3, 41);
            }
            else
            {
                this.PutByte(0x1E, 2);
                this.PutByte(3, 63);
            }
        }

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 3);
            }
        }

        public ushort ATK1Min
        {
            set
            {
                this.PutUShort(value, 5);
            }
        }

        public ushort ATK2Min
        {
            set
            {
                this.PutUShort(value, 7);
            }
        }

        public ushort ATK3Min
        {
            set
            {
                this.PutUShort(value, 9);
            }
        }

        public ushort ATK1Max
        {
            set
            {
                this.PutUShort(value, 11);
            }
        }

        public ushort ATK2Max
        {
            set
            {
                this.PutUShort(value, 13);
            }
        }

        public ushort ATK3Max
        {
            set
            {
                this.PutUShort(value, 15);
            }
        }

        public ushort MATKMin
        {
            set
            {
                this.PutUShort(value, 17);
            }
        }

        public ushort MATKMax
        {
            set
            {
                this.PutUShort(value, 19);
            }
        }

        public ushort DefBase
        {
            set
            {
                this.PutUShort(value, 21);
            }
        }

        public ushort DefAddition
        {
            set
            {
                this.PutUShort(value, 23);
            }
        }

        public ushort MDefBase
        {
            set
            {
                this.PutUShort(value, 25);
            }
        }

        public ushort MDefAddition
        {
            set
            {
                this.PutUShort(value, 27);
            }
        }

        public ushort HitMelee
        {
            set
            {
                this.PutUShort(value, 29);
            }
        }

        public ushort HitRanged
        {
            set
            {
                this.PutUShort(value, 31);
            }
        }

        public ushort HitMagic
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 33);
            }
        }

        public ushort HitCritical
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 35);
            }
        }

        public ushort AvoidMelee
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUShort(value, 33);
                else
                    this.PutUShort(value, 37);
            }
        }

        public ushort AvoidRanged
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUShort(value, 35);
                else
                    this.PutUShort(value, 39);
            }
        }

        public ushort AvoidMagic
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 41);
            }
        }

        public ushort AvoidCritical
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 43);
            }
        }

        public ushort HealHP
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 45);
            }
        }

        public ushort HealMP
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 47);
            }
        }

        public ushort HealSP
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 49);
            }
        }

        public short ASPD
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutShort(value, 51);
                else if (Configuration.Instance.Version == SagaLib.Version.Saga17)
                    this.PutShort(value, 33);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutShort(value, 37);
            }
        }

        public short CSPD
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutShort(value, 53);
                else if (Configuration.Instance.Version == SagaLib.Version.Saga17)
                    this.PutShort(value, 35);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutShort(value, 39);
            }
        }

        public uint HP
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                    this.PutUShort((ushort)value, 64);
                else if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort((ushort)value, 66);
                else
                    this.PutUInt(value, 42);
            }
        }

        public uint MP
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                    this.PutUShort((ushort)value, 66);
                else if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort((ushort)value, 70);
                else
                    this.PutUInt(value, 46);
            }
        }

        public uint SP
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                    this.PutUShort((ushort)value, 68);
                else if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort((ushort)value, 74);
                else
                    this.PutUInt(value, 50);
            }
        }

        public ushort Capacity
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                    this.PutUShort(value, 70);
                else if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 76);
                else if (Configuration.Instance.Version == SagaLib.Version.Saga17)
                    this.PutUShort(value, 56);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt((uint)value, 54);
            }
        }

        public ushort Payload
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                    this.PutUShort(value, 72);
                else if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                    this.PutUShort(value, 78);
                else if(Configuration.Instance.Version == SagaLib.Version.Saga17)
                    this.PutUShort(value, 56);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt((uint)value, 58);
            }
        }

    }
}