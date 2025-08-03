using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_DEMIC_DATA : Packet
    {
        public SSMG_DEM_DEMIC_DATA()
        {
            this.data = new byte[166];
            this.offset = 2;
            this.ID = 0x1E4A;

            Size = 81;
        }

        public byte Page
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public byte Size
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

        public short[,] Chips
        {
            set
            {
                offset = 4;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        PutShort(value[j, i]);
                    }
                }
            }
        }
    }
}

