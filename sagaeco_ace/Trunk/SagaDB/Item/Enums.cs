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
        EFFECT,
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

        IRIS_CARD,
        CSWAR_MARIO,
        BAIT_INSECT,
        BAIT_BIRD,
        BAIT_WATER_ANIMAL,
        BAIT_ANIMAL,
        SEED,
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
        COSTUME,
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
        CARD,
        ROPE,
        SOCKS,
        BULLET,
        ARROW,
        JOINT_SYMBOL, 
        BACK_DEMON,
        PET,
        RIDE_PET,
        PET_NEKOMATA,
        
        PARTS_HEAD,
        PARTS_BODY,
        PARTS_LEG,
        PARTS_BACK,
        PARTS_BLOW,
        PARTS_SLASH,
        PARTS_STAB,
        PARTS_LONGRANGE,
        DEMIC_CHIP,
        //不明
        SETPARTS_BLOW,


        FURNITURE,
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
        MONEY,
        STAMP,
        ITDGN_CREATE,

        FG_GARDEN_MODELHOUSE,
        FG_ROOM_WALL,
        FG_ROOM_FLOOR,
        FG_GARDEN_FLOOR,
        FG_FLYING_SAIL,
        FG_SEED,

        FF_CASTLE,
        FF_BOOTH,
        FF_ATTRACTION_UNIT,
        EXGUN,
        FF_BODY,
        FF_FLOOR,
        FACECHANGE,
        EXEMOTION,
        EXSWORD,

        UNION_COSTUME,
        UNION_WEAPON,
        SETPARTS_STAB,
        EFFECT,
        SETPARTS_SLASH,

        PARTNER,
        RIDE_PET_ROBOT,
        //SCROLL,
        RIDE_PARTNER,
        FF_ROOM_FLOOR,
        FF_ROOM_WALL,
        //THROW,
        UNION_ACTCUBE,
        UNION_FOOD

    }
    public enum EquipSound
    {
        SHORT_SWORD = 1,
        SWORD = 2,
        RAPIER = 3,
        CLAW = 4,
        HAMMER = 6,
        AXE = 7,
        SPEAR = 8,
        STAFF = 9,
    }
    public enum ContainerType
    {
        OTHER_WAREHOUSE,
        //Bag spaces
        BODY = 2,
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
        EFFECT,
        EXGUN,
        EXSWORD,

        WAREHOUSE = 30,
        GOLEMWAREHOUSE = 31,
        FG_WAREHOUSE = 51,

        HEAD2 = 206,
        HEAD_ACCE2,
        FACE_ACCE2,
        FACE2,
        CHEST_ACCE2,
        UPPER_BODY2,
        LOWER_BODY2,
        BACK2,
        RIGHT_HAND2,
        LEFT_HAND2,
        SHOES2,
        SOCKS2,
        PET2,

        ETC_WEAPON,
        NONE = -1,
    }

    public enum WarehousePlace
    {
        Acropolis,
        FarEast,
        IronSouth,
        Northan,
        MiningCamp,
        Morg,
        FederalOfIronSouth,
        KingdomOfNorthan,
        Tonka,
        MermaidsHome,
        MaimaiCamp,
        RepublicOfFarEast = 12,
        TowerGoesToHeaven,
        WestFord,
        ECOTown,
        Current = 0x1E,
    }

    public enum InventoryAddResult
    {
        NEW_INDEX,
        STACKED,
        MIXED,
        ERROR,
    }

    public enum InventoryDeleteResult
    {
        ALL_DELETED,
        STACK_UPDATED,
        ERROR,
    }
}
