using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Marionette;
using SagaDB.Quests;
using SagaDB.Party;
using SagaDB.DEMIC;
using SagaDB.Navi;

namespace SagaDB.Actor
{
    [Serializable]
    #region 商人商店部分..
    public class PlayerShopItem
    {
        uint inventoryID;
        uint itemID;
        ushort count;
        uint price;

        public uint InventoryID { get { return this.inventoryID; } set { this.inventoryID = value; } }
        public uint ItemID { get { return this.itemID; } set { this.itemID = value; } }
        public ushort Count { get { return this.count; } set { this.count = value; } }
        public uint Price { get { return this.price; } set { this.price = value; } }
    }
    #endregion

    public class ActorPC : Actor, IStats
    {

        public List<BBS.Mail> Mails = new List<BBS.Mail>();
        public List<BBS.Gift> Gifts = new List<BBS.Gift>();


        uint charID;
        [NonSerialized]
        Account account;
        PC_RACE race;
        PC_GENDER gender;
        byte tailStyle;//尾巴形狀
        byte wingStyle;//翅膀形狀
        byte wingColor;//翅膀顏色
        ushort hairStyle;
        byte hairColor;
        ushort wig;
        ushort face;
        PC_JOB job;
        PC_JOB jointJob = PC_JOB.NONE;
        byte lv, dlv, djlv, jjlv;
        byte jlv1, jlv2x, jlv2t, jlv3;

        ushort questRemaining;
        uint fame;
        string sign;
        DEM_FORM form;
        short cl, dcl;
        short epUsed, depUsed;
        ActorEvent tentActor;

        MotionType motion;
        bool motion_loop, online;
        List<DateTime> timeonline;

        ushort str, dex, intel, vit, agi, mag, dstr, ddex, dintel, dvit, dagi, dmag;
        ushort statspoints, dstatspoints, skillpoint, skillpoint2x, skillpoint2t, skillpoint3;

        bool dreseve;
        uint wrpRanking;
        Marionette.Marionette marionette;
        DateTime nextMarionetteTime = DateTime.Now;

        ActorPet pet;
        Quest quest;
        DateTime questNextTime, epLoginDate, epGreetingDate;

        public byte WaitType;//站姿

        //虚拟站街角色
        public bool Fictitious;

        public uint LastAttackActorID;
        public bool AutoAttack;

        Navi.Navi navi = new Navi.Navi(NaviFactory.Instance.Navi);

        public bool CheckEquipmentPermissions = false;

        public uint[] equips;
        /// <summary>
        /// 伪造ACTOR用装备栏
        /// </summary>
        public uint[] Equips { get { return this.equips; } set { this.equips = value; } }

        /// <summary>
        /// 当前激活的套装ID
        /// </summary>
        public uint EquipSetID { get; set; }

        public class KillInfo
        {
            public bool isFinish = false;
            public int Count { set; get; }
            public int TotalCount { set; get; }
        }
        /// <summary>
        /// 击杀列表
        /// </summary>
        public Dictionary<uint, KillInfo> KillList = new Dictionary<uint, KillInfo>();
        /// <summary>
        /// 任务标记 (byte为列表ID)
        /// </summary>
        public Navi.Navi Navi { get { return this.navi; } set { this.navi = value; } }

        #region 商人商店部分..
        Dictionary<uint, PlayerShopItem> playershoplist = new Dictionary<uint, PlayerShopItem>();
        /// <summary>
        /// 玩家贩卖的道具
        /// </summary>
        public Dictionary<uint, PlayerShopItem> Playershoplist { get { return this.playershoplist; } }
        #endregion
        public class NPCHide
        {
            public int NPCID { set; get; }
            public byte state { set; get; }
        }
        /// <summary>
        /// 自动显示隐藏NPC列表（地图ID，state = 0 显示  state = 1 隐藏）
        /// </summary>
        public Dictionary<uint, uint> NpcShowList = new Dictionary<uint, uint>();
        /// <summary>
        /// 1转职业技能
        /// </summary>
        public Dictionary<uint, Skill.Skill> Skills = new Dictionary<uint, SagaDB.Skill.Skill>();
        /// <summary>
        /// 2转职业技能
        /// </summary>
        public Dictionary<uint, Skill.Skill> Skills2 = new Dictionary<uint, SagaDB.Skill.Skill>();
        /// <summary>
        /// 2转职业保留技能
        /// </summary>
        public Dictionary<uint, Skill.Skill> SkillsReserve = new Dictionary<uint, SagaDB.Skill.Skill>();

        //TT添加的部分

        /// <summary>
        /// 2-1职业技能
        /// </summary>
        public Dictionary<uint, Skill.Skill> Skills2_1 = new Dictionary<uint, SagaDB.Skill.Skill>();
        /// <summary>
        /// 2-2职业技能
        /// </summary>
        public Dictionary<uint, Skill.Skill> Skills2_2 = new Dictionary<uint, SagaDB.Skill.Skill>();
        /// <summary>
        /// 3转职业技能
        /// </summary>
        public Dictionary<uint, Skill.Skill> Skills3 = new Dictionary<uint, SagaDB.Skill.Skill>();


        byte lv1;
        /// <summary>
        /// 转生前等级
        /// </summary>
        public byte Level1
        {
            get
            {
                return lv1;
            }
            set
            {
                lv1 = value;
            }
        }

        /// <summary>
        /// 转生标记
        /// </summary>
        public bool Rebirth
        {
            get
            {
                return job == Job3;
            }
        }
        //END

        uint size, cp, ecoin;
        ulong cexp, jexp, dcexp, djexp, jjexp;

        //冒险经验
        ulong explorerExp;

        int wrp, gold;

        byte slot;

        byte battleStatus;
        uint save_map;
        byte save_x;
        byte save_y;

        Inventory inventory;

        Dictionary<uint, Dictionary<uint, bool>> npcStates = new Dictionary<uint, Dictionary<uint, bool>>();

        VariableHolder<string, string> aStrVar = new VariableHolder<string, string>("");
        VariableHolder<string, int> aIntVar = new VariableHolder<string, int>(0);
        VariableHolderA<string, BitMask> aMaskVar = new VariableHolderA<string, BitMask>();
        VariableHolder<string, string> cStrVar = new VariableHolder<string, string>("");
        VariableHolderA<string, BitMask> cMaskVar = new VariableHolderA<string, BitMask>();
        VariableHolder<string, int> cIntVar = new VariableHolder<string, int>(0);
        VariableHolder<string, string> tStrVar = new VariableHolder<string, string>("");
        VariableHolder<string, int> tIntVar = new VariableHolder<string, int>(0);
        VariableHolderA<string, BitMask> tMask = new VariableHolderA<string, BitMask>();

        uint possessionTarget;
        PossessionPosition possessionPosition;

        Party.Party party;
        Ring.Ring ring;
        FGarden.FGarden fgarden;

        uint vpoints, usedVPoints;
        ActorGolem golem;
        uint dungeonID;
        /// <summary>
        /// 變身圖片ID
        /// </summary>
        uint tranceID = 0;
        Stamp stamp = new Stamp();

        PlayerMode mode = PlayerMode.NORMAL;
        /// <summary>
        /// 尾巴形狀
        /// </summary>
        public byte TailStyle
        {
            get
            {
                return tailStyle;
            }
            set
            {
                tailStyle = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }
        /// <summary>
        /// 翅膀形狀
        /// </summary>
        public byte WingStyle
        {
            get
            {
                return wingStyle;
            }
            set
            {
                wingStyle = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }
        /// <summary>
        /// 翅膀顏色
        /// </summary>
        public byte WingColor
        {
            get
            {
                return wingColor;
            }
            set
            {
                wingColor = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }
        /// <summary>
        /// 体积
        /// </summary>
        public uint Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }


        public ActorPC()
        {
            this.type = ActorType.PC;
            this.sightRange = Global.MAX_SIGHT_RANGE;
            this.Speed = 420;
            this.inventory = new Inventory(this);
        }

        /// <summary>
        /// 存在于数据库的CharID
        /// </summary>
        public uint CharID
        {
            get
            {
                return charID;
            }
            set
            {
                charID = value;
            }
        }

        /// <summary>
        /// 该玩家的帐号信息
        /// </summary>
        public Account Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
            }
        }

        /// <summary>
        /// 种族
        /// </summary>
        public PC_RACE Race
        {
            get
            {
                return race;
            }
            set
            {
                race = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public PC_GENDER Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        /// <summary>
        /// 发型
        /// </summary>
        public ushort HairStyle
        {
            get
            {
                return hairStyle;
            }
            set
            {
                hairStyle = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// 头发颜色
        /// </summary>
        public byte HairColor
        {
            get
            {
                return hairColor;
            }
            set
            {
                hairColor = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// 假发??
        /// </summary>
        public ushort Wig
        {
            get
            {
                return wig;
            }
            set
            {
                wig = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// 脸
        /// </summary>
        public ushort Face
        {
            get
            {
                return face;
            }
            set
            {
                face = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// 职业
        /// </summary>
        public PC_JOB Job
        {
            get
            {
                return job;
            }
            set
            {
                job = value;
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public override byte Level
        {
            get
            {
                return lv;
            }
            set
            {
                lv = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 恶魔界的基础等级
        /// </summary>
        public byte DominionLevel
        {
            get { return dlv; }
            set
            {
                this.dlv = value; if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 恶魔界的职业等级
        /// </summary>
        public byte DominionJobLevel
        {
            get { return djlv; }
            set
            {
                this.djlv = value; if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 联合职业等级
        /// </summary>
        public byte JointJobLevel
        {
            get { return jjlv; }
            set
            {
                this.jjlv = value; if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 当前职业等级
        /// </summary>
        public byte CurrentJobLevel
        {
            get
            {
                if (this.Job == this.JobBasic)
                    return this.JobLevel1;
                if (this.Job == this.Job2X)
                    return this.JobLevel2X;
                if (this.Job == this.Job2T)
                    return this.JobLevel2T;
                if (this.Job == this.Job3)
                    return this.JobLevel3;
                return this.JobLevel1;
            }
        }

        /// <summary>
        /// 1转职业等级
        /// </summary>
        public byte JobLevel1
        {
            get
            {
                return jlv1;
            }
            set
            {
                jlv1 = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 剩余任务数
        /// </summary>
        public ushort QuestRemaining
        {
            get
            {
                return questRemaining;
            }
            set
            {
                questRemaining = value;
            }
        }

        /// <summary>
        /// 2-1职业等级
        /// </summary>
        public byte JobLevel2X
        {
            get
            {
                return jlv2x;
            }
            set
            {
                jlv2x = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 2－2职业等级
        /// </summary>
        public byte JobLevel2T
        {
            get
            {
                return jlv2t;
            }
            set
            {
                jlv2t = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 3职业等级
        /// </summary>
        public byte JobLevel3
        {
            get
            {
                return jlv3;
            }
            set
            {
                jlv3 = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// 人物所占用的存储槽
        /// </summary>
        public byte Slot
        {
            get
            {
                return slot;
            }
            set
            {
                slot = value;
            }
        }

        public bool InDominionWorld
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID))
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return true;
                    else
                        return false;
                }
                else
                {
                    uint oriMap = this.MapID / 1000 * 1000;
                    if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(oriMap))
                    {
                        Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[oriMap];
                        if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
        }

        public List<ActorPC> PossesionedActors
        {
            get
            {
                List<ActorPC> list = new List<ActorPC>();
                if (this.inventory != null)
                {
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                    {
                        if (this.inventory.Equipments[EnumEquipSlot.CHEST_ACCE].PossessionedActor != null)
                            if (!list.Contains(this.inventory.Equipments[EnumEquipSlot.CHEST_ACCE].PossessionedActor))
                                list.Add(this.inventory.Equipments[EnumEquipSlot.CHEST_ACCE].PossessionedActor);
                    }
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                    {
                        if (this.inventory.Equipments[EnumEquipSlot.UPPER_BODY].PossessionedActor != null)
                            if (!list.Contains(this.inventory.Equipments[EnumEquipSlot.UPPER_BODY].PossessionedActor))
                                list.Add(this.inventory.Equipments[EnumEquipSlot.UPPER_BODY].PossessionedActor);
                    }
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (this.inventory.Equipments[EnumEquipSlot.RIGHT_HAND].PossessionedActor != null)
                            if (!list.Contains(this.inventory.Equipments[EnumEquipSlot.RIGHT_HAND].PossessionedActor))
                                list.Add(this.inventory.Equipments[EnumEquipSlot.RIGHT_HAND].PossessionedActor);
                    }
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if (this.inventory.Equipments[EnumEquipSlot.LEFT_HAND].PossessionedActor != null)
                            if (!list.Contains(this.inventory.Equipments[EnumEquipSlot.LEFT_HAND].PossessionedActor))
                                list.Add(this.inventory.Equipments[EnumEquipSlot.LEFT_HAND].PossessionedActor);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 该玩家对应的联合职业
        /// </summary>
        public PC_JOB JobJoint
        {
            get
            {
                return jointJob;
            }
            set
            {
                jointJob = value;
            }
        }

        /// <summary>
        /// 该玩家对应的1转职业
        /// </summary>
        public PC_JOB JobBasic
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                    case PC_JOB.GLADIATOR:
                        return PC_JOB.SWORDMAN;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                    case PC_JOB.GUARDIAN:
                        return PC_JOB.FENCER;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                    case PC_JOB.ERASER:
                        return PC_JOB.SCOUT;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                    case PC_JOB.HAWKEYE:
                        return PC_JOB.ARCHER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                    case PC_JOB.FORCEMASTER:
                        return PC_JOB.WIZARD;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                    case PC_JOB.ASTRALIST:
                        return PC_JOB.SHAMAN;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                    case PC_JOB.CARDINAL:
                        return PC_JOB.VATES;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                    case PC_JOB.SOULTAKER:
                        return PC_JOB.WARLOCK;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                    case PC_JOB.MAESTRO:
                        return PC_JOB.TATARABE;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                    case PC_JOB.HARVEST:
                        return PC_JOB.FARMASIST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                    case PC_JOB.STRIDER:
                        return PC_JOB.RANGER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                    case PC_JOB.ROYALDEALER:
                        return PC_JOB.MERCHANT;
                    default:
                        return PC_JOB.NOVICE;
                }
                /*
                int intJob = (int)this.job;
                if (intJob > 0 && intJob < 120)
                    return (PC_JOB)((intJob / 10) * 10 + 1);
                else
                    return PC_JOB.NOVICE;
                 //*/
            }
        }

        /// <summary>
        /// 该玩家对应的2－1职业
        /// </summary>
        public PC_JOB Job2X
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                    case PC_JOB.GLADIATOR:
                        return PC_JOB.BLADEMASTER;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                    case PC_JOB.GUARDIAN:
                        return PC_JOB.KNIGHT;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                    case PC_JOB.ERASER:
                        return PC_JOB.ASSASSIN;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                    case PC_JOB.HAWKEYE:
                        return PC_JOB.STRIKER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                    case PC_JOB.FORCEMASTER:
                        return PC_JOB.SORCERER;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                    case PC_JOB.ASTRALIST:
                        return PC_JOB.ELEMENTER;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                    case PC_JOB.CARDINAL:
                        return PC_JOB.DRUID;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                    case PC_JOB.SOULTAKER:
                        return PC_JOB.CABALIST;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                    case PC_JOB.MAESTRO:
                        return PC_JOB.BLACKSMITH;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                    case PC_JOB.HARVEST:
                        return PC_JOB.ALCHEMIST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                    case PC_JOB.STRIDER:
                        return PC_JOB.EXPLORER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                    case PC_JOB.ROYALDEALER:
                        return PC_JOB.TRADER;
                    default:
                        return PC_JOB.NONE;
                }
                /*
                int intJob = (int)this.job;
                if (intJob > 0 && intJob < 120)
                    return (PC_JOB)((intJob / 10) * 10 + 3);
                else
                    return PC_JOB.NOVICE;//*/
            }
        }

        /// <summary>
        /// 该玩家对应的2－2职业
        /// </summary>
        public PC_JOB Job2T
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                    case PC_JOB.GLADIATOR:
                        return PC_JOB.BOUNTYHUNTER;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                    case PC_JOB.GUARDIAN:
                        return PC_JOB.DARKSTALKER;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                    case PC_JOB.ERASER:
                        return PC_JOB.COMMAND;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                    case PC_JOB.HAWKEYE:
                        return PC_JOB.GUNNER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                    case PC_JOB.FORCEMASTER:
                        return PC_JOB.SAGE;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                    case PC_JOB.ASTRALIST:
                        return PC_JOB.ENCHANTER;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                    case PC_JOB.CARDINAL:
                        return PC_JOB.BARD;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                    case PC_JOB.SOULTAKER:
                        return PC_JOB.NECROMANCER;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                    case PC_JOB.MAESTRO:
                        return PC_JOB.MACHINERY;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                    case PC_JOB.HARVEST:
                        return PC_JOB.MARIONEST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                    case PC_JOB.STRIDER:
                        return PC_JOB.TREASUREHUNTER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                    case PC_JOB.ROYALDEALER:
                        return PC_JOB.GAMBLER;
                    default:
                        return PC_JOB.NONE;
                }
                /*
                int intJob = (int)this.job;
                if (intJob > 0 && intJob < 120)
                    return (PC_JOB)((intJob / 10) * 10 + 5);
                else
                    return PC_JOB.NOVICE;
                //*/
            }
        }

        /// <summary>
        /// 该玩家对应的3职业
        /// </summary>
        public PC_JOB Job3
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                    case PC_JOB.GLADIATOR:
                        return PC_JOB.GLADIATOR;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                    case PC_JOB.GUARDIAN:
                        return PC_JOB.GUARDIAN;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                    case PC_JOB.ERASER:
                        return PC_JOB.ERASER;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                    case PC_JOB.HAWKEYE:
                        return PC_JOB.HAWKEYE;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                    case PC_JOB.FORCEMASTER:
                        return PC_JOB.FORCEMASTER;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                    case PC_JOB.ASTRALIST:
                        return PC_JOB.ASTRALIST;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                    case PC_JOB.CARDINAL:
                        return PC_JOB.CARDINAL;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                    case PC_JOB.SOULTAKER:
                        return PC_JOB.SOULTAKER;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                    case PC_JOB.MAESTRO:
                        return PC_JOB.MAESTRO;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                    case PC_JOB.HARVEST:
                        return PC_JOB.HARVEST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                    case PC_JOB.STRIDER:
                        return PC_JOB.STRIDER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                    case PC_JOB.ROYALDEALER:
                        return PC_JOB.ROYALDEALER;
                    default:
                        return PC_JOB.NONE;
                }
                /*
                int intJob = (int)this.job;
                if (intJob > 0 && intJob < 120)
                    return (PC_JOB)((intJob / 10) * 10 + 5);
                else
                    return PC_JOB.NOVICE;
                //*/
            }
        }

        /// <summary>
        /// JobType
        /// </summary>
        public JobType JobType
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                    case PC_JOB.GLADIATOR:
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                    case PC_JOB.GUARDIAN:
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                    case PC_JOB.ERASER:
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                    case PC_JOB.HAWKEYE:
                        return JobType.FIGHTER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                    case PC_JOB.FORCEMASTER:
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                    case PC_JOB.ASTRALIST:
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                    case PC_JOB.CARDINAL:
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                    case PC_JOB.SOULTAKER:
                        return JobType.SPELLUSER;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                    case PC_JOB.MAESTRO:
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                    case PC_JOB.HARVEST:
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                    case PC_JOB.STRIDER:
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                    case PC_JOB.ROYALDEALER:
                        return JobType.BACKPACKER;
                    default:
                        return JobType.NOVICE;
                }
                /*
                int intJob = (int)this.job;
                if (intJob > 0 && intJob < 40)
                    return JobType.FIGHTER;
                else if (intJob > 40 && intJob < 80)
                    return JobType.SPELLUSER;
                else if (intJob > 80 && intJob < 120)
                    return JobType.BACKPACKER;
                else
                    return JobType.NOVICE;
                //*/
            }
        }

        /// <summary>
        /// 玩家的Str
        /// </summary>
        public ushort Str
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.dstr;
                    else
                        return this.str;
                }
                return this.str;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.dstr = value;
                    else
                        this.str = value;
                }
                else
                    this.str = value;
            }
        }
        public ushort Dex
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.ddex;
                    else
                        return this.dex;
                }
                return this.dex;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.ddex = value;
                    else
                        this.dex = value;
                }
                else
                    this.dex = value;
            }
        }
        public ushort Int
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.dintel;
                    else
                        return this.intel;
                }
                return this.intel;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.dintel = value;
                    else
                        this.intel = value;
                }
                else
                    this.intel = value;
            }
        }
        public ushort Vit
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.dvit;
                    else
                        return this.vit;
                }
                return this.vit;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.dvit = value;
                    else
                        this.vit = value;
                }
                else
                    this.vit = value;
            }
        }
        public ushort Agi
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.dagi;
                    else
                        return this.agi;
                }
                return this.agi;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.dagi = value;
                    else
                        this.agi = value;
                }
                else
                    this.agi = value;
            }
        }
        public ushort Mag
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.dmag;
                    else
                        return this.mag;
                }
                return this.mag;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.dmag = value;
                    else
                        this.mag = value;
                }
                else
                    this.mag = value;
            }
        }

        public ushort DominionStr { get { return this.dstr; } set { this.dstr = value; } }
        public ushort DominionDex { get { return this.ddex; } set { this.ddex = value; } }
        public ushort DominionInt { get { return this.dintel; } set { this.dintel = value; } }
        public ushort DominionVit { get { return this.dvit; } set { this.dvit = value; } }
        public ushort DominionAgi { get { return this.dagi; } set { this.dagi = value; } }
        public ushort DominionMag { get { return this.dmag; } set { this.dmag = value; } }


        public int Gold
        {
            get { return this.gold; }
            set
            {
                if (value > 99999999)
                    value = 99999999;
                if (value < 0)
                    value = 0;
                if (value - gold != 0)
                    Logger.LogGoldChange(this.Name + "(" + this.charID + ")", (int)(value - gold));
                this.gold = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.GOLD, 0);
            }
        }

        public uint CP
        {
            get { return this.cp; }
            set
            {
                if (value > 99999999)
                    value = 99999999;
                int balance = (int)(value - this.cp);

                this.cp = value;

                if (e != null)
                    e.PropertyUpdate(UpdateEvent.CP, balance);
            }
        }

        public uint ECoin
        {
            get { return this.ecoin; }
            set
            {
                if (value > 99999999)
                    value = 99999999;
                int balance = (int)(value - this.ecoin);

                this.ecoin = value;

                if (e != null)
                    e.PropertyUpdate(UpdateEvent.ECoin, balance);
            }
        }

        /// <summary>
        /// 玩家已消费的EP
        /// </summary>
        public short EPUsed { get { return this.epUsed; } set { this.epUsed = value; } }

        /// <summary>
        /// 玩家在恶魔界已消费的EP
        /// </summary>
        public short DominionEPUsed { get { return this.depUsed; } set { this.depUsed = value; } }

        /// <summary>
        /// DEM族的Cost Limit
        /// </summary>
        public short CL { get { return this.cl; } set { this.cl = value; } }

        /// <summary>
        /// 玩家恶魔界的Cost Limit
        /// </summary>
        public short DominionCL { get { return this.dcl; } set { this.dcl = value; } }

        public ulong CEXP { get { return this.cexp; } set { this.cexp = value; } }
        public ulong JEXP { get { return this.jexp; } set { this.jexp = value; } }


        /// <summary>
        /// 冒险阶级经验值
        /// </summary>
        public ulong ExplorerEXP { get { return this.explorerExp; } set { this.explorerExp = value; } }

        /// <summary>
        /// 恶魔界的基础经验值
        /// </summary>
        public ulong DominionCEXP { get { return this.dcexp; } set { this.dcexp = value; } }

        /// <summary>
        /// 恶魔界的职业经验值
        /// </summary>
        public ulong DominionJEXP { get { return this.djexp; } set { this.djexp = value; } }

        /// <summary>
        /// 联合职业经验值
        /// </summary>
        public ulong JointJEXP { get { return this.jjexp; } set { this.jjexp = value; } }

        /// <summary>
        /// WRP
        /// </summary>
        public int WRP
        {
            get { return this.wrp; }
            set
            {
                int balance = (int)(value - this.wrp);
                this.wrp = value;
                if (this.e != null)
                    this.e.PropertyUpdate(UpdateEvent.WRP, balance);
            }
        }

        /// <summary>
        /// 玩家在线时长
        /// </summary>
        public List<DateTime> TimeOnline { get { return this.timeonline; } set { this.timeonline = value; } }
        /// <summary>
        /// 玩家是否在线
        /// </summary>
        public bool Online { get { return this.online; } set { this.online = value; } }
        /// <summary>
        /// 记录点地图ID
        /// </summary>
        public uint SaveMap { get { return this.save_map; } set { this.save_map = value; } }
        /// <summary>
        /// 记录点X坐标
        /// </summary>
        public byte SaveX { get { return this.save_x; } set { this.save_x = value; } }
        /// <summary>
        /// 记录点Y坐标
        /// </summary>
        public byte SaveY { get { return this.save_y; } set { this.save_y = value; } }

        /// <summary>
        /// DEM形态
        /// </summary>
        public DEM_FORM Form { get { return this.form; } set { this.form = value; } }

        /// <summary>
        /// 玩家是否处于战斗状态
        /// </summary>
        public byte BattleStatus { get { return this.battleStatus; } set { this.battleStatus = value; } }
        /// <summary>
        /// 剩余人物属性点
        /// </summary>
        public ushort StatsPoint
        {
            get
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        return this.dstatspoints;
                    else
                        return this.statspoints;
                }
                return this.statspoints;
            }
            set
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    Map.MapInfo map = Map.MapInfoFactory.Instance.MapInfo[this.MapID];
                    if (map.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                        this.dstatspoints = value;
                    else
                        this.statspoints = value;
                }
                else
                    this.statspoints = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// 恶魔界的人物属性点
        /// </summary>
        public ushort DominionStatsPoint { get { return this.dstatspoints; } set { this.dstatspoints = value; if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0); } }
        /// <summary>
        /// 剩余1转技能点
        /// </summary>
        public ushort SkillPoint { get { return this.skillpoint; } set { this.skillpoint = value; if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0); } }
        /// <summary>
        /// 剩余2－1技能点
        /// </summary>
        public ushort SkillPoint2X { get { return this.skillpoint2x; } set { this.skillpoint2x = value; if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0); } }
        /// <summary>
        /// 剩余2－2技能点
        /// </summary>
        public ushort SkillPoint2T { get { return this.skillpoint2t; } set { this.skillpoint2t = value; if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0); } }
        /// <summary>
        /// 剩余3技能点
        /// </summary>
        public ushort SkillPoint3 { get { return this.skillpoint3; } set { this.skillpoint3 = value; if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0); } }

        /// <summary>
        /// 道具栏
        /// </summary>
        public Inventory Inventory
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        /// <summary>
        /// 动作
        /// </summary>
        public MotionType Motion { get { return this.motion; } set { this.motion = value; } }
        public bool MotionLoop { get { return this.motion_loop; } set { this.motion_loop = value; } }

        /// <summary>
        /// 帐号专有字符串变量集
        /// </summary>
        public VariableHolder<string, string> AStr { get { return this.aStrVar; } }
        /// <summary>
        /// 帐号专有整数变量集
        /// </summary>
        public VariableHolder<string, int> AInt { get { return this.aIntVar; } }
        /// <summary>
        /// 人物专有字符串变量集
        /// </summary>
        public VariableHolder<string, string> CStr { get { return this.cStrVar; } }
        /// <summary>
        /// 人物专有整数变量集
        /// </summary>
        public VariableHolder<string, int> CInt { get { return this.cIntVar; } }
        /// <summary>
        /// 临时字符串变量集
        /// </summary>
        public VariableHolder<string, string> TStr { get { return this.tStrVar; } }
        /// <summary>
        /// 临时整数变量集
        /// </summary>
        public VariableHolder<string, int> TInt { get { return this.tIntVar; } }

        /// <summary>
        /// 临时标识变量集
        /// </summary>
        public VariableHolderA<string, BitMask> TMask { get { return this.tMask; } }

        /// <summary>
        /// 人物专有标识变量集
        /// </summary>
        public VariableHolderA<string, BitMask> CMask { get { return this.cMaskVar; } }

        /// <summary>
        /// 帐号专有标识变量集
        /// </summary>
        public VariableHolderA<string, BitMask> AMask { get { return this.aMaskVar; } }

        /// <summary>
        /// 清楚所有变量集，玩家下线后用于释放资源
        /// </summary>
        public void ClearVarialbes()
        {
            aIntVar = null;
            aStrVar = null;
            aMaskVar = null;
            cIntVar = null;
            cStrVar = null;
            cMaskVar = null;
            tIntVar = null;
            tStrVar = null;
            tMask = null;
        }

        /// <summary>
        /// 玩家目前的变身木偶形态
        /// </summary>
        public Marionette.Marionette Marionette { get { return this.marionette; } set { this.marionette = value; } }
        /// <summary>
        /// 下一次可以使用变身木偶的时间
        /// </summary>
        public DateTime NextMarionetteTime { get { return this.nextMarionetteTime; } set { this.nextMarionetteTime = value; } }

        /// <summary>
        /// 玩家目前放出的宠物
        /// </summary>
        public ActorPet Pet { get { return this.pet; } set { this.pet = value; } }

        /// <summary>
        /// 凭依对象
        /// </summary>
        public uint PossessionTarget { get { return this.possessionTarget; } set { this.possessionTarget = value; } }
        /// <summary>
        /// 凭依位置
        /// </summary>
        public PossessionPosition PossessionPosition { get { return this.possessionPosition; } set { this.possessionPosition = value; } }

        /// <summary>
        /// 目前执行中的任务
        /// </summary>
        public Quest Quest { get { return this.quest; } set { this.quest = value; } }

        /// <summary>
        /// 任务点下次重置的时间
        /// </summary>
        public DateTime QuestNextResetTime { get { return this.questNextTime; } set { this.questNextTime = value; } }

        /// <summary>
        /// EP登陆回复重置时间
        /// </summary>
        public DateTime EPLoginTime { get { return this.epLoginDate; } set { this.epLoginDate = value; } }

        /// <summary>
        /// EP打招呼回复重置时间
        /// </summary>
        public DateTime EPGreetingTime { get { return this.epGreetingDate; } set { this.epGreetingDate = value; } }

        /// <summary>
        /// 声望
        /// </summary>
        public uint Fame
        {
            get
            {
                return this.fame;
            }
            set
            {
                int dif = 0;
                if (value > int.MaxValue)
                    this.fame = 0;
                else
                {
                    dif = (int)(value - this.fame);
                    this.fame = value;
                }
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.Frame, dif);
            }
        }

        /// <summary>
        /// 队伍
        /// </summary>
        public Party.Party Party { get { return this.party; } set { this.party = value; } }

        /// <summary>
        /// 军团
        /// </summary>
        public Ring.Ring Ring { get { return this.ring; } set { this.ring = value; } }

        /// <summary>
        /// 看板
        /// </summary>
        public string Sign { get { return this.sign; } set { this.sign = value; } }

        /// <summary>
        /// 玩家的模式
        /// </summary>
        public PlayerMode Mode { get { return this.mode; } set { this.mode = value; if (e != null) e.PropertyUpdate(UpdateEvent.MODE, 0); } }

        /// <summary>
        /// 玩家的飞空庭
        /// </summary>
        public FGarden.FGarden FGarden { get { return this.fgarden; } set { this.fgarden = value; } }

        /// <summary>
        /// 帐篷Actor
        /// </summary>
        public ActorEvent TenkActor { get { return this.tentActor; } set { this.tentActor = value; } }


        /// <summary>
        /// 玩家在虚拟商城的点券
        /// </summary>
        public uint VShopPoints
        {
            get
            {
                if (e != null)
                    e.PropertyRead(UpdateEvent.VCASH_POINT);
                return this.vpoints;
            }
            set
            {
                this.vpoints = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.VCASH_POINT, 0);
            }
        }

        /// <summary>
        /// 玩家已用掉的点券
        /// </summary>
        public uint UsedVShopPoints
        {
            get
            {
                if (e != null)
                    e.PropertyRead(UpdateEvent.VCASH_POINT);
                return this.usedVPoints;
            }
            set
            {
                this.usedVPoints = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.VCASH_POINT, 0);
            }
        }

        /// <summary>
        /// 玩家目前活动中的石像
        /// </summary>
        public ActorGolem Golem { get { return this.golem; } set { this.golem = value; } }

        /// <summary>
        /// 玩家创建的DungeonID
        /// </summary>
        public uint DungeonID { get { return this.dungeonID; } set { this.dungeonID = value; } }

        /// <summary>
        /// 玩家收集的印章
        /// </summary>
        public Stamp Stamp { get { return this.stamp; } }

        /// <summary>
        /// 是否解放恶魔界保留技能列表
        /// </summary>
        public bool DominionReserveSkill { get { return this.dreseve; } set { this.dreseve = value; } }

        /// <summary>
        /// WRP排行
        /// </summary>
        public uint WRPRanking { get { return this.wrpRanking; } set { this.wrpRanking = value; } }

        /// <summary>
        /// 變身的圖片ID
        /// </summary>
        public uint TranceID { get { return this.tranceID; } set { this.tranceID = value; } }

        /// <summary>
        /// NPC显示/隐藏状态
        /// </summary>
        public Dictionary<uint, Dictionary<uint, bool>> NPCStates { get { return this.npcStates; } }

    }
}
