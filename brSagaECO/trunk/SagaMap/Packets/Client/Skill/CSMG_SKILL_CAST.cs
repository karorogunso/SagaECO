using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_SKILL_CAST : Packet
    {
        public CSMG_SKILL_CAST()
        {
            this.offset = 2;
            this.data = new byte[14];
        }

        public ushort SkillID
        {
            get
            {
                return this.GetUShort(2);
            }
            set
            {
                this.PutUShort(value, 2);
            }
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(4);
            }
            set
            {
                this.PutUInt(value, 4);
            }
        }

        public byte X
        {
            get
            {
                return this.GetByte(8);
            }
            set
            {
                this.PutByte(value, 8);
            }
        }

        public byte Y
        {
            get
            {
                return this.GetByte(9);
            }
            set
            {
                this.PutByte(value, 9);
            }
        }

        public byte SkillLv
        {
            get
            {
                return this.GetByte(10);
            }
            set
            {
                this.PutByte(value, 10);
            }
        }

        public short Random
        {
            get
            {
                return this.GetShort(11);
            }
            set
            {
                this.PutShort(value, 11);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_SKILL_CAST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillCast(this);
        }

    }
}