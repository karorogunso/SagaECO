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
            this.data = new byte[81];
            this.offset = 2;
            this.ID = 0x09E9;
            
        }

        public ActorPC Player
        {
            set
            {
                this.PutUInt(value.ActorID, 2);
                this.PutByte(14, 6);
                Dictionary<EnumEquipSlot, Item> equips;
                if (value.Form != DEM_FORM.MACHINA_FORM)
                {
                    equips = value.Inventory.Equipments;
                }
                else
                    equips = value.Inventory.Parts;
                if (value.Marionette == null)
                {
                    for (int j = 0; j < 14; j++)
                    {
                        if (equips.ContainsKey((EnumEquipSlot)j))
                        {
                            Item item = equips[(EnumEquipSlot)j];
                            if (j==9 && item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && item.EquipSlot.Count == 1)
                            {
                                if (item.PictID == 0)
                                    this.PutUInt(item.BaseData.imageID+10000000, (ushort)(7 + j * 4));
                                else
                                    this.PutUInt(item.PictID + 10000000, (ushort)(7 + j * 4));
                            }
                            else
                            {
                                if (item.Stack == 0) continue;
                                if (item.PictID == 0)
                                    this.PutUInt(item.BaseData.imageID, (ushort)(7 + j * 4));
                                else
                                    this.PutUInt(item.PictID, (ushort)(7 + j * 4));
                            }
                        }
                    }
                }
                else
                {
                    this.PutUInt(value.Marionette.PictID, 7);
                }

                //left hand weapon motion
                this.PutByte(3, 63);
                if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND) && value.Marionette == null)
                {
                    Item leftHand = equips[EnumEquipSlot.LEFT_HAND];
                    this.PutByte(leftHand.BaseData.handMotion, 64);
                    try
                    {
                        this.PutByte((byte)(EquipSound)Enum.Parse(typeof(EquipSound), leftHand.BaseData.itemType.ToString()), 66);
                    }
                    catch
                    {
                        this.PutByte(leftHand.BaseData.handMotion2, 66);
                    }
                }
                //right hand weapon motion
                this.PutByte(3, 67);
                if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND) && value.Marionette == null)
                {
                    Item rightHand = equips[EnumEquipSlot.RIGHT_HAND];
                    this.PutByte(rightHand.BaseData.handMotion, 68);
                    try
                    {
                        this.PutByte((byte)(EquipSound)Enum.Parse(typeof(EquipSound), rightHand.BaseData.itemType.ToString()), 70);
                    }
                    catch
                    {
                        this.PutByte(rightHand.BaseData.handMotion2, 70);
                    }
                }
                /*
                this.PutByte(192, 64);
                this.PutByte(192, 65);
                this.PutByte(192, 66);

                this.PutByte(192, 68);
                this.PutByte(192, 69);
                this.PutByte(192, 70);
                */
                //riding motion
                this.PutByte(3, 71);
                if (equips.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                {
                    if (value.Pet.Ride)
                    {
                        Item petItem = equips[EnumEquipSlot.PET];
                        this.PutByte(petItem.BaseData.handMotion, 72);
                        this.PutByte(petItem.BaseData.handMotion2, 74);
                    }
                }

                if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null)
                {
                    if (value.Pet.Ride)
                    {
                        this.PutUInt(value.Inventory.Equipments[EnumEquipSlot.PET].ItemID, 75);
                    }
                }

            }
        }
    }
}

