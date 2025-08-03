using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_MESSAGE_GALMODE : Packet
    {
        public SSMG_NPC_MESSAGE_GALMODE()
        {
            this.data = new byte[19];
            this.offset = 2;
            this.ID = 0x0606;
            if (Configuration.Instance.Version <= SagaLib.Version.Saga18)
            {
                this.PutUInt(1, 2);
                this.PutUInt(1, 15);
            }
        }
        public uint Mode
        {
            set
            {
                this.PutUInt(value, 2);
                //0 normal mode
                //1 galgame mode
            }
        }
        public UIType UIType
        {
            set
            {
                this.PutInt((int)value, 6);
                if (value != 0)
                    this.PutByte(0, 18);
                else
                    this.PutByte(1, 18);
            }
        }
        public int X
        {
            set
            {
                this.PutInt(value, 10);
            }
        }

        public int Y
        {
            set
            {
                this.PutInt(value, 14);
            }
        }

        public byte Unknown
        {
            set
            {
                this.PutByte(value, 18);
                //0 or 1, bool expected
            }
        }
    }
}
