using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_VOICE_PLAY : Packet
    {
            public SSMG_NPC_VOICE_PLAY()
            {
                this.data = new byte[7];
                this.offset = 2;
                this.ID = 0x0622;
            }
            public uint VoiceID
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
        }
}

