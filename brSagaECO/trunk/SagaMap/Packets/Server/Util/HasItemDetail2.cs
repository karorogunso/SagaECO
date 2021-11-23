using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public abstract class HasItemDetail2 : Packet
    {
        uint price = 0;
        ushort shopCount = 0;
        public HasItemDetail2()
        {
           
        }

        public uint Price
        {
            set
            {
                this.price = value;
            }
        }

        public ushort ShopCount
        {
            set
            {
                this.shopCount = value;
            }
        }

        protected Item ItemDetail
        {
            set
            {
                this.PutUInt(value.ItemID);
                this.PutUInt(value.PictID);
                this.offset += 1;//4 bytes fusion + 1 byte place
                int identify = value.identified;
                if (value.Locked)
                    identify |= 0x20;
                this.PutInt(identify);
                this.PutUShort(value.Durability);
                this.PutUShort(value.maxDurability);
                if (value.BaseData.itemType != ItemType.PET && value.BaseData.itemType != ItemType.PET_NEKOMATA && value.BaseData.itemType != ItemType.RIDE_PET
                    && value.BaseData.itemType != ItemType.BACK_DEMON)
                    this.PutUShort(value.Refine); // 残り強化回数
                else
                    this.PutUShort(0);
                //Iris Cards
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_Iris)
                {
                    this.PutUShort(value.CurrentSlot);
                    for (int i = 0; i < 10; i++)
                    {
                        if (i < value.Cards.Count)
                            this.PutUInt(value.Cards[i].ID);
                        else
                            this.PutUInt(0);
                    }
                }
                this.PutByte(0); //  染色
                this.PutUShort(value.Stack);
                if (price == 0)
                    this.PutUInt(value.BaseData.price);
                else
                    this.PutInt(0);
                this.PutShort(0); // 商品個数
                this.PutUShort(value.BaseData.possessionWeight);
                this.offset += 8;
                //this.PutUShort(value.BaseData.weight);
                //this.PutUShort(value.BaseData.volume);
                //this.PutUShort(value.BaseData.possibleSkill);
                //this.PutUShort(value.BaseData.possessionWeight);
                this.PutUShort(value.BaseData.passiveSkill);
                this.PutUShort(value.BaseData.possessionSkill);
                this.PutUShort(value.BaseData.possessionPassiveSkill);
                this.PutShort((short)(value.BaseData.str + value.Str));
                this.PutShort((short)(value.BaseData.mag + value.Mag));
                this.PutShort((short)(value.BaseData.vit + value.Vit));
                this.PutShort((short)(value.BaseData.dex + value.Dex));
                this.PutShort((short)(value.BaseData.agi + value.Agi));
                this.PutShort((short)(value.BaseData.intel + value.Int));
                this.PutShort(value.BaseData.luk);
                this.PutShort(value.BaseData.cha);
                this.PutShort((short)(value.BaseData.hp + value.HP));
                this.PutShort((short)(value.BaseData.sp + value.SP));
                this.PutShort((short)(value.BaseData.mp + value.MP));
                this.PutShort((short)(value.BaseData.speedUp + value.SpeedUp));
                this.PutShort((short)(value.BaseData.atk1 + value.Atk1));
                this.PutShort((short)(value.BaseData.atk2 + value.Atk2));
                this.PutShort((short)(value.BaseData.atk3 + value.Atk3));
                this.PutShort((short)(value.BaseData.matk + value.MAtk));
                this.PutShort((short)(value.BaseData.def + value.Def));
                this.PutShort((short)(value.BaseData.mdef + value.MDef));
                this.PutShort((short)(value.BaseData.hitMelee + value.HitMelee));
                this.PutShort((short)(value.BaseData.hitRanged + value.HitRanged));
                this.PutShort((short)(value.BaseData.hitMagic + value.HitMagic));
                this.PutShort((short)(value.BaseData.avoidMelee + value.AvoidMelee));
                this.PutShort((short)(value.BaseData.avoidRanged + value.AvoidRanged));
                this.PutShort((short)(value.BaseData.avoidMagic + value.AvoidMagic));
                this.PutShort((short)(value.BaseData.hitCritical + value.HitCritical));
                this.PutShort((short)(value.BaseData.avoidCritical + value.AvoidCritical));
                this.PutShort(value.BaseData.hpRecover);
                this.PutShort(value.BaseData.mpRecover);
                this.offset += 2;
                for (int i = 0; i < 7; i++)
                {
                    if (value.BaseData.element.ContainsKey((Elements)i))
                    {
                        this.PutShort(value.BaseData.element[(Elements)i]);
                    }
                }

                for (int i = 1; i <= 9; i++)
                {
                    if (value.BaseData.abnormalStatus.ContainsKey((AbnormalStatus)i))
                    {
                        this.PutShort(value.BaseData.abnormalStatus[(AbnormalStatus)i]);
                    }
                }

                this.PutShort(0);// ペットステ（攻撃速度
                this.PutShort(0);// ペットステ（詠唱速度
                this.PutShort(0); // ペットステ？（スタミナ回復力？倉では参照されない。
                this.PutInt(0);//unknown6
                this.PutInt(0);//unknown7
                this.PutUInt(price);//price 商品の値段（露天商）（上の方のpriceの値と一致するとは限らない
                this.PutUShort(shopCount);//num 販売個数（露天商）（上の方の個数と一緒?
                this.PutInt(0);//unknown8
                this.PutShort(0);//unknown9
                this.PutShort(0);//name_length
                this.PutByte(0);//name
                this.PutByte(0);//unknown10
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_Iris)
                {
                    if (value.Rental)
                    {
                        this.PutInt((int)(value.RentalTime - DateTime.Now).TotalSeconds);
                        this.PutByte(1);
                    }
                    else
                    {
                        this.PutInt(-1);//貸出品のとき残り貸出期間(秒)、それ以外-1　
                        this.PutByte(0);//貸出アイテムフラグ
                    }
                }
                /*
                  short  atk_speed;      
short  mgk_speed;      
short  heal_stamina?; 

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
                DWORD? unknown11;      // 貸出品のとき残り貸出期間(秒)、それ以外-1　
 BYTE  unkwnon12;      // 貸出アイテムフラグ

                 */

            }
        }
    }
}

