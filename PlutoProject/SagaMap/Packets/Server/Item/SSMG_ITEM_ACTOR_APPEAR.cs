using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Map;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ACTOR_APPEAR : Packet
    {
        public SSMG_ITEM_ACTOR_APPEAR()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                this.data = new byte[26];
            else
            this.data = new byte[29];
            this.offset = 2;
            this.ID = 0x07D5; 
        }

        public ActorItem Item
        {
            set
            {
                MapInfo info = Manager.MapManager.Instance.GetMap(value.MapID).Info;
                this.PutUInt(value.ActorID, 2);
                this.PutUInt(value.Item.ItemID, 6);
                this.PutByte(Global.PosX16to8(value.X, info.width), 10);
                this.PutByte(Global.PosY16to8(value.Y, info.height), 11);
                this.PutUShort(value.Item.Stack, 12);
                this.PutUInt(10, 14);//Unknown
                if(value.PossessionItem)
                    this.PutByte(1, 22);//type, possession item is 1, otherwise 0
                else
                    this.PutByte(0, 22);//type, possession item is 1, otherwise 0
                byte[] buf = Global.Unicode.GetBytes(value.Comment + "\0");
                byte count = (byte)buf.Length;
                byte[] buff = new byte[29 + count];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte(count, 23);
                this.PutBytes(buf, 24);
                this.PutByte(value.Item.identified, (ushort)(27 + count));
                byte fusion = 0;
                if (value.Item.PictID != 0)
                    fusion = 1;
                this.PutByte(fusion, (ushort)(28 + count));
            }
        }
    }
}

