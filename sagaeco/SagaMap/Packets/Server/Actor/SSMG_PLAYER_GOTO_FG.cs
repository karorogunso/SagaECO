using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_GOTO_FG : Packet
    {
        public SSMG_PLAYER_GOTO_FG()
        {
            this.data = new byte[57];
            this.offset = 2;
            this.ID = 0x1BE4;

            this.PutByte(9, 9);
            this.PutByte(9, 46);
            this.PutByte(1, 56);
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 7);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 8);
            }
        }

        public Dictionary<FGardenSlot, uint> Equiptments
        {
            set
            {
                for (int i = 0; i < 8; i++)
                {
                    this.PutUInt(value[(FGardenSlot)i], (ushort)(10 + i * 4));
                }
            }
        }
    }
}

