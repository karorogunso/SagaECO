using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_UNIONPET_INFO : Packet
    {
        public SSMG_ACTOR_UNIONPET_INFO()
        {
            this.data = new byte[45];
            this.offset = 2;
            this.ID = 0x217A;
        }

        public uint unknownID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte Level
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
        public uint EXPPercentage
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
        public ushort Rank
        {
            set
            {
                this.PutUShort(value, 11);
            }
        }
        public byte ReliabilityColor
        {
            set
            {
                this.PutByte(value, 13);
            }
        }
        public ushort ReliabilityUpRate
        {
            set
            {
                this.PutUShort(value, 14);
            }
        }
        public uint NextFeedTime
        {
            set
            {
                this.PutUInt(value, 16);
            }
        }
        public byte AIMode
        {
            set
            {
                this.PutByte(value, 20);
            }
        }
        public uint Unknown1
        {
            set
            {
                this.PutUInt(value, 21);
            }
        }
        public byte Unknown2
        {
            set
            {
                this.PutByte(value, 25);
            }
        }
        public byte SkillCount
        {
            set
            {
                this.PutByte(value, 26);
            }
        }
        public byte Unknown3
        {
            set
            {
                this.PutByte(value, 27);
            }
        }
        public ushort StatePoint
        {
            set
            {
                this.PutUShort(value, 28);
            }
        }
        public byte Unknown4
        {
            set
            {
                this.PutByte(6, 30);
            }
        }
        public byte ATKUP
        {
            set
            {
                this.PutByte(value, 31);
            }
        }
        public byte MATKUP
        {
            set
            {
                this.PutByte(value, 32);
            }
        }
        public byte VITUP
        {
            set
            {
                this.PutByte(value, 33);
            }
        }
        public byte INTUP
        {
            set
            {
                this.PutByte(value, 34);
            }
        }
        public byte DEXUP
        {
            set
            {
                this.PutByte(value, 35);
            }
        }
        public byte AGIUP
        {
            set
            {
                this.PutByte(value, 36);
            }
        }
        public uint WeaponID
        {
            set
            {
                this.PutUInt(value, 37);
            }
        }
        public uint ArmorID
        {
            set
            {
                this.PutUInt(value, 41);
            }
        }
    }
}

