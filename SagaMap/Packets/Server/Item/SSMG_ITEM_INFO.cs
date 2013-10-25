using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_INFO : Packet
    {
        public SSMG_ITEM_INFO()
        {
            this.data = new byte[171];
            this.offset = 2;
            this.ID = 0x0203;   
        }

        public Item Item
        {
            set
            {
                this.PutByte(0xa6, 3);
                this.PutUInt(value.ItemID, 8);
                this.PutUInt(value.identified, 17);
                this.PutUShort(value.durability, 21);
                this.PutUShort(value.maxDurability, 23);
                this.PutUShort(0, 25); // 残り強化回数
                this.PutByte(0, 27); //  染色
                this.PutUShort(value.stack, 28);
                this.PutUInt(value.BaseData.price, 30);
                this.PutUShort(0, 34); // 商品個数
                this.PutUShort(value.BaseData.possessionWeight, 36);
                //this.PutUShort(value.BaseData.weight, 38);
                //this.PutUShort(value.BaseData.volume, 40);
                //this.PutUShort(value.BaseData.possibleSkill, 42);
                //this.PutUShort(value.BaseData.possessionWeight, 44);
                this.PutUShort(value.BaseData.passiveSkill, 46);
                this.PutUShort(value.BaseData.possessionSkill, 48);
                this.PutUShort(value.BaseData.possessionPassiveSkill, 50);
                this.PutShort(value.BaseData.str, 52);
                this.PutShort(value.BaseData.mag, 54);
                this.PutShort(value.BaseData.vit, 56);
                this.PutShort(value.BaseData.dex, 58);
                this.PutShort(value.BaseData.agi, 60);
                this.PutShort(value.BaseData.intel, 62);
                this.PutShort(value.BaseData.luk, 64);
                this.PutShort(value.BaseData.cha, 66);
                this.PutShort(value.BaseData.hp, 68);
                this.PutShort(value.BaseData.sp, 70);
                this.PutShort(value.BaseData.mp, 72);
                this.PutShort(value.BaseData.speedUp, 74);
                this.PutShort(value.BaseData.atk1, 76);
                this.PutShort(value.BaseData.atk2, 78);
                this.PutShort(value.BaseData.atk3, 80);
                this.PutShort(value.BaseData.matk, 82);
                this.PutShort(value.BaseData.def, 84);
                this.PutShort(value.BaseData.mdef, 86);
                this.PutShort(value.BaseData.hitMelee, 88);
                this.PutShort(value.BaseData.hitRanged, 90);
                this.PutShort(value.BaseData.hitMagic, 92);
                this.PutShort(value.BaseData.avoidMelee, 94);
                this.PutShort(value.BaseData.avoidRanged, 96);
                this.PutShort(value.BaseData.avoidMagic, 98);
                this.PutShort(value.BaseData.hitCritical, 100);
                this.PutShort(value.BaseData.avoidCritical, 102);
                this.PutShort(value.BaseData.hpRecover, 104);
                this.PutShort(value.BaseData.mpRecover, 106);
                for (int i = 0; i < 7; i++)
                {
                    if (value.BaseData.element.ContainsKey((Elements)i))
                    {
                        this.PutShort(value.BaseData.hpRecover, (ushort)(110 + i * 2));
                    }
                }
                for (int i = 1; i <= 9; i++)
                {
                    if (value.BaseData.abnormalStatus.ContainsKey((AbnormalStatus)i))
                    {
                        this.PutShort(value.BaseData.hpRecover, (ushort)(124 + i * 2));
                    }
                }
                /*
                  short  atk_speed;      // ペットステ（攻撃速度
short  mgk_speed;      // ペットステ（詠唱速度
short  heal_stamina?;  // ペットステ？（スタミナ回復力？倉では参照されない。

DWORD  unknown6;       //
 WORD  unknown7;       //
DWORD  price;          // 商品の値段（露天商）（上の方のpriceの値と一致するとは限らない
 WORD  num;            // 販売個数（露天商）（上の方の個数と一緒?
DWORD  unknown8;       //
 WORD  unknown9;       //

 WORD  name_length;    // 次の名前の文字列長と同じ？
 TSTR  name;           // 固有ネーム（ペットの名前とか
                         //（";ab";という名前ならname_length = 0003, name = 03 'a' 'b' '\0'
 BYTE  unknown10;      // 0固定？

                 */

            }
        }

        public byte Size
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }

        /// <summary>
        /// 見た目,フィギュア,スケッチ情報（ItemID or MobIDが入る
        /// </summary>
        public uint Fusion
        {
            set
            {
                this.PutUInt(value, 12);
            }
        }

        public ContainerType Container
        {
            set
            {
                this.PutByte((byte)value, 16);
            }
        }

        public bool Identified
        {
            set
            {
                if (value == true)
                    this.PutUInt(1, 17);
                else
                    this.PutUInt(0, 17);
            }
        }

        public ushort Durability
        {
            set
            {
                this.PutUShort(value, 21);
            }
        }

        public ushort DurabilityMax
        {
            set
            {
                this.PutUShort(value, 23);
            }
        }

        public ushort Enhancement
        {
            set
            {
                this.PutUShort(value, 25);
            }
        }

        public byte Dye
        {
            set
            {
                this.PutByte(value, 27);
            }
        }

        public uint Price
        {
            set
            {
                this.PutUInt(value, 28);
            }
        }

        public ushort SalesNumber
        {
            set
            {
                this.PutUShort(value, 32);
            }
        }

        public short PossessionWeight
        {
            set
            {
                this.PutShort(value, 34);
            }
        }

        public short Weight
        {
            set
            {
                this.PutShort(value, 36);
            }
        }

        public short Capacity
        {
            set
            {
                this.PutShort(value, 38);
            }
        }

        public short SkillUse
        {
            set
            {
                this.PutShort(value, 40);
            }
        }

        public short SkillActive
        {
            set
            {
                this.PutShort(value, 42);
            }
        }

        public short SkillPassive
        {
            set
            {
                this.PutShort(value, 44);
            }
        }

        public short SkillPossesionActive
        {
            set
            {
                this.PutShort(value, 46);
            }
        }

        public short SkillPossessionPassive
        {
            set
            {
                this.PutShort(value, 48);
            }
        }

        public short Str
        {
            set
            {
                this.PutShort(value, 50);
            }
        }

        public short Mag
        {
            set
            {
                this.PutShort(value, 52);
            }
        }

        public short Vit
        {
            set
            {
                this.PutShort(value, 54);
            }
        }

        public short Dex
        {
            set
            {
                this.PutShort(value, 56);
            }
        }

        public short Agi
        {
            set
            {
                this.PutShort(value, 58);
            }
        }

        public short Int
        {
            set
            {
                this.PutShort(value, 60);
            }
        }

        public short Luk
        {
            set
            {
                this.PutShort(value, 62);
            }
        }

        public short Cha
        {
            set
            {
                this.PutShort(value, 64);
            }
        }

        public short HP
        {
            set
            {
                this.PutShort(value, 66);
            }
        }

        public short SP
        {
            set
            {
                this.PutShort(value, 68);
            }
        }

        public short MP
        {
            set
            {
                this.PutShort(value, 70);
            }
        }

        public short Speed
        {
            set
            {
                this.PutShort(value, 72);
            }
        }

        public short Attack1
        {
            set
            {
                this.PutShort(value, 74);
            }
        }

        public short Attack2
        {
            set
            {
                this.PutShort(value, 76);
            }
        }

        public short Attack3
        {
            set
            {
                this.PutShort(value, 78);
            }
        }

        public short MATK
        {
            set
            {
                this.PutShort(value, 80);
            }
        }

        public short Def
        {
            set
            {
                this.PutShort(value, 82);
            }
        }

        public short MDef
        {
            set
            {
                this.PutShort(value, 84);
            }
        }

        public short HitMelee
        {
            set
            {
                this.PutShort(value, 86);
            }
        }

        public short HitRanged
        {
            set
            {
                this.PutShort(value, 88);
            }
        }

        public short HitMagic
        {
            set
            {
                this.PutShort(value, 90);
            }
        }

        public short AvoidMelee
        {
            set
            {
                this.PutShort(value, 92);
            }
        }

        public short AvoidRanged
        {
            set
            {
                this.PutShort(value, 94);
            }
        }

        public short AvoidMagic
        {
            set
            {
                this.PutShort(value, 96);
            }
        }

        public short HitCritical
        {
            set
            {
                this.PutShort(value, 98);
            }
        }

        public short AvoidCritical
        {
            set
            {
                this.PutShort(value, 100);
            }
        }

        public short Unknown
        {
            set
            {
                this.PutShort(value, 102);
            }
        }

        public short Unknown2
        {
            set
            {
                this.PutShort(value, 104);
            }
        }

        public short Fire
        {
            set
            {
                this.PutShort(value, 106);
            }
        }

        public short Water
        {
            set
            {
                this.PutShort(value, 108);
            }
        }

        public short Wind
        {
            set
            {
                this.PutShort(value, 110);
            }
        }

        public short Earth
        {
            set
            {
                this.PutShort(value, 112);
            }
        }

        public short Light
        {
            set
            {
                this.PutShort(value, 114);
            }
        }

        public short Dark
        {
            set
            {
                this.PutShort(value, 116);
            }
        }

        public short Poison
        {
            set
            {
                this.PutShort(value, 118);
            }
        }

        public short Stone
        {
            set
            {
                this.PutShort(value, 120);
            }
        }

        public short Paralyze
        {
            set
            {
                this.PutShort(value, 122);
            }
        }

        public short Sleep
        {
            set
            {
                this.PutShort(value, 124);
            }
        }

        public short Silence
        {
            set
            {
                this.PutShort(value, 126);
            }
        }

        public short Slow
        {
            set
            {
                this.PutShort(value, 128);
            }
        }

        public short Confuse
        {
            set
            {
                this.PutShort(value, 130);
            }
        }

        public short Freeze
        {
            set
            {
                this.PutShort(value, 132);
            }
        }

        public short Stan
        {
            set
            {
                this.PutShort(value, 134);
            }
        }

        public short ASPD
        {
            set
            {
                this.PutShort(value, 136);
            }
        }

        public short CSPD
        {
            set
            {
                this.PutShort(value, 138);
            }
        }

        public short Stamina
        {
            set
            {
                this.PutShort(value, 140);
            }
        }

        public uint Unknown3
        {
            set
            {
                this.PutUInt(value, 142);
            }
        }

        public ushort Unknown4
        {
            set
            {
                this.PutUShort(value, 146);
            }
        }

        public uint Price2
        {
            set
            {
                this.PutUInt(value, 148);
            }
        }

        public short Num
        {
            set
            {
                this.PutShort(value, 150);
            }
        }
        /*
        DWORD unknown8;       //
        WORD unknown9;       //

        WORD name_length;    // 次の名前の文字列長と同じ？
        TSTR name;           // 固有ネーム（ペットの名前とか
        //（";ab";という名前ならname_length = 0003, name = 03 'a' 'b' '\0'
        BYTE unknown10;      // 0固定？
        */
    }
}

