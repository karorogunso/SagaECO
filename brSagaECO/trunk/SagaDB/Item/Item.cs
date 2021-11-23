using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Iris;
using System.IO;

namespace SagaDB.Item
{
    [Serializable]
    public class Item
    {
        public class ItemData
        {
            public string name, desc;
            public uint id, price;
            public uint iconID;
            public uint imageID;
            public ushort equipVolume, possessionWeight, weight, volume;
            public ItemType itemType;
            public uint repairItem, enhancementItem;
            public uint events;
            public bool receipt, dye, stock, doubleHand, usable;
            public byte color;
            public ushort durability;
            public PC_JOB jointJob;
            public uint eventID, effectID;
            public ushort activateSkill, possibleSkill, passiveSkill, possessionSkill, possessionPassiveSkill;
            public TargetType target;
            public ActiveType activeType;
            public byte range;
            public uint duration;
            public byte effectRange;
            public bool isRate;
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
            public bool possibleRebirth;
            public ushort possibleStr, possibleDex, possibleInt, possibleVit, possibleAgi, possibleMag, possibleLuk, possibleCha;
            public Dictionary<PC_JOB, bool> possibleJob = new Dictionary<PC_JOB, bool>();
            public Dictionary<Country, bool> possibleCountry = new Dictionary<Country, bool>();
            public uint marionetteID, petID;

            public byte currentSlot, maxSlot;

            public byte handMotion, handMotion2;
            public bool noTrade;
            public override string ToString()
            {
                return this.name;
            }
        }
        uint db_id, id;
        ushort stack;
        ushort durability;
        public byte identified;
        bool locked;
        bool changeMode, changeMode2;
        private uint slot;

        bool masterenhance = false;
        byte lifeenhance = 0;
        byte powerenhance = 0;
        byte critenhance = 0;
        byte magenhance = 0;

        ushort refine;

        short hp, mp, sp, weightUp, volumeUp, speedUp;
        short str, dex, intel, vit, agi, mag, luk, cha;
        short atk1, atk2, atk3, matk, def, mdef;
        short hitMelee, hitRanged, hitMagic;
        short avoidMelee, avoidRanged, avoidMagic;
        short hitCritical, avoidCritical;
        short hpRecover, mpRecover;
        short aspd, cspd;
        uint pict_id;
        bool rental;
        DateTime rentalTime = DateTime.Now;

        byte currentSlot, maxSlot;
 
        List<Iris.IrisCard> cards = new List<SagaDB.Iris.IrisCard>();

        /// <summary>
        /// 道具版本
        /// </summary>
        public ushort Version = 5;

        [NonSerialized]
        Actor.ActorPC possessionedActor;
        [NonSerialized]
        Actor.ActorPC possessionOwner;

        /// <summary>
        /// 是否潜在强化
        /// </summary>
        public bool MasterEnhance { get { return this.masterenhance; } set { this.masterenhance = value; } }

        /// <summary>
        /// 生命强化次数
        /// </summary>
        public byte LifeEnhance { get { return this.lifeenhance; } set { this.lifeenhance = value; } }
        /// <summary>
        /// 力量强化次数
        /// </summary>
        public byte PowerEnhance { get { return this.powerenhance; } set { this.powerenhance = value; } }
        /// <summary>
        /// 致命强化次数
        /// </summary>
        public byte CritEnhance { get { return this.critenhance; } set { this.critenhance = value; } }
        /// <summary>
        /// 魔力强化次数
        /// </summary>
        public byte MagEnhance { get { return this.magenhance; } set { this.magenhance = value; } }
        /// <summary>
        /// 装备提升的HP
        /// </summary>
        public short HP { get { return this.hp; } set { this.hp = value; } }
        /// <summary>
        /// 装备提升的MP
        /// </summary>
        public short MP { get { return this.mp; } set { this.mp = value; } }
        /// <summary>
        /// 装备提升的SP
        /// </summary>
        public short SP { get { return this.sp; } set { this.sp = value; } }
        /// <summary>
        /// 装备提升的负重
        /// </summary>
        public short WeightUp { get { return this.weightUp; } set { this.weightUp = value; } }
        /// <summary>
        /// 装备提升的体积
        /// </summary>
        public short VolumeUp { get { return this.volumeUp; } set { this.volumeUp = value; } }
        /// <summary>
        /// 装备提升的速度
        /// </summary>
        public short SpeedUp { get { return this.speedUp; } set { this.speedUp = value; } }
        /// <summary>
        /// 装备提升的Str
        /// </summary>
        public short Str { get { return this.str; } set { this.str = value; } }
        /// <summary>
        /// 装备提升的Dex
        /// </summary>
        public short Dex { get { return this.dex; } set { this.dex = value; } }
        /// <summary>
        /// 装备提升的Int
        /// </summary>
        public short Int { get { return this.intel; } set { this.intel = value; } }
        /// <summary>
        /// 装备提升的Vit
        /// </summary>
        public short Vit { get { return this.vit; } set { this.vit = value; } }
        /// <summary>
        /// 装备提升的Agi
        /// </summary>
        public short Agi { get { return this.agi; } set { this.agi = value; } }
        /// <summary>
        /// 装备提升的Mag
        /// </summary>
        public short Mag { get { return this.mag; } set { this.mag = value; } }
        /// <summary>
        /// 装备提升的Atk1
        /// </summary>
        public short Atk1 { get { return this.atk1; } set { this.atk1 = value; } }
        /// <summary>
        /// 装备提升的Atk2
        /// </summary>
        public short Atk2 { get { return this.atk2; } set { this.atk2 = value; } }
        /// <summary>
        /// 装备提升的Atk3
        /// </summary>
        public short Atk3 { get { return this.atk3; } set { this.atk3 = value; } }
        /// <summary>
        /// 装备提升的MAtk
        /// </summary>
        public short MAtk { get { return this.matk; } set { this.matk = value; } }
        /// <summary>
        /// 装备提升的Def
        /// </summary>
        public short Def { get { return this.def; } set { this.def = value; } }
        /// <summary>
        /// 装备提升的MDef
        /// </summary>
        public short MDef { get { return this.mdef; } set { this.mdef = value; } }
        /// <summary>
        /// 装备提升的HitMelee
        /// </summary>
        public short HitMelee { get { return this.hitMelee; } set { this.hitMelee = value; } }
        /// <summary>
        /// 装备提升的HitMagic
        /// </summary>
        public short HitMagic { get { return this.hitMagic; } set { this.hitMagic = value; } }
        /// <summary>
        /// 装备提升的HitRanged
        /// </summary>
        public short HitRanged { get { return this.hitRanged; } set { this.hitRanged = value; } }
        /// <summary>
        /// 装备提升的HitMelee
        /// </summary>
        public short AvoidMelee { get { return this.avoidMelee; } set { this.avoidMelee = value; } }
        /// <summary>
        /// 装备提升的AvoidMagic
        /// </summary>
        public short AvoidMagic { get { return this.avoidMagic; } set { this.avoidMagic = value; } }
        /// <summary>
        /// 装备提升的AvoidRanged
        /// </summary>
        public short AvoidRanged { get { return this.avoidRanged; } set { this.avoidRanged = value; } }
        /// <summary>
        /// 装备提升的HitCritical
        /// </summary>
        public short HitCritical { get { return this.hitCritical; } set { this.hitCritical = value; } }
        /// <summary>
        /// 装备提升的HitMagic
        /// </summary>
        public short AvoidCritical { get { return this.avoidCritical; } set { this.avoidCritical = value; } }
        /// <summary>
        /// 装备提升的HPRecover
        /// </summary>
        public short HPRecover { get { return this.hpRecover; } set { this.hpRecover = value; } }
        /// <summary>
        /// 装备提升的SPRecover
        /// </summary>
        public short MPRecover { get { return this.mpRecover; } set { this.mpRecover = value; } }

        /// <summary>
        /// 装备提升的ASPD
        /// </summary>
        public short ASPD { get { return this.aspd; } set { this.aspd = value; } }

        /// <summary>
        /// 装备提升的CSPD
        /// </summary>
        public short CSPD { get { return this.cspd; } set { this.cspd = value; } }

        /// <summary>
        /// 是否是出租道具
        /// </summary>
        public bool Rental { get { return this.rental; } set { this.rental = value; } }

        /// <summary>
        /// 出租道具到期时间
        /// </summary>
        public DateTime RentalTime { get { return this.rentalTime; } set { this.rentalTime = value; } }

        public uint PictID { get { return this.pict_id; } set { this.pict_id = value; } }

        public uint ItemID { get { return this.id; } }
        public uint DBID { get { return this.db_id; } set { this.db_id = value; } }
        public ushort maxDurability { get { return this.BaseData.durability; } }
        public uint Slot { get { return this.slot; } set { this.slot = value; } }

        public ItemData BaseData
        {
            get
            {
                ItemData baseData = null;
                if (baseData == null)
                {
                    if (ItemFactory.Instance.Items.ContainsKey(this.id))
                        baseData = ItemFactory.Instance.Items[this.id];
                    else
                        baseData = new ItemData();
                }
                return baseData;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public ushort Stack { get { return this.stack; } set { this.stack = value; } }
        /// <summary>
        /// 持久
        /// </summary>
        public ushort Durability { get { return this.durability; } set { this.durability = value; } }
        
        /// <summary>
        /// 凭依在此道具上的Actor
        /// </summary>
        public Actor.ActorPC PossessionedActor { get { return this.possessionedActor; } set { this.possessionedActor = value; } }
        /// <summary>
        /// 此道具被凭依前的主人
        /// </summary>
        public Actor.ActorPC PossessionOwner { get { return this.possessionOwner; } set { this.possessionOwner = value; } }

        /// <summary>
        /// 装备当前插槽
        /// </summary>
        public byte CurrentSlot
        {
            get
            {
                if (this.currentSlot == 0 && this.currentSlot != this.BaseData.currentSlot)
                    this.currentSlot = this.BaseData.currentSlot;
                return this.currentSlot;
            }
            set { this.currentSlot = value; }
        }

        /// <summary>
        /// 装备最大插槽
        /// </summary>
        public byte MaxSlot { 
            get {
                if (this.maxSlot == 0 && this.maxSlot != this.BaseData.maxSlot)
                    this.maxSlot = this.BaseData.maxSlot; 
                return this.maxSlot;
            }
            set { this.maxSlot = value; }
        }

        /// <summary>
        /// 已经插入的卡
        /// </summary>
        public List<Iris.IrisCard> Cards
        {
            get
            {
                if (cards == null)
                    cards = new List<IrisCard>();
                return cards;
            }
        }

        /// <summary>
        /// 装备强化次数
        /// </summary>
        public ushort Refine { get { return this.refine; } set { this.refine = value; } }

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

        public bool Locked
        {
            get
            {
                return this.locked;
            }
            set
            {
                this.locked = value;
            }
        }
        public bool ChangeMode
        {
            get
            {
                return this.changeMode;
            }
            set
            {
                this.changeMode = value;
            }
        }
        public bool ChangeMode2
        {
            get
            {
                return this.changeMode2;
            }
            set
            {
                this.changeMode2 = value;
            }
        }
        public Item()
        {

        }

        public Item(ItemData baseData)
        {
            this.id = baseData.id;
           
        }

        public Item(Stream InputStream)
        {
            FromStream(InputStream);
        }

        public void ToStream(System.IO.Stream ms)
        {
            BinaryWriter bw = new BinaryWriter(ms);
            //Version
            bw.Write(Version);
            
            //Version 1
            bw.Write(this.id);
            bw.Write(this.durability);
            bw.Write(this.stack);
            bw.Write(this.identified);
            bw.Write(this.agi);
            bw.Write(this.atk1);
            bw.Write(this.atk2);
            bw.Write(this.atk3);
            bw.Write(this.avoidCritical);
            bw.Write(this.avoidMagic);
            bw.Write(this.avoidMelee);
            bw.Write(this.avoidRanged);
            bw.Write(this.cha);
            bw.Write(this.def);
            bw.Write(this.dex);
            bw.Write(this.hitCritical);
            bw.Write(this.hitMagic);
            bw.Write(this.hitMelee);
            bw.Write(this.hitRanged);
            bw.Write(this.hp);
            bw.Write(this.hpRecover);
            bw.Write(this.intel);
            bw.Write(this.luk);
            bw.Write(this.mag);
            bw.Write(this.matk);
            bw.Write(this.mdef);
            bw.Write(this.mp);
            bw.Write(this.mpRecover);
            bw.Write(this.sp);
            bw.Write(this.speedUp);
            bw.Write(this.str);
            bw.Write(this.vit);
            bw.Write(this.volumeUp);
            bw.Write(this.weightUp);
            bw.Write(this.aspd);
            bw.Write(this.cspd);
            bw.Write(this.refine);
            bw.Write(this.pict_id);
            bw.Write(this.slot);

            //Version 2
            bw.Write(this.currentSlot);
            if (cards == null)
                cards = new List<IrisCard>();
            bw.Write((byte)this.cards.Count);
            foreach (Iris.IrisCard i in this.cards)
            {
                bw.Write(i.ID);
            }
            bw.Write(this.locked);

            //Version 3
            bw.Write(this.rental);
            bw.Write(this.rentalTime.ToBinary());

            //Version 4 
            bw.Write(this.changeMode);
            bw.Write(this.changeMode2);

            //Version 5
            bw.Write(this.MasterEnhance);
            bw.Write(this.LifeEnhance);
            bw.Write(this.PowerEnhance);
            bw.Write(this.CritEnhance);
            bw.Write(this.MagEnhance);
        }

        public void FromStream(Stream InputStream)
        {
            try
            {
                BinaryReader br = new BinaryReader(InputStream);
                ushort item_version = br.ReadUInt16();
                if (item_version >= 1)
                {
                    this.id = br.ReadUInt32();
                    this.durability = br.ReadUInt16();
                    this.stack = br.ReadUInt16();
                    this.identified = br.ReadByte();
                    this.agi = br.ReadInt16();
                    this.atk1 = br.ReadInt16();
                    this.atk2 = br.ReadInt16();
                    this.atk3 = br.ReadInt16();
                    this.avoidCritical = br.ReadInt16();
                    this.avoidMagic = br.ReadInt16();
                    this.avoidMelee = br.ReadInt16();
                    this.avoidRanged = br.ReadInt16();
                    this.cha = br.ReadInt16();
                    this.def = br.ReadInt16();
                    this.dex = br.ReadInt16();
                    this.hitCritical = br.ReadInt16();
                    this.hitMagic = br.ReadInt16();
                    this.hitMelee = br.ReadInt16();
                    this.hitRanged = br.ReadInt16();
                    this.hp = br.ReadInt16();
                    this.hpRecover = br.ReadInt16();
                    this.intel = br.ReadInt16();
                    this.luk = br.ReadInt16();
                    this.mag = br.ReadInt16();
                    this.matk = br.ReadInt16();
                    this.mdef = br.ReadInt16();
                    this.mp = br.ReadInt16();
                    this.mpRecover = br.ReadInt16();
                    this.sp = br.ReadInt16();
                    this.speedUp = br.ReadInt16();
                    this.str = br.ReadInt16();
                    this.vit = br.ReadInt16();
                    this.volumeUp = br.ReadInt16();
                    this.weightUp = br.ReadInt16();
                    this.aspd = br.ReadInt16();
                    this.cspd = br.ReadInt16();
                    this.refine = br.ReadUInt16();
                    this.pict_id = br.ReadUInt32();
                    this.slot = br.ReadUInt32();
                }
                if (item_version >= 2)
                {
                    this.currentSlot = br.ReadByte();
                    int count = br.ReadByte();
                    for (int i = 0; i < count; i++)
                    {
                        uint id = br.ReadUInt32();
                        if (Iris.IrisCardFactory.Instance.Items.ContainsKey(id))
                        {
                            this.cards.Add(Iris.IrisCardFactory.Instance.Items[id]);
                        }
                    }
                    this.locked = br.ReadBoolean();
                }
                if (item_version >= 3)
                {
                    this.rental = br.ReadBoolean();
                    this.rentalTime = DateTime.FromBinary(br.ReadInt64());
                }
                if (item_version >= 4)
                {
                    this.changeMode = br.ReadBoolean();
                    this.changeMode2 = br.ReadBoolean();
                }
                if(item_version >= 5)
                {
                    this.MasterEnhance = br.ReadBoolean();
                    this.LifeEnhance = br.ReadByte();
                    this.PowerEnhance = br.ReadByte();
                    this.CritEnhance = br.ReadByte();
                    this.MagEnhance = br.ReadByte();
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void Clear()
        {
            this.agi = 0;
            this.atk1 = 0;
            this.atk2 = 0;
            this.atk3 = 0;
            this.avoidCritical = 0;
            this.avoidMagic = 0;
            this.avoidMelee = 0;
            this.avoidRanged = 0;
            this.cha = 0;
            this.def = 0;
            this.dex = 0;
            this.hitCritical = 0;
            this.hitMagic = 0;
            this.hitMelee = 0;
            this.hitRanged = 0;
            this.hp = 0;
            this.hpRecover = 0;
            this.intel = 0;
            this.luk = 0;
            this.mag = 0;
            this.matk = 0;
            this.mdef = 0;
            this.mp = 0;
            this.mpRecover = 0;
            this.sp = 0;
            this.speedUp = 0;
            this.str = 0;
            this.vit = 0;
            this.volumeUp = 0;
            this.weightUp = 0;
            this.aspd = 0;
            this.cspd = 0;
            this.Refine = 0;
            this.LifeEnhance = 0;
            this.PowerEnhance = 0;
            this.CritEnhance = 0;
            this.MagEnhance = 0;
        }

        public Item Clone()
        {
            Item item = new Item();
            item.id = this.id;
            item.db_id = this.db_id;
            item.durability = this.durability;
            item.stack = this.stack;
            item.identified = this.identified;
            item.PossessionedActor = this.PossessionedActor;
            item.PossessionOwner = this.PossessionOwner;
            item.agi = this.agi;
            item.atk1 = this.atk1;
            item.atk2 = this.atk2;
            item.atk3 = this.atk3;
            item.avoidCritical = this.avoidCritical;
            item.avoidMagic = this.avoidMagic;
            item.avoidMelee = this.avoidMelee;
            item.avoidRanged = this.avoidRanged;
            item.cha = this.cha;
            item.def = this.def;
            item.dex = this.dex;
            item.hitCritical = this.hitCritical;
            item.hitMagic = this.hitMagic;
            item.hitMelee = this.hitMelee;
            item.hitRanged = this.hitRanged;
            item.hp = this.hp;
            item.hpRecover = this.hpRecover;
            item.intel = this.intel;
            item.luk = this.luk;
            item.mag = this.mag;
            item.matk = this.matk;
            item.mdef = this.mdef;
            item.mp = this.mp;
            item.mpRecover = this.mpRecover;
            item.sp = this.sp;
            item.speedUp = this.speedUp;
            item.stack = this.stack;
            item.str = this.str;
            item.vit = this.vit;
            item.volumeUp = this.volumeUp;
            item.weightUp = this.weightUp;
            item.refine = this.refine;
            item.aspd = this.aspd;
            item.cspd = this.cspd;
            item.pict_id = this.pict_id;
            item.currentSlot = this.currentSlot;
            item.maxSlot = this.maxSlot;
            item.locked = this.locked;
            item.changeMode = this.changeMode;
            item.changeMode2 = this.changeMode2;
            foreach(Iris.IrisCard i in this.cards)
            {
                item.cards.Add(i);
            }
            item.rental = this.rental;
            item.rentalTime = this.rentalTime;
            item.refine = this.Refine;
            item.MasterEnhance = this.MasterEnhance;
            item.LifeEnhance = this.LifeEnhance;
            item.PowerEnhance = this.PowerEnhance;
            item.CritEnhance = this.CritEnhance;
            item.MagEnhance = this.MagEnhance;
            return item;
        }

        public bool Stackable
        {
            get
            {
                if (this.BaseData.stock == true)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 检查是否是装备
        /// </summary>
        public bool IsEquipt
        {
            get
            {
                int type = (int)this.BaseData.itemType;
                if (type >= (int)ItemType.ACCESORY_HEAD && type <= (int)ItemType.PET_NEKOMATA)
                    return true;
                else if (type == (int)ItemType.EXSWORD || type == (int)ItemType.EXGUN)
                    return true;
                else if (type == (int)ItemType.EFFECT)
                    return true;
                else if ((type == (int)ItemType.PARTNER) || (type == (int)ItemType.RIDE_PARTNER))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 检查是否是DEM部件
        /// </summary>
        public bool IsParts
        {
            get
            {
                int type = (int)this.BaseData.itemType;
                if (type >= (int)ItemType.PARTS_HEAD && type <= (int)ItemType.PARTS_LONGRANGE)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 检查一个道具是否是武器
        /// </summary>
        public bool IsWeapon
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.AXE:
                    case ItemType.BOOK:
                    case ItemType.HAMMER:
                    case ItemType.HANDBAG:
                    case ItemType.RAPIER:
                    case ItemType.SHORT_SWORD:
                    case ItemType.SPEAR:
                    case ItemType.STAFF:
                    case ItemType.SWORD:
                    case ItemType.ROPE:
                    case ItemType.BOW:
                    case ItemType.DUALGUN:
                    case ItemType.GUN:
                    case ItemType.RIFLE:
                    case ItemType.CARD:
                    case ItemType.THROW:
                    case ItemType.CLAW:
                    case ItemType.STRINGS:
                    case ItemType.ETC_WEAPON:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 检查武器是否需要弹药（是否是弓箭枪械类）
        /// </summary>
        public bool NeedAmmo
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.BOW:
                    case ItemType.DUALGUN:
                    case ItemType.GUN:
                    case ItemType.RIFLE:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 检查是否是弹药(不含card和throw)
        /// </summary>
        public bool IsAmmo
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.ARROW:
                    case ItemType.BULLET:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 检查道具是否是衣服
        /// </summary>
        public bool IsArmor
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.ONEPIECE:
                    case ItemType.COSTUME:
                    case ItemType.BODYSUIT:
                    case ItemType.WEDDING:
                    case ItemType.OVERALLS:
                    case ItemType.ARMOR_UPPER:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 检查道具是否是宠物
        /// </summary>
        public bool IsPet
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.PET:
                    case ItemType.PET_NEKOMATA:
                    case ItemType.RIDE_PET:
                    case ItemType.RIDE_PET_ROBOT:
                    case ItemType.PARTNER:
                    case ItemType.RIDE_PARTNER:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 检查道具是否是partner
        /// </summary>
        public bool IsPartner
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.PARTNER:
                    case ItemType.RIDE_PARTNER:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 取得该装备需要的装备槽 
        /// </summary>
        public List<EnumEquipSlot> EquipSlot
        {
            get
            {
                List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
                if (!IsEquipt && !IsParts)
                    Logger.ShowDebug("Cannot equip a non equipment item!", Logger.defaultlogger);
                switch (this.BaseData.itemType)
                {
                    //head&face
                    case ItemType.ACCESORY_HEAD:
                        slots.Add(EnumEquipSlot.HEAD_ACCE);
                        slots.Add(EnumEquipSlot.HEAD);
                        break;
                    case ItemType.PARTS_HEAD:
                    case ItemType.HELM:
                        slots.Add(EnumEquipSlot.HEAD);
                        break;
                    case ItemType.ACCESORY_FACE:
                        slots.Add(EnumEquipSlot.FACE_ACCE);
                        break;
                    case ItemType.FULLFACE:
                        slots.Add(EnumEquipSlot.HEAD);
                        slots.Add(EnumEquipSlot.FACE);
                        slots.Add(EnumEquipSlot.HEAD_ACCE);
                        slots.Add(EnumEquipSlot.FACE_ACCE);
                        break;
                    //necklace
                    case ItemType.ACCESORY_NECK:
                    case ItemType.JOINT_SYMBOL:
                        slots.Add(EnumEquipSlot.CHEST_ACCE);
                        break;
                    //tops
                    case ItemType.PARTS_BODY:
                    case ItemType.ARMOR_UPPER:
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        break;
                    case ItemType.OVERALLS:
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.ONEPIECE:
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    //downs
                    case ItemType.PARTS_LEG:
                    case ItemType.SLACKS:
                    case ItemType.ARMOR_LOWER:
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.SOCKS:
                        slots.Add(EnumEquipSlot.SOCKS);
                        break;
                    //shoes
                    case ItemType.SHOES:
                    case ItemType.BOOTS:
                    case ItemType.HALFBOOTS:
                        slots.Add(EnumEquipSlot.SHOES);
                        break;
                    case ItemType.LONGBOOTS:
                        slots.Add(EnumEquipSlot.SHOES);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    //weapons&ammos&shield&lefts
                    case ItemType.PARTS_LONGRANGE:
                    case ItemType.PARTS_SLASH:
                    case ItemType.PARTS_STAB:
                    case ItemType.PARTS_BLOW:
                    case ItemType.BOOK:
                    case ItemType.STAFF:
                    case ItemType.SWORD:
                    case ItemType.AXE:
                    case ItemType.SPEAR:
                    case ItemType.RAPIER:
                    case ItemType.CARD:
                    case ItemType.HANDBAG:
                    case ItemType.SHORT_SWORD:
                    case ItemType.ETC_WEAPON:
                    case ItemType.THROW:
                    case ItemType.ROPE:
                    case ItemType.HAMMER:
                        if (this.BaseData.doubleHand)
                        {
                            slots.Add(EnumEquipSlot.RIGHT_HAND);
                            slots.Add(EnumEquipSlot.LEFT_HAND);
                        }
                        else
                        {
                            slots.Add(EnumEquipSlot.RIGHT_HAND);
                        }
                        break;
                    case ItemType.BOW:
                    case ItemType.GUN:
                    case ItemType.RIFLE:
                    case ItemType.DUALGUN:
                        slots.Add(EnumEquipSlot.RIGHT_HAND);
                        break;
                    case ItemType.CLAW:
                    case ItemType.STRINGS:
                        slots.Add(EnumEquipSlot.RIGHT_HAND);
                        slots.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.ACCESORY_FINGER:
                    case ItemType.SHIELD:
                    case ItemType.LEFT_HANDBAG:
                    case ItemType.BULLET:
                    case ItemType.ARROW:
                        slots.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    //backs&pets&effects
                    case ItemType.PARTS_BACK:
                    case ItemType.BACKPACK:
                        slots.Add(EnumEquipSlot.BACK);
                        break;
                    case ItemType.BACK_DEMON:
                        slots.Add(EnumEquipSlot.PET);
                        slots.Add(EnumEquipSlot.BACK);
                        break;
                    case ItemType.PET_NEKOMATA:
                    case ItemType.PET:
                    case ItemType.RIDE_PET:
                    case ItemType.PARTNER:
                    case ItemType.RIDE_PARTNER:
                        slots.Add(EnumEquipSlot.PET);
                        break;
                    case ItemType.EFFECT:
                        slots.Add(EnumEquipSlot.EFFECT);
                        break;
                    //complexes
                    case ItemType.EXSWORD:
                    case ItemType.EXGUN:
                        slots.Add(EnumEquipSlot.RIGHT_HAND);
                        slots.Add(EnumEquipSlot.SHOES);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.WEDDING:
                        slots.Add(EnumEquipSlot.RIGHT_HAND);
                        slots.Add(EnumEquipSlot.LEFT_HAND);
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        slots.Add(EnumEquipSlot.PET);
                        break;
                    case ItemType.BODYSUIT:
                    case ItemType.FACEBODYSUIT:
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        slots.Add(EnumEquipSlot.SHOES);
                        slots.Add(EnumEquipSlot.SOCKS);
                        break;
                    case ItemType.COSTUME:
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        slots.Add(EnumEquipSlot.HEAD_ACCE);
                        slots.Add(EnumEquipSlot.HEAD);
                        slots.Add(EnumEquipSlot.FACE);
                        slots.Add(EnumEquipSlot.FACE_ACCE);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        slots.Add(EnumEquipSlot.SHOES);
                        slots.Add(EnumEquipSlot.SOCKS);
                        break;
                    case ItemType.EQ_ALLSLOT:
                        slots.Add(EnumEquipSlot.UPPER_BODY);
                        slots.Add(EnumEquipSlot.HEAD);
                        slots.Add(EnumEquipSlot.FACE);
                        slots.Add(EnumEquipSlot.FACE_ACCE);
                        slots.Add(EnumEquipSlot.CHEST_ACCE);
                        slots.Add(EnumEquipSlot.RIGHT_HAND);
                        slots.Add(EnumEquipSlot.LEFT_HAND);
                        slots.Add(EnumEquipSlot.BACK);
                        slots.Add(EnumEquipSlot.LOWER_BODY);
                        slots.Add(EnumEquipSlot.SHOES);
                        slots.Add(EnumEquipSlot.SOCKS);
                        slots.Add(EnumEquipSlot.PET);
                        break;
                }
                return slots;
            }
        }

        public ATTACK_TYPE AttackType
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.SWORD:
                    case ItemType.CARD:
                    case ItemType.SHORT_SWORD:
                    case ItemType.PARTS_SLASH:
                        return ATTACK_TYPE.SLASH;
                    case ItemType.RIFLE:
                    case ItemType.CLAW:
                    case ItemType.BOW:
                    case ItemType.GUN:
                    case ItemType.DUALGUN:
                    case ItemType.SPEAR:
                    case ItemType.ARROW:
                    case ItemType.RAPIER:
                    case ItemType.THROW:
                    case ItemType.PARTS_STAB:
                    case ItemType.PARTS_LONGRANGE:
                        return ATTACK_TYPE.STAB;
                    case ItemType.LEFT_HANDBAG:
                    case ItemType.HANDBAG:
                    case ItemType.HAMMER:
                    case ItemType.AXE:
                    case ItemType.BOOK:
                    case ItemType.STAFF:
                    case ItemType.ETC_WEAPON:
                    case ItemType.STRINGS:
                    case ItemType.PARTS_BLOW:                    
                        return ATTACK_TYPE.BLOW;
                    default:
                        return ATTACK_TYPE.BLOW;
                }
            }
        }

        /// <summary>
        /// 装备所有插的卡的能力向量
        ///<param name="deck">是否是牌面</param>
        /// </summary>
        public List<AbilityVector> AbilityVectors(bool deck)
        {
            List<AbilityVector> list = new List<AbilityVector>();
            List<IrisCard> cards = new List<IrisCard>();
            if (deck && this.cards.Count > 0)
                cards.Add(this.cards[this.cards.Count - 1]);
            else
                cards = this.cards;
            foreach (IrisCard i in cards)
            {
                foreach (AbilityVector j in i.Abilities.Keys)
                {
                    if (!list.Contains(j))
                        list.Add(j);
                }
            }
            return list;
        }

        /// <summary>
        /// 取得能力向量的值或等级
        /// </summary>
        /// <param name="deck">是否是牌面</param>
        /// <param name="lv">是否是取得向量等级信息而非值信息</param>
        /// <returns></returns>
        public Dictionary<AbilityVector, int> VectorValues(bool deck, bool lv)
        {
            Dictionary<AbilityVector, int> list = new Dictionary<AbilityVector, int>();
            List<IrisCard> cards = new List<IrisCard>();
            if (deck && this.cards.Count > 0)
                cards.Add(this.cards[this.cards.Count - 1]);
            else
                cards = this.cards;

            foreach (IrisCard i in cards)
            {
                foreach (AbilityVector j in i.Abilities.Keys)
                {
                    if (!list.ContainsKey(j))
                        list.Add(j, i.Abilities[j]);
                    else
                        list[j] += i.Abilities[j];
                }
            }

            if (lv)
            {
                int[] lvs = new int[10] { 1, 30, 80, 150, 250, 370, 510, 660, 820, 999 };
                AbilityVector[] keys = list.Keys.ToArray();
                foreach (AbilityVector i in keys)
                {
                    int value = list[i];
                    int level = 0;
                    foreach (int j in lvs)
                    {
                        if (value >= j)
                            level++;
                        else
                            break;
                    }
                    list[i] = level;
                }
            }
            return list;
        }

        /// <summary>
        /// 取得装备卡片的Release能力
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public Dictionary<ReleaseAbility, int> ReleaseAbilities(bool deck)
        {
            return ReleaseAbilities(VectorValues(deck, true));
        }

        public static Dictionary<ReleaseAbility, int> ReleaseAbilities(Dictionary<AbilityVector, int> vectors)
        {
            Dictionary<ReleaseAbility, int> list = new Dictionary<ReleaseAbility, int>();
            
            foreach (AbilityVector i in vectors.Keys)
            {
                Dictionary<ReleaseAbility, int> ability = i.Abilities[(byte)vectors[i]];
                foreach (ReleaseAbility j in ability.Keys)
                {
                    if (list.ContainsKey(j))
                        list[j] += ability[j];
                    else
                        list.Add(j, ability[j]);
                }
            }
            return list;
        }

        /// <summary>
        /// 取得插入的卡的属性加成
        /// </summary>
        /// <param name="deck">是否是牌面</param>
        /// <returns></returns>
        public Dictionary<Elements, int> Elements(bool deck)
        {
            Dictionary<Elements, int> list = new Dictionary<Elements, int>();
            List<IrisCard> cards = new List<IrisCard>();
            if (deck && this.cards.Count > 0)
                cards.Add(this.cards[this.cards.Count - 1]);
            else
                cards = this.cards;
            list.Add(SagaLib.Elements.Neutral, 0);
            list.Add(SagaLib.Elements.Fire, 0);
            list.Add(SagaLib.Elements.Water, 0);
            list.Add(SagaLib.Elements.Wind, 0);
            list.Add(SagaLib.Elements.Earth, 0);
            list.Add(SagaLib.Elements.Holy, 0);
            list.Add(SagaLib.Elements.Dark, 0);

            foreach (IrisCard i in cards)
            {
                foreach (SagaLib.Elements j in i.Elements.Keys)
                {
                    if (!list.ContainsKey(j))
                        list.Add(j, i.Elements[j]);
                    else
                        list[j] += i.Elements[j];
                }
            }
            return list;
        }
    }
}
