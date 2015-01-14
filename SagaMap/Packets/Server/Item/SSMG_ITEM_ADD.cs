using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ADD : Packet
    {
        public SSMG_ITEM_ADD()
        {
            this.data = new byte[170];
            this.offset = 2;
            this.ID = 0x09D4;   
        }

        public Item Item
        {
            set
            {
                this.PutByte(0xa6, 2);
                this.PutUInt(value.ItemID, 7);
                this.PutUInt(value.identified, 16);
                this.PutUShort(value.durability, 20);
                this.PutUShort(value.maxDurability, 22);
                this.PutUShort(0, 24); // 残り強化回数
                this.PutByte(0, 26); //  染色
                this.PutUShort(value.stack, 27);
                this.PutUInt(value.BaseData.price, 29);
                this.PutUShort(0, 33); // 商品個数
                this.PutUShort(value.BaseData.possessionWeight, 35);
                //this.PutUShort(value.BaseData.weight, 37);
                //this.PutUShort(value.BaseData.volume, 39);
                //this.PutUShort(value.BaseData.possibleSkill, 41);
                //this.PutUShort(value.BaseData.possessionWeight, 43);
                this.PutUShort(value.BaseData.passiveSkill, 45);
                this.PutUShort(value.BaseData.possessionSkill, 47);
                this.PutUShort(value.BaseData.possessionPassiveSkill, 49);
                this.PutShort(value.BaseData.str, 51);
                this.PutShort(value.BaseData.mag, 53);
                this.PutShort(value.BaseData.vit, 55);
                this.PutShort(value.BaseData.dex, 57);
                this.PutShort(value.BaseData.agi, 59);
                this.PutShort(value.BaseData.intel, 61);
                this.PutShort(value.BaseData.luk, 63);
                this.PutShort(value.BaseData.cha, 65);
                this.PutShort(value.BaseData.hp, 67);
                this.PutShort(value.BaseData.sp, 69);
                this.PutShort(value.BaseData.mp, 71);
                this.PutShort(value.BaseData.speedUp, 73);
                this.PutShort(value.BaseData.atk1, 75);
                this.PutShort(value.BaseData.atk2, 77);
                this.PutShort(value.BaseData.atk3, 79);
                this.PutShort(value.BaseData.matk, 81);
                this.PutShort(value.BaseData.def, 83);
                this.PutShort(value.BaseData.mdef, 85);
                this.PutShort(value.BaseData.hitMelee, 87);
                this.PutShort(value.BaseData.hitRanged, 89);
                this.PutShort(value.BaseData.hitMagic, 91);
                this.PutShort(value.BaseData.avoidMelee, 93);
                this.PutShort(value.BaseData.avoidRanged, 95);
                this.PutShort(value.BaseData.avoidMagic, 97);
                this.PutShort(value.BaseData.hitCritical, 99);
                this.PutShort(value.BaseData.avoidCritical, 101);
                this.PutShort(value.BaseData.hpRecover, 103);
                this.PutShort(value.BaseData.mpRecover, 105);
                for (int i = 0; i < 7; i++)
                {
                    if (value.BaseData.element.ContainsKey((Elements)i))
                    {
                        this.PutShort(value.BaseData.hpRecover, (ushort)(109 + i * 2));
                    }
                }
                for (int i = 1; i <= 9; i++)
                {
                    if (value.BaseData.abnormalStatus.ContainsKey((AbnormalStatus)i))
                    {
                        this.PutShort(value.BaseData.hpRecover, (ushort)(123 + i * 2));
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
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public ContainerType Container
        {
            set
            {
                this.PutByte((byte)value, 15);
            }
        }

        
    }
}

