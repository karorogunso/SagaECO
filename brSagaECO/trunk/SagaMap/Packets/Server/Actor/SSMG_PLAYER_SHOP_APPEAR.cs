using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_APPEAR : Packet
    {
        public SSMG_PLAYER_SHOP_APPEAR()
        {
            this.data = new byte[8]; //TitleBytes.Length+2+4+5
            this.offset = 2;
            this.ID = 0x1902;
        }
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte button
        {
            set
            {
                this.PutByte(value, 6);//开关 0关1开
            }
        }

        public string Title
        {
            set
            {

                byte[] title = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[8 + title.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)title.Length, 7);
                this.PutBytes(title, 8);

                //string www = string.Format("DL:{0}    L:{1}     TL{2}     T:{3}   buf", this.data.Length, value.Length, title.Length, value);
                //Logger.ShowError(www);
                
            }
        }
    }
}

