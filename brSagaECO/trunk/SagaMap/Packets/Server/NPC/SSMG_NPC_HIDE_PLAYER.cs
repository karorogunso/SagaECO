using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_HIDE_PLAYERS : Packet
    {
        public SSMG_NPC_HIDE_PLAYERS()
        {
            this.data = new byte[11];
            this.offset = 2;
            if(Configuration.Instance.Version>=SagaLib.Version.Saga18)
                this.ID = 0x0615; //predicted
            else
                this.ID = 0x0613;
        }

        public uint unknown1
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint unknown2
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
        public byte unknown3
        {
            set
            {
                this.PutByte(value, 10);
            }
        }
    }
}

