using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLSTool
{
    public class ItemType
    {
        public string[] NONE = new string[] { "NONE", };
        public string[] FOOD = new string[] { "FOOD", };
        public string[] MARIO
        = new string[] {
            "CSWAR_MARIO",
            "MARIONETTE",
            "GOLEM",
        };
        public string[] SCROLL
        = new string[] {
            "SCROLL",
        };
        public string[] HEADWEAR
        = new string[] {
            "HELM",
            "ACCESORY_HEAD",
            "ACCESORY_FACE",
            "FULLFACE",
        };
        public string[] SOCKS
        = new string[] {
            "SOCKS",
        };
        public string[] NECKLACE
        = new string[] {
            "ACCESORY_NECK",
            "JOINT_SYMBOL",
        };
        public string[] DEMPARTS
        = new string[] {
            "PARTS_HEAD",
            "PARTS_BODY",
            "PARTS_LEG",
            "PARTS_BACK",
            "PARTS_BLOW",
            "PARTS_SLASH",
            "PARTS_STAB",
            "PARTS_LONGRANGE",
            "SETPARTS_BLOW",
            "UNION_COSTUME",
            "UNION_WEAPON",
            "SETPARTS_STAB",
        };
        public string[] FURNITURE
        = new string[] {
            "FG_GARDEN_MODELHOUSE",
            "FG_ROOM_WALL",
            "FG_ROOM_FLOOR",
            "FG_GARDEN_FLOOR",
            "FG_FLYING_SAIL",
            "FG_SEED",
            "FF_CASTLE",
            "FF_BOOTH",
           "FF_ATTRACTION_UNIT",
            "EXGUN",
            "FF_BODY",
            "FF_FLOOR",
            "FACECHANGE",
            "EXEMOTION",
            "EXSWORD",
            "FURNITURE",
        };
        public string[] LONGBOOTS
        = new string[] {
            
            "LONGBOOTS",
        };
        public string[] DRESS
        = new string[] {
            "ONEPIECE",
            "COSTUME",
            "BODYSUIT",
            "WEDDING",
            "OVERALLS",
            "FACEBODYSUIT",
        };
        public string[] SLACKS
        = new string[] {
            "SLACKS",
            "HALFBOOTS",
        };
        public string[] POTION
        = new string[] {
            "POTION",
        };
        public string[] IRIS_CARD
        = new string[] {
            "IRIS_CARD",
        };
        public string[] SEED
        = new string[] {
            "SEED",
        };
        public string[] SHIELD
        = new string[] {
            "SHIELD",
        };
        public string[] SHOES
        = new string[] {
            "BOOTS",
            "SHOES",
        };
        public string[] WEAPON
        = new string[] {
            "CLAW",
            "HAMMER",
            "STAFF",
            "SWORD",
            "AXE",
            "SPEAR",
            "BOW",
            "GUN",
            "ETC_WEAPON",
            "ACCESORY_FINGER",
            "SHORT_SWORD",
            "RAPIER",
            "STRINGS",
            "BOOK",
            "DUALGUN",
            "RIFLE",
            "THROW",
            "ROPE",
            "CARD",
        };
        public string[] BAG
        = new string[] {
            "BACKPACK",
            "LEFT_HANDBAG",
            "HANDBAG",
        };
        public string[] USE
        = new string[] {
            "USE",
            "BULLET",
            "ARROW",
        };
        public string[] PET
        = new string[] {
            "BACK_DEMON",
            "PET",
            "RIDE_PET",
            "PET_NEKOMATA",
        };
        public string[] DEMSKILL
        = new string[] {
            "DEMIC_CHIP",
        };
        public string[] BOX
        = new string[] {
            "TREASURE_BOX",
            "CONTAINER",
            "TIMBER_BOX",
        };
        public string[] ARMOR
        = new string[] {
            "ARMOR_UPPER",
            "ARMOR_LOWER",
            "EQ_ALLSLOT",
        };
        public string[] OTHER
        = new string[] {
            "FREESCROLL",
            "SKETCHBOOK",
            "ROBOT_GROW_ATKHIT",
            "ROBOT_GROW_MAGSKLHIT",
            "ROBOT_GROW_ATKDAM",
            "ROBOT_GROW_MAGSKLDAM",
            "ROBOT_GROW_CRIHIT",
            "ROBOT_GROW_MEAL",
            "MONEY",
            "STAMP",
            "ITDGN_CREATE",
            "PETFOOD",
            "PETFOOD_MACHINE",
            "PETFOOD_MAGIC_CREATURE",
            "PETFOOD_HUMAN",
            "PETFOOD_UNDEAD",
            "PETFOOD_INSECT",
            "PETFOOD_BIRD",
            "PETFOOD_WATER_ANIMAL",
            "BAIT_INSECT",
            "BAIT_BIRD",
            "BAIT_WATER_ANIMAL",
            "BAIT_ANIMAL",
        };
    }
    public enum Elements
    {
        Neutral,
        Fire,
        Water,
        Wind,
        Earth,
        Holy,
        Dark,
    }
    public enum AbnormalStatus
    {
        Poisen,
        Stone,
        Paralyse,
        Sleep,
        Silence,
        鈍足,
        Confused,
        Frosen,
        Stun,
    }
    public enum PC_RACE
    {
        EMIL,
        TITANIA,
        DOMINION,
        DEM,
        NONE,
    }
    public enum PC_GENDER
    {
        MALE,
        FEMALE,
        NONE
    }
    public enum PC_JOB
    {
        NOVICE = 0,
        SWORDMAN = 1,
        BLADEMASTER = 3,
        BOUNTYHUNTER = 5,
        GLADIATOR = 7,
        FENCER = 11,
        KNIGHT = 13,
        DARKSTALKER = 15,
        GUARDIAN = 17,
        SCOUT = 21,
        ASSASSIN = 23,
        COMMAND = 25,
        ERASER = 27,
        ARCHER = 31,
        STRIKER = 33,
        GUNNER = 35,
        HAWKEYE = 37,
        WIZARD = 41,
        SORCERER = 43,
        SAGE = 45,
        FORCEMASTER = 47,
        SHAMAN = 51,
        ELEMENTER = 53,
        ENCHANTER = 55,
        ASTRALIST = 57,
        VATES = 61,
        DRUID = 63,
        BARD = 65,
        CARDINAL = 67,
        WARLOCK = 71,
        CABALIST = 73,
        NECROMANCER = 75,
        SOULTAKER = 77,
        TATARABE = 81,
        BLACKSMITH = 83,
        MACHINERY = 85,
        MAESTRO = 87,
        FARMASIST = 91,
        ALCHEMIST = 93,
        MARIONEST = 95,
        HARVEST = 97,
        RANGER = 101,
        EXPLORER = 103,
        TREASUREHUNTER = 105,
        STRIDER = 107,
        MERCHANT = 111,
        TRADER = 113,
        GAMBLER = 115,
        ROYALDEALER = 117,

        //新增的超级初心者
        JOKER,


        BREEDER = 1041,
        GARDNER = 1042,
        NONE,
    }
    public enum TargetType
    {
        NONE,
        SELF,
        ONE,
        COORDINATES,
        CIRCLE,
    }
    public enum ActiveType
    {
        NONE,
        CIRCLE,
        ONE,
        WALL,
        DOUGHNUT,
    }
    public enum Country
    {
        East = 1,
        West,
        South,
        North,
    }

    public enum ItemTypes
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
        CARD,

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
    }
    public class ItemTypeEnum
    {
        public enum NONE
        {
            NONE,
        }
        public enum FOOD
        {
            FOOD,
        }
        public enum MARIO
        {
            CSWAR_MARIO,
            MARIONETTE,
            GOLEM,
        }
        public enum SCROLL
        {
            SCROLL,
        }
        public enum HEADWEAR
        {
            HELM,
            ACCESORY_HEAD,
            ACCESORY_FACE,
            FULLFACE,
        }
        public enum SOCKS
        {
            SOCKS,
        }
        public enum NACKLACE
        {
            ACCESORY_NECK,
            JOINT_SYMBOL,
        }
        public enum DEMPARTS
        {
            PARTS_HEAD,
            PARTS_BODY,
            PARTS_LEG,
            PARTS_BACK,
            PARTS_BLOW,
            PARTS_SLASH,
            PARTS_STAB,
            PARTS_LONGRANGE,
            SETPARTS_BLOW,
            UNION_COSTUME,
            UNION_WEAPON,
            SETPARTS_STAB,
        }
        public enum FURNITURE
        {
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
            FURNITURE,
        }
        public enum LONGBOOTS
        {
            LONGBOOTS,
        }
        public enum DRESS
        {
            ONEPIECE,
            COSTUME,
            BODYSUIT,
            WEDDING,
            OVERALLS,
            FACEBODYSUIT,
        }
        public enum SLACKS
        {
            SLACKS,
            HALFBOOTS,
        }
        public enum POTION
        {
            POTION,
        }
        public enum IRIS_CARD
        {
            IRIS_CARD,
        }
        public enum SEED
        {
            SEED,
        }
        public enum SHIELD
        {
            SHIELD,
        }
        public enum SHOES
        {
            BOOTS,
            SHOES,
        }
        public enum WEAPON
        {
            CLAW,
            HAMMER,
            STAFF,
            SWORD,
            AXE,
            SPEAR,
            BOW,
            GUN,
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
            CARD,
        }
        public enum BAG
        {
            BACKPACK,
            LEFT_HANDBAG,
            HANDBAG,
        }
        public enum USE
        {
            USE,
            BULLET,
            ARROW,
        }
        public enum PET
        {
            BACK_DEMON,
            PET,
            RIDE_PET,
            PET_NEKOMATA,
        }
        public enum DEMSKILL
        {
            DEMIC_CHIP,
        }
        public enum BOX
        {
            TREASURE_BOX,
            CONTAINER,
            TIMBER_BOX,
        }
        public enum ARMOR
        {
            ARMOR_UPPER,
            ARMOR_LOWER,
            EQ_ALLSLOT,
        }
        public enum OTHER
        {
            FREESCROLL,
            SKETCHBOOK,
            ROBOT_GROW_ATKHIT,
            ROBOT_GROW_MAGSKLHIT,
            ROBOT_GROW_ATKDAM,
            ROBOT_GROW_MAGSKLDAM,
            ROBOT_GROW_CRIHIT,
            ROBOT_GROW_MEAL,
            MONEY,
            STAMP,
            ITDGN_CREATE,
            PETFOOD,
            PETFOOD_MACHINE,
            PETFOOD_MAGIC_CREATURE,
            PETFOOD_HUMAN,
            PETFOOD_UNDEAD,
            PETFOOD_INSECT,
            PETFOOD_BIRD,
            PETFOOD_WATER_ANIMAL,
            BAIT_INSECT,
            BAIT_BIRD,
            BAIT_WATER_ANIMAL,
            BAIT_ANIMAL,
        }
    }
}
