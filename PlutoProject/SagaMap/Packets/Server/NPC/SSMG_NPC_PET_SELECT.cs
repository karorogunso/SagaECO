using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_PET_SELECT : Packet
    {
        public enum SelType
        {
            Recover,
            Rebirth,
            None
        }

        public SSMG_NPC_PET_SELECT()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x12CA;
        }

        public SelType Type
        {
            set
            {
                this.PutInt((int)value, 2);
            }
        }

        public List<Item> Pets
        {
            set
            {
                byte[][] names = new byte[value.Count][];
                uint[] slots = new uint[value.Count];

                byte len = 0;
                for(int i=0;i<names.Length;i++)
                {
                    names[i] = Global.Unicode.GetBytes(value[i].BaseData.name);
                    len += (byte)(names[i].Length + 1);
                    slots[i] = value[i].Slot;
                }

                byte[] buf = new byte[9 + value.Count * 8 + len];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.offset = 6;
                this.PutByte((byte)slots.Length);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(slots[i]);
                }
                this.PutByte((byte)names.Length);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)names[i].Length);
                    this.PutBytes(names[i]);
                }
                this.PutByte((byte)value.Count);
            }
        }
    }
}

