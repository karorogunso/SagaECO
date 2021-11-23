using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public enum PC_RACE
    {
        /// <summary>
        /// 埃米尔
        /// </summary>
        EMIL,
        /// <summary>
        /// 提塔尼亚
        /// </summary>
        TITANIA,
        /// <summary>
        /// 多米尼翁
        /// </summary>
        DOMINION,
        /// <summary>
        /// DEM
        /// </summary>
        DEM,
        NONE,
    }

    public enum DEM_FORM
    {
        /// <summary>
        /// 正常形态
        /// </summary>
        NORMAL_FORM,
        /// <summary>
        /// 机械形态
        /// </summary>
        MACHINA_FORM,
    }

    public enum PC_GENDER
    {
        /// <summary>
        /// 男性
        /// </summary>
        MALE,
        /// <summary>
        /// 女性
        /// </summary>
        FEMALE,
        NONE
    }

    public enum BATTLE_STATUS
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        NORMAL,
        /// <summary>
        /// 战斗状态
        /// </summary>
        BATTLE,
    }

    public enum ATTACK_TYPE
    {
        /// <summary>
        /// 打击
        /// </summary>
        BLOW,
        /// <summary>
        /// 斩击
        /// </summary>
        SLASH,
        /// <summary>
        /// 戳刺
        /// </summary>
        STAB,

    }

    public enum PC_JOB
    {
        /// <summary>
        /// 初学者
        /// </summary>
        NOVICE = 0,
        /// <summary>
        /// 剑士
        /// </summary>
        SWORDMAN = 1,
        /// <summary>
        /// 剑圣
        /// </summary>
        BLADEMASTER = 3,
        /// <summary>
        /// 赏金猎人
        /// </summary>
        BOUNTYHUNTER = 5,
        /// <summary>
        /// 剑斗士
        /// </summary>
        GLADIATOR = 7,
        /// <summary>
        /// 剑术家
        /// </summary>
        FENCER = 11,
        /// <summary>
        /// 骑士
        /// </summary>
        KNIGHT = 13,
        /// <summary>
        /// 黑暗骑士
        /// </summary>
        DARKSTALKER = 15,
        /// <summary>
        /// 守护者
        /// </summary>
        GUARDIAN = 17,
        /// <summary>
        /// 斥候
        /// </summary>
        SCOUT = 21,
        /// <summary>
        /// 刺客
        /// </summary>
        ASSASSIN = 23,
        /// <summary>
        /// 指挥官
        /// </summary>
        COMMAND = 25,
        /// <summary>
        /// 肃清者
        /// </summary>
        ERASER = 27,
        /// <summary>
        /// 弓箭手
        /// </summary>
        ARCHER = 31,
        /// <summary>
        /// 猎人
        /// </summary>
        STRIKER = 33,
        /// <summary>
        /// 枪手
        /// </summary>
        GUNNER = 35,
        /// <summary>
        /// 隼
        /// </summary>
        HAWKEYE = 37,
        /// <summary>
        /// 魔法师
        /// </summary>
        WIZARD = 41,
        /// <summary>
        /// 巫师
        /// </summary>
        SORCERER = 43,
        /// <summary>
        /// 贤者
        /// </summary>
        SAGE = 45,
        /// <summary>
        /// 原力大师
        /// </summary>
        FORCEMASTER = 47,
        /// <summary>
        /// 萨满
        /// </summary>
        SHAMAN = 51,
        /// <summary>
        /// 元素使
        /// </summary>
        ELEMENTER = 53,
        /// <summary>
        /// 附魔师
        /// </summary>
        ENCHANTER = 55,
        /// <summary>
        /// 星灵使
        /// </summary>
        ASTRALIST = 57,
        /// <summary>
        /// 修女
        /// </summary>
        VATES = 61,
        /// <summary>
        /// 德鲁伊
        /// </summary>
        DRUID = 63,
        /// <summary>
        /// 诗人
        /// </summary>
        BARD = 65,
        /// <summary>
        /// 主教
        /// </summary>
        CARDINAL = 67,
        /// <summary>
        /// 术士
        /// </summary>
        WARLOCK = 71,
        /// <summary>
        /// 秘术师
        /// </summary>
        CABALIST = 73,
        /// <summary>
        /// 死灵使
        /// </summary>
        NECROMANCER = 75,
        /// <summary>
        /// 噬魂者
        /// </summary>
        SOULTAKER = 77,
        /// <summary>
        /// 矿工
        /// </summary>
        TATARABE = 81,
        /// <summary>
        /// 铁匠
        /// </summary>
        BLACKSMITH = 83,
        /// <summary>
        /// 机械师
        /// </summary>
        MACHINERY = 85,
        /// <summary>
        /// 
        /// </summary>
        MAESTRO = 87,
        /// <summary>
        /// 农民
        /// </summary>
        FARMASIST = 91,
        /// <summary>
        /// 炼金术士
        /// </summary>
        ALCHEMIST = 93,
        /// <summary>
        /// 
        /// </summary>
        MARIONEST = 95,
        /// <summary>
        /// 丰收者
        /// </summary>
        HARVEST = 97,
        /// <summary>
        /// 游侠
        /// </summary>
        RANGER = 101,
        /// <summary>
        /// 探险者
        /// </summary>
        EXPLORER = 103,
        /// <summary>
        /// 财宝猎人
        /// </summary>
        TREASUREHUNTER = 105,
        /// <summary>
        /// 
        /// </summary>
        STRIDER = 107,
        /// <summary>
        /// 商人
        /// </summary>
        MERCHANT = 111,
        /// <summary>
        /// 贸易商
        /// </summary>
        TRADER = 113,
        /// <summary>
        /// 赌徒
        /// </summary>
        GAMBLER = 115,
        /// <summary>
        /// 皇家贸易商
        /// </summary>
        ROYALDEALER = 117,

        //新增的超级初心者
        JOKER,
        BREEDER = 1041,
        GARDNER = 1042,
        NONE,
    }

    public enum JobType
    {
        /// <summary>
        /// 初心者
        /// </summary>
        NOVICE,
        /// <summary>
        /// F系
        /// </summary>
        FIGHTER,
        /// <summary>
        /// SU系
        /// </summary>
        SPELLUSER,
        /// <summary>
        /// BP系
        /// </summary>
        BACKPACKER,
    }

    public enum ActorType
    {
        /// <summary>
        /// 玩家
        /// </summary>
        PC,
        /// <summary>
        /// 怪物
        /// </summary>
        MOB,
        /// <summary>
        /// 道具
        /// </summary>
        ITEM,
        /// <summary>
        /// 宠物
        /// </summary>
        PET,
        /// <summary>
        /// 技能对象
        /// </summary>
        SKILL,
        /// <summary>
        /// 影子对象
        /// </summary>
        SHADOW,
        /// <summary>
        /// 事件对象
        /// </summary>
        EVENT,
        /// <summary>
        /// 家具
        /// </summary>
        FURNITURE,
        /// <summary>
        /// 石像
        /// </summary>
        GOLEM,
        /// <summary>
        /// 家具单元
        /// </summary>
        FURNITUREUNIT,
        /// <summary>
        /// 搭档
        /// </summary>
        PARTNER,
        /// <summary>
        /// AN?
        /// </summary>
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
        Frame,
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
