using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_PC_INFO : Packet
    {
        public SSMG_ACTOR_PC_INFO()
        {
            this.data = new byte[130];
            this.offset = 2;
            this.ID = 0x020E;   
        }

        public ActorPC Actor
        {
            set
            {
                byte[] buf, buff;
                byte size;
                ushort offset;
                this.PutUInt(value.ActorID, 2);
                this.PutUInt(value.CharID, 6);

                buf = Global.Unicode.GetBytes(value.Name + "\0");
                size = (byte)buf.Length;
                buff = new byte[129 + size];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte(size, 10);
                this.PutBytes(buf, 11);
                offset = (ushort)(11 + size);

                this.PutByte((byte)value.Race, offset);
                this.PutByte((byte)value.Gender, (ushort)(offset + 1));
                this.PutByte(value.HairStyle, (ushort)(offset + 2));
                this.PutByte(value.HairColor, (ushort)(offset + 3));
                this.PutByte(value.Wig, (ushort)(offset + 4));
                this.PutByte(0xff, (ushort)(offset + 5));
                this.PutByte(value.Face, (ushort)(offset + 6));
                this.PutByte(0x0D, (ushort)(offset + 10));
                for (int j = 0; j < 13; j++)
                {
                    if (value.Inventory.Equipments.ContainsKey((EnumEquipSlot)j))
                    {
                        Item item = value.Inventory.Equipments[(EnumEquipSlot)j];
                        this.PutUInt(item.ItemID, (ushort)(offset + 11 + j * 4));
                    }
                }

                this.PutBytes(new byte[3] { 3, 0x13, 0 }, (ushort)(offset + 63));
                this.PutBytes(new byte[3] { 0, 3, 1 }, (ushort)(offset + 66));
                this.PutBytes(new byte[3] { 0, 1, 0x3 }, (ushort)(offset + 69));


                this.PutByte(1, (ushort)(offset + 81));//party name
                this.PutByte(1, (ushort)(offset + 83));//party name

                this.PutByte(1, (ushort)(offset + 88));//Ring name

                this.PutByte(1, (ushort)(offset + 90));//Ring master

                this.PutByte(1, (ushort)(offset + 91));//Sign name

                this.PutByte(1, (ushort)(offset + 93));//shop name

                this.PutUInt(1000, (ushort)(offset + 96));//size
                this.PutUInt(2, (ushort)(offset + 106));//size


                /*WORD motion; // ただし座り(135)や移動や武器・騎乗ペットによるモーションの場合0
                DWORD unknown; // 0?
                DWORD mode1   //2 r0fa7参照
                DWORD mode2   //0 r0fa7参照
                BYTE emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                BYTE metamo; //メタモーバトルのチーム　1花2岩
                BYTE unknown; //1にすると/joyのモーションを取る（マリオネット変身時。）2にすると～
                BYTE unknown; // 0?
                BYTE guest; // ゲストIDかどうか
                BYTE level; // レベル（ペットは1固定
                DWORD wrp_rank; // WRP順位（ペットは -1固定。別のパケで主人の値が送られてくる
                */
            }
        }

        
    }
}

