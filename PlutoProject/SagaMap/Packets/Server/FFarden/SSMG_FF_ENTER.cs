using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FF_ENTER : Packet
    {
        public SSMG_FF_ENTER()
        {
            this.data = new byte[37];
            this.offset = 2;
            this.ID = 0x2008;
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
        public uint RingID
        {
            set
            {
                this.PutUInt(value, 9);
            }
        }
        public uint RingHouseID
        {
            set
            {
                //Furiture ID
                this.PutByte(0x03, 13);
                this.PutUInt(value, 14);
            }
        }
        public uint RingHouseBedID
        {
            set
            {
                //Furiture ID
                this.PutUInt(value, 18);
            }
        }
        public uint RingHouseWallID
        {
            set
            {
                //Furiture ID
                this.PutUInt(value, 22);
            }
        }
        public ushort HouseX
        {
            set
            {
                this.PutUShort(value, 26);
            }
        }
        public ushort HouseY
        {
            set
            {
                this.PutUShort(value, 28);
            }
        }
        public ushort HouseDir
        {
            set
            {
                this.PutUShort(value, 30);
            }
        }
    }
}

