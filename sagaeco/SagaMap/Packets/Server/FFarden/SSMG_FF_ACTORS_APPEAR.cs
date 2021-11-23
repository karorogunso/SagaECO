using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FF_ACTORS_APPEAR : Packet
    {
        public SSMG_FF_ACTORS_APPEAR()
        {
            this.data = new byte[6];
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

        public List<ActorFurniture> List
        {
            set
            {
                if (value.Count > 0)
                {
                    byte[] buf = new byte[18 + value.Count * 27];
                    this.data.CopyTo(buf, 0);
                    this.data = buf;
                    this.offset = 6;
                    this.PutByte((byte)value.Count);//ActorID
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//ItemID
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//PictID
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//Unknown
                    this.offset += (ushort)(value.Count);
                    this.PutByte((byte)value.Count);//X
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Y
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Z
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Xaxis
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Yaxis
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Zaxis
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Motion
                    this.offset += (ushort)(value.Count * 2);
                    this.PutByte((byte)value.Count);//Name
                    for (int i = 0; i < value.Count; i++)
                    {
                        ActorFurniture af = value[i];
                        this.PutUInt(af.ActorID, (ushort)(7 + i * 4));
                        this.PutUInt(af.ItemID, (ushort)(8 + value.Count * 4 + i * 4));
                        this.PutUInt(af.PictID, (ushort)(9 + value.Count * 8 + i * 4));
                        this.PutByte(0x0, (ushort)(10 + value.Count * 12 + i));//Unknown
                        this.PutShort(af.X, (ushort)(11+ value.Count * 13 + i * 2));
                        this.PutShort(af.Y, (ushort)(12 + value.Count * 15 + i * 2));
                        this.PutShort(af.Z, (ushort)(13 + value.Count * 17 + i * 2));
                        this.PutShort(af.Xaxis, (ushort)(14 + value.Count * 19 + i * 2));
                        this.PutShort(af.Yaxis, (ushort)(15 + value.Count * 21 + i * 2));
                        this.PutShort(af.Zaxis, (ushort)(16 + value.Count * 23 + i * 2));
                        this.PutUShort(af.Motion, (ushort)(17 + value.Count * 25 + i * 2));

                        ushort ind = (ushort)this.data.Length;
                        byte[] name = Global.Unicode.GetBytes(af.Name + "\0");
                        buf = new byte[this.data.Length + name.Length + 1];
                        this.data.CopyTo(buf, 0);
                        this.data = buf;
                        this.offset = ind;
                        this.PutByte((byte)name.Length);
                        this.PutBytes(name);
                    }
                }
            }
        }
    }
}