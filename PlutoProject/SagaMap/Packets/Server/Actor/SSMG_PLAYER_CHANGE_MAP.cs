using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_CHANGE_MAP : Packet
    {
        public SSMG_PLAYER_CHANGE_MAP()
        {
            this.data = new byte[17];
            this.offset = 2;
            this.ID = 0x11FD;

            DungeonDir = 4;
            DungeonX = 255;
            DungeonY = 255;

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

        public byte DungeonDir
        {
            set
            {
                this.PutByte(value, 9);
            }
        }

        public byte DungeonX
        {
            set
            {
                this.PutByte(value, 10);
            }
        }

        public byte DungeonY
        {
            set
            {
                this.PutByte(value, 11);
            }
        }

        public bool FGTakeOff
        {
            set
            {
                if (value)
                {
                    this.PutByte(1, 16);
                }
            }
        }
    }
}

