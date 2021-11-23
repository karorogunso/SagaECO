using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaMap.Manager;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.Item;
using SagaDB.Skill;
using SagaDB.Quests;
using SagaDB.Npc;

namespace SagaMap.Scripting
{
    public enum FGardenParts
    {
        /// <summary>
        /// 飛空庭の土台 
        /// </summary>
        Foundation = 0x1,
        /// <summary>
        /// 飛空庭エンジン 
        /// </summary>
        Engine = 0x2,
        /// <summary>
        /// 飛空庭の回転帆　1枚 
        /// </summary>
        Sail1 = 0x4,
        /// <summary>
        /// 飛空庭の回転帆　2枚 
        /// </summary>
        Sail2 = 0x8,
        /// <summary>
        /// 飛空庭の回転帆　3枚 
        /// </summary>
        Sail3 = 0x10,
        /// <summary>
        /// 飛空庭の回転帆　4枚 
        /// </summary>
        Sail4 = 0x20,
        /// <summary>
        /// 飛空庭の回転帆　5枚 
        /// </summary>
        Sail5 = 0x40,
        /// <summary>
        /// 飛空庭の揃った回転帆
        /// </summary>
        SailComplete = 0x80,
        /// <summary>
        /// 飛空庭のろくろ 
        /// </summary>
        Wheel = 0x100,
        /// <summary>
        /// 操舵輪 
        /// </summary>
        Steer = 0x200,
        /// <summary>
        /// 触媒
        /// </summary>
        Catalyst = 0x400,
    }

    public abstract partial class Event
    {
        public static string GetItemNameByType(ItemType type)
        {
            switch (type)
            {
                case ItemType.FOOD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FOOD;
                case ItemType.PETFOOD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_PETFOOD;
                case ItemType.ACCESORY_HEAD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_HEAD;
                case ItemType.ACCESORY_FINGER:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_FINGER;
                case ItemType.ACCESORY_FACE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_FACE0;
                case ItemType.HELM:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_HELM;
                case ItemType.BULLET:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BULLET;
                case ItemType.ARROW:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ARROW;
                case ItemType.BOW:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BOW;
                case ItemType.GUN:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_GUN;
                case ItemType.RIFLE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_GUN;
                case ItemType.BOOTS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BOOTS;
                case ItemType.ROPE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ROPE;
                case ItemType.HAMMER:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_HAMMER;
                case ItemType.ARMOR_UPPER:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ARMOR_UPPER;
                case ItemType.OVERALLS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_OVERALLS;
                case ItemType.BODYSUIT:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BODYSUIT;
                case ItemType.WEDDING:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BODYSUIT;
                case ItemType.FACEBODYSUIT:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FACEBODYSUIT;
                case ItemType.FULLFACE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FULLFACE;
                case ItemType.HALFBOOTS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_HALFBOOTS;
                case ItemType.LONGBOOTS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_LONGBOOTS;
                case ItemType.SHOES:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SHOES;
                case ItemType.SHIELD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SHIELD;
                case ItemType.LEFT_HANDBAG:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_LEFT_HANDBAG;
                case ItemType.ONEPIECE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ONEPIECE;
                case ItemType.COSTUME:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_COSTUME;
                case ItemType.BOOK:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BOOK;
                case ItemType.STAFF:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_STAFF;
                case ItemType.SWORD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SWORD;
                case ItemType.AXE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_AXE;
                case ItemType.SPEAR:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SPEAR;
                case ItemType.RAPIER:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_RAPIER;
                case ItemType.CARD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_CARD;
                case ItemType.HANDBAG:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_HANDBAG;
                case ItemType.SHORT_SWORD:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SHORT_SWORD;
                case ItemType.ETC_WEAPON:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ETC_WEAPON;
                case ItemType.THROW:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_THROW;
                case ItemType.SLACKS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SLACKS;
                case ItemType.ARMOR_LOWER:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ARMOR_LOWER;
                case ItemType.ACCESORY_NECK:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_NECK;
                case ItemType.BACKPACK:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BACKPACK;
                case ItemType.SOCKS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SOCKS;
                case ItemType.PET_NEKOMATA:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_PET_NEKOMATA;
                case ItemType.PET:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_PET;
                case ItemType.RIDE_PET:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_RIDE_PET;
                case ItemType.BACK_DEMON:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_BACK_DEMON;
                case ItemType.DUALGUN:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_GUN;
                case ItemType.CLAW:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_CLAW;
                case ItemType.STRINGS:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_STAFF;
                case ItemType.POTION:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_POTION;
                case ItemType.USE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_USE;
                case ItemType.NONE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_NONE;
                case ItemType.SEED:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SEED;
                case ItemType.SCROLL:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_SCROLL;
                case ItemType.FURNITURE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FG_FURNITURE;
                case ItemType.FG_GARDEN_MODELHOUSE:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FG_BASEBUILD;
                case ItemType.FG_ROOM_WALL:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FG_ROOM_WALL;
                case ItemType.FG_ROOM_FLOOR:
                    return LocalManager.Instance.Strings.ITEM_UNIDENTIFIED_FG_ROOM_FLOOR;
                default:
                    return type.ToString();
            }
        }
    }
}
