using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_PET_GROW : Packet
    {
        public enum GrowType
        {
            HP,
            SP,
            MP,
            Speed,
            ATK1,
            ATK2,
            ATK3,
            MATK,
            Def,
            MDef,
            HitMelee,
            HitRanged,
            HitMagic,
            AvoidMelee,
            AvoidRanged,
            AvoidMagic,
            Critical,
            AvoidCri,
            Recover,
            MPRecover,
            Stamina,
            ASPD,
            CSPD,
        }

        public SSMG_ACTOR_PET_GROW()
        {
            this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x12C0;
        }

        public uint PetActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint OwnerActorID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public GrowType Type
        {
            set
            {
                this.PutUInt((uint)value, 10);
            }
        }

        public uint Value
        {
            set
            {
                PutUInt(value, 14);
            }
        }
    }
}

