using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Item
{
    public class Item
    {
        public class ItemData
        {
            public string name;
            public uint id, price;
            public ushort equipVolume, possessionWeight, weight, volume;
            public ItemType itemType;
            public uint repairItem, enhancementItem;
            public uint events;
            public bool receipt, dye, stock, doubleHand, usable;
            public byte color;
            public ushort durability;
            public uint eventID, effectID;
            public ushort activateSkill, possibleSkill, passiveSkill, possessionSkill, possessionPassiveSkill;
            public TargetType target;
            public ActiveType activeType;
            public byte range;
            public uint duration;
            public byte effectRange;
            public uint cast, delay;
            public short hp, mp, sp, weightUp, volumeUp, speedUp;
            public short str, dex, intel, vit, agi, mag, luk, cha;
            public short atk1, atk2, atk3, matk, def, mdef;
            public short hitMelee, hitRanged, hitMagic;
            public short avoidMelee, avoidRanged, avoidMagic;
            public short hitCritical, avoidCritical;
            public short hpRecover, mpRecover;
            public Dictionary<Elements, short> element = new Dictionary<Elements, short>();
            public Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
            public Dictionary<PC_RACE, bool> possibleRace = new Dictionary<PC_RACE, bool>();
            public Dictionary<PC_GENDER, bool> possibleGender = new Dictionary<PC_GENDER, bool>();
            public byte possibleLv;
            public ushort possibleStr, possibleDex, possibleInt, possibleVit, possibleAgi, possibleMag, possibleLuk, possibleCha;
            public Dictionary<PC_JOB, bool> possibleJob = new Dictionary<PC_JOB, bool>();
            public Dictionary<Country, bool> possibleCountry = new Dictionary<Country, bool>();

            public override string ToString()
            {
                return this.name;
            }
        }

        public ItemData BaseData;
        public byte stack;
        public ushort durability;
        public byte identified;
        private uint slot;

        public uint ItemID { get { return this.BaseData.id; } }
        public ushort maxDurability { get { return this.BaseData.durability; } }
        public uint Slot { get { return this.slot; } set { this.slot = value; } }

        public bool Identified
        {
            get
            {
                if (this.identified == 0)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value == true)
                    this.identified = 1;
                else
                    this.identified = 0;
            }
        }
        public Item Clone()
        {
            Item item = new Item();
            item.BaseData = this.BaseData;
            item.durability = this.durability;
            item.stack = this.stack;
            item.identified = this.identified;
            return item;
        }

        public bool Stackable
        {
            get
            {
                int type = (int)this.BaseData.itemType;
                if (type >= (int)ItemType.ACCESORY_HEAD && type <= (int)ItemType.SOCKS)
                    return false;
                else
                    return true;
            }
        }

        public bool IsEquipt
        {
            get
            {
                int type = (int)this.BaseData.itemType;
                if (type >= (int)ItemType.ACCESORY_HEAD && type <= (int)ItemType.SOCKS)
                    return true;
                else
                    return false;
            }
        }

        public EnumEquipSlot EquipSlot
        {
            get
            {
                if (!IsEquipt)
                    Logger.ShowDebug("Cannot equip a non equipment item!", Logger.defaultlogger);
                switch (this.BaseData.itemType)
                {
                    case ItemType.ACCESORY_HEAD:
                        return EnumEquipSlot.HEAD_ACCE;
                    case ItemType.ACCESORY_FACE:
                        return EnumEquipSlot.FACE_ACCE;
                    case ItemType.HELM:
                        return EnumEquipSlot.HEAD;
                    case ItemType.BOOTS:
                        return EnumEquipSlot.SHOES;
                    case ItemType.CLAW:
                        return EnumEquipSlot.RIGHT_HAND;
                    case ItemType.HAMMER :
                        return EnumEquipSlot.RIGHT_HAND;
                    case ItemType.ARMOR_UPPER:
                        return EnumEquipSlot.UPPER_BODY;
                    case ItemType.FULLFACE:
                        return EnumEquipSlot.FACE;
                    case ItemType.LONGBOOTS :
                    case ItemType.SHOES:
                    case ItemType.HALFBOOTS:
                        return EnumEquipSlot.SHOES;
                    case ItemType.SHIELD :
                    case ItemType.LEFT_HANDBAG:
                        return EnumEquipSlot.LEFT_HAND;
                    case ItemType.ONEPIECE:
                        return EnumEquipSlot.UPPER_BODY;
                    case ItemType.BODYSUIT:
                    case ItemType.WEDDING:
                        return EnumEquipSlot.UPPER_BODY;
                    case ItemType.STAFF:
                    case ItemType.SWORD:
                    case ItemType.AXE:
                    case ItemType.SPEAR:
                    case ItemType.BOW:
                    case ItemType.HANDBAG:
                    case ItemType.GUN :
                    case ItemType.SHORT_SWORD:
                        return EnumEquipSlot.RIGHT_HAND;
                    case ItemType.ARMOR_LOWER:
                        return EnumEquipSlot.LOWER_BODY;
                    case ItemType.ACCESORY_NECK:
                        return EnumEquipSlot.CHEST_ACCE;
                    case ItemType.BACKPACK:
                        return EnumEquipSlot.BACK;
                    case ItemType.SOCKS:
                        return EnumEquipSlot.SOCKS;
                }
                return EnumEquipSlot.BACK;
            }
        }
    }
}
