using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_CHANGE_BGM : Packet
    {
        public SSMG_NPC_CHANGE_BGM()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.data = new byte[13];
                this.ID = 0x05F2; //predicted
            }
            else
            {
                this.data = new byte[12];
                this.ID = 0x05EE;
            }
            this.offset = 2;
        }

        public uint SoundID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Loop
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public uint Volume
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 8);
                else
                    this.PutUInt(value, 7);
            }
        }

        public byte Balance
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutByte(value, 12);
                else
                    this.PutByte(value, 11);
            }
        }
    }
}

