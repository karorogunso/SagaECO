using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_INFO : Packet
    {
        public SSMG_PLAYER_INFO()
        {
            if (Configuration.Instance.Version == SagaLib.Version.Saga6)
            {
                this.data = new byte[210];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
            {
                this.data = new byte[219];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                this.data = new byte[222];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
            {
                this.data = new byte[225];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga13)
            {
                this.data = new byte[243];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga14)
            {
                this.data = new byte[252];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
            {
                this.data = new byte[235];
                this.offset = 2;
                this.ID = 0x01FF;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
            {
                uint length = 250;
                length += 8;//15年12月8日加入，对应版本449
                length += 4;//15年12月25日加入，对应版本452
                length += 8;//16年6月28日加入，对应版本469
                length += 9;//16年6月28日加入，对应版本482
                length += 17;//17年4月1日加入，对应版本497，增加称号系统
                this.data = new byte[length];
                this.offset = 2;
                this.ID = 0x01FF;
            }
        }

        public ActorPC Player
        {
            set
            {
                #region Saga17
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                {
                    Map info = Manager.MapManager.Instance.GetMap(value.MapID);
                    this.PutUInt(value.ActorID, 2);
                    this.PutUInt(value.CharID, 6);

                    this.PutUInt(1, 10);//15年12月8日加入，对应版本449

                    string name = value.Name;
                    //name = "糖果_" + value.Account.AccountID.ToString() + Global.Random.Next(10000, 99999);//万圣节活动

                    /*if (value.FirstName != "")
                        name = value.FirstName + "·" + name;*/
                    name = name.Replace("\0", "");
                    byte[] buf = Global.Unicode.GetBytes(name);
                    byte[] buff = new byte[this.data.Length + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutUShort((ushort)(buf.Length + 1), 14);
                    this.PutBytes(buf, 16);
                    ushort offset = (ushort)(17 + buf.Length);

                    this.PutByte((byte)value.Race, offset);
                    this.PutByte((byte)value.Form, offset+1);
                    this.PutByte((byte)value.Gender, offset+2);

                    this.PutUShort(value.HairStyle, offset + 3);
                    this.PutByte(value.HairColor, offset + 5);
                    this.PutUShort(value.Wig, offset + 6);

                    this.PutByte(0xFF, offset + 8);
                    this.PutUShort(value.Face, offset + 9);

                    this.PutByte(value.TailStyle, offset + 12);
                    this.PutByte(value.WingStyle, offset + 13);
                    this.PutByte(value.WingColor, offset + 14);

                    this.PutUInt(value.MapID, offset + 15);
                    offset += 3;
                    this.PutByte(Global.PosX16to8(value.X, info.Width), offset + 16);
                    this.PutByte(Global.PosY16to8(value.Y, info.Height), offset + 17);
                    this.PutByte((byte)(value.Dir / 45), offset + 18);

                    this.PutUInt(value.HP, offset + 19);
                    this.PutUInt(value.MaxHP, offset + 23);
                    this.PutUInt(value.MP, offset + 27);
                    this.PutUInt(value.MaxMP, offset + 31);
                    this.PutUInt(value.SP, offset + 35);
                    this.PutUInt(value.MaxSP, offset + 39);
                    this.PutUInt(value.EP, offset + 43);
                    this.PutUInt(value.MaxEP, offset + 47);
                    if (Manager.MapManager.Instance.GetMap(value.MapID).Info.Flag.Test(MapFlags.Dominion))
                        this.PutShort(value.DominionCL, offset + 51);
                    else
                        this.PutShort(value.CL, offset + 51);

                    this.PutByte(8, offset + 53);
                    this.PutUShort(value.Str, offset + 54);
                    this.PutUShort(value.Dex, offset + 56);
                    this.PutUShort(value.Int, offset + 58);
                    this.PutUShort(value.Vit, offset + 60);
                    this.PutUShort(value.Agi, offset + 62);
                    this.PutUShort(value.Mag, offset + 64);
                    this.PutUShort(100, offset + 66);
                    this.PutUShort(100 ,offset + 68);

                    

                    this.PutByte(0x14, offset + 70);

                    if (value.PossessionTarget == 0)
                        this.PutUInt(0xFFFFFFFF, offset + 113);
                    else
                    {
                        Actor possession = info.GetActor(value.PossessionTarget);
                        if (possession.type != ActorType.ITEM)
                            this.PutUInt(value.PossessionTarget, offset + 113);
                        else
                            this.PutUInt(value.ActorID, offset + 113);           
                    }
                    if (value.PossessionTarget == 0)
                        this.PutByte(0xFF, offset + 117);
                    else
                        this.PutByte((byte)value.PossessionPosition, offset + 117);

                    this.PutUInt(0, offset + 118);//15年12月8日加入，对应版本449
                    offset += 4;//15年12月8日加入，对应版本449
                    this.PutUInt((uint)value.Gold, offset + 118);
                    this.PutByte((byte)value.Status.attackType, offset + 122);
                    this.PutUInt(0, offset + 123);
                    offset += 4;
                    offset += 4;//15年12月8日加入，对应版本452
                    this.PutByte(14, offset + 123);
                    for (int j = 0; j < 15; j++)
                    {
                        if (value.Inventory.Equipments.ContainsKey((EnumEquipSlot)j))
                        {
                            Item item = value.Inventory.Equipments[(EnumEquipSlot)j];
                            if (item.Stack == 0) continue;
                            if (item.PictID == 0)
                                this.PutUInt(item.BaseData.imageID, offset + 124+ j * 4);
                            else
                                this.PutUInt(item.PictID, offset + 124 + j * 4);
                            if (value.Job == PC_JOB.HAWKEYE && j == 9 && value.TInt["斥候远程模式"] != 1 && !item.BaseData.doubleHand)
                                PutUInt(0, offset + 124 + j * 4);
                            if (value.Job == PC_JOB.HAWKEYE && j == 8 && value.TInt["斥候远程模式"] == 1 && !item.BaseData.doubleHand)
                                PutUInt(0, offset + 124 + j * 4);
                        }
                    }
                    offset += 4;
                    ////////////////左手动作////////////////
                    this.PutByte(3, offset + 176);
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) &&
                        value.Marionette == null && value.TranceID == 0)
                    {
                        Item leftHand = value.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                        /*if (leftHand.BaseData.handMotion < 255)
                            this.PutByte((byte)leftHand.BaseData.handMotion, offset + 177);
                        else//*/
                        PutUShort(leftHand.HandMotion != 0 ? leftHand.HandMotion : leftHand.BaseData.handMotion, offset + 177);
                        offset += 2;//v282
                        try
                        {
                            if (leftHand.HandMotion2 != 0)
                                PutUShort(leftHand.HandMotion2, offset + 179);
                            else
                                PutUShort((ushort)(EquipSound)Enum.Parse(typeof(EquipSound), leftHand.BaseData.itemType.ToString()), offset + 179);
                            //this.PutUShort((ushort)leftHand.BaseData.itemType, offset + 179);
                        }
                        catch
                        {
                            PutUShort(leftHand.BaseData.handMotion2, offset + 179);
                        }
                        offset += 1;
                    }
                    else
                    {
                        offset += 3;
                    }

                    ////////////////右手动作////////////////
                    this.PutByte(3, offset + 180);
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) &&
                        value.Marionette == null && value.TranceID == 0)
                    {
                        Item rightHand = value.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        /*/if(rightHand.BaseData.handMotion < 255)
                            this.PutByte((byte)rightHand.BaseData.handMotion, offset + 181);
                        else//*/
                        PutUShort(rightHand.HandMotion != 0 ? rightHand.HandMotion : rightHand.BaseData.handMotion, offset + 181);
                        //PutUShort(rightHand.BaseData.handMotion, offset + 181);
                        offset += 2;//v282
                        try
                        {
                            if (rightHand.HandMotion2 != 0)
                                PutUShort(rightHand.HandMotion2, offset + 183);
                            else
                                this.PutUShort((byte)(EquipSound)Enum.Parse(typeof(EquipSound), rightHand.BaseData.itemType.ToString()), offset + 183);
                            //this.PutUShort((ushort)rightHand.BaseData.itemType, offset + 183);
                        }
                        catch
                        {

                            this.PutUShort(rightHand.BaseData.handMotion2, offset + 183);
                        }
                        offset += 1;
                    }
                    else
                    {
                        offset += 3;
                    }

                    //////////////////骑乘//////////////////
                    this.PutByte(3, offset + 184);
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            Item pet = value.Inventory.Equipments[EnumEquipSlot.PET];
                            /*/
                            if(pet.BaseData.handMotion > 255)
                            this.PutUShort(pet.BaseData.handMotion, offset + 185);
                            else
                                this.PutByte((byte)pet.BaseData.handMotion, offset + 185);//*/
                            this.PutUShort(pet.BaseData.handMotion, offset + 185);

                            //this.PutUShort(pet.BaseData.handMotion2, offset + 187);
                            this.PutByte(0xff, offset + 189);
                            this.PutByte(0xff, offset + 190);
                        }
                    }
                    //*/
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                    {
                        if (value.Pet.Ride)
                        {
                            this.PutUInt(value.Inventory.Equipments[EnumEquipSlot.PET].ItemID, offset + 191);
                        }
                    }
                    //*/
                    offset += 3;//v282


                    this.PutUInt(value.Range, (ushort)(offset + 193));
                    switch (value.Mode)
                    {
                        case PlayerMode.NORMAL:
                            this.PutInt(2, (ushort)(offset + 201));//mode1
                            this.PutInt(0, (ushort)(offset + 205));//mode2
                            break;
                        case PlayerMode.COLISEUM_MODE:
                            this.PutInt(0x42, (ushort)(offset + 201));//mode1
                            this.PutInt(1, (ushort)(offset + 205));//mode2
                            break;
                        case PlayerMode.KNIGHT_WAR:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            break;
                        case PlayerMode.WRP:
                            this.PutInt(0x102, (ushort)(offset + 201));//mode1
                            this.PutInt(4, (ushort)(offset + 205));//mode2
                            break;
                        case PlayerMode.KNIGHT_EAST:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            this.PutByte(1, (ushort)(offset + 209));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 210));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_WEST:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            this.PutByte(2, (ushort)(offset + 209));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 210));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_SOUTH:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            this.PutByte(4, (ushort)(offset + 209));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 210));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_NORTH:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            this.PutByte(8, (ushort)(offset + 209));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(0, (ushort)(offset + 210));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_FLOWER:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            this.PutByte(0, (ushort)(offset + 209));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(1, (ushort)(offset + 210));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                        case PlayerMode.KNIGHT_ROCK:
                            this.PutInt(0x22, (ushort)(offset + 201));//mode1
                            this.PutInt(2, (ushort)(offset + 205));//mode2
                            this.PutByte(0, (ushort)(offset + 209));//emblem; //演習時のエンブレムとか　1東2西4南8北Aヒーロー状態
                            this.PutByte(2, (ushort)(offset + 210));//metamo; //メタモーバトルのチーム　1花2岩
                            break;
                    }
                    this.PutByte(value.WaitType, offset + 225);
                    /*this.PutShort(0x28a7, offset + 213);
                    this.PutShort(0x2b63, offset + 217);
                    this.PutByte(2, offset + 219);*/
                    this.PutByte(1, offset + 219);
                    this.PutByte(15, offset + 223);
                    this.PutUShort(value.UsingPaperID, offset + 226);
                    this.PutUShort(0, (ushort)(offset + 228));//unknown ver469
                    this.PutUInt(132150, (ushort)(offset + 230));//unknown ver469
                    PutByte(4, offset + 234);
                    if (value.type == ActorType.PC)
                    {
                        PutUInt((uint)(value).AInt["称号_主语"], offset + 235);
                        PutUInt((uint)(value).AInt["称号_连词"], offset + 239);
                        PutUInt((uint)(value).AInt["称号_谓语"], offset + 243);
                        PutUInt((uint)(value).AInt["称号_战斗"], offset + 247);
                    }
                }
                #endregion
            }
        }
    }
}
        
