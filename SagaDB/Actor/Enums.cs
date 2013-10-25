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
    }

    public enum PC_GENDER
    {
        MALE,
        FEMALE,
    }

    public enum PC_JOB
    {
        NOVICE = 0,
        SWORDMAN = 1,
        BLADEMASTER = 3,
        BOUNTYHUNTER = 5,
        FENCER = 11,
        KNIGHT = 13,
        DARKSTALKER = 15,
        SCOUT = 21,
        ASSASSIN = 23,
        COMMAND = 25,
        ARCHER = 31,
        STRIKER = 33,
        GUNNER = 35,
        WIZARD = 41,
        SORCERER = 43,
        SAGE = 45,
        SHAMAN = 51,
        ELEMENTER = 53,
        ENCHANTER = 55,
        VATES = 61,
        DRUID = 63,
        BARD = 65,
        WARLOCK = 71,
        CABALIST = 73,
        NECROMANCER = 75,
        TATARABE = 81,
        BLACKSMITH = 83,
        MACHINERY = 85,
        FARMASIST = 91,
        ALCHEMIST = 93,
        MARIONEST = 95,
        RANGER = 101,
        EXPLORER = 103,
        TREASUREHUNTER = 105,
        MERCHANT = 111,
        TRADER = 113,
        GAMBLER = 115,
    }

    public enum ActorType
    {
        PC,
        NPC,
        ITEM,
    }    
}
