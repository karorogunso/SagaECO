using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public enum PC_RACE
    {
        EMIL,
        TITANIA,
        DOMINION,
        DEM,
        NONE,
    }

    public enum DEM_FORM
    {
        NORMAL_FORM,
        MACHINA_FORM,
        NONE
    }

    public enum PC_GENDER
    {
        MALE,
        FEMALE,
        NONE
    }

    public enum BATTLE_STATUS
    {
        NORMAL,
        BATTLE,
    }

    public enum ATTACK_TYPE
    {
        BLOW,
        SLASH,
        STAB,

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
        JOKER = 120,


        BREEDER = 1041,
        GARDNER = 1042,
        NONE,
    }

    public enum JobType
    {
        NOVICE,
        FIGHTER,
        SPELLUSER,
        BACKPACKER,
    }

    public enum ActorType
    {
        PC,
        MOB,
        ITEM,
        PET,
        SKILL,
        SHADOW,
        EVENT,
        FURNITURE,
        GOLEM,
        FURNITUREUNIT,
        PARTNER,
        ANOTHERMOB,
    }

    public enum UpdateEvent
    {
        GOLD,
        CP,
        EP,
        ECoin,
        SPEED,
        CHAR_INFO,
        LEVEL,
        STAT_POINT,
        MODE,
        EVENT_TITLE,
        VCASH_POINT,
        WRP,
        QUEST_POINT,
        MOTION,
    }

    public enum PlayerMode
    {
        NORMAL,
        KNIGHT_WAR,
        COLISEUM_MODE,
        WRP,
        KNIGHT_EAST,
        KNIGHT_WEST,
        KNIGHT_SOUTH,
        KNIGHT_NORTH,
        KNIGHT_FLOWER,
        KNIGHT_ROCK,
    }

    public enum ActorEventTypes
    {
        ROPE,
        TENT,
    }
}
