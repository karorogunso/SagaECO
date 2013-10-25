using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Item
{
    public enum EnumEquipSlot
    {
        HEAD,
        HEAD_ACCE,
        FACE,
        FACE_ACCE,
        CHEST_ACCE,
        UPPER_BODY,
        LOWER_BODY,
        BACK,
        RIGHT_HAND,
        LEFT_HAND,
        SHOES,
        SOCKS,
        PET,
        /*HEAD = 6,
        CHEST_ACCE = 10,
        RIGHT = 14,
        SHOES = 16,*/
    }

    public enum ItemType
    {
        NONE,
        POTION,
        FOOD,
        USE,

        PETFOOD,
        PETFOOD_MACHINE,
        PETFOOD_MAGIC_CREATURE,
        PETFOOD_HUMAN,
        PETFOOD_UNDEAD,
        PETFOOD_INSECT,
        PETFOOD_BIRD,
        PETFOOD_WATER_ANIMAL,

        CSWAR_MARIO,
        BAIT_INSECT,
        BAIT_BIRD,
        BAIT_WATER_ANIMAL,
        BAIT_ANIMAL,
        SEED,
        PET,
        SCROLL,
        MARIONETTE,
        GOLEM,

        ACCESORY_HEAD,
        ACCESORY_FACE,
        HELM,
        BOOTS,
        CLAW,
        HAMMER,
        ARMOR_UPPER,
        FULLFACE,
        LONGBOOTS,
        SHIELD,
        ONEPIECE,
        BODYSUIT,
        STAFF,
        SWORD,
        AXE,
        SPEAR,
        BOW,
        HANDBAG,
        GUN,
        ARMOR_LOWER,
        EQ_ALLSLOT,
        WEDDING,
        OVERALLS,
        FACEBODYSUIT,
        SLACKS,
        SHOES,
        HALFBOOTS,
        ACCESORY_NECK,
        BACKPACK,
        LEFT_HANDBAG,
        ETC_WEAPON,
        ACCESORY_FINGER,
        SHORT_SWORD,
        RAPIER,
        STRINGS,
        BOOK,
        DUALGUN,
        RIFLE,
        THROW,
        ROPE,
        SOCKS,

        FURNITURE,
        PET_NEKOMATA,
        RIDE_PET,
        FREESCROLL,
        SKETCHBOOK,
        TREASURE_BOX,
        CONTAINER,
        TIMBER_BOX,
        ROBOT_GROW_ATKHIT,
        ROBOT_GROW_MAGSKLHIT,
        ROBOT_GROW_ATKDAM,
        ROBOT_GROW_MAGSKLDAM,
        ROBOT_GROW_CRIHIT,
        ROBOT_GROW_MEAL,
        BULLET,
        ARROW,
        MONEY,
        BACK_DEMON,
        STAMP,
        ITDGN_CREATE,
        CARD,

        FG_GARDEN_MODELHOUSE,
        FG_ROOM_WALL,
        FG_ROOM_FLOOR,
        FG_GARDEN_FLOOR,
        FG_FLYING_SAIL,
    }

    public enum ContainerType
    {
        OTHER_WAREHOUSE,
        //Bag spaces
        BODY=2,
        RIGHT_BAG,
        LEFT_BAG,
        BACK_BAG,

        //Equipments
        HEAD,
        HEAD_ACCE,
        FACE_ACCE,
        FACE,
        CHEST_ACCE,
        UPPER_BODY,
        LOWER_BODY,
        BACK,
        RIGHT_HAND,
        LEFT_HAND,
        SHOES,
        SOCKS,
        PET,

        WAREHOUSE=30,
        FG_WAREHOUSE=51,
    }

    public enum InventoryAddResult
    {
        NEW_INDEX,
        STACKED,
    }

    public enum InventoryDeleteResult
    {
        ALL_DELETED,
        STACK_UPDATED,
    }
}
