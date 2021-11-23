using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_INFO : Packet
    {
        int page;
        public SSMG_STAMP_INFO()
        {
             if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                this.data = new byte[29];
            this.data = new byte[25];
            this.offset = 2;
            this.ID = 0x1BBC;
            this.Page = 0;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                this.PutByte(0x0b, 6);
            else
                this.PutByte(0x0b, 2);
        }

        public int Page
        {
            get { return page; }
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutInt(value, 2);
                page = value;
            }
        }

        public Stamp Stamp
        {
            set
            {
                if (Page == 0)
                {
                    this.PutShort((short)value[StampGenre.Special].Value, 3);
                    this.PutShort((short)value[StampGenre.Pururu].Value, 5);
                    this.PutShort((short)value[StampGenre.Field].Value, 7);
                    this.PutShort((short)value[StampGenre.Coast].Value, 9);
                    this.PutShort((short)value[StampGenre.Wild].Value, 11);
                    this.PutShort((short)value[StampGenre.Cave].Value, 13);
                    this.PutShort((short)value[StampGenre.Snow].Value, 15);
                    this.PutShort((short)value[StampGenre.Colliery].Value, 17);
                    this.PutShort((short)value[StampGenre.Northan].Value, 19);
                    this.PutShort((short)value[StampGenre.IronSouth].Value, 21);
                    this.PutShort((short)value[StampGenre.SouthDungeon].Value, 23);
                }
                if (Page == 1)
                {
                    this.PutShort((short)value[StampGenre.Special].Value, 3);
                    this.PutShort((short)value[StampGenre.NorthanUnderground].Value, 5);
                    this.PutShort((short)value[StampGenre.EastDungeon].Value, 7);
                    this.PutShort((short)value[StampGenre.BreadTreeForest].Value, 9);
                    this.PutShort((short)value[StampGenre.GodWildCave].Value, 11);
                    this.PutShort((short)value[StampGenre.SeaCave].Value, 13);
                    this.PutShort((short)value[StampGenre.LightTower].Value, 15);
                    this.PutShort((short)value[StampGenre.UndeadCity].Value, 17);
                    this.PutShort((short)value[StampGenre.MaimaiDungeon].Value, 19);
                    this.PutShort((short)value[StampGenre.TitaniaField].Value, 21);
                    this.PutShort((short)value[StampGenre.DominianField].Value, 23);
                }
            }
        }
    }
}
