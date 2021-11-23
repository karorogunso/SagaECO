using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_ACTOR_APPEAR_LIST : Packet
    {
        public SSMG_FG_ACTOR_APPEAR_LIST()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1BEF;
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public List<ActorFurniture> fs
        {
            set
            {
                byte count = (byte)value.Count;

                //ActorID
                offset = 6;
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutUInt(value[i].ActorID);

                //ItemID
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutUInt(value[i].ItemID);

                //PictID
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutUInt(value[i].PictID);

                //unknown
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutByte(0);

                //X
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutShort(value[i].X);

                //Y
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutShort(value[i].Y);

                //Z
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutShort(value[i].Z);

                //Xaxis
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutShort(value[i].Xaxis);

                //Yaxis
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutShort(value[i].Yaxis);

                //Zaxis
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutShort(value[i].Zaxis);

                //Motion
                PutByte(count);
                for (int i = 0; i < count; i++)
                    PutUShort(value[i].Motion);

                //NAME
                PutByte(count);
                for (int i = 0; i < count; i++)
                {
                    byte[] name = Global.Unicode.GetBytes("未知家具");
                    if (value[i].Name != null)
                        name = Global.Unicode.GetBytes(value[i].Name);

                    PutByte((byte)name.Length);
                    PutBytes(name);
                }
            }
        }
    }
}

