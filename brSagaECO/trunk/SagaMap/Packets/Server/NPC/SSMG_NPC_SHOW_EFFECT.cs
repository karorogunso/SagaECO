using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOW_EFFECT : Packet
    {
        public SSMG_NPC_SHOW_EFFECT()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.data = new byte[25];
                this.ID = 0x0600;
            }
            else
            {
                this.data = new byte[14];
                this.ID = 0x05FC;
            }
            this.offset = 2;
            this.PutByte(0xff, 10);
            this.PutByte(0xff, 11);
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.PutUInt(0xFFFFFFFF, 12);
                this.PutByte(0xFF, 18);
                this.PutUInt(0xFFFFFFFF, 19);
                this.PutByte(0xFF, 23);
            }
            else
            {
                this.PutByte(1, 12);
                this.PutByte(0xA0, 13);
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint EffectID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public byte X
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 14);
                else
                    this.PutByte(value, 10);
            }
        }

        public byte Y
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 15);
                else
                    this.PutByte(value, 11);
            }
        }

        public ushort Height
        {
            set
            {
                if (Configuration.Instance.Version>=SagaLib.Version.Saga18)
                    this.PutUShort(value, 16);
            }
        }

        public bool OneTime
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                {
                    if (value)
                        this.PutByte(1, 24);
                    else
                        this.PutByte(0, 24);
                }
                else
                {
                    if (value)
                        this.PutByte(1, 12);
                    else
                        this.PutByte(0, 12);
                }
            }
        }
    }
}

