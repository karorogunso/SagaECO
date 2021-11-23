using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaDB.Skill
{
    public enum SkillFlags
    {
        NONE=0,
        NO_INTERRUPT=0x1,
        MAGIC=0x2,
        PHYSIC=0x4,
        PARTY_ONLY=0x8,
        ATTACK=0x10,
        CAN_HAS_TARGET=0x20,
        SUPPORT=0x40,
        HOLY=0x80,

        DEAD_ONLY=0x200,
        KIT_RELATED=0x400,
        NO_POSSESSION=0x800,
        NOT_BEEN_POSSESSED=0x1000,
        HEART_SKILL=0x2000,
    }

    public enum EquipFlags
    {
        HAND = 0x1,
        SWORD = 0x2,
        SHORT_SWORD = 0x4,
        HAMMER = 0x8,
        AXE = 0x10,
        SPEAR = 0x20,
        THROW = 0x40,
        BOW = 0x80,
        SHIELD = 0x100,
        GUN = 0x200,
        BAG = 0x400,
        CLAW = 0x800,
        RAPIER = 0x1000,
        KNUCKLE = 0x2000,
        DUALGUN = 0x4000,
        RIFLE = 0x8000,
        STRINGS = 0x10000,
        INSTRUMENT2 = 0x20000,
        ROPE = 0x40000,
        CARD = 0x80000,
        NONE = 0x100000,
        BOOK = 0x200000,
        STAFF = 0x400000,
        ETC_WEAPON = 0x800000,
        EQ_ALLSLOT =0x1000000,
        HANDBAG = 0x2000000,
        LEFT_HANDBAGHANDBAG = 0x4000000,
        //DEM打 0x800000,
        //DEM斩 0x1000000,
        //DEM刺 0x2000000,
        //DEM远 0x4000000,

    }

    public class SkillData
    {
        public uint id;
        public ushort icon;
        public string name;
        public string description;
        public bool active;
        public byte maxLv, lv;
        public byte target, target2, effectRange, castRange;
        public sbyte range;
        public ushort mp, sp, ep;
        public byte joblv;
        public uint effect;
        public int eFlag1, eFlag2, eFlag3;
        public int castTime, delay, SingleCD;
        public ushort nHumei4, nHumei5, nHumei6, nHumei7, nHumei9, nHumei10, nAnim1, nAnim2, nAnim3;
        public int skillFlag, nHumei8, effect1, effect2, effect3, effect4, effect5, effect6, effect7, effect8, effect9, nHumei2;
        public BitMask<SkillFlags> flag = new BitMask<SkillFlags>();
        public BitMask<EquipFlags> equipFlag = new BitMask<EquipFlags>();

        public override string ToString()
        {
            return this.name;
        }
    }

    public class Skill
    {
        SkillData baseData;
        byte lv;
        byte joblv;
        bool nosave;

        public SkillData BaseData { get { return this.baseData; } set { this.baseData = value; } }
        public uint ID { get { return this.baseData.id; } }
        public string Name { get { return this.baseData.name; } }
        public bool NoSave { get { return this.nosave; } set { this.nosave = value; } }
        public byte MaxLevel { get { return this.baseData.maxLv; } }
        public byte Level { get { return this.lv; } set { this.lv = value; } }
        public byte JobLv { get { return this.joblv; } set { this.joblv = value; } }
        public ushort MP { get { return this.baseData.mp; } }
        public ushort SP { get { return this.baseData.sp; } }
        public ushort EP { get { return this.baseData.ep; } }
        public sbyte Range { get { return this.baseData.range; } }
        public uint Effect { get { return this.baseData.effect; } set { this.baseData.effect = value; } }
        public byte EffectRange { get { return this.baseData.effectRange; } set { this.baseData.effectRange = value; } }
        public byte CastRange { get { return this.baseData.castRange; } }
        public byte Target { get { return this.baseData.target; } set { this.baseData.target = value; } }
        public byte Target2 { get { return this.baseData.target2; } set { this.baseData.target2 = value; } }
        public int CastTime { get { return this.baseData.castTime; } }
        public int Delay { get { return this.baseData.delay; } }
        public int SinglgCD { get { return this.baseData.SingleCD; } }
        public bool Magical { get { return this.baseData.flag.Test(SkillFlags.MAGIC); } }
        public bool Physical { get { return this.baseData.flag.Test(SkillFlags.PHYSIC); } }
        public bool PartyOnly { get { return this.baseData.flag.Test(SkillFlags.PARTY_ONLY); } }
        public bool Attack { get { return this.baseData.flag.Test(SkillFlags.ATTACK); } }
        public bool CanHasTarget { get { return this.baseData.flag.Test(SkillFlags.CAN_HAS_TARGET); } }
        public bool Support { get { return this.baseData.flag.Test(SkillFlags.SUPPORT); } }
        public bool DeadOnly { get { return this.baseData.flag.Test(SkillFlags.DEAD_ONLY); } }
        public bool NoPossession { get { return this.baseData.flag.Test(SkillFlags.NO_POSSESSION); } }
        public bool NotBeenPossessed { get { return this.baseData.flag.Test(SkillFlags.NOT_BEEN_POSSESSED); } }
        

        public override string ToString()
        {
            return this.baseData.name;
        }
    }
}
