using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Mob;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_PC_INFO : Packet
    {
        public SSMG_ACTOR_PC_INFO()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                //this.data = new byte[166];
                this.data = new byte[239];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                this.data = new byte[145];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga14)
                this.data = new byte[150];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga13)
                this.data = new byte[145];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[140];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[137];
            else
                this.data = new byte[144];
            this.offset = 2;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.ID = 0x020D;
            else
                this.ID = 0x020E;
        }
        ActorShadow SetShadow
        {
            set
            {
                #region Old
                if (Configuration.Instance.Version < SagaLib.Version.Saga17)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;
                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(0xFFFFFFFF, 6);

                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    if (value.Owner.Marionette == null)
                    {
                        this.PutByte((byte)value.Owner.Race, offset);
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            offset++;
                            this.PutByte((byte)value.Owner.Form, offset);
                        }
                        this.PutByte((byte)value.Owner.Gender, (ushort)(offset + 1));
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                        {
                            this.PutUShort(value.Owner.HairStyle, (ushort)(offset + 2));
                            this.PutByte(value.Owner.HairColor, (ushort)(offset + 4));
                            this.PutUShort(value.Owner.Wig, (ushort)(offset + 5));
                            this.PutByte(0xff, (ushort)(offset + 7));
                            this.PutUShort(value.Owner.Face, (ushort)(offset + 8));
                            offset++;
                            this.PutByte(0, (ushort)(offset + 8));//unknown
                            offset += 2;
                        }
                        else
                        {
                            this.PutUShort(value.Owner.HairStyle, (ushort)(offset + 2));
                            this.PutByte(value.Owner.HairColor, (ushort)(offset + 3));
                            this.PutUShort(value.Owner.Wig, (ushort)(offset + 4));
                            this.PutByte(0xff, (ushort)(offset + 5));
                            this.PutUShort(value.Owner.Face, (ushort)(offset + 6));
                        }
                    }
                    else
                    {
                        this.PutByte(0xff, offset);
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            offset++;
                            this.PutByte(0xff, offset);
                        }
                        this.PutByte(0xff, (ushort)(offset + 1));
                        this.PutByte(0xff, (ushort)(offset + 2));
                        this.PutByte(0xff, (ushort)(offset + 3));
                        this.PutByte(0xff, (ushort)(offset + 4));
                        this.PutByte(0xff, (ushort)(offset + 5));
                        this.PutByte(0xff, (ushort)(offset + 6));
                        this.PutByte(0xff, (ushort)(offset + 7));
                        this.PutByte(0xff, (ushort)(offset + 8));
                        this.PutByte(0xff, (ushort)(offset + 9));
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                        {
                            this.PutByte(0xff, (ushort)(++offset + 9));
                            this.PutByte(0xff, (ushort)(++offset + 9));
                            this.PutByte(0xff, (ushort)(++offset + 9));
                        }
                    }
                    this.PutByte(0x0D, (ushort)(offset + 10));
                    if (value.Owner.Marionette == null)
                    {
                        for (int j = 0; j < 14; j++)
                        {
                            if (value.Owner.Inventory.Equipments.ContainsKey((EnumEquipSlot)j))
                            {
                                Item item = value.Owner.Inventory.Equipments[(EnumEquipSlot)j];
                                if (item.Stack == 0) continue;
                                if (item.PictID == 0)
                                    this.PutUInt(item.BaseData.imageID, (ushort)(offset + 11 + j * 4));
                                else
                                    this.PutUInt(item.PictID, (ushort)(offset + 11 + j * 4));
                            }
                        }
                    }
                    else
                    {
                        this.PutUInt(value.Owner.Marionette.PictID, (ushort)(offset + 11));
                    }

                    //left hand weapon motion
                    this.PutByte(3, (ushort)(offset + 63));
                    if (value.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if (value.Owner.Marionette == null)
                        {
                            Item leftHand = value.Owner.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                            this.PutUShort(leftHand.BaseData.handMotion, (ushort)(offset + 64));
                            this.PutByte(leftHand.BaseData.handMotion2, (ushort)(offset + 66));
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 64));
                    }

                    //right hand weapon motion
                    this.PutByte(3, (ushort)(offset + 67));
                    if (value.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (value.Owner.Marionette == null)
                        {
                            Item rightHand = value.Owner.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                            this.PutUShort(rightHand.BaseData.handMotion, (ushort)(offset + 68));
                            this.PutByte(rightHand.BaseData.handMotion2, (ushort)(offset + 80));
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 68));
                    }

                    this.PutByte(3, (ushort)(offset + 71));

                    this.PutByte(0, (ushort)(offset + 80));
                    this.PutByte(1, (ushort)(offset + 81));//party name
                    this.PutByte(0, (ushort)(offset + 83));//party name

                    this.PutByte(1, (ushort)(offset + 88));//Ring name

                    this.PutByte(0, (ushort)(offset + 90));//Ring master

                    this.PutByte(1, (ushort)(offset + 91));//Sign name

                    this.PutByte(1, (ushort)(offset + 93));//shop name

                    this.PutUInt(500, (ushort)(offset + 96));//size
                    this.PutUInt(2, (ushort)(offset + 106));//size

                    this.PutUShort(0, (ushort)(offset + 110));
                }
                #endregion
                #region Saga17
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;

                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(0xFFFFFFFF, 6);

                    ///////////////玩家角色名///////////////
                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;//角色名长度
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    ////////////////玩家外观////////////////
                    if (value.Owner.Marionette == null)
                    {
                        this.PutByte((byte)value.Owner.Race, offset);
                        this.PutByte((byte)value.Owner.Form, offset + 1);
                        this.PutByte((byte)value.Owner.Gender, offset + 2);
                        this.PutUShort(value.Owner.HairStyle, offset + 3);
                        this.PutByte(value.Owner.HairColor, offset + 5);
                        this.PutUShort(value.Owner.Wig, offset + 6);
                        this.PutByte(0xff, offset + 8);
                        this.PutUShort(value.Owner.Face, offset + 9);
                        //3转外观
                        this.PutByte(value.Owner.TailStyle, offset + 11);
                        this.PutByte(value.Owner.WingStyle, offset + 13);
                        this.PutByte(value.Owner.WingColor, offset + 14);
                    }
                    else
                    {
                        this.PutByte(0xff, offset);
                        this.PutByte(0xff, offset + 1);
                        this.PutByte(0xff, offset + 2);
                        this.PutByte(0xff, offset + 3);
                        this.PutByte(0xff, offset + 4);
                        this.PutByte(0xff, offset + 5);
                        this.PutByte(0xff, offset + 6);
                        this.PutByte(0xff, offset + 7);
                        this.PutByte(0xff, offset + 8);
                        this.PutByte(0xff, offset + 9);
                        this.PutByte(0xff, offset + 10);
                        this.PutByte(0xff, offset + 11);
                        this.PutByte(0xff, offset + 12);
                        this.PutByte(0xff, offset + 13);
                        this.PutByte(0xff, offset + 14);
                    }

                    this.PutByte(0x0E, offset + 15);

                    ////////////////玩家装备////////////////
                    Dictionary<EnumEquipSlot, Item> equips;
                    if (value.Owner.Form != DEM_FORM.MACHINA_FORM)
                        equips = value.Owner.Inventory.Equipments;
                    else
                        equips = value.Owner.Inventory.Parts;
                    if (value.Owner.Marionette == null)
                    {
                        if (value.Owner.TranceID == 0)
                            for (int j = 0; j < 14; j++)
                            {
                                if (equips.ContainsKey((EnumEquipSlot)j))
                                {
                                    Item item = equips[(EnumEquipSlot)j];
                                    if (item.Stack == 0) continue;
                                    if (item.PictID == 0)
                                        this.PutUInt(item.BaseData.imageID, offset + 16 + j * 4);
                                    else if (item.BaseData.itemType != ItemType.PET_NEKOMATA)
                                        this.PutUInt(item.PictID, offset + 16 + j * 4);
                                }
                            }
                        else
                            this.PutUInt(value.Owner.TranceID, offset + 16);
                    }
                    else
                        this.PutUInt(value.Owner.Marionette.PictID, offset + 16);

                    ////////////////左手动作////////////////
                    this.PutByte(3, offset + 72);
                    if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if (value.Owner.Marionette == null && value.Owner.TranceID == 0)
                        {
                            Item leftHand = equips[EnumEquipSlot.LEFT_HAND];
                            this.PutUShort(leftHand.BaseData.handMotion, offset + 73);
                            this.PutByte(leftHand.BaseData.handMotion2, offset + 75);
                        }
                    }

                    ////////////////右手动作////////////////
                    this.PutByte(3, offset + 76);
                    if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (value.Owner.Marionette == null && value.Owner.TranceID == 0)
                        {
                            Item rightHand = equips[EnumEquipSlot.RIGHT_HAND];
                            this.PutUShort(rightHand.BaseData.handMotion, offset + 77);
                            this.PutByte(rightHand.BaseData.handMotion2, offset + 79);
                        }
                    }

                    //////////////////骑乘//////////////////
                    this.PutByte(3, offset + 80);
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Owner.Pet != null)
                    {
                        if (value.Owner.Pet.Ride)
                        {
                            Item pet = equips[EnumEquipSlot.PET];
                            this.PutUShort(pet.BaseData.handMotion, offset + 81);
                            this.PutByte(pet.BaseData.handMotion2, offset + 83);
                        }
                    }
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Owner.Pet != null)
                    {
                        if (value.Owner.Pet.Ride)
                        {
                            this.PutUInt(equips[EnumEquipSlot.PET].ItemID, offset + 84);
                        }
                    }
                    //BYTE ride_color; //乗り物の染色値
                    this.PutByte(0, offset + 89);

                    offset += 4;//2015年12月10日，对应449版本

                    ////////////////队伍信息////////////////
                    if (value.Owner.Party != null)
                    {
                        buf = Global.Unicode.GetBytes(value.Owner.Party.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, offset + 90);
                        this.PutBytes(buf, offset + 91);
                        offset += (ushort)(buf.Length - 1);
                        this.PutByte(0, offset + 92);
                    }
                    else
                    {
                        this.PutByte(1, offset + 90);
                        this.PutByte(1, offset + 92);
                    }
                    //UINT UNKNOMW
                    ////////////////军团信息////////////////
                    if (value.Owner.Ring != null)
                    {
                        buf = Global.Unicode.GetBytes(value.Owner.Ring.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, offset + 97);
                        this.PutBytes(buf, offset + 98);
                        offset += (ushort)(buf.Length - 1);
                        this.PutByte(0, offset + 99);
                    }
                    else
                    {
                        this.PutByte(1, offset + 97);
                        this.PutByte(1, offset + 99);
                    }

                    ///////////////聊天室信息///////////////
                    buf = Global.Unicode.GetBytes(value.Owner.Sign + "\0");
                    buff = new byte[this.data.Length + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, offset + 100);
                    this.PutBytes(buf, offset + 101);
                    offset += (ushort)(buf.Length - 1);

                    /////////////////露天商店////////////////
                    this.PutByte(1, offset + 102);

                    this.PutUInt(500, offset + 105);

                    this.PutUShort((ushort)value.Owner.Motion, offset + 109);

                    this.PutUInt(0, offset + 111);//unknown

                    /////////////////阵容信息////////////////
                    this.PutUInt(2, offset + 115);
                    this.PutUInt(0, offset + 119);

                    /////////////////等级信息////////////////
                    this.PutUInt(value.Level, offset + 128);
                    this.PutUInt(value.Owner.WRPRanking, offset + 132);
                    this.PutByte(0xFF, offset + 140);//师徒图标
                }
                #endregion
            }
        }
        ActorMob SetMob
        {
            set
            {
                byte[] buf, buff;
                byte size;
                ushort offset;

                this.PutUInt(value.ActorID, 2);
                this.PutUInt(0xFFFFFFFF, 6);

                buf = Global.Unicode.GetBytes(value.Name + "\0");
                size = (byte)buf.Length;//角色名长度
                buff = new byte[this.data.Length - 1 + size];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte(size, 10);
                this.PutBytes(buf, 11);
                offset = (ushort)(11 + size);

                this.PutByte(0xff, offset);
                this.PutByte(0xff, offset + 1);
                this.PutByte(0xff, offset + 2);
                this.PutByte(0xff, offset + 3);
                this.PutByte(0xff, offset + 4);
                this.PutByte(0xff, offset + 5);
                this.PutByte(0xff, offset + 6);
                this.PutByte(0xff, offset + 7);
                this.PutByte(0xff, offset + 8);
                this.PutByte(0xff, offset + 9);
                this.PutByte(0xff, offset + 10);
                this.PutByte(0xff, offset + 11);
                this.PutByte(0xff, offset + 12);
                this.PutByte(0xff, offset + 13);
                this.PutByte(0xff, offset + 14);
                this.PutByte(14, offset + 15);
                if (value.IllusionPictID != 0)   //变形状态
                    this.PutUInt(value.IllusionPictID, (ushort)(offset + 16));
                else if (value.PictID != 0)
                    this.PutUInt(value.PictID, (ushort)(offset + 16));
                else if (value.AnotherID != 0)
                    this.PutUInt(91000013, (ushort)(offset + 16));
                else if (value.BaseData.pictid != 0)
                    this.PutUInt(value.BaseData.pictid, (ushort)(offset + 16));
                this.PutByte(3, offset + 72);
                this.PutByte(3, offset + 79);
                this.PutByte(3, offset + 86);
                if (value.RideID != 0 && ItemFactory.Instance.Items.ContainsKey(value.RideID))
                {
                    Item pet = ItemFactory.Instance.GetItem(value.RideID);
                    this.PutByte((byte)pet.BaseData.handMotion, offset + 81);
                    this.PutByte(pet.BaseData.handMotion2, offset + 83);
                    this.PutUInt(pet.ItemID, offset + 84);
                }
                this.PutByte(0, offset + 89);

                //offset += 4;//2015年12月10日，对应449版本
                this.PutByte(1, offset + 103);
                //this.PutByte(0, offset + );
                ////////////////军团信息////////////////
                this.PutByte(1, offset + 110);
                //this.PutByte(0, offset + 99);
                this.PutByte(1, offset + 113);

                this.PutByte(1, offset + 115);

                if (value.TInt["playersize"] != 0)
                    this.PutUInt((uint)value.TInt["playersize"], offset + 118);
                else
                    this.PutUInt(900, offset + 118);

                //offset++;//2015年12月11日，对应449版本

                this.PutUInt(2, offset + 129);
                //this.PutUInt(0, offset + 119);

                this.PutByte(0x1, offset + 145);
                this.PutUInt(0xffffffff, offset + 146);
                this.PutUInt(0xffffffff, offset + 150);
                this.PutByte(0xff, offset + 154); //2015年12月11日，对应449版本
                this.PutUInt(0xffffffff, offset + 162);
                buff = new byte[this.data.Length + 1];
                this.data.CopyTo(buff, 0);
                this.data = buff;
            }
        }

        ActorPC SetPC
        {
            set
            {
                #region Saga14
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9 && Configuration.Instance.Version < SagaLib.Version.Saga14_2)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;
                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(value.CharID, 6);

                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    if (value.Marionette == null && value.TranceID == 0)
                    {
                        this.PutByte((byte)value.Race, offset);
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            offset++;
                            this.PutByte((byte)value.Form, offset);
                        }
                        this.PutByte((byte)value.Gender, (ushort)(offset + 1));
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                        {
                            this.PutUShort(value.HairStyle, (ushort)(offset + 2));
                            this.PutByte(value.HairColor, (ushort)(offset + 4));
                            this.PutUShort(value.Wig, (ushort)(offset + 5));

                            this.PutByte(0xff, (ushort)(offset + 7));
                            this.PutUShort(value.Face, (ushort)(offset + 8));
                            offset++;
                            this.PutByte(value.TailStyle, (ushort)(offset + 9));//3轉外觀
                            this.PutByte(value.WingStyle, (ushort)(offset + 10));//3轉外觀
                            this.PutByte(value.WingColor, (ushort)(offset + 11));//3轉外觀
                            offset += 2;
                        }
                        else
                        {
                            this.PutUShort(value.HairStyle, (ushort)(offset + 2));
                            this.PutByte(value.HairColor, (ushort)(offset + 3));
                            this.PutUShort(value.Wig, (ushort)(offset + 4));
                            this.PutByte(0xff, (ushort)(offset + 5));
                            this.PutUShort(value.Face, (ushort)(offset + 6));
                        }
                    }
                    else
                    {
                        this.PutByte(0xff, offset);
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            offset++;
                            this.PutByte(0xff, offset);
                        }
                        this.PutByte(0xff, (ushort)(offset + 1));
                        this.PutByte(0xff, (ushort)(offset + 2));
                        this.PutByte(0xff, (ushort)(offset + 3));
                        this.PutByte(0xff, (ushort)(offset + 4));
                        this.PutByte(0xff, (ushort)(offset + 5));
                        this.PutByte(0xff, (ushort)(offset + 6));
                        this.PutByte(0xff, (ushort)(offset + 7));
                        this.PutByte(0xff, (ushort)(offset + 8));
                        this.PutByte(0xff, (ushort)(offset + 9));
                        if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                        {
                            this.PutByte(0xff, (ushort)(++offset + 9));
                            this.PutByte(0xff, (ushort)(++offset + 9));
                            this.PutByte(0xff, (ushort)(++offset + 9));
                        }
                    }
                    Dictionary<EnumEquipSlot, Item> equips;
                    if (value.Form != DEM_FORM.MACHINA_FORM)
                        equips = value.Inventory.Equipments;
                    else
                        equips = value.Inventory.Parts;
                    this.PutByte(0x0D, (ushort)(offset + 10));
                    if (value.Marionette == null)
                    {
                        if (value.TranceID == 0)
                        {
                            for (int j = 0; j < 14; j++)
                            {
                                if (equips.ContainsKey((EnumEquipSlot)j))
                                {
                                    Item item = equips[(EnumEquipSlot)j];
                                    if (item.Stack == 0) continue;
                                    if (item.PictID == 0)
                                        this.PutUInt(item.BaseData.imageID, (ushort)(offset + 11 + j * 4));
                                    else if (item.BaseData.itemType != ItemType.PET_NEKOMATA)
                                        this.PutUInt(item.PictID, (ushort)(offset + 11 + j * 4));
                                }
                            }
                        }
                        else
                        {
                            this.PutUInt(value.TranceID, (ushort)(offset + 11));
                        }
                    }
                    else
                    {
                        this.PutUInt(value.Marionette.PictID, (ushort)(offset + 11));
                    }

                    //left hand weapon motion
                    this.PutByte(3, (ushort)(offset + 63));
                    if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if (value.Marionette == null)
                        {
                            Item leftHand = equips[EnumEquipSlot.LEFT_HAND];
                            this.PutUShort(leftHand.BaseData.handMotion, (ushort)(offset + 64));
                            this.PutByte(leftHand.BaseData.handMotion2, (ushort)(offset + 65));
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 64));
                    }

                    //right hand weapon motion
                    this.PutByte(3, (ushort)(offset + 67));
                    if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (value.Marionette == null)
                        {
                            Item rightHand = equips[EnumEquipSlot.RIGHT_HAND];
                            this.PutUShort(rightHand.BaseData.handMotion, (ushort)(offset + 68));
                            this.PutUShort(rightHand.BaseData.handMotion2, (ushort)(offset + 69));
                            if (rightHand.BaseData.itemType == ItemType.SHORT_SWORD || rightHand.BaseData.itemType == ItemType.SWORD)
                                this.PutByte(1, (ushort)(offset + 70));//匕首
                            else if (rightHand.BaseData.itemType == ItemType.SWORD)
                                this.PutByte(2, (ushort)(offset + 70));//剑
                            else if (rightHand.BaseData.itemType == ItemType.RAPIER)
                                this.PutByte(3, (ushort)(offset + 70));//长剑
                            else if (rightHand.BaseData.itemType == ItemType.CLAW)
                                this.PutByte(4, (ushort)(offset + 70));//爪
                            else if (rightHand.BaseData.itemType == ItemType.HAMMER)
                                this.PutByte(6, (ushort)(offset + 70));//锤
                            else if (rightHand.BaseData.itemType == ItemType.AXE)
                                this.PutByte(7, (ushort)(offset + 70));//斧
                            else if (rightHand.BaseData.itemType == ItemType.SPEAR)
                                this.PutByte(8, (ushort)(offset + 70));//矛
                            else if (rightHand.BaseData.itemType == ItemType.STAFF)
                                this.PutByte(9, (ushort)(offset + 70));//杖
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 68));
                    }

                    //riding motion
                    this.PutByte(3, (ushort)(offset + 71));
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            Item pet = equips[EnumEquipSlot.PET];
                            this.PutUShort(pet.BaseData.handMotion, (ushort)(offset + 72));
                            this.PutUShort(pet.BaseData.handMotion2, (ushort)(offset + 73));
                        }
                    }
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            this.PutUInt(equips[EnumEquipSlot.PET].ItemID, (ushort)(offset + 75));
                        }
                    }
                    //BYTE ride_color;  //乗り物の染色値
                    this.PutByte(value.BattleStatus, (ushort)(offset + 80));
                    if (value.Party == null)
                    {
                        this.PutByte(1, (ushort)(offset + 81));//party name
                        this.PutByte(1, (ushort)(offset + 83));//party name
                    }
                    else
                    {
                        buf = Global.Unicode.GetBytes(value.Party.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, (ushort)(offset + 81));
                        this.PutBytes(buf, (ushort)(offset + 82));
                        offset += (ushort)(buf.Length - 1);
                        if (value == value.Party.Leader)
                            this.PutByte(1, (ushort)(offset + 83));//party name
                        else
                            this.PutByte(0, (ushort)(offset + 83));//party name
                    }
                    if (value.Ring == null)
                    {
                        this.PutByte(1, (ushort)(offset + 88));//Ring name
                        this.PutByte(1, (ushort)(offset + 90));//Ring master
                    }
                    else
                    {
                        buf = Global.Unicode.GetBytes(value.Ring.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, (ushort)(offset + 88));
                        this.PutBytes(buf, (ushort)(offset + 89));
                        offset += (ushort)(buf.Length - 1);
                        if (value == value.Ring.Leader)
                            this.PutByte(1, (ushort)(offset + 90));//party name
                        else
                            this.PutByte(0, (ushort)(offset + 90));//party name
                    }

                    buf = Global.Unicode.GetBytes(value.Sign + "\0");
                    buff = new byte[this.data.Length + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, (ushort)(offset + 91));//sign name
                    this.PutBytes(buf, (ushort)(offset + 92));
                    offset += (ushort)(buf.Length - 1);

                    /*if (value.Shoptitle != null)
                    {
                        buf = Global.Unicode.GetBytes(value.Shoptitle + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, (ushort)(offset + 93));//shop name
                        this.PutBytes(buf, (ushort)(offset + 94));
                        offset += (ushort)(buf.Length - 1);
                        if (value.Shopswitch == 1)
                            this.PutByte(1, (ushort)(offset + 95));
                        else
                            this.PutByte(0, (ushort)(offset + 95));
                    }
                    else*/
                    this.PutByte(1, (ushort)(offset + 93));//shop name

                    this.PutUInt(1000, (ushort)(offset + 96));//size

                    this.PutUShort((ushort)value.Motion, (ushort)(offset + 100));

                    this.PutUInt(0, (ushort)(offset + 102));//unknown
                    switch (value.Mode)
                    {
                        case PlayerMode.NORMAL:
                            this.PutInt(2, (ushort)(offset + 106));//mode1
                            this.PutInt(0, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.COLISEUM_MODE:
                            this.PutInt(0x42, (ushort)(offset + 106));//mode1
                            this.PutInt(1, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.KNIGHT_WAR:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.WRP:
                            this.PutInt(0x102, (ushort)(offset + 106));//mode1
                            this.PutInt(4, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.KNIGHT_EAST:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(1, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_WEST:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(2, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_SOUTH:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(4, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_NORTH:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(8, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_FLOWER:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(0, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(1, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_ROCK:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(0, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(2, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                    }
                    //Logger.ShowInfo("SSMG_PLAYER_PC_INFO");
                    this.PutByte(0, (ushort)(offset + 116));
                    this.PutByte(0, (ushort)(offset + 117));
                    this.PutByte(0, (ushort)(offset + 118));
                    this.PutByte(value.Level, (ushort)(offset + 119));
                    this.PutUInt(value.WRPRanking, (ushort)(offset + 120));//// WRP順位（ペットは -1固定。別のパケで主人の値が送られてくる

                    /*
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
                #endregion
                #region Saga14_2
                if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2 && Configuration.Instance.Version < SagaLib.Version.Saga17)
                {

                    byte[] buf, buff;
                    byte size;
                    ushort offset;
                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(value.CharID, 6);

                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);
                    if (value.Marionette == null && value.TranceID == 0)
                    {
                        this.PutByte((byte)value.Race, offset);
                        offset++;
                        this.PutByte((byte)value.Form, offset);
                        this.PutByte((byte)value.Gender, (ushort)(offset + 1));
                        this.PutUShort(value.HairStyle, (ushort)(offset + 2));
                        this.PutByte(value.HairColor, (ushort)(offset + 4));
                        this.PutUShort(value.Wig, (ushort)(offset + 5));

                        this.PutByte(0xff, (ushort)(offset + 7));
                        this.PutUShort(value.Face, (ushort)(offset + 8));//unknown
                        offset += 2;
                        this.PutByte(value.TailStyle, (ushort)(offset + 9));//3轉外觀
                        this.PutByte(value.WingStyle, (ushort)(offset + 10));//3轉外觀
                        this.PutByte(value.WingColor, (ushort)(offset + 11));//3轉外觀
                        offset += 2;
                    }
                    else
                    {
                        this.PutByte(0xff, offset);

                        if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            offset++;
                            this.PutByte(0xff, offset);
                        }
                        this.PutByte(0xff, (ushort)(offset + 1));
                        this.PutByte(0xff, (ushort)(offset + 2));
                        this.PutByte(0xff, (ushort)(offset + 3));
                        this.PutByte(0xff, (ushort)(offset + 4));
                        this.PutByte(0xff, (ushort)(offset + 5));
                        this.PutByte(0xff, (ushort)(offset + 6));
                        this.PutByte(0xff, (ushort)(offset + 7));
                        this.PutByte(0xff, (ushort)(offset + 8));
                        offset++;
                        this.PutByte(0, (ushort)(offset + 8));//unknown
                        this.PutByte(0xff, (ushort)(offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                    }
                    Dictionary<EnumEquipSlot, Item> equips;
                    if (value.Form != DEM_FORM.MACHINA_FORM)
                        equips = value.Inventory.Equipments;
                    else
                        equips = value.Inventory.Parts;
                    this.PutByte(0x0E, (ushort)(offset + 10));
                    if (value.Marionette == null)
                    {
                        if (value.TranceID == 0)
                        {
                            for (int j = 0; j < 14; j++)
                            {
                                if (equips.ContainsKey((EnumEquipSlot)j))
                                {
                                    Item item = equips[(EnumEquipSlot)j];
                                    if (item.Stack == 0) continue;
                                    if (item.PictID == 0)
                                        this.PutUInt(item.BaseData.imageID, (ushort)(offset + 11 + j * 4));
                                    else if (item.BaseData.itemType != ItemType.PET_NEKOMATA)
                                        this.PutUInt(item.PictID, (ushort)(offset + 11 + j * 4));
                                }
                            }
                        }
                        else
                        {
                            this.PutUInt(value.TranceID, (ushort)(offset + 11));
                        }
                    }
                    else
                    {
                        this.PutUInt(value.Marionette.PictID, (ushort)(offset + 11));
                    }
                    offset += 4;
                    //left hand weapon motion
                    this.PutByte(3, (ushort)(offset + 63));
                    if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if (value.Marionette == null)
                        {
                            Item leftHand = equips[EnumEquipSlot.LEFT_HAND];
                            this.PutUShort(leftHand.BaseData.handMotion, (ushort)(offset + 64));//
                            this.PutUShort(leftHand.BaseData.handMotion2, (ushort)(offset + 65));//
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 64));
                    }

                    if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (value.Marionette == null)
                        {
                            Item rightHand = equips[EnumEquipSlot.RIGHT_HAND];
                            this.PutUShort(rightHand.BaseData.handMotion, (ushort)(offset + 66));//
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 64));
                    }
                    //right hand weapon motion
                    this.PutByte(3, (ushort)(offset + 67));
                    if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (value.Marionette == null)
                        {
                            Item rightHand = equips[EnumEquipSlot.RIGHT_HAND];
                            this.PutUShort(rightHand.BaseData.handMotion, (ushort)(offset + 68));
                            this.PutUShort(rightHand.BaseData.handMotion2, (ushort)(offset + 69));
                            if (rightHand.BaseData.itemType == ItemType.SHORT_SWORD || rightHand.BaseData.itemType == ItemType.SWORD)
                                this.PutByte(1, (ushort)(offset + 70));//匕首
                            else if (rightHand.BaseData.itemType == ItemType.SWORD)
                                this.PutByte(2, (ushort)(offset + 70));//剑
                            else if (rightHand.BaseData.itemType == ItemType.RAPIER)
                                this.PutByte(3, (ushort)(offset + 70));//长剑
                            else if (rightHand.BaseData.itemType == ItemType.CLAW)
                                this.PutByte(4, (ushort)(offset + 70));//爪
                            else if (rightHand.BaseData.itemType == ItemType.HAMMER)
                                this.PutByte(6, (ushort)(offset + 70));//锤
                            else if (rightHand.BaseData.itemType == ItemType.AXE)
                                this.PutByte(7, (ushort)(offset + 70));//斧
                            else if (rightHand.BaseData.itemType == ItemType.SPEAR)
                                this.PutByte(8, (ushort)(offset + 70));//矛
                            else if (rightHand.BaseData.itemType == ItemType.STAFF)
                                this.PutByte(9, (ushort)(offset + 70));//杖
                        }
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(offset + 68));
                    }

                    //riding motion
                    this.PutByte(3, (ushort)(offset + 71));
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            Item pet = equips[EnumEquipSlot.PET];
                            this.PutUShort(pet.BaseData.handMotion, (ushort)(offset + 72));
                            this.PutUShort(pet.BaseData.handMotion2, (ushort)(offset + 73));
                        }
                    }
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            this.PutUInt(equips[EnumEquipSlot.PET].ItemID, (ushort)(offset + 75));
                        }
                    }

                    //BYTE ride_color;  //乗り物の染色値
                    this.PutByte(0, (ushort)(offset + 80));
                    if (value.Party == null)
                    {
                        this.PutByte(1, (ushort)(offset + 81));//party name
                        this.PutByte(1, (ushort)(offset + 83));//party name
                    }
                    else
                    {
                        buf = Global.Unicode.GetBytes(value.Party.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, (ushort)(offset + 81));
                        this.PutBytes(buf, (ushort)(offset + 82));
                        offset += (ushort)(buf.Length - 1);
                        if (value == value.Party.Leader)
                            this.PutByte(1, (ushort)(offset + 83));//party name
                        else
                            this.PutByte(0, (ushort)(offset + 83));//party name
                    }
                    if (value.Ring == null)
                    {
                        this.PutByte(1, (ushort)(offset + 88));//Ring name
                        this.PutByte(1, (ushort)(offset + 90));//Ring master
                    }
                    else
                    {
                        buf = Global.Unicode.GetBytes(value.Ring.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, (ushort)(offset + 88));
                        this.PutBytes(buf, (ushort)(offset + 89));
                        offset += (ushort)(buf.Length - 1);
                        if (value == value.Ring.Leader)
                            this.PutByte(1, (ushort)(offset + 90));//party name
                        else
                            this.PutByte(0, (ushort)(offset + 90));//party name
                    }

                    buf = Global.Unicode.GetBytes(value.Sign + "\0");
                    buff = new byte[this.data.Length + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, (ushort)(offset + 91));//sign name
                    this.PutBytes(buf, (ushort)(offset + 92));
                    offset += (ushort)(buf.Length - 1);


                    this.PutByte(1, (ushort)(offset + 93));//shop name

                    this.PutUInt(1000, (ushort)(offset + 96));//size

                    this.PutUShort((ushort)value.Motion, (ushort)(offset + 100));

                    this.PutUInt(0, (ushort)(offset + 102));//unknown
                    switch (value.Mode)
                    {
                        case PlayerMode.NORMAL:
                            this.PutInt(2, (ushort)(offset + 106));//mode1
                            this.PutInt(0, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.COLISEUM_MODE:
                            this.PutInt(0x42, (ushort)(offset + 106));//mode1
                            this.PutInt(1, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.KNIGHT_WAR:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.WRP:
                            this.PutInt(0x102, (ushort)(offset + 106));//mode1
                            this.PutInt(4, (ushort)(offset + 110));//mode2
                            break;
                        case PlayerMode.KNIGHT_EAST:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(1, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_WEST:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(2, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_SOUTH:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(4, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_NORTH:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(8, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_FLOWER:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(0, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(1, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_ROCK:
                            this.PutInt(0x22, (ushort)(offset + 106));//mode1
                            this.PutInt(2, (ushort)(offset + 110));//mode2
                            this.PutByte(0, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(2, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                    }

                    this.PutByte(0, (ushort)(offset + 116));
                    this.PutByte(0, (ushort)(offset + 117));
                    this.PutByte(0, (ushort)(offset + 118));
                    this.PutByte(value.Level, (ushort)(offset + 120));
                    this.PutUInt(value.WRPRanking, (ushort)(offset + 121));
                    this.PutByte(0xFF, (ushort)(offset + 129));
                }
                #endregion
                #region Saga17
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;

                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(value.CharID, 6);

                    ///////////////玩家角色名///////////////
                    string name = value.Name;
                    //name = "糖果_" +value.Account.AccountID.ToString() +Global.Random.Next(10000, 99999);//万圣节活动

                    /*if (value.FirstName != "")
                        name = value.FirstName + "·" + name;*/
                    buf = Global.Unicode.GetBytes(name + "\0");
                    size = (byte)buf.Length;//角色名长度
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    ////////////////玩家外观////////////////


                    //Logger.ShowInfo(appearance.Name+" "+appearance.CharID.ToString() + " " + value.TInt["幻化"].ToString());
                    //if (value.TInt["幻化"] != 0)   //玩家处于幻化状态
                    //{
                    //    var chr =
                    //        from c in Manager.MapClientManager.Instance.OnlinePlayer
                    //        where c.Character.CharID == (uint)value.TInt["幻化"]
                    //        select c;
                    //    if (chr.Count()!=0)
                    //    {

                    //        appearance = chr.First().Character;  //tranceTarger为幻化目标。
                    //        //Logger.ShowInfo(appearance.CharID.ToString());
                    //    }
                    //}

                    //value.appearance.

                    if (value.appearance.MarionettePictID == 0 && value.Marionette == null && value.IllusionPictID == 0 && value.TranceID == 0)
                    {
                        PutByte((byte)(value.appearance.Race == PC_RACE.NONE ? value.Race : value.appearance.Race), offset);
                        PutByte((byte)(value.appearance.Form == DEM_FORM.NONE ? value.Form : value.appearance.Form), offset + 1);
                        PutByte((byte)(value.appearance.Gender == PC_GENDER.NONE ? value.Gender : value.appearance.Gender), offset + 2);
                        PutUShort((value.appearance.HairStyle == 0 ? value.HairStyle : value.appearance.HairStyle), offset + 3);
                        PutByte((value.appearance.HairColor == 0 ? value.HairColor : value.appearance.HairColor), offset + 5);
                        PutUShort((value.appearance.Wig == 0 ? value.Wig : value.appearance.Wig), offset + 6);
                        PutByte(0xff, offset + 8);
                        PutUShort((value.appearance.Face == 0 ? value.Face : value.appearance.Face), offset + 9);

                        PutByte(0x00, offset + 11);//未知
                                                   //3转外观

                        PutByte((value.appearance.TailStyle == byte.MaxValue ? value.TailStyle : value.appearance.TailStyle), offset + 12);
                        PutByte((value.appearance.WingStyle == byte.MaxValue ? value.WingStyle : value.appearance.WingStyle), offset + 13);
                        PutByte((value.appearance.WingColor == byte.MaxValue ? value.WingColor : value.appearance.WingColor), offset + 14);
                    }
                    else //如果外观目标处于木偶或是变身状态
                    {
                        this.PutByte(0xff, offset);
                        this.PutByte(0xff, offset + 1);
                        this.PutByte(0xff, offset + 2);
                        this.PutByte(0xff, offset + 3);
                        this.PutByte(0xff, offset + 4);
                        this.PutByte(0xff, offset + 5);
                        this.PutByte(0xff, offset + 6);
                        this.PutByte(0xff, offset + 7);
                        this.PutByte(0xff, offset + 8);
                        this.PutByte(0xff, offset + 9);
                        this.PutByte(0xff, offset + 10);
                        this.PutByte(0xff, offset + 11);
                        this.PutByte(0xff, offset + 12);
                        this.PutByte(0xff, offset + 13);
                        this.PutByte(0xff, offset + 14);
                    }
                    this.PutByte(14, offset + 15);

                    ////////////////玩家装备////////////////
                    Dictionary<EnumEquipSlot, Item> equips, appequips;  //幻化外观
                    if (value.Form != DEM_FORM.MACHINA_FORM)
                        equips = value.Inventory.Equipments;
                    else
                        equips = value.Inventory.Parts;

                    appequips = value.appearance.Equips;

                    if (value.Marionette == null && value.appearance.MarionettePictID == 0)
                    {
                        if (value.TranceID == 0 && value.IllusionPictID == 0)
                            for (int j = 0; j < 14; j++)
                            {
                                if (appequips.ContainsKey((EnumEquipSlot)j) || equips.ContainsKey((EnumEquipSlot)j))
                                {
                                    //取得外观装备的内容
                                    Item item = appequips.ContainsKey((EnumEquipSlot)j) ? appequips[(EnumEquipSlot)j] : equips[(EnumEquipSlot)j];
                                    if (item == null || item.Stack == 0) continue;
                                    if (item.PictID == 0)
                                        PutUInt(item.BaseData.imageID, offset + 16 + j * 4);
                                    else if (item.BaseData.itemType != ItemType.PET_NEKOMATA && item.BaseData.itemType != ItemType.PARTNER && item.BaseData.itemType != ItemType.RIDE_PARTNER)
                                        PutUInt(item.PictID, offset + 16 + j * 4);

                                    //斥候隐藏不用的武器外观

                                    if (value.Job == PC_JOB.HAWKEYE && j == 9 && value.TInt["斥候远程模式"] != 1 && !item.BaseData.doubleHand)
                                        PutUInt(0, offset + 16 + j * 4);
                                    if (value.Job == PC_JOB.HAWKEYE && j == 8 && value.TInt["斥候远程模式"] == 1 && !item.BaseData.doubleHand)
                                        PutUInt(0, offset + 16 + j * 4);
                                }
                            }
                        else
                            PutUInt((value.IllusionPictID == 0 ? value.TranceID : value.IllusionPictID), offset + 16);
                    }
                    else
                        PutUInt((value.appearance.MarionettePictID == 0 ? value.Marionette.PictID : value.appearance.MarionettePictID), offset + 16);



                    //offset += 4;
                    ////////////////左手动作////////////////
                    this.PutByte(3, offset + 72);
                    if ((appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) || equips.ContainsKey(EnumEquipSlot.LEFT_HAND)) &&
                        value.Marionette == null && value.TranceID == 0)
                    {

                        Item leftHand = appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) ? appequips[EnumEquipSlot.LEFT_HAND] : equips[EnumEquipSlot.LEFT_HAND];
                        if (leftHand.BaseData.handMotion > 255)
                            PutUShort(leftHand.BaseData.handMotion, offset + 73);
                        else
                            PutByte((byte)leftHand.BaseData.handMotion, offset + 74);
                        offset += 2;
                        try
                        {
                            PutUShort((byte)(EquipSound)Enum.Parse(typeof(EquipSound), leftHand.BaseData.itemType.ToString()), offset + 75);
                            //this.PutUShort((ushort)leftHand.BaseData.itemType, offset + 75);
                        }
                        catch
                        {
                            PutUShort(leftHand.BaseData.handMotion2, offset + 75);
                        }
                        offset += 1;
                        ItemType it = leftHand.BaseData.itemType;
                        if (value.TInt["斥候远程模式"] == 1 && value.Job == PC_JOB.HAWKEYE)
                        {
                            byte s = 0;
                            if (it == ItemType.BOW)
                                s = 0x0A;
                            if (it == ItemType.RIFLE)
                                s = 0x0B;
                            if (it == ItemType.GUN)
                                s = 0x0C;
                            if (it == ItemType.DUALGUN)
                                s = 0x0D;
                            PutByte(s, offset + 75);
                        }
                    }
                    else
                    {
                        offset += 3;
                    }

                    ////////////////右手动作////////////////
                    this.PutByte(3, offset + 76);
                    if ((appequips.ContainsKey(EnumEquipSlot.RIGHT_HAND) || equips.ContainsKey(EnumEquipSlot.RIGHT_HAND)) &&
                        value.Marionette == null && value.TranceID == 0)
                    {

                        Item rightHand = appequips.ContainsKey(EnumEquipSlot.RIGHT_HAND) ? appequips[EnumEquipSlot.RIGHT_HAND] : equips[EnumEquipSlot.RIGHT_HAND];
                        if (rightHand.BaseData.handMotion > 255)
                            this.PutUShort(rightHand.BaseData.handMotion, offset + 77);
                        else
                            this.PutByte((byte)rightHand.BaseData.handMotion, offset + 78);
                        offset += 2;
                        try
                        {
                            this.PutByte((byte)(EquipSound)Enum.Parse(typeof(EquipSound), rightHand.BaseData.itemType.ToString()), offset + 79);
                            //this.PutUShort((ushort)rightHand.BaseData.itemType, offset + 79);
                        }
                        catch
                        {

                            this.PutByte(rightHand.BaseData.handMotion2, offset + 79);
                        }
                        offset += 1;
                    }
                    else
                    {
                        offset += 3;
                    }

                    //////////////////骑乘//////////////////
                    this.PutByte(3, offset + 80);
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null &&
                        value.Pet.Ride)
                    {
                        Item pet = equips[EnumEquipSlot.PET];
                        if (pet.BaseData.handMotion > 255)
                            this.PutUShort(pet.BaseData.handMotion, offset + 81);
                        else
                            this.PutByte((byte)pet.BaseData.handMotion, offset + 82);
                        offset += 2;
                        //this.PutByte(pet.BaseData.handMotion2, offset + 83);
                        this.PutByte(0xff, offset + 83);
                        this.PutByte(0xff, offset + 84);
                        offset += 1;

                    }
                    else
                    {
                        offset += 3;
                    }

                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            this.PutUInt(equips[EnumEquipSlot.PET].ItemID, offset + 84);
                        }
                    }
                    //BYTE ride_color; //乗り物の染色値
                    this.PutByte(0, offset + 89);

                    offset += 4;//2015年12月10日，对应449版本
                    //offset += 5;//2016年11月1日

                    ////////////////队伍信息////////////////
                    if (value.Party != null)
                    {
                        buf = Global.Unicode.GetBytes(value.Party.Name + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, offset + 90);
                        this.PutBytes(buf, offset + 91);
                        offset += (ushort)(buf.Length - 1);
                        if (value == value.Party.Leader)
                            this.PutByte(1, offset + 92);
                        else
                            this.PutByte(0, offset + 92);
                    }
                    else
                    {
                        this.PutByte(1, offset + 90);
                        this.PutByte(1, offset + 92);
                    }

                    this.offset -= 2;
                    //UINT UNKNOMW
                    ////////////////军团信息////////////////
                    if (value.PlayerTitle != "")
                    {
                        buf = Global.Unicode.GetBytes(value.PlayerTitle + "\0");
                        buff = new byte[this.data.Length + buf.Length];
                        this.data.CopyTo(buff, 0);
                        this.data = buff;
                        this.PutByte((byte)buf.Length, offset + 97);
                        this.PutBytes(buf, offset + 98);
                        offset += (ushort)(buf.Length - 1);
                        if (value.Ring != null && value == value.Ring.Leader)
                            this.PutByte(1, offset + 99);
                        else
                            this.PutByte(0, offset + 99);
                    }
                    else
                    {
                        this.PutByte(1, offset + 97);
                        this.PutByte(1, offset + 99);
                    }

                    ///////////////聊天室信息///////////////
                    buf = Global.Unicode.GetBytes(value.Sign + "\0");
                    buff = new byte[this.data.Length + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, offset + 100);
                    this.PutBytes(buf, offset + 101);
                    offset += (ushort)(buf.Length - 1);

                    /////////////////露天商店////////////////
                    if (!value.Fictitious)
                    {
                        if (SagaMap.Network.Client.MapClient.FromActorPC(value).Shopswitch == 0)
                            this.PutByte(1, offset + 102);
                        else
                        {
                            buf = Global.Unicode.GetBytes(SagaMap.Network.Client.MapClient.FromActorPC(value).Shoptitle + "\0");
                            buff = new byte[this.data.Length + buf.Length];
                            this.data.CopyTo(buff, 0);
                            this.data = buff;
                            this.PutByte((byte)buf.Length, offset + 102);
                            this.PutBytes(buf, offset + 103);
                            offset += (ushort)(buf.Length - 1);
                            this.PutByte(1, offset + 104);
                        }
                    }
                    else
                        this.PutByte(1, offset + 102);
                    if (value.TInt["playersize"] != 0)
                        this.PutUInt((uint)value.TInt["playersize"], offset + 105);
                    else
                        this.PutUInt(1000, offset + 105);

                    this.PutUShort((ushort)value.Motion, offset + 109);
                    if (value.EMotionLoop)
                        this.PutByte((byte)value.EMotion, offset + 111);

                    //this.PutUInt(0, offset + 111);//unknown

                    /////////////////阵容信息////////////////
                    switch (value.Mode)
                    {
                        case PlayerMode.NORMAL:
                            this.PutUInt(2, offset + 116);
                            this.PutUInt(0, offset + 120);
                            break;
                        case PlayerMode.COLISEUM_MODE:
                            this.PutInt(0x42, (ushort)(offset + 116));//mode1
                            this.PutInt(1, (ushort)(offset + 120));//mode2
                            break;
                        case PlayerMode.KNIGHT_WAR:
                            this.PutInt(0x22, (ushort)(offset + 116));//mode1
                            this.PutInt(2, (ushort)(offset + 120));//mode2
                            break;
                        case PlayerMode.WRP:
                            this.PutInt(0x102, (ushort)(offset + 116));//mode1
                            this.PutInt(4, (ushort)(offset + 120));//mode2
                            break;
                        case PlayerMode.KNIGHT_EAST:
                            this.PutInt(0x22, (ushort)(offset + 116));//mode1
                            this.PutInt(2, (ushort)(offset + 120));//mode2
                            this.PutInt(1, (ushort)(offset + 124));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            //this.PutInt(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_WEST:
                            this.PutInt(0x22, (ushort)(offset + 116));//mode1
                            this.PutInt(2, (ushort)(offset + 120));//mode2
                            this.PutInt(2, (ushort)(offset + 124));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            //this.PutInt(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_SOUTH:
                            this.PutInt(0x22, (ushort)(offset + 116));//mode1
                            this.PutInt(2, (ushort)(offset + 120));//mode2
                            this.PutInt(4, (ushort)(offset + 124));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            //this.PutInt(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_NORTH:
                            this.PutInt(0x22, (ushort)(offset + 116));//mode1
                            this.PutInt(2, (ushort)(offset + 120));//mode2
                            this.PutInt(8, (ushort)(offset + 124));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            //this.PutInt(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                    }

                    this.PutByte(0x00, offset + 128);//不明
                    this.offset += 1;
                    /////////////////等级信息////////////////
                    this.PutUInt(value.Level, offset + 128);
                    this.PutUInt(value.WRPRanking, offset + 132);


                    this.offset -= 1;

                    this.PutByte(0xFF, offset + 141);//师徒图标
                    this.PutByte(value.WaitType, offset + 143);//2015年12月28日加入
                    this.PutUShort(value.UsingPaperID, offset + 144);//2015年12月28日加入
                    PutByte(4, offset + 146);
                    PutUInt(value.PossessionPartnerSlotIDinRightHand, offset + 147);
                    PutUInt(value.PossessionPartnerSlotIDinLeftHand, offset + 151);
                    PutUInt(value.PossessionPartnerSlotIDinAccesory, offset + 155);
                    PutUInt(value.PossessionPartnerSlotIDinClothes, offset + 159);
                    PutByte(4, offset + 163);
                    PutUInt(value.PossessionPartnerSlotIDinRightHand, offset + 164);
                    PutUInt(value.PossessionPartnerSlotIDinLeftHand, offset + 168);
                    PutUInt(value.PossessionPartnerSlotIDinAccesory, offset + 172);
                    PutUInt(value.PossessionPartnerSlotIDinClothes, offset + 176);
                    //unknown
                    PutByte(4, offset + 180);
                    PutUInt(0, offset + 181);
                    PutUInt(0, offset + 185);
                    PutUInt(0, offset + 189);
                    PutUInt(0, offset + 193);
                    PutUInt(0, offset + 197);//unknown
                    //称号部分
                    PutByte(3, offset + 202);
                    PutUInt((uint)value.AInt["称号_主语"], offset + 203);
                    PutUInt((uint)value.AInt["称号_连词"], offset + 207);
                    PutUInt((uint)value.AInt["称号_谓语"], offset + 211);
                    //PutUInt((uint)value.AInt["称号_战斗"], offset + 176);
                }
                #endregion
            }
        }


        ActorPet SetPet
        {
            set
            {
                #region Saga14
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9 && Configuration.Instance.Version < SagaLib.Version.Saga14_2)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;
                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(0xFFFFFFFF, 6);

                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    this.PutByte(0xff, offset);
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        offset++;
                        this.PutByte(0xff, offset);
                    }
                    this.PutByte(0xff, (ushort)(offset + 1));
                    this.PutByte(0xff, (ushort)(offset + 2));
                    this.PutByte(0xff, (ushort)(offset + 3));
                    this.PutByte(0xff, (ushort)(offset + 4));
                    this.PutByte(0xff, (ushort)(offset + 5));
                    this.PutByte(0xff, (ushort)(offset + 6));
                    this.PutByte(0xff, (ushort)(offset + 7));
                    this.PutByte(0xff, (ushort)(offset + 8));
                    this.PutByte(0xff, (ushort)(offset + 9));
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    {
                        this.PutByte(0xff, (ushort)(++offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                    }

                    this.PutByte(0x0D, (ushort)(offset + 10));

                    if (value.PictID != 0)
                        this.PutUInt(value.PictID, (ushort)(offset + 11));
                    else if (value.BaseData.pictid != 0)
                        this.PutUInt(value.BaseData.pictid, (ushort)(offset + 11));
                    else
                        this.PutUInt(value.PetID, (ushort)(offset + 11));



                    //left hand weapon motion
                    this.PutByte(3, (ushort)(offset + 63));
                    //right hand weapon motion
                    this.PutByte(3, (ushort)(offset + 67));
                    //riding motion
                    this.PutByte(3, (ushort)(offset + 71));

                    this.PutByte(0, (ushort)(offset + 80));
                    this.PutByte(1, (ushort)(offset + 81));//party name
                    this.PutByte(1, (ushort)(offset + 83));//party name

                    this.PutByte(1, (ushort)(offset + 88));//Ring name

                    this.PutByte(1, (ushort)(offset + 90));//Ring master

                    this.PutByte(1, (ushort)(offset + 91));//Sign name

                    this.PutByte(1, (ushort)(offset + 93));//shop name

                    this.PutUInt(1000, (ushort)(offset + 96));//size
                    this.PutUInt(2, (ushort)(offset + 106));//size

                    this.PutUShort(0, (ushort)(offset + 110));

                    this.PutByte(0, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                    this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                    this.PutByte(0, (ushort)(offset + 116));
                    this.PutByte(0, (ushort)(offset + 117));
                    this.PutByte(0, (ushort)(offset + 118));
                    this.PutByte(1, (ushort)(offset + 119));
                    this.PutUInt(0xffffffff, (ushort)(offset + 120));//// WRP順位（ペットは -1固定。別のパケで主人の値が送られてくる
                    /*
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
                #endregion
                #region Saga14_2
                if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2 && Configuration.Instance.Version < SagaLib.Version.Saga17)
                {
                    this.data = new byte[154];
                    this.offset = 2;
                    this.ID = 0x020E;

                    byte[] buf, buff;
                    byte size;
                    ushort offset;
                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(0xFFFFFFFF, 6);

                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)22;
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    this.PutByte(0xff, offset);
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        offset++;
                        this.PutByte(0xff, offset);
                    }
                    this.PutByte(0xff, (ushort)(offset + 1));
                    this.PutByte(0xff, (ushort)(offset + 2));
                    this.PutByte(0xff, (ushort)(offset + 3));
                    this.PutByte(0xff, (ushort)(offset + 4));
                    this.PutByte(0xff, (ushort)(offset + 5));
                    this.PutByte(0xff, (ushort)(offset + 6));
                    this.PutByte(0xff, (ushort)(offset + 7));
                    this.PutByte(0xff, (ushort)(offset + 8));
                    this.PutByte(0xff, (ushort)(offset + 9));
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    {
                        this.PutByte(0xff, (ushort)(++offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                        this.PutByte(0xff, (ushort)(++offset + 9));
                    }
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                    {
                        this.PutByte(0xff, (ushort)(++offset + 9));
                    }
                    this.PutByte(0x0D, (ushort)(offset + 10));

                    if (value.PictID != 0)
                        this.PutUInt(value.PictID, (ushort)(offset + 11));
                    else if (value.BaseData.pictid != 0)
                        this.PutUInt(value.BaseData.pictid, (ushort)(offset + 11));
                    else
                        this.PutUInt(value.PetID, (ushort)(offset + 11));



                    //left hand weapon motion
                    this.PutByte(3, (ushort)(offset + 63));
                    //right hand weapon motion
                    this.PutByte(3, (ushort)(offset + 67));
                    //riding motion
                    this.PutByte(3, (ushort)(offset + 71));

                    this.PutByte(0, (ushort)(offset + 80));
                    this.PutByte(1, (ushort)(offset + 81));//party name
                    this.PutByte(0, (ushort)(offset + 83));//party name

                    this.PutByte(1, (ushort)(offset + 88));//Ring name

                    this.PutByte(0, (ushort)(offset + 90));//Ring master

                    this.PutByte(1, (ushort)(offset + 91));//Sign name

                    this.PutByte(1, (ushort)(offset + 93));//shop name

                    this.PutUInt(1100, (ushort)(offset + 96));//size
                    this.PutUInt(2, (ushort)(offset + 106));//size

                    this.PutUShort(0, (ushort)(offset + 110));

                    this.PutByte(0, (ushort)(offset + 114));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                    this.PutByte(0, (ushort)(offset + 115));//metamo; //メタモーバトルのチーム　1花2岩
                    this.PutByte(0, (ushort)(offset + 116));
                    this.PutByte(0, (ushort)(offset + 117));
                    this.PutByte(0, (ushort)(offset + 118));
                    this.PutByte(1, (ushort)(offset + 119));
                    this.PutUInt(0xffffffff, (ushort)(offset + 120));//// WRP順位（ペットは -1固定。別のパケで主人の値が送られてくる
                    this.PutUInt(0xffffffff, (ushort)(offset + 124));
                    this.PutByte(0xff, (ushort)(offset + 128));
                    /*
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
                #endregion
                #region Saga17
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;

                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(0xFFFFFFFF, 6);

                    ///////////////玩家角色名///////////////
                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;//角色名长度
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    ////////////////玩家外观////////////////
                    this.PutByte(0xff, offset);
                    this.PutByte(0xff, offset + 1);
                    this.PutByte(0xff, offset + 2);
                    this.PutByte(0xff, offset + 3);
                    this.PutByte(0xff, offset + 4);
                    this.PutByte(0xff, offset + 5);
                    this.PutByte(0xff, offset + 6);
                    this.PutByte(0xff, offset + 7);
                    this.PutByte(0xff, offset + 8);
                    this.PutByte(0xff, offset + 9);
                    this.PutByte(0xff, offset + 10);
                    this.PutByte(0xff, offset + 11);
                    this.PutByte(0xff, offset + 12);
                    this.PutByte(0xff, offset + 13);
                    this.PutByte(0xff, offset + 14);


                    this.PutByte(0x0E, offset + 15);

                    ////////////////玩家装备////////////////
                    if (value.PictID != 0)
                        this.PutUInt(value.PictID, (ushort)(offset + 16));
                    else if (value.BaseData.pictid != 0)
                        this.PutUInt(value.BaseData.pictid, (ushort)(offset + 16));
                    else
                        this.PutUInt(value.PetID, (ushort)(offset + 16));

                    ////////////////左手动作////////////////
                    this.PutByte(3, offset + 72);

                    ////////////////右手动作////////////////
                    this.PutByte(3, offset + 76);

                    //////////////////骑乘//////////////////
                    this.PutByte(3, offset + 80);

                    //BYTE ride_color; //乗り物の染色値
                    this.PutByte(0, offset + 89);

                    offset += 4;//2015年12月10日，对应449版本

                    ////////////////队伍信息////////////////
                    this.PutByte(1, offset + 90);
                    this.PutByte(0, offset + 92);
                    //UINT UNKNOMW
                    ////////////////军团信息////////////////
                    this.PutByte(1, offset + 97);
                    this.PutByte(0, offset + 99);

                    ///////////////聊天室信息///////////////
                    this.PutByte(1, offset + 100);

                    /////////////////露天商店////////////////
                    this.PutByte(1, offset + 102);

                    this.PutUInt(1000, offset + 105);

                    offset++;//2015年12月11日，对应449版本

                    /////////////////阵容信息////////////////
                    this.PutUInt(2, offset + 115);
                    this.PutUInt(0, offset + 119);

                    /////////////////等级信息////////////////
                    this.PutByte(0x1, offset + 131);
                    /*if (Manager.MapManager.Instance.GetMap(value.MapID).Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.PutUInt(0xffffffff, offset + 128);
                    else
                        this.PutUInt(0xffffffff, offset + 128);*/
                    this.PutUInt(0xffffffff, offset + 132);
                    this.PutUInt(0xffffffff, offset + 136);
                    this.PutByte(0xff, offset + 140); //2015年12月11日，对应449版本
                    this.PutUInt(0xffffffff, offset + 145); //2015年12月11日，对应449版本
                    //this.PutByte(0xFF, offset + 144);//师徒图标


                    //2015年12月11日，对应449版本
                    buff = new byte[this.data.Length + 1];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                }
                #endregion
            }
        }

        ActorPartner SetPartner
        {
            set
            {
                #region Saga17
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    byte[] buf, buff;
                    byte size;
                    ushort offset;

                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(0xFFFFFFFF, 6);

                    buf = Global.Unicode.GetBytes(value.Name + "\0");
                    size = (byte)buf.Length;//角色名长度
                    buff = new byte[this.data.Length - 1 + size];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte(size, 10);
                    this.PutBytes(buf, 11);
                    offset = (ushort)(11 + size);

                    this.PutByte(0xff, offset);
                    this.PutByte(0xff, offset + 1);
                    this.PutByte(0xff, offset + 2);
                    this.PutByte(0xff, offset + 3);
                    this.PutByte(0xff, offset + 4);
                    this.PutByte(0xff, offset + 5);
                    this.PutByte(0xff, offset + 6);
                    this.PutByte(0xff, offset + 7);
                    this.PutByte(0xff, offset + 8);
                    this.PutByte(0xff, offset + 9);
                    this.PutByte(0xff, offset + 10);
                    this.PutByte(0xff, offset + 11);
                    this.PutByte(0xff, offset + 12);
                    this.PutByte(0xff, offset + 13);
                    this.PutByte(0xff, offset + 14);
                    this.PutByte(14, offset + 15);
                    if (value.PictID != 0)
                        this.PutUInt(value.PictID, (ushort)(offset + 16));

                    else if (value.BaseData.pictid != 0)
                        this.PutUInt(value.BaseData.pictid, (ushort)(offset + 16));
                    this.PutByte(3, offset + 72);
                    this.PutByte(3, offset + 79);
                    this.PutByte(3, offset + 86);

                    this.PutByte(0, offset + 89);

                    //offset += 4;//2015年12月10日，对应449版本
                    this.PutByte(1, offset + 103);
                    //this.PutByte(0, offset + );
                    ////////////////军团信息////////////////
                    this.PutByte(1, offset + 110);
                    //this.PutByte(0, offset + 99);
                    this.PutByte(1, offset + 113);

                    this.PutByte(1, offset + 115);

                    this.PutUInt(1000, offset + 118);

                    this.PutUShort((ushort)value.Motion, offset + 122);
                    //offset++;//2015年12月11日，对应449版本

                    this.PutUInt(2, offset + 129);
                    //this.PutUInt(0, offset + 119);

                    this.PutByte(0x1, offset + 145);
                    this.PutUInt(0xffffffff, offset + 146);
                    this.PutUInt(0xffffffff, offset + 150);
                    this.PutByte(0xff, offset + 154); //2015年12月11日，对应449版本
                    this.PutUInt(0xffffffff, offset + 162);
                    buff = new byte[this.data.Length + 1];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;

                }
                #endregion
            }
        }
        public Actor Actor
        {
            set
            {
                if (value.type == ActorType.PC)
                {
                    this.SetPC = (ActorPC)value;
                }
                else if (value.type == ActorType.PET)
                {
                    this.SetPet = (ActorPet)value;
                }
                else if (value.type == ActorType.SHADOW)
                {
                    this.SetShadow = (ActorShadow)value;
                }
                else if (value.type == ActorType.MOB)
                {
                    this.SetMob = (ActorMob)value;
                }
                else if (value.type == ActorType.PARTNER)
                {
                    this.SetPartner = (ActorPartner)value;
                }
            }
        }


    }
}

