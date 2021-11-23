using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_APPEAR_OPEN : Packet
    {
        public SSMG_PLAYER_SHOP_APPEAR_OPEN()
        {
            this.data = new byte[12]; //TitleBytes.Length+2+4+5
            this.offset = 2;
            this.ID = 0x190F;
        }
        public ushort botton
        {
            set
            {
                this.PutUInt(2, 2);//unknown
                this.PutUShort(value, 6);//1开0关
                this.PutUInt(1, 8);//unknown
            }
        }
    }
}

