using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public abstract class HasItemDetail2 : Packet
    {
        ulong price = 0;
        ushort shopCount = 0;
        public HasItemDetail2()
        {
           
        }

        public ulong Price
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
                if (value.Refine > 0)
                    if (value.EquipSlot.Count > 0)
                        if (value.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE || value.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND ||
                            value.EquipSlot[0] == EnumEquipSlot.LEFT_HAND || value.EquipSlot[0] == EnumEquipSlot.UPPER_BODY)
                            ItemFactory.Instance.CalcRefineBouns(value);
                this.PutUInt(value.ItemID);
                this.PutUInt(value.PictID);
                this.offset += 1;//4 bytes fusion + 1 byte place
                int identify = value.identified;
                if (value.Locked)
                    identify |= 0x20;
                if (value.ChangeMode)
                    identify |= 0x81;
                if (value.Old)
                    identify |= 0x41;
                this.PutInt(0);
                this.PutInt(identify);//新增??? not partner or not partner initialized then 01 if has partner initialed then 0801
                this.PutUShort(value.Durability);
                this.PutUShort(value.maxDurability);
                if (value.BaseData.itemType != ItemType.PET && value.BaseData.itemType != ItemType.PET_NEKOMATA && value.BaseData.itemType != ItemType.RIDE_PET
                    && value.BaseData.itemType != ItemType.BACK_DEMON && value.BaseData.itemType != ItemType.PARTNER && value.BaseData.itemType != ItemType.RIDE_PARTNER)
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
                //this.PutUShort(value.BaseData.possessionWeight);
                //this.offset += 8;
                this.PutShort(100);//unknown2016年2月21日
                this.PutShort(110);//unknown2016年2月21日
                this.PutShort((short)(value.BaseData.weightUp+value.WeightUp + value.atk_refine*10));
                this.PutShort((short)(value.BaseData.volumeUp + value.VolumeUp + value.matk_refine * 10));
                //this.PutUShort(value.BaseData.possibleSkill);
                //this.PutUShort(value.BaseData.possessionWeight);
                /*this.PutUShort(value.BaseData.passiveSkill);
                this.PutUShort(value.BaseData.possessionSkill);
                this.PutUShort(value.BaseData.possessionPassiveSkill);*/
                this.PutShort((short)(value.BaseData.str + value.Str + value.Potential_Str));
                this.PutShort((short)(value.BaseData.mag + value.Mag + value.Potential_Mag));
                this.PutShort((short)(value.BaseData.vit + value.Vit + value.Potential_Vit));
                this.PutShort((short)(value.BaseData.dex + value.Dex + value.Potential_Dex));
                this.PutShort((short)(value.BaseData.agi + value.Agi + value.Potential_Agi));
                this.PutShort((short)(value.BaseData.intel + value.Int + value.Potential_Int));
                this.PutShort((short)(value.BaseData.luk + value.Luk));
                this.PutShort((short)(value.BaseData.cha + value.Cha));
                this.PutShort((short)(value.BaseData.hp + value.HP + value.hp_refine + value.Potential_HP));
                this.PutShort((short)(value.BaseData.sp + value.SP));
                this.PutShort((short)(value.BaseData.mp + value.MP));
                this.PutShort((short)(value.BaseData.speedUp + value.SpeedUp));
                this.PutShort((short)(value.BaseData.atk1 + value.Atk1 + value.atkrate_refine + value.Potential_ATK_rate));
                this.PutShort((short)(value.BaseData.atk2 + value.Atk2 + value.atkrate_refine + value.Potential_ATK_rate));
                this.PutShort((short)(value.BaseData.atk3 + value.Atk3 + value.atkrate_refine + value.Potential_ATK_rate));
                this.PutShort((short)(value.BaseData.matk + value.MAtk + value.matkrate_refine + value.Potential_MATK_rate));
                this.PutShort((short)(value.BaseData.def + value.Def + value.defrate_refine));
                this.PutShort((short)(value.BaseData.mdef + value.MDef + value.mdefrate_refine));
                this.PutShort((short)(value.BaseData.hitMelee + value.HitMelee));
                this.PutShort((short)(value.BaseData.hitRanged + value.HitRanged));
                this.PutShort((short)(value.BaseData.hitMagic + value.HitMagic));
                this.PutShort((short)(value.BaseData.avoidMelee + value.AvoidMelee));
                this.PutShort((short)(value.BaseData.avoidRanged + value.AvoidRanged));
                this.PutShort((short)(value.BaseData.avoidMagic + value.AvoidMagic));
                this.PutShort((short)(value.BaseData.hitCritical + value.HitCritical + value.cri_refine));
                this.PutShort((short)(value.BaseData.avoidCritical + value.AvoidCritical));
                this.PutShort((short)(value.BaseData.hpRecover + value.HPRecover + value.recover_refine));//recovery both???
                this.PutShort((short)(value.BaseData.mpRecover + value.MPRecover));
                this.PutShort((short)(value.BaseData.spRecover + value.SPRecover));
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

                this.PutShort(0);//unknown2016年2月21日
                this.PutShort((short)(value.ASPD + value.spd_refine + value.Potential_Aspd));// ペットステ（攻撃速度
                this.PutShort((short)(value.CSPD + value.spd_refine + value.Potential_Cspd));// ペットステ（詠唱速度
                this.PutShort(0); // ペットステ？（スタミナ回復力？倉では参照されない。
                this.PutInt(0);//unknown6
                this.PutInt(0);//unknown7
                //this.PutShort(0);
                this.PutULong(price);//price 商品の値段（露天商）（上の方のpriceの値と一致するとは限らない
                this.PutUShort(shopCount);//num 販売個数（露天商）（上の方の個数と一緒?
                this.PutShort(0);
                //this.PutInt(0);//unknown8
                this.PutShort(0);//unknown9
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

