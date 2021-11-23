using SagaLib;
using System.Collections.Generic;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_OPEN_MIRROR_WINDOW : Packet
    {
        public SSMG_PLAYER_OPEN_MIRROR_WINDOW()
        {
            this.data = new byte[100];
            this.ID = 0x02B3;
            this.offset = 2;
        }

        public List<ushort> SetFace
        {
            set
            {
                this.PutByte(20);

                for (int i = 0; i < 20; i++)
                {
                    if (i < value.Count)
                        this.PutUShort(value[i]);
                    else
                        this.PutUShort(0xFFFF);
                }
            }
        }

        public List<ushort> SetHairStyle
        {
            set
            {
                this.PutByte(20);
                for (int i = 0; i < 20; i++)
                {
                    if (i < value.Count)
                        this.PutUShort(value[i]);
                    else
                        this.PutUShort(0xFFFF);
                }
            }
        }

        public List<ushort> SetHairColor
        {
            set
            {
                this.PutByte(20);
                for (int i = 0; i < 20; i++)
                {
                    if (i < value.Count)
                        this.PutUShort(value[i]);
                    else
                        this.PutUShort(0xFFFF);
                }
            }
        }

        public List<uint> SetUnknow
        {
            set
            {
                this.PutByte(20);
                for (int i = 0; i < 20; i++)
                {
                    if (i < value.Count)
                        this.PutUInt(0xFFFFFFFF);
                    else
                        this.PutUInt(0x00000000);
                }
            }
        }

        public List<byte> SetHairColorStorageSlot
        {
            set
            {
                this.PutByte(20);
                for (int i = 0; i < 20; i++)
                {
                    if (i < value.Count)
                        this.PutByte(0x32);
                    else
                        this.PutByte(0xFF);
                }
            }
        }
    }
}
