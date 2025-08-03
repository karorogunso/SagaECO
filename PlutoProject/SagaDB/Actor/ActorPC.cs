using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Iris;
using SagaDB.Marionette;
using SagaDB.Quests;
using SagaDB.Party;
using SagaDB.DEMIC;
using SagaDB.Navi;
using SagaDB.Tamaire;
using SagaDB.DualJob;

namespace SagaDB.Actor
{
    [Serializable]
    #region 商人商店部分..
    public class PlayerShopItem
    {
        uint inventoryID;
        uint itemID;
        ushort count;
        ulong price;

        public uint InventoryID { get { return this.inventoryID; } set { this.inventoryID = value; } }
        public uint ItemID { get { return this.itemID; } set { this.itemID = value; } }
        public ushort Count { get { return this.count; } set { this.count = value; } }
        public ulong Price { get { return this.price; } set { this.price = value; } }
    }
    #endregion

    #region 幻化外观部分..
    public class Appearance
    {
        //为none时将维持原样
        private PC_RACE race = PC_RACE.NONE;
        private PC_GENDER gender = PC_GENDER.NONE;
        private DEM_FORM form = DEM_FORM.NONE;

        //-1时不改变
        private byte tailStyle = byte.MaxValue;//尾巴形狀
        private byte wingStyle = byte.MaxValue;//翅膀形狀
        private byte wingColor = byte.MaxValue;//翅膀顏色

        //其他都是0时不变
        private ushort hairStyle = 0;
        private byte hairColor = 0;
        private ushort wig = 0;
        private ushort face = 0;


        private uint marionettePictID = 0; //只存储目标的木偶外观，不存储其他无用的木偶性能信息

        //null时隐藏（设置为0），不存在时跳过
        Dictionary<EnumEquipSlot, Item.Item> equips = new Dictionary<EnumEquipSlot, Item.Item>();

        public PC_RACE Race { get { return race; } set { race = value; } }
        public PC_GENDER Gender { get { return gender; } set { gender = value; } }
        public DEM_FORM Form { get { return form; }set { form = value; } }
        public byte WingStyle
        {
            get { return wingStyle; } set { wingStyle = value; } }
        public byte TailStyle
        {
            get { return tailStyle; }
            set { tailStyle = value; }
        }
        public byte WingColor
        {
            get { return wingColor; }
            set { wingColor = value; }
        }
        public ushort HairStyle
        {
            get { return hairStyle; }
            set { hairStyle = value; }
        }
        public byte HairColor
        {
            get { return hairColor; }
            set { hairColor = value; }
        }
        public ushort Wig
        {
            get { return wig; }
            set { wig = value; }
        }
        public ushort Face
        {
            get { return face; }
            set { face = value; }
        }
        public uint MarionettePictID
        {
            get { return marionettePictID; }
            set { marionettePictID = value; }
        }
        public Dictionary<EnumEquipSlot, Item.Item> Equips
        {
            get { return equips; }
            set { equips = value; }
        }

        public bool Illusion()
        {
            bool result = true;
            result &= (race == PC_RACE.NONE) && (gender == PC_GENDER.NONE) && (form == DEM_FORM.NONE);
            result &= (tailStyle == byte.MaxValue) && (wingStyle == byte.MaxValue) && (wingColor == byte.MaxValue);
            result &= (hairStyle == 0) && (hairColor == 0) && (wig == 0) && (face == 0);
            result &= marionettePictID == 0;
            result &= equips.Count == 0;
            return !result;
        }
    }
    #endregion

    /// <summary>
    /// 副职基本信息
    /// </summary>
    [Serializable]
    public class PlayerDualJobInfo
    {
        public byte DualJobID = 0;
        public byte DualJobLevel = 0;
        public ulong DualJobExp = 0;
    }

    public enum PlayerUsingShopType
    {
        None,
        GShop,
        NCShop
    }

    public class ActorPC : Actor, IStats
    {
        public uint JobLV_GLADIATOR;
        public uint JobLV_HAWKEYE;
        public uint JobLV_FORCEMASTER;
        public uint JobLV_CARDINAL;

        public List<BBS.Mail> Mails = new List<BBS.Mail>();
        public List<BBS.Gift> Gifts = new List<BBS.Gift>();

        //副职技能列表
        public List<Skill.Skill> DualJobSkill = new List<Skill.Skill>();
        //当前副职ID
        public byte DualJobID = 0;
        //当前副职等级
        public byte DualJobLevel = 0;
        //当前玩家可用副职清单
        public Dictionary<byte, PlayerDualJobInfo> PlayerDualJobList = new Dictionary<byte, PlayerDualJobInfo>();
        public uint NowUseFurnitureID = 0;
        //当前玩家旅人目錄
        public Dictionary<uint, bool> MosterGuide = new Dictionary<uint, bool>();


        public PlayerUsingShopType UsingShopType = PlayerUsingShopType.None;

        uint charID;
        [NonSerialized]
        Account account;
        PC_RACE race;
        PC_GENDER gender;
        byte tailStyle;//尾巴形狀
        byte wingStyle;//翅膀形狀
        byte wingColor;//翅膀顏色

        uint furnitureID;

        public Appearance appearance; //幻化目标外观

        ushort hairStyle;
        byte hairColor;
        ushort wig;
        ushort face;
        PC_JOB job;
        PC_JOB jointJob = PC_JOB.NONE;
        byte lv, dlv, djlv, jjlv;
        byte jlv1;
        byte jlv2x;
        byte jlv2t;

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

        public bool canTrade = true, canParty = true, canPossession = true, canRing = true, showRevive = true, canWork = true, canMentor = true, showEquipment = true, canChangePartnerDisplay = true, canFriend = true;

        /// <summary>
        /// 玩家当前目标，供搭档使用。
        /// </summary>
        public Actor PartnerTartget;

        ushort str, dex, intel, vit, agi, mag, dstr, ddex, dintel, dvit, dagi, dmag;
        ushort statspoints, skillpoint, skillpoint2x, skillpoint2t, dstatspoints;
        ushort exstatpoint;
        byte exskillpoint;

        bool dreseve;
        uint wrpRanking;
        Marionette.Marionette marionette;
        DateTime nextMarionetteTime = DateTime.Now;

        ActorPet pet;
        ActorPartner partner;
        Quest quest;
        DateTime questNextTime, epLoginDate, epGreetingDate;

        //Iris system
        Dictionary<AbilityVector, int> irisabilityvalues = new Dictionary<AbilityVector, int>();
        Dictionary<AbilityVector, int> irisabilitylevels = new Dictionary<AbilityVector, int>();
        /// <summary>
        /// PC's Iris Ability Value Points
        /// </summary>
        public Dictionary<AbilityVector, int> IrisAbilityValues
        {
            get
            {
                return irisabilityvalues;
            }
        }
        /// <summary>
        /// PC's Iris Ability Value Levels
        /// </summary>
        public Dictionary<AbilityVector, int> IrisAbilityLevels
        {
            get
            {
                return irisabilitylevels;
            }
        }
        /// <summary>
        /// Reset PC's Iris Abilities
        /// </summary>
        public void ClearIrisAbilities()
        {
            this.IrisAbilityValues.Clear();
            this.IrisAbilityLevels.Clear();
        }

        /// <summary>
        /// 站姿
        /// </summary>
        public byte WaitType
        {
            get
            {
                return (byte)CInt["WaitType"];
            }
            set
            {
                CInt["WaitType"] = value;
            }
        }

        public bool Fictitious;
        public uint LastAttackActorID;
        public bool AutoAttack = false;

        public short MaxHealMpForWeapon;

        /// <summary>
        /// 凭依在右手的宠物道具位置
        /// </summary>
        public uint PossessionPartnerSlotIDinRightHand;
        /// <summary>
        /// 凭依在左手的宠物道具位置
        /// </summary>
        public uint PossessionPartnerSlotIDinLeftHand;
        /// <summary>
        /// 凭依在衣服的宠物道具位置
        /// </summary>
        public uint PossessionPartnerSlotIDinClothes;
        /// <summary>
        /// 凭依在项链的宠物道具位置
        /// </summary>
        public uint PossessionPartnerSlotIDinAccesory;

        //Navi.Navi navi = new Navi.Navi(NaviFactory.Instance.Navi);

        public uint[] equips;
        /// <summary>
        /// 伪造ACTOR用装备栏
        /// </summary>
        public uint[] Equips { get { return this.equips; } set { this.equips = value; } }
        public class KillInfo
        {
            public bool isFinish = false;
            public int Count { set; get; }
            public int TotalCount { set; get; }
        }
        /// <summary>
        /// 拼图装备相关
        /// </summary>
        public Dictionary<uint, AnotherDetail> AnotherPapers = new Dictionary<uint, AnotherDetail>();

        /// <summary>
        /// 正在使用的拼图ID
        /// </summary>
        public ushort UsingPaperID { set; get; }

        /// <summary>
        /// 正在使用的称号ID
        /// </summary>
        public ushort PlayerTitleID { set; get; }

        /// <summary>
        /// 玩家的称号名
        /// </summary>
        public string PlayerTitle = "";
        /// <summary>
        /// 称号的前缀
        /// </summary>
        public string FirstName = "";

        /// <summary>
        /// 显示前缀开关
        /// </summary>
        public byte ShowFirstName = 0;

        /// <summary>
        /// 击杀列表
        /// </summary>
        public Dictionary<uint, KillInfo> KillList = new Dictionary<uint, KillInfo>();
        /// <summary>
        /// 任务标记 (byte为列表ID)
        /// </summary>
        //public Navi.Navi Navi { get { return this.navi; } set { this.navi = value; } }
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
        public Dictionary<uint, NPCHide> NpcShowList = new Dictionary<uint, NPCHide>();
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

        ushort skillpoint3;

        /// <summary>
        /// 剩余3技能点
        /// </summary>
        public ushort SkillPoint3 { get { return this.skillpoint3; } set { this.skillpoint3 = value; if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0); } }

        byte jlv3;
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

        int wrp;
        long gold;
        byte slot;

        byte battleStatus;
        uint save_map;
        byte save_x;
        byte save_y;

        //Fish bait
        uint equipedbaitid;

        Inventory inventory;
        

        VariableHolder<string, string> aStrVar = new VariableHolder<string, string>("");
        VariableHolder<string, int> aIntVar = new VariableHolder<string, int>(0);
        VariableHolder<string, long> aLongVar = new VariableHolder<string, long>(0);
        VariableHolderA<string, BitMask> aMaskVar = new VariableHolderA<string, BitMask>();
        VariableHolder<string, string> cStrVar = new VariableHolder<string, string>("");
        VariableHolderA<string, BitMask> cMaskVar = new VariableHolderA<string, BitMask>();
        VariableHolder<string, int> cIntVar = new VariableHolder<string, int>(0);
        VariableHolder<string, string> tStrVar = new VariableHolder<string, string>("");
        VariableHolder<string, int> tIntVar = new VariableHolder<string, int>(0);
        VariableHolderA<string, BitMask> tMask = new VariableHolderA<string, BitMask>();
        VariableHolderA<string, DateTime> tTimeVar = new VariableHolderA<string, DateTime>();

        VariableHolderA<string, VariableHolderA<string, int>> aDicVar = new VariableHolderA<string, VariableHolderA<string, int>>();

        VariableHolderA<string, VariableHolderA<int, int>> cIntDicVar = new VariableHolderA<string, VariableHolderA<int, int>>();
        uint possessionTarget;
        PossessionPosition possessionPosition;

        Party.Party party;
        Ring.Ring ring;
        Team.Team team;
        Group.Group group;
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
        /// 當前已裝備的魚餌
        /// </summary>
        public uint EquipedBaitID { get { return this.equipedbaitid; } set { this.equipedbaitid = value; } }

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
            this.Speed = 410;
            this.inventory = new Inventory(this);
            appearance = new Appearance();
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
        /// 假发
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
                if (this.DualJobID != 0)
                    return this.PlayerDualJobList[DualJobID].DualJobLevel;
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
                return (ushort)AInt["剩余任务点数"];
            }
            set
            {
                AInt["剩余任务点数"] = value;
                /*if (e != null)
                    e.PropertyUpdate(UpdateEvent.QUEST_POINT, 0);*/
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
        /*
        /// <summary>
        /// 物理熟练度等级
        /// </summary>
        public byte DefLv
        {
            get
            {
                return deflv;
            }
            set
            {
                deflv = value;
            }
        }
        /// <summary>
        /// 魔法熟练度等级
        /// </summary>
        public byte MDefLv
        {
            get
            {
                return mdeflv;
            }
            set
            {
                mdeflv = value;
            }
        }
        /// <summary>
        /// 物理熟练度点数
        /// </summary>
        public uint DefPoint
        {
            get
            {
                return defpoint;
            }
            set
            {
                defpoint = value;
            }
        }
        /// <summary>
        /// 物理熟练度点数
        /// </summary>
        public uint MDefPoint
        {
            get
            {
                return mdefpoint;
            }
            set
            {
                mdefpoint = value;
            }
        }*/
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
                        return PC_JOB.NOVICE;
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
                        return PC_JOB.NOVICE;
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
                        return PC_JOB.JOKER;
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
                return this.str;
            }
            set
            {
                this.str = value;
            }
        }
        public ushort Dex
        {
            get
            {
                return this.dex;
            }
            set
            {
                this.dex = value;
            }
        }
        public ushort Int
        {
            get
            {
                return this.intel;
            }
            set
            {
                this.intel = value;
            }
        }
        public ushort Vit
        {
            get
            {
                return this.vit;
            }
            set
            {
                this.vit = value;
            }
        }
        public ushort Agi
        {
            get
            {
                return this.agi;
            }
            set
            {
                this.agi = value;
            }
        }
        public ushort Mag
        {
            get
            {
                return this.mag;
            }
            set
            {
                this.mag = value;
            }
        }

        public ushort DominionStr { get { return this.dstr; } set { this.dstr = value; } }
        public ushort DominionDex { get { return this.ddex; } set { this.ddex = value; } }
        public ushort DominionInt { get { return this.dintel; } set { this.dintel = value; } }
        public ushort DominionVit { get { return this.dvit; } set { this.dvit = value; } }
        public ushort DominionAgi { get { return this.dagi; } set { this.dagi = value; } }
        public ushort DominionMag { get { return this.dmag; } set { this.dmag = value; } }

        DateTime GoldLine = DateTime.Now;
        int Goldlimit = 0;
        public long Gold
        {
            get { return this.gold; }
            set
            {
                if (value > 999999999999)
                    value = 999999999999;
                if (value < 0)
                    value = 0;
                if (value - gold != 0)
                {
                    if (gold != 0)
                    {
                        Logger.LogGoldChange(this.Name + "(" + this.charID + ")", (int)(value - gold));
                        if (value > 100000)
                            Logger.LogGoldChange("[金钱异常收入！]:" + Name + "(" + charID + ")", (int)(value - gold));
                    }
                    if (GoldLine + new TimeSpan(0, 0, 15, 0) > CPLine)
                        Goldlimit = 0;
                    Goldlimit += (int)(value - gold);
                    if (Goldlimit > 500000)
                        Logger.LogGoldChange("[Gold15分钟内收入过多警告！]:" + Name + "(" + charID + ")", Goldlimit);
                    GoldLine = DateTime.Now;
                }
                int gg = (int)(value - gold);
                if (gg > 0)
                    CInt[Name + "角色收入"] += gg;
                else
                    CInt[Name + "角色支出"] += -gg;
                gold = value;
                if (e != null)
                    e.PropertyUpdate(UpdateEvent.GOLD, 0);
            }
        }
        DateTime CPLine = DateTime.Now;
        int CPlimit = 0;
        public uint CP
        {
            get { return this.cp; }
            set
            {
                if (value > 99999999)
                    value = 99999999;
                int balance = (int)(value - cp);
                if (value - cp != 0)
                {
                    if (cp != 0)
                    {
                        Logger.LogGoldChange("[CP]:" + Name + "(" + charID + ")", balance);
                        if (value > 5000)
                            Logger.LogGoldChange("[CP异常收入！]:" + Name + "(" + charID + ")", balance);
                    }
                    if (CPLine + new TimeSpan(0, 0, 15, 0) > CPLine)
                        CPlimit = 0;
                    CPlimit += (int)balance;
                    if (CPlimit > 30000)
                        Logger.LogGoldChange("[CP15分钟内收入过多警告！]:" + Name + "(" + charID + ")", CPlimit);
                    CPLine = DateTime.Now;
                }
                cp = value;

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
                return (ushort)(statspoints);
            }
            set
            {
                this.statspoints = value;
                if (e != null) e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// EX属性点
        /// </summary>
        public ushort EXStatPoint { get { return this.exstatpoint; } set { this.exstatpoint = value; } }

        /// <summary>
        /// EX技能点
        /// </summary>
        public byte EXSkillPoint { get { return this.exskillpoint; } set { this.exskillpoint = value; } }
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
        public byte EMotion;
        public bool EMotionLoop;
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
        /// 帐号专有长整数变量集
        /// </summary>
        public VariableHolder<string, long> ALong { get { return this.aLongVar; } }
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
        //public VariableHolder<string, string> TStr { get { return this.tStrVar; } }
        /// <summary>
        /// 临时整数变量集
        /// </summary>
        //public VariableHolder<string, int> TInt { get { return this.tIntVar; } }

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

        public VariableHolderA<string, DateTime> TTime { get { return this.tTimeVar; } }

        public VariableHolderA<string, VariableHolderA<string, int>> Adict { get { return this.aDicVar; } }

        /// <summary>
        /// 人物专有双Int字典变量集
        /// </summary>
        public VariableHolderA<string, VariableHolderA<int, int>> CIDict { get { return this.cIntDicVar; } }
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

        //public EmotionType Emotion { get { return this.emotion; } set { this.emotion = value; } }

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
        /// 玩家目前放出的partner
        /// </summary>
        public ActorPartner Partner { get { return this.partner; } set { this.partner = value; } }

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
        public uint Fame { get { return this.fame; } set { if (value > int.MaxValue) this.fame = 0; else this.fame = value; } }

        /// <summary>
        /// 队伍
        /// </summary>
        public Party.Party Party { get { return this.party; } set { this.party = value; } }

        /// <summary>
        /// 军团
        /// </summary>
        public Ring.Ring Ring { get { return this.ring; } set { this.ring = value; } }

        /// <summary>
        /// 團隊
        /// </summary>
        public Team.Team Team { get { return this.team; } set { this.team = value; } }

        /// <summary>
        /// 小組
        /// </summary>
        public Group.Group Group { get { return this.group; } set { this.group = value; } }

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
        //public Dictionary<uint, Dictionary<uint, bool>> NPCStates { get { return this.npcStates; } }
        Dictionary<uint,  bool> npcStates = new Dictionary<uint, bool>();
        public Dictionary<uint,bool> NPCStates { get { return this.npcStates; } }


        public uint FurnitureID { get { return this.furnitureID; } set { this.furnitureID = value; } }
        public uint FurnitureID_old { get; set; }

        /// <summary>
        /// 防御战窗口是否可见
        /// </summary>
        public bool DefWarShow
        {
            get { return this.TInt["DefWarShow"] != 0; }
            set
            {
                if (value)
                    this.TInt["DefWarShow"] = 1;
                else
                    this.TInt["DefWarShow"] = 0;
            }
        }

        /// <summary>
        /// 玩家选择的副本难度
        /// </summary>
        public byte DungeonsDifc;
        /// <summary>
        /// 玩家在副本的死亡次数限制
        /// </summary>
        public byte DungeonsReviveCount;

        TamaireLending tamaireLending;
        /// <summary>
        /// 玩家借出的"心"
        /// </summary>
        public TamaireLending TamaireLending { get { return this.tamaireLending; } set { this.tamaireLending = value; } }

        TamaireRental tamaireRental;
        /// <summary>
        /// 玩家租用的"心"
        /// </summary>
        public TamaireRental TamaireRental { get { return this.tamaireRental; } set { this.tamaireRental = value; } }

        int abyssfloor;
        /// <summary>
        /// 玩家儲存的奈落階層
        /// </summary>
        public int AbyssFloor { get { return this.abyssfloor; } set { this.abyssfloor = value; } }

        uint master=0;
        /// <summary>
        /// 玩家的師父
        /// </summary>
        public uint Master { get { return this.master; } set { this.master = value; } }

        List<uint> pupilins = new List<uint>();
        /// <summary>
        /// 玩家的徒弟
        /// </summary>
        public List<uint> Pupilins{get{ return this.pupilins;} set { this.pupilins = value; } }

        byte pupilinlimit = 1;
        /// <summary>
        /// 玩家的徒弟上限
        /// </summary>
        public byte PupilinLimit { get { return this.pupilinlimit; } set { this.pupilinlimit = value; } }
    }
}
