using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ACTOR_EQUIP_UPDATE : Packet
    {
        public SSMG_ITEM_ACTOR_EQUIP_UPDATE()
        {
            //this.data = new byte[81];
            this.data = new byte[90];
            this.offset = 2;
            this.ID = 0x09E9;

        }

        public ActorPC Player
        {
            set
            {
                this.PutUInt(value.ActorID, 2);
                this.PutByte(14, 6);

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
                                uint pid = 0;
                                Item item = appequips.ContainsKey((EnumEquipSlot)j) ? appequips[(EnumEquipSlot)j] : equips[(EnumEquipSlot)j];
                                if (j == 9 && item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && item.EquipSlot.Count == 1)
                                {
                                    if (item.PictID == 0)
                                        pid = item.BaseData.imageID + 10000000;
                                    else
                                        pid = item.PictID + 10000000;

                                    if (value.TInt["斥候远程模式"] == 1 && value.Job == PC_JOB.HAWKEYE && item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && !item.BaseData.doubleHand) pid = 0;
                                    else if (value.TInt["斥候远程模式"] == 0 && value.Job == PC_JOB.HAWKEYE && item.EquipSlot.Contains(EnumEquipSlot.LEFT_HAND) && !item.BaseData.doubleHand) pid = 0;
                                    this.PutUInt(pid + 10000000, (ushort)(7 + j * 4));
                                }
                                else
                                {
                                    if (item == null || item.Stack == 0) continue;
                                    if (item.PictID == 0)
                                        pid = item.BaseData.imageID;
                                    else if (item.BaseData.itemType != ItemType.PET_NEKOMATA && item.BaseData.itemType != ItemType.PARTNER && item.BaseData.itemType != ItemType.RIDE_PARTNER)
                                        pid = item.PictID;
                                    if (value.TInt["斥候远程模式"] == 1 && value.Job == PC_JOB.HAWKEYE && item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && !item.BaseData.doubleHand) pid = 0;
                                    else if (value.TInt["斥候远程模式"] == 0 && value.Job == PC_JOB.HAWKEYE && item.EquipSlot.Contains(EnumEquipSlot.LEFT_HAND) && !item.BaseData.doubleHand) pid = 0;
                                    this.PutUInt(pid, (ushort)(7 + j * 4));
                                }
                            }
                        }
                    else
                        PutUInt((value.IllusionPictID == 0 ? value.TranceID : value.IllusionPictID), 7);
                }
                else
                {
                    PutUInt((value.appearance.MarionettePictID == 0 ? value.Marionette.PictID : value.appearance.MarionettePictID), 7);
                }

                //left hand weapon motion
                this.PutByte(3, 63);
                if ((appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) || equips.ContainsKey(EnumEquipSlot.LEFT_HAND)) &&
                        value.Marionette == null && value.TranceID == 0)
                {
                    Item leftHand = appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) ? appequips[EnumEquipSlot.LEFT_HAND] : equips[EnumEquipSlot.LEFT_HAND];
                    /*/
                    if(leftHand.BaseData.handMotion > 255)
                    this.PutUShort(leftHand.BaseData.handMotion, 64);
                    else
                        this.PutByte((byte)leftHand.BaseData.handMotion, 64);//*/
                    this.PutUShort(leftHand.BaseData.handMotion, 64);
                    //this.offset += 2;
                    try
                    {
                        this.PutUShort((byte)(EquipSound)Enum.Parse(typeof(EquipSound), leftHand.BaseData.itemType.ToString()), 68);
                        //this.PutUShort((ushort)leftHand.BaseData.itemType, 68);
                    }
                    catch
                    {
                        this.PutUShort(leftHand.BaseData.handMotion2, 68);
                    }
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
                        PutByte(s, 69);
                    }
                }
                //right hand weapon motion
                this.PutByte(3, 70);
                if ((appequips.ContainsKey(EnumEquipSlot.RIGHT_HAND) || equips.ContainsKey(EnumEquipSlot.RIGHT_HAND)) &&
                        value.Marionette == null && value.TranceID == 0)
                {
                    Item rightHand = appequips.ContainsKey(EnumEquipSlot.RIGHT_HAND) ? appequips[EnumEquipSlot.RIGHT_HAND] : equips[EnumEquipSlot.RIGHT_HAND];
                    /*
                    if (rightHand.BaseData.handMotion > 255)
                        this.PutUShort(rightHand.BaseData.handMotion, 68);
                    else
                        this.PutByte((byte)rightHand.BaseData.handMotion, 68);//*/
                    this.PutUShort(rightHand.BaseData.handMotion, 71);
                    try
                    {
                        this.PutByte((byte)(EquipSound)Enum.Parse(typeof(EquipSound), rightHand.BaseData.itemType.ToString()), 75);
                        //this.PutUShort((ushort)rightHand.BaseData.itemType, 75);
                    }
                    catch
                    {
                        this.PutUShort(rightHand.BaseData.handMotion2, 75);
                    }
                }
                //if ((appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) || equips.ContainsKey(EnumEquipSlot.LEFT_HAND)) && value.Marionette == null && value.TranceID == 0)
                //{
                //    Item leftHand = appequips.ContainsKey(EnumEquipSlot.LEFT_HAND) ? appequips[EnumEquipSlot.LEFT_HAND] : equips[EnumEquipSlot.LEFT_HAND];
                //    /*/
                //    if(leftHand.BaseData.handMotion > 255)
                //    this.PutUShort(leftHand.BaseData.handMotion, 64);
                //    else
                //        this.PutByte((byte)leftHand.BaseData.handMotion, 64);//*/
                //    Item lhillusionitemid = ItemFactory.Instance.GetItem(leftHand.PictID);
                //    this.PutUShort(lhillusionitemid.BaseData.handMotion, 64);
                //    //this.offset += 2;
                //    try
                //    {
                //        this.PutUShort((byte)(EquipSound)Enum.Parse(typeof(EquipSound), lhillusionitemid.BaseData.itemType.ToString()), 68);
                //        //this.PutUShort((ushort)leftHand.BaseData.itemType, 68);
                //    }
                //    catch
                //    {
                //        this.PutUShort(leftHand.BaseData.handMotion2, 68);
                //    }
                //    ItemType it = leftHand.BaseData.itemType;
                //    if (value.TInt["斥候远程模式"] == 1 && value.Job == PC_JOB.HAWKEYE)
                //    {
                //        byte s = 0;
                //        if (it == ItemType.BOW)
                //            s = 0x0A;
                //        if (it == ItemType.RIFLE)
                //            s = 0x0B;
                //        if (it == ItemType.GUN)
                //            s = 0x0C;
                //        if (it == ItemType.DUALGUN)
                //            s = 0x0D;
                //        PutByte(s, 69);
                //    }
                //}
                ////right hand weapon motion
                //this.PutByte(3, 70);
                //if ((appequips.ContainsKey(EnumEquipSlot.RIGHT_HAND) || equips.ContainsKey(EnumEquipSlot.RIGHT_HAND)) && value.Marionette == null && value.TranceID == 0)
                //{
                //    Item rightHand = appequips.ContainsKey(EnumEquipSlot.RIGHT_HAND) ? appequips[EnumEquipSlot.RIGHT_HAND] : equips[EnumEquipSlot.RIGHT_HAND];
                //    /*
                //    if (rightHand.BaseData.handMotion > 255)
                //        this.PutUShort(rightHand.BaseData.handMotion, 68);
                //    else
                //        this.PutByte((byte)rightHand.BaseData.handMotion, 68);//*/
                //    Item rhillusionitemid = ItemFactory.Instance.GetItem(rightHand.PictID);
                //    this.PutUShort(rhillusionitemid.BaseData.handMotion, 71);
                //    try
                //    {
                //        this.PutByte((byte)(EquipSound)Enum.Parse(typeof(EquipSound), rhillusionitemid.BaseData.itemType.ToString()), 75);
                //        //this.PutUShort((ushort)rightHand.BaseData.itemType, 75);
                //    }
                //    catch
                //    {
                //        this.PutUShort(rightHand.BaseData.handMotion2, 75);
                //    }
                //}
                /*
                this.PutByte(192, 64);
                this.PutByte(192, 65);
                this.PutByte(192, 66);

                this.PutByte(192, 68);
                this.PutByte(192, 69);
                this.PutByte(192, 70);
                */
                //riding motion

                this.PutByte(3, 77);
                if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                {
                    if (value.Pet.Ride)
                    {
                        Item petItem = equips[EnumEquipSlot.PET];
                        /*/
                        if (petItem.BaseData.handMotion > 255)
                            this.PutUShort(petItem.BaseData.handMotion, 78);
                        else
                            this.PutByte((byte)petItem.BaseData.handMotion, 79);/*/
                        this.PutUShort(petItem.BaseData.handMotion, 78);

                        this.PutByte(0xff, 82);
                        this.PutByte(0xff, 83);
                    }
                }

                if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                {
                    if (value.Pet.Ride)
                    {
                        this.PutUInt(value.Inventory.Equipments[EnumEquipSlot.PET].ItemID, 84);
                    }
                }

                this.PutByte(0x0, 88);
                this.PutByte(0x0, 89);

            }
        }
    }
}