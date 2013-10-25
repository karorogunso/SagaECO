using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_LEVEL : Packet
    {
        public SSMG_PLAYER_LEVEL()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x023A;
        }

        public byte Level
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public byte JobLevel
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

        public byte JobLevel2X
        {
            set
            {
                this.PutByte(value, 4);
            }
        }

        public byte JobLevel2T
        {
            set
            {
                this.PutByte(value, 5);
            }
        }

        public ushort BonusPoint
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public ushort SkillPoint
        {
            set
            {
                this.PutUShort(value, 8);
            }
        }

        public ushort Skill2XPoint
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }

        public ushort Skill2TPoint
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }

    }
}
        
