using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

using SagaDB.Actor;

namespace SagaLogin.Packets.Client
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
                int offset;
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    offset = GetDataOffset() + 3;
                else
                    offset = GetDataOffset() + 2;
                return this.GetByte((ushort)offset);
            }
        }

        public byte HairColor
        {
            get
            {
                int offset;
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    offset = GetDataOffset() + 4;
                else
                    offset = GetDataOffset() + 3; 
                return this.GetByte((ushort)offset);
            }
        }

        public ushort Face
        {
            get
            {
                int offset;
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    offset = GetDataOffset() + 5;
                else
                    offset = GetDataOffset() + 4; 
                return this.GetUShort((ushort)offset);
            }
        }

        private byte GetDataOffset()
        {
            return (byte)(4 + this.GetByte(3));
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_CHAR_CREATE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnCharCreate(this);
        }

    }
}