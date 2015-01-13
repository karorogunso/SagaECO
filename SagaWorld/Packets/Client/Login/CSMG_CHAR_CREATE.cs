using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaWorld;
using SagaWorld.Network.Client;

using SagaDB.Actor;

namespace SagaWorld.Packets.Client
{
    public class CSMG_CHAR_CREATE : Packet
    {
        public CSMG_CHAR_CREATE()
        {
            this.offset = 2;
        }

        public byte Slot
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public string Name
        {
            get
            {
                byte size;
                byte[] buf;
                size = this.GetByte(3);
                buf = this.GetBytes(size, 4);
                string res = Global.Unicode.GetString(buf);
                res = res.Replace("\0", "");
                return res;
            }
        }

        public PC_RACE Race
        {
            get
            {
                int offset = GetDataOffset();
                return (PC_RACE)this.GetByte((ushort)offset);
            }
        }

        public PC_GENDER Gender
        {
            get
            {
                int offset = GetDataOffset() + 1;
                return (PC_GENDER)this.GetByte((ushort)offset);
            }
        }

        public byte HairStyle
        {
            get
            {
                int offset = GetDataOffset() + 2;
                return this.GetByte((ushort)offset);
            }
        }

        public byte HairColor
        {
            get
            {
                int offset = GetDataOffset() + 3;
                return this.GetByte((ushort)offset);
            }
        }

        public byte Face
        {
            get
            {
                int offset = GetDataOffset() + 4;
                return this.GetByte((ushort)offset);
            }
        }

        private byte GetDataOffset()
        {
            return (byte)(4 + this.GetByte(3));
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaWorld.Packets.Client.CSMG_CHAR_CREATE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((WorldClient)(client)).OnCharCreate(this);
        }

    }
}