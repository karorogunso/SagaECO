using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_EFFECT : Packet
    {
//DWORD  item_id;          // アイテムID
//ABYTE  unknown1;         // 
//DWORD  from_chara_id;    // アイテム使用者のサーバキャラID？
//ADWORD target_chara_id?; // 
//AWORD  hp;               // HPダメージ(マイナスの場合回復)
//AWORD  mp;               // MPダメージ(マイナスの場合回復)
//AWORD  sp;               // SPダメージ(マイナスの場合回復)
//ADWORD color_flag;       // 数字の色(MISS Avoid Criticalも？

        byte combo;
        public SSMG_ITEM_EFFECT(byte combo)
        {
            this.data = new byte[21 + 4 * combo + 6 * combo + 4 * combo];
            this.offset = 2;
            this.ID = 0x09c8;
            this.combo = combo;
            this.PutByte(1, 4);
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }


        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(combo, 10);
                for (int i = 0; i < combo; i++)
                {
                    this.PutUInt(value[i].ActorID, (ushort)(11 + i * 4));
                }
            }
        }



        public void SetHP(short[] hp)
        {
            this.PutByte(combo, (ushort)(11 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                this.PutShort(hp[i], (ushort)(12 + combo * 4 + i * 2));
            }
        }

        public void SetMP(short[] mp)
        {
            this.PutByte(combo, (ushort)(12 + combo * 4 + combo * 2));
            for (int i = 0; i < combo; i++)
            {
                this.PutShort(mp[i], (ushort)(13 + combo * 4 + combo * 2 + i * 2));
            }
        }

        public void SetSP(short[] sp)
        {
            this.PutByte(combo, (ushort)(13 + combo * 4 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                this.PutShort(sp[i], (ushort)(14 + combo * 4 + combo * 4 + i * 2));
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(14 + combo * 4 + combo * 6));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)(15 + combo * 4 + combo * 6 + i * 4));
            }
        }

 
    }
}

