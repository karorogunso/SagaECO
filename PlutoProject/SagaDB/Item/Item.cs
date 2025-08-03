using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Partner;
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
            public uint equipVolume, possessionWeight, weight, volume;
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
            public short hpRecover, mpRecover, spRecover;
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

            public ushort handMotion;
            public byte handMotion2;
            public bool noTrade;
            public ItemAddition ItemAddition = new ItemAddition();
            public override string ToString()
            {
                return name;
            }
        }
        uint db_id, id;
        string name = "";
        ushort stack;
        ushort durability, maxdurability;
        public byte identified;
        bool old; //旧物品标记
        bool potential, release;//潜强，性能解放。
        byte dye; //染色相关
        bool locked;
        bool changeMode, changeMode2;
        private uint slot;

        short hp, mp, sp, weightUp, volumeUp, speedUp;
        short str, dex, intel, vit, agi, mag, luk, cha;
        short atk1, atk2, atk3, matk, def, mdef;
        short hitMelee, hitRanged, hitMagic;
        short avoidMelee, avoidRanged, avoidMagic;
        short hitCritical, avoidCritical;
        short hpRecover, mpRecover, spRecover;
        short aspd, cspd;
        uint pict_id;
        bool rental;
        DateTime rentalTime = DateTime.Now;
        byte partnerLevel, partnerRebirth;
        byte currentSlot, maxSlot;

        byte lifeenhance = 0;
        byte powerenhance = 0;
        byte critenhance = 0;
        byte magenhance = 0;

        ushort refine;

        public short atk_refine, matk_refine, hp_refine, def_refine, mdef_refine, recover_refine, cri_refine, spd_refine, atkrate_refine, matkrate_refine,
            defrate_refine, mdefrate_refine, hit_refine, mhit_refine;
        List<Iris.IrisCard> cards = new List<SagaDB.Iris.IrisCard>();

        public class EnItem
        {
            public short hp, mp, sp, weightUp, volumeUp, speedUp;
            public short str, dex, intel, vit, agi, mag;
            public short atk1, atk2, atk3, matk, def, mdef;
            public short aspd, cspd;
        }

        Dictionary<RefineType, ushort> refineType;


        uint refine_sharp, refine_enchanted, refine_vitality, refine_regeneration, refine_lucky, refine_dexterity, refine_ATKrate, refine_MATKrate, refine_def, refine_mdef, refine_hit, refine_mhit;

        /// <summary>
        /// 该道具的宠物ID
        /// </summary>
        uint actorpartnerid;

        /// <summary>
        /// 道具版本
        /// </summary>
        static ushort Version = 11;

        [NonSerialized]
        ActorPC possessionedActor;
        [NonSerialized]
        ActorPC possessionOwner;

        public uint Refine_Sharp { get { return refine_sharp; } set { refine_sharp = value; } }
        public uint Refine_Enchanted { get { return refine_enchanted; } set { refine_enchanted = value; } }
        public uint Refine_Vitality { get { return refine_vitality; } set { refine_vitality = value; } }
        public uint Refine_Regeneration { get { return refine_regeneration; } set { refine_regeneration = value; } }
        public uint Refine_Hit { get { return refine_def; } set { refine_def = value; } }
        public uint Refine_Mhit { get { return refine_mdef; } set { refine_mdef = value; } }
        public uint Refine_Lucky { get { return refine_lucky; } set { refine_lucky = value; } }
        public uint Refine_Dexterity { get { return refine_dexterity; } set { refine_dexterity = value; } }
        public uint Refine_ATKrate { get { return refine_ATKrate; } set { refine_ATKrate = value; } }
        public uint Refine_MATKrate { get { return refine_MATKrate; } set { refine_MATKrate = value; } }
        public uint Refine_Def { get { return refine_def; } set { refine_def = value; } }
        public uint Refine_Mdef { get { return refine_mdef; } set { refine_mdef = value; } }

        public Dictionary<Elements, short> Element = new Dictionary<Elements, short>();

        /// <summary>
        /// 该装备附魔的其他套装装备
        /// </summary>
        public Dictionary<uint, EnItem> EnChangeList = new Dictionary<uint, EnItem>();

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
        /// 强化装备提升的HP
        /// </summary>
        public short HP { get { return hp; } set { hp = value; } }
        /// <summary>
        /// 强化装备提升的MP
        /// </summary>
        public short MP { get { return mp; } set { mp = value; } }
        /// <summary>
        /// 强化装备提升的SP
        /// </summary>
        public short SP { get { return sp; } set { sp = value; } }
        /// <summary>
        /// 强化装备提升的负重
        /// </summary>
        public short WeightUp { get { return weightUp; } set { weightUp = value; } }
        /// <summary>
        /// 强化装备提升的体积
        /// </summary>
        public short VolumeUp { get { return volumeUp; } set { volumeUp = value; } }
        /// <summary>
        /// 强化装备提升的速度
        /// </summary>
        public short SpeedUp { get { return speedUp; } set { speedUp = value; } }
        /// <summary>
        /// 强化装备提升的Str
        /// </summary>
        public short Str { get { return str; } set { str = value; } }
        /// <summary>
        /// 强化装备提升的Dex
        /// </summary>
        public short Dex { get { return dex; } set { dex = value; } }
        /// <summary>
        /// 强化装备提升的Int
        /// </summary>
        public short Int { get { return intel; } set { intel = value; } }
        /// <summary>
        /// 强化装备提升的Vit
        /// </summary>
        public short Vit { get { return vit; } set { vit = value; } }
        /// <summary>
        /// 强化装备提升的Agi
        /// </summary>
        public short Agi { get { return agi; } set { agi = value; } }
        /// <summary>
        /// 强化装备提升的Mag
        /// </summary>
        public short Mag { get { return mag; } set { mag = value; } }
        /// <summary>
        /// 强化装备提升的Mag
        /// </summary>
        public short Luk { get { return luk; } set { luk = value; } }
        /// <summary>
        /// 强化装备提升的Mag
        /// </summary>
        public short Cha { get { return cha; } set { cha = value; } }
        /// <summary>
        /// 强化装备提升的Atk1
        /// </summary>
        public short Atk1 { get { return atk1; } set { atk1 = value; } }
        /// <summary>
        /// 强化装备提升的Atk2
        /// </summary>
        public short Atk2 { get { return atk2; } set { atk2 = value; } }
        /// <summary>
        /// 强化装备提升的Atk3
        /// </summary>
        public short Atk3 { get { return atk3; } set { atk3 = value; } }
        /// <summary>
        /// 强化装备提升的MAtk
        /// </summary>
        public short MAtk { get { return matk; } set { matk = value; } }
        /// <summary>
        /// 强化装备提升的Def
        /// </summary>
        public short Def { get { return def; } set { def = value; } }
        /// <summary>
        /// 强化装备提升的MDef
        /// </summary>
        public short MDef { get { return mdef; } set { mdef = value; } }
        /// <summary>
        /// 强化装备提升的HitMelee
        /// </summary>
        public short HitMelee { get { return hitMelee; } set { hitMelee = value; } }
        /// <summary>
        /// 强化装备提升的HitMagic
        /// </summary>
        public short HitMagic { get { return hitMagic; } set { hitMagic = value; } }
        /// <summary>
        /// 强化装备提升的HitRanged
        /// </summary>
        public short HitRanged { get { return hitRanged; } set { hitRanged = value; } }
        /// <summary>
        /// 强化装备提升的AvoidMelee
        /// </summary>
        public short AvoidMelee { get { return avoidMelee; } set { avoidMelee = value; } }
        /// <summary>
        /// 强化装备提升的AvoidMagic
        /// </summary>
        public short AvoidMagic { get { return avoidMagic; } set { avoidMagic = value; } }
        /// <summary>
        /// 强化装备提升的AvoidRanged
        /// </summary>
        public short AvoidRanged { get { return avoidRanged; } set { avoidRanged = value; } }
        /// <summary>
        /// 强化装备提升的HitCritical
        /// </summary>
        public short HitCritical { get { return hitCritical; } set { hitCritical = value; } }
        /// <summary>
        /// 强化装备提升的AvoidCritical
        /// </summary>
        public short AvoidCritical { get { return avoidCritical; } set { avoidCritical = value; } }
        /// <summary>
        /// 强化装备提升的HPRecover 宠物相关 待检查
        /// </summary>
        public short HPRecover { get { return hpRecover; } set { hpRecover = value; } }
        /// <summary>
        /// 强化装备提升的MPRecover 宠物相关 待检查 暂时不要使用
        /// </summary>
        public short MPRecover { get { return mpRecover; } set { mpRecover = value; } }
        /// <summary>
        /// 强化装备提升的SPRecover 宠物相关 待检查 暂时不要使用
        /// </summary>
        public short SPRecover { get { return spRecover; } set { spRecover = value; } }

        /// <summary>
        /// 强化装备提升的ASPD 宠物相关 待检查
        /// </summary>
        public short ASPD { get { return aspd; } set { aspd = value; } }

        /// <summary>
        /// 强化装备提升的CSPD 宠物相关 待检查
        /// </summary>
        public short CSPD { get { return cspd; } set { cspd = value; } }

        /// <summary>
        /// 道具详细情报显示的名字
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// 是否是旧版本道具
        /// </summary>
        public bool Old { get { return old; } set { old = value; } }

        /// <summary>
        /// 是否已经潜在强化
        /// </summary>
        public bool Potential { get { return potential; } set { potential = value; } }

        /// <summary>
        /// 是否已经性能解放
        /// </summary>
        public bool Release { get { return release; } set { release = value; } }

        /// <summary>
        /// Partner等级
        /// </summary>
        public byte PartnerLevel { get { return partnerLevel; } set { partnerLevel = value; } }

        /// <summary>
        /// Partner转生标志 0=未转生 1=已转生
        /// </summary>
        public byte PartnerRebirth { get { return partnerRebirth; } set { partnerRebirth = value; } }


        /// <summary>
        /// 是否是出租道具
        /// </summary>
        public bool Rental { get { return rental; } set { rental = value; } }

        /// <summary>
        /// 出租道具到期时间
        /// </summary>
        public DateTime RentalTime { get { return rentalTime; } set { rentalTime = value; } }

        public uint PictID { get { return pict_id; } set { pict_id = value; } }

        public uint ItemID { get { return id; } }
        public uint DBID { get { return db_id; } set { db_id = value; } }
        public ushort maxDurability { get { return maxdurability; } set { maxdurability = value; } }
        public uint Slot { get { return slot; } set { slot = value; } }

        public ItemData BaseData
        {
            get
            {
                ItemData baseData = null;
                if (baseData == null)
                {
                    if (ItemFactory.Instance.Items.ContainsKey(id))
                        baseData = ItemFactory.Instance.Items[id];
                    else
                        baseData = new ItemData();
                }
                return baseData;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public ushort Stack { get { return stack; } set { stack = value; } }
        /// <summary>
        /// 染色
        /// </summary>
        public byte Dye { get { return dye; } set { dye = value; } }
        /// <summary>
        /// 持久
        /// </summary>
        public ushort Durability { get { return durability; } set { durability = value; } }

        /// <summary>
        /// 凭依在此道具上的Actor
        /// </summary>
        public Actor.ActorPC PossessionedActor { get { return possessionedActor; } set { possessionedActor = value; } }
        /// <summary>
        /// 此道具被凭依前的主人
        /// </summary>
        public Actor.ActorPC PossessionOwner { get { return possessionOwner; } set { possessionOwner = value; } }

        /// <summary>
        /// 装备当前插槽
        /// </summary>
        public byte CurrentSlot
        {
            get
            {
                if (currentSlot == 0 && currentSlot != BaseData.currentSlot)
                    currentSlot = BaseData.currentSlot;
                return currentSlot;
            }
            set { currentSlot = value; }
        }

        /// <summary>
        /// 装备最大插槽
        /// </summary>
        public byte MaxSlot
        {
            get
            {
                if (maxSlot == 0 && maxSlot != BaseData.maxSlot)
                    maxSlot = BaseData.maxSlot;
                return maxSlot;
            }
            set { maxSlot = value; }
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
        public ushort Refine { get { return refine; } set { refine = value; } }

        public bool Identified
        {
            get
            {
                if (identified == 0)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value == true)
                    identified = 1;
                else
                    identified = 0;
            }
        }

        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
            }
        }
        /// <summary>
        /// 觉醒状态
        /// </summary>
        public bool ChangeMode
        {
            get
            {
                return changeMode;
            }
            set
            {
                changeMode = value;
            }
        }
        public bool ChangeMode2
        {
            get
            {
                return changeMode2;
            }
            set
            {
                changeMode2 = value;
            }
        }
        /// <summary>
        /// 强化类型
        /// </summary>
        public Dictionary<RefineType, ushort> RefineType
        {
            get
            {
                if (refineType == null)
                    refineType = new Dictionary<RefineType, ushort>();
                if (!refineType.ContainsKey((RefineType)0))
                    refineType.Add(((RefineType)0), 0);
                if (!refineType.ContainsKey((RefineType)1))
                    refineType.Add(((RefineType)1), 0);
                if (!refineType.ContainsKey((RefineType)2))
                    refineType.Add(((RefineType)2), 0);
                if (!refineType.ContainsKey((RefineType)3))
                    refineType.Add(((RefineType)3), 0);
                if (!refineType.ContainsKey((RefineType)4))
                    refineType.Add(((RefineType)4), 0);
                if (!refineType.ContainsKey((RefineType)5))
                    refineType.Add(((RefineType)5), 0);
                if (!refineType.ContainsKey((RefineType)6))
                    refineType.Add(((RefineType)6), 0);
                if (!refineType.ContainsKey((RefineType)7))
                    refineType.Add(((RefineType)7), 0);
                if (!refineType.ContainsKey((RefineType)8))
                    refineType.Add(((RefineType)8), 0);
                if (!refineType.ContainsKey((RefineType)9))
                    refineType.Add(((RefineType)9), 0);
                if (!refineType.ContainsKey((RefineType)10))
                    refineType.Add(((RefineType)10), 0);
                if (!refineType.ContainsKey((RefineType)11))
                    refineType.Add(((RefineType)11), 0);
                return refineType;
            }
            set
            {
                refineType = value;
            }
        }

        public Item()
        {
        }

        public Item(ItemData baseData)
        {
            id = baseData.id;
            maxdurability = baseData.durability;
            //出错备份Element = baseData.element;
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
            bw.Write(id);
            bw.Write(durability);
            bw.Write(stack);
            bw.Write(identified);
            bw.Write(agi);
            bw.Write(atk1);
            bw.Write(atk2);
            bw.Write(atk3);
            bw.Write(avoidCritical);
            bw.Write(avoidMagic);
            bw.Write(avoidMelee);
            bw.Write(avoidRanged);
            bw.Write(cha);
            bw.Write(def);
            bw.Write(dex);
            bw.Write(hitCritical);
            bw.Write(hitMagic);
            bw.Write(hitMelee);
            bw.Write(hitRanged);
            bw.Write(hp);
            bw.Write(hpRecover);
            bw.Write(intel);
            bw.Write(luk);
            bw.Write(mag);
            bw.Write(matk);
            bw.Write(mdef);
            bw.Write(mp);
            bw.Write(mpRecover);
            bw.Write(sp);
            bw.Write(speedUp);
            bw.Write(str);
            bw.Write(vit);
            bw.Write(volumeUp);
            bw.Write(weightUp);
            bw.Write(aspd);
            bw.Write(cspd);
            bw.Write(refine);
            bw.Write(pict_id);
            bw.Write(slot);

            //Version 2
            bw.Write(currentSlot);
            if (cards == null)
                cards = new List<IrisCard>();
            bw.Write((byte)cards.Count);
            foreach (Iris.IrisCard i in cards)
            {
                bw.Write(i.ID);
            }
            bw.Write(locked);

            //Version 3
            bw.Write(rental);
            bw.Write(rentalTime.ToBinary());

            //Version 4 
            bw.Write(changeMode);
            bw.Write(changeMode2);

            //Version 5
            foreach (var item in RefineType)
                bw.Write(item.Value);

            //Version 6
            bw.Write(refine_sharp);
            bw.Write(refine_enchanted);
            bw.Write(refine_vitality);
            bw.Write(refine_regeneration);
            bw.Write(refine_lucky);
            bw.Write(refine_dexterity);
            bw.Write(refine_ATKrate);
            bw.Write(refine_MATKrate);
            bw.Write(refine_def);
            bw.Write(refine_mdef);

            //version 7
            bw.Write(refine_hit);
            bw.Write(refine_mhit);
            bw.Write(actorpartnerid);

            //version 8

            bw.Write(name);
            bw.Write(partnerLevel);
            bw.Write(partnerRebirth);
            bw.Write(old);
            bw.Write(potential);
            bw.Write(release);

            //version 9
            bw.Write(maxdurability);

            //version 10
            bw.Write(lifeenhance);
            bw.Write(powerenhance);
            bw.Write(critenhance);
            bw.Write(magenhance);

            //version 11
            bw.Write(Dye);

            ////version 12
            //if (Element.ContainsKey(Elements.Neutral))
            //    bw.Write(Element[Elements.Neutral]);
            //else
            //    bw.Write((short)0);
            //if (Element.ContainsKey(Elements.Holy))
            //    bw.Write(Element[Elements.Holy]);
            //else
            //    bw.Write((short)0);
            //if (Element.ContainsKey(Elements.Dark))
            //    bw.Write(Element[Elements.Dark]);
            //else
            //    bw.Write((short)0);
            //if (Element.ContainsKey(Elements.Fire))
            //    bw.Write(Element[Elements.Fire]);
            //else
            //    bw.Write((short)0);
            //if (Element.ContainsKey(Elements.Water))
            //    bw.Write(Element[Elements.Water]);
            //else
            //    bw.Write((short)0);
            //if (Element.ContainsKey(Elements.Earth))
            //    bw.Write(Element[Elements.Earth]);
            //else
            //    bw.Write((short)0);
            //if (Element.ContainsKey(Elements.Wind))
            //    bw.Write(Element[Elements.Wind]);
            //else
            //    bw.Write((short)0);
        }

        public void FromStream(Stream InputStream)
        {
            try
            {
                BinaryReader br = new BinaryReader(InputStream);
                ushort item_version = br.ReadUInt16();
                if (item_version >= 1)
                {
                    id = br.ReadUInt32();
                    durability = br.ReadUInt16();
                    stack = br.ReadUInt16();
                    identified = br.ReadByte();
                    agi = br.ReadInt16();
                    atk1 = br.ReadInt16();
                    atk2 = br.ReadInt16();
                    atk3 = br.ReadInt16();
                    avoidCritical = br.ReadInt16();
                    avoidMagic = br.ReadInt16();
                    avoidMelee = br.ReadInt16();
                    avoidRanged = br.ReadInt16();
                    cha = br.ReadInt16();
                    def = br.ReadInt16();
                    dex = br.ReadInt16();
                    hitCritical = br.ReadInt16();
                    hitMagic = br.ReadInt16();
                    hitMelee = br.ReadInt16();
                    hitRanged = br.ReadInt16();
                    hp = br.ReadInt16();
                    hpRecover = br.ReadInt16();
                    intel = br.ReadInt16();
                    luk = br.ReadInt16();
                    mag = br.ReadInt16();
                    matk = br.ReadInt16();
                    mdef = br.ReadInt16();
                    mp = br.ReadInt16();
                    mpRecover = br.ReadInt16();
                    sp = br.ReadInt16();
                    //this.spRecover = br.ReadInt16();
                    speedUp = br.ReadInt16();
                    str = br.ReadInt16();
                    vit = br.ReadInt16();
                    volumeUp = br.ReadInt16();
                    weightUp = br.ReadInt16();
                    aspd = br.ReadInt16();
                    cspd = br.ReadInt16();
                    refine = br.ReadUInt16();
                    pict_id = br.ReadUInt32();
                    slot = br.ReadUInt32();
                }
                if (item_version >= 2)
                {
                    currentSlot = br.ReadByte();
                    int count = br.ReadByte();
                    for (int i = 0; i < count; i++)
                    {
                        uint id = br.ReadUInt32();
                        if (Iris.IrisCardFactory.Instance.Items.ContainsKey(id))
                        {
                            cards.Add(Iris.IrisCardFactory.Instance.Items[id]);
                        }
                    }
                    locked = br.ReadBoolean();
                }
                if (item_version >= 3)
                {
                    rental = br.ReadBoolean();
                    rentalTime = DateTime.FromBinary(br.ReadInt64());
                }
                if (item_version >= 4)
                {
                    changeMode = br.ReadBoolean();
                    changeMode2 = br.ReadBoolean();
                }
                if (item_version >= 5)
                    for (int i = 0; i < 12; i++)
                        RefineType[(RefineType)i] = (ushort)br.ReadInt16();
                if (item_version >= 6)
                {
                    refine_sharp = br.ReadUInt32();
                    refine_enchanted = br.ReadUInt32();
                    refine_vitality = br.ReadUInt32();
                    refine_regeneration = br.ReadUInt32();
                    refine_lucky = br.ReadUInt32();
                    refine_dexterity = br.ReadUInt32();
                    refine_ATKrate = br.ReadUInt32();
                    refine_MATKrate = br.ReadUInt32();
                    refine_def = br.ReadUInt32();
                    refine_mdef = br.ReadUInt32();
                }
                if (item_version >= 7)
                {
                    refine_hit = br.ReadUInt32();
                    refine_mhit = br.ReadUInt32();
                    actorpartnerid = br.ReadUInt32();
                }
                if (item_version >= 8)
                {
                    name = br.ReadString();
                    partnerLevel = br.ReadByte();
                    partnerRebirth = br.ReadByte();
                    old = br.ReadBoolean();
                    potential = br.ReadBoolean();
                    release = br.ReadBoolean();
                }

                if (item_version >= 9)
                {
                    maxDurability = br.ReadUInt16();
                }

                if (item_version >= 10)
                {
                    lifeenhance = br.ReadByte();
                    powerenhance = br.ReadByte();
                    critenhance = br.ReadByte();
                    magenhance = br.ReadByte();
                }

                if (item_version >= 11)
                {
                    dye = br.ReadByte();
                }

                //if (item_version >= 12)
                //{
                //    Element[Elements.Neutral] = br.ReadInt16();
                //    Element[Elements.Holy] = br.ReadInt16();
                //    Element[Elements.Dark] = br.ReadInt16();
                //    Element[Elements.Fire] = br.ReadInt16();
                //    Element[Elements.Water] = br.ReadInt16();
                //    Element[Elements.Earth] = br.ReadInt16();
                //    Element[Elements.Wind] = br.ReadInt16();
                //}
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void Clear()
        {
            agi = 0;
            atk1 = 0;
            atk2 = 0;
            atk3 = 0;
            avoidCritical = 0;
            avoidMagic = 0;
            avoidMelee = 0;
            avoidRanged = 0;
            cha = 0;
            def = 0;
            dex = 0;
            hitCritical = 0;
            hitMagic = 0;
            hitMelee = 0;
            hitRanged = 0;
            hp = 0;
            hpRecover = 0;
            intel = 0;
            luk = 0;
            mag = 0;
            matk = 0;
            mdef = 0;
            mp = 0;
            mpRecover = 0;
            sp = 0;
            spRecover = 0;
            speedUp = 0;
            str = 0;
            vit = 0;
            volumeUp = 0;
            weightUp = 0;
            aspd = 0;
            cspd = 0;
            name = "";//虽然不明白是什么,还原吧
            partnerLevel = 0;
            partnerRebirth = 0;
            refine = 0;

            powerenhance = 0;
            critenhance = 0;
            lifeenhance = 0;
            magenhance = 0;

            old = false;
            potential = false;
            release = false;

            //错误备份
            //Element[Elements.Neutral] = 0;
            //Element[Elements.Holy] = 0;
            //Element[Elements.Dark] = 0;
            //Element[Elements.Fire] = 0;
            //Element[Elements.Water] = 0;
            //Element[Elements.Earth] = 0;
            //Element[Elements.Wind] = 0;

            dye = 0;
        }

        public Item Clone()
        {
            Item item = new Item();
            item.id = id;
            item.db_id = db_id;
            item.durability = durability;
            item.maxdurability = maxDurability;
            item.stack = stack;
            item.identified = identified;
            item.PossessionedActor = PossessionedActor;
            item.PossessionOwner = PossessionOwner;
            item.agi = agi;
            item.atk1 = atk1;
            item.atk2 = atk2;
            item.atk3 = atk3;
            item.avoidCritical = avoidCritical;
            item.avoidMagic = avoidMagic;
            item.avoidMelee = avoidMelee;
            item.avoidRanged = avoidRanged;
            item.cha = cha;
            item.def = def;
            item.dex = dex;
            item.hitCritical = hitCritical;
            item.hitMagic = hitMagic;
            item.hitMelee = hitMelee;
            item.hitRanged = hitRanged;
            item.hp = hp;
            item.hpRecover = hpRecover;
            item.intel = intel;
            item.luk = luk;
            item.mag = mag;
            item.matk = matk;
            item.mdef = mdef;
            item.mp = mp;
            item.mpRecover = mpRecover;
            item.sp = sp;
            item.spRecover = spRecover;
            item.speedUp = speedUp;
            item.stack = stack;
            item.str = str;
            item.vit = vit;
            item.volumeUp = volumeUp;
            item.weightUp = weightUp;
            item.refine = refine;
            item.aspd = aspd;
            item.cspd = cspd;
            item.pict_id = pict_id;
            item.currentSlot = currentSlot;
            item.maxSlot = maxSlot;
            item.locked = locked;
            item.changeMode = changeMode;
            item.changeMode2 = changeMode2;
            foreach (Iris.IrisCard i in cards)
            {
                item.cards.Add(i);
            }
            item.rental = rental;
            item.rentalTime = rentalTime;
            item.refine_sharp = refine_sharp;
            item.refine_enchanted = refine_enchanted;
            item.refine_vitality = refine_vitality;
            item.refine_regeneration = refine_regeneration;
            item.refine_lucky = refine_lucky;
            item.refine_dexterity = refine_dexterity;
            item.refine_ATKrate = refine_ATKrate;
            item.refine_MATKrate = refine_MATKrate;
            item.refine_def = refine_def;
            item.refine_mdef = refine_mdef;
            item.hit_refine = hit_refine;
            item.mhit_refine = mhit_refine;
            item.actorpartnerid = actorpartnerid;

            item.name = name;
            item.partnerLevel = partnerLevel;
            item.partnerRebirth = partnerRebirth;
            item.old = old;
            item.potential = potential;
            item.release = release;

            item.lifeenhance = lifeenhance;
            item.powerenhance = powerenhance;
            item.critenhance = critenhance;
            item.magenhance = magenhance;
            item.dye = dye;

            //错误备份
            //item.Element = new Dictionary<Elements, short>();
            //if (Element.Count != 7)
            //{
            //    Element.Add(Elements.Neutral, 0);
            //    Element.Add(Elements.Holy, 0);
            //    Element.Add(Elements.Dark, 0);
            //    Element.Add(Elements.Fire, 0);
            //    Element.Add(Elements.Water, 0);
            //    Element.Add(Elements.Earth, 0);
            //    Element.Add(Elements.Wind, 0);
            //}
            //else
            //    item.Element = Element;

            return item;
        }

        public bool Stackable
        {
            get
            {
                if (BaseData.stock == true)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 检查是否是装备 这样的判定太蛋疼了 有空我要改掉！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        /// </summary>
        public bool IsEquipt
        {
            get
            {
                int type = (int)BaseData.itemType;
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
                int type = (int)BaseData.itemType;
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
                switch (BaseData.itemType)
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
                switch (BaseData.itemType)
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
                switch (BaseData.itemType)
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
                switch (BaseData.itemType)
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
                switch (BaseData.itemType)
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
                switch (BaseData.itemType)
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
                switch (BaseData.itemType)
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
                        if (BaseData.doubleHand)
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
                        //slots.Add(EnumEquipSlot.PET);
                        slots.Add(EnumEquipSlot.BACK);
                        break;
                    case ItemType.PET_NEKOMATA:
                    case ItemType.PET:
                    case ItemType.RIDE_PET:
                    case ItemType.PARTNER:
                    case ItemType.RIDE_PARTNER:
                    case ItemType.RIDE_PET_ROBOT:
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
        /// <summary>
        /// 取得该Partner装备需要的装备槽 
        /// </summary>
        public List<EnumPartnerEquipSlot> PartnerEquipSlot
        {
            get
            {
                List<EnumPartnerEquipSlot> slots = new List<EnumPartnerEquipSlot>();
                if ((BaseData.itemType != ItemType.UNION_WEAPON) && (BaseData.itemType != ItemType.UNION_COSTUME))
                    Logger.ShowDebug("Cannot equip partner a non partner equipment item!", Logger.defaultlogger);
                switch (BaseData.itemType)
                {
                    //head&face
                    case ItemType.UNION_WEAPON:
                        slots.Add(EnumPartnerEquipSlot.WEAPON);
                        break;
                    case ItemType.UNION_COSTUME:
                        slots.Add(EnumPartnerEquipSlot.COSTUME);
                        break;
                }
                return slots;
            }
        }

        public ATTACK_TYPE AttackType
        {
            get
            {
                switch (BaseData.itemType)
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
                int[] lvs = new int[10] { 1, 30, 80, 150, 250, 370, 510, 660, 820, 999 }; //new settings
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
                Dictionary<ReleaseAbility, int> ability = i.ReleaseAbilities[(byte)vectors[i]];
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
        public Dictionary<Elements, int> IrisElements(bool deck)
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

        public static Dictionary<Elements, int> ElementsZero()
        {
            Dictionary<Elements, int> list = new Dictionary<Elements, int>();
            list.Add(SagaLib.Elements.Neutral, 0);
            list.Add(SagaLib.Elements.Fire, 0);
            list.Add(SagaLib.Elements.Water, 0);
            list.Add(SagaLib.Elements.Wind, 0);
            list.Add(SagaLib.Elements.Earth, 0);
            list.Add(SagaLib.Elements.Holy, 0);
            list.Add(SagaLib.Elements.Dark, 0);
            return list;
        }

        public uint ActorPartnerID
        {
            get
            {
                return actorpartnerid;
            }
            set
            {
                actorpartnerid = value;
            }
        }
    }
}
