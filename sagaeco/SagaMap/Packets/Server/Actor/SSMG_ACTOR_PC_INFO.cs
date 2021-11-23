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
            this.data = new byte[239];
            this.offset = 2;
            this.ID = 0x020D;
        }
        ActorShadow SetShadow
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
                    IConcurrentDictionary<EnumEquipSlot, Item> equips;
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

                            PutUShort(leftHand.BaseData.handMotion, offset + 73);
                            PutUShort(leftHand.BaseData.handMotion2, offset + 77);
                        }
                    }
                    offset += 3;
                   ////////////////右手动作////////////////
                   this.PutByte(3, offset + 76);
                    if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (value.Owner.Marionette == null && value.Owner.TranceID == 0)
                        {
                            Item rightHand = equips[EnumEquipSlot.RIGHT_HAND];
                            this.PutUShort(rightHand.BaseData.handMotion, offset + 77);
                            this.PutUShort(rightHand.BaseData.handMotion2, offset + 79);
                        }
                    }
                    offset += 3;
                    //////////////////骑乘//////////////////
                    this.PutByte(3, offset + 80);
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Owner.Pet != null)
                    {
                        if (value.Owner.Pet.Ride)
                        {
                            Item pet = equips[EnumEquipSlot.PET];
                            this.PutUShort(pet.BaseData.handMotion, offset + 81);
                            this.PutUShort(pet.BaseData.handMotion2, offset + 83);
                        }
                    }
                    if (equips.ContainsKey(EnumEquipSlot.PET) && value.Owner.Pet != null)
                    {
                        if (value.Owner.Pet.Ride)
                        {
                            this.PutUInt(equips[EnumEquipSlot.PET].ItemID, offset + 84);
                        }
                    }
                    offset += 3;
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
                    if (Manager.MapManager.Instance.GetMap(value.MapID).Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.PutUInt(value.Owner.DominionLevel, offset + 128);
                    else
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
                    this.PutUShort(pet.BaseData.handMotion, offset + 87);
                    this.PutShort(-1, offset + 91);
                    this.PutUInt(pet.ItemID, offset + 93);
                }
                //this.PutByte(0, offset + 89);

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
                    IConcurrentDictionary<EnumEquipSlot, Item> equips, appequips;  //幻化外观
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
                                        PutUInt(ItemFactory.Instance.GetItem(item.PictID).BaseData.imageID, offset + 16 + j * 4);

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
                        PutUShort(leftHand.HandMotion != 0 ? leftHand.HandMotion : leftHand.BaseData.handMotion, offset + 73);
                        offset += 2;
                        try
                        {
                            if (leftHand.HandMotion2 != 0)
                                PutUShort(leftHand.HandMotion2, offset + 75);
                            else
                                PutUShort((ushort)(EquipSound)Enum.Parse(typeof(EquipSound), leftHand.BaseData.itemType.ToString()), offset + 75);
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
                        if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND) && value.TInt["斥候远程模式"] == 1 && value.Job == PC_JOB.HAWKEYE)
                            rightHand = appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) ? appequips[EnumEquipSlot.LEFT_HAND] : equips[EnumEquipSlot.LEFT_HAND];
                        PutUShort(rightHand.HandMotion != 0 ? rightHand.HandMotion : rightHand.BaseData.handMotion, offset + 77);
                        offset += 2;
                        try
                        {
                            if (rightHand.HandMotion2 != 0)
                                PutUShort(rightHand.HandMotion2, offset + 79);
                            else
                                PutUShort((ushort)(EquipSound)Enum.Parse(typeof(EquipSound), rightHand.BaseData.itemType.ToString()), offset + 79);

                            //this.PutUShort((ushort)rightHand.BaseData.itemType, offset + 79);
                        }
                        catch
                        {
                            PutUShort(rightHand.BaseData.handMotion2, offset + 79);
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
                        PutUShort(pet.BaseData.handMotion, offset + 81);
                        offset += 2;
                        //this.PutByte(pet.BaseData.handMotion2, offset + 83);
                        this.PutShort(-1, offset + 83);
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
                    if (Manager.MapManager.Instance.GetMap(value.MapID).Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.PutUInt(value.DominionLevel, offset + 128);
                    else
                        this.PutUInt(value.Level, offset + 128);
                    this.PutUInt(value.WRPRanking, offset + 132);


                    this.offset -= 1;

                    if (value.AInt["名称后缀图标"] != 0)
                        this.PutByte((byte)(value.AInt["名称后缀图标"] - 1), offset + 141);
                    else
                        PutByte(0xFF, offset + 141);//师徒图标
                    PutByte(value.WaitType, offset + 143);//2015年12月28日加入
                    PutUShort(value.UsingPaperID, offset + 144);//2015年12月28日加入
                    if (value.TInt["大逃杀模式"] != 1)
                    {
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
                    }
                    //unknown
                    PutByte(4, offset + 180);
                    PutUInt(0, offset + 181);
                    PutUInt(0, offset + 185);
                    PutUInt(0, offset + 189);
                    PutUInt(0, offset + 193);
                    PutUInt(0, offset + 197);//unknown
                    //称号部分
                    PutByte(3, offset + 202);
                    if (value.TInt["大逃杀模式"] != 1)
                    {
                        PutUInt((uint)value.AInt["称号_主语"], offset + 203);
                        PutUInt((uint)value.AInt["称号_连词"], offset + 207);
                        PutUInt((uint)value.AInt["称号_谓语"], offset + 211);
                    }
                    //PutUInt((uint)value.AInt["称号_战斗"], offset + 176);
                }
                #endregion
            }
        }


        ActorPet SetPet
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

