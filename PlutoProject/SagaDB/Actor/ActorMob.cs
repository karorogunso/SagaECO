using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class ActorMob : Actor, IStats
    {
        protected Mob.MobData baseData;

        byte level;

        public uint MobID { get { return this.baseData.id; } }
        public ushort Str { get { return this.baseData.str; } set { } }
        public ushort Dex { get { return this.baseData.dex; } set { } }
        public ushort Int { get { return this.baseData.intel; } set { } }
        public ushort Vit { get { return this.baseData.vit; } set { } }
        public ushort Agi { get { return this.baseData.agi; } set { } }
        public ushort Mag { get { return this.baseData.mag; } set { } }
        public override byte Level
        {
            get
            {
                if (level != baseData.level)
                    return level;
                else
                    return baseData.level;
            }
            set
            {
                level = value;
            }
        }

        public uint baseExp = 0, jobExp = 0;
        public bool FirstDefending = false;
        public List<Mob.MobData.DropData> dropItems = new List<Mob.MobData.DropData>();
        public List<Mob.MobData.DropData> dropItemsSpecial = new List<Mob.MobData.DropData>();
        public float range = 0;
        VariableHolder<string, int> tIntVar = new VariableHolder<string, int>(0);
        VariableHolder<string, string> tStrVar = new VariableHolder<string, string>("");
        Actor owner;
        public Actor Owner { get { return this.owner; } set { this.owner = value; } }

        /// <summary>
        /// Another怪物ID
        /// </summary>
        public uint AnotherID;
        /// <summary>
        /// 骑宠ID
        /// </summary>
        public uint RideID;
        /// <summary>
        /// 阵营 0敌人 1友军 2中立（绿名）
        /// </summary>
        public uint Camp;
        /// <summary>
        /// 记录被攻击，事件用
        /// </summary>
        public byte AttackedForEvent;

        /// <summary>
        /// 强制的攻击目标，挑衅使用
        /// </summary>
        public Actor PriorityTartget;

        public Mob.MobData BaseData
        {
            get
            {
                return this.baseData;
            }
        }
        public class MobInfo
        {
            public byte level = 0;
            public float range = 0;
            public uint maxhp, maxmp, maxsp;
            public string name;
            public ushort speed, atk_min, atk_max, matk_min, matk_max, def, mdef, def_add, mdef_add, hit_magic, hit_melee, hit_ranged, hit_critical, avoid_magic,
                avoid_melee, avoid_ranged, avoid_critical;
            public bool undead;
            public short Aspd, Cspd;
            public Dictionary<SagaLib.Elements, int> elements = new Dictionary<SagaLib.Elements, int>();
            public Dictionary<SagaLib.AbnormalStatus, short> abnormalstatus = new Dictionary<SagaLib.AbnormalStatus, short>();
            public Race Race;
            public ATTACK_TYPE AttackType;
            public uint baseExp, jobExp;
            public List<Mob.MobData.DropData> dropItems = new List<Mob.MobData.DropData>();
            public List<Mob.MobData.DropData> dropItemsSpecial = new List<Mob.MobData.DropData>();
            public MobInfo()
            {
                this.elements.Add(SagaLib.Elements.Neutral, 0);
                this.elements.Add(SagaLib.Elements.Fire, 0);
                this.elements.Add(SagaLib.Elements.Water, 0);
                this.elements.Add(SagaLib.Elements.Wind, 0);
                this.elements.Add(SagaLib.Elements.Earth, 0);
                this.elements.Add(SagaLib.Elements.Holy, 0);
                this.elements.Add(SagaLib.Elements.Dark, 0);

                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Confused, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Frosen, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Paralyse, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Poisen, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Silence, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Sleep, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Stone, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.Stun, 0);
                this.abnormalstatus.Add(SagaLib.AbnormalStatus.MoveSpeedDown, 0);
            }
            /*public uint MaxHP { get { return this.maxhp; } set { } }
            public uint MaxMP { get { return this.maxmp; } set { } }
            public uint MaxSP { get { return this.maxsp; } set { } }
            public string Name { get { return this.name; } set { } }
            public ushort Speed { get { return this.speed; } set { } }
            public ushort Atk_min { get { return this.atk_min; } set { } }
            public ushort Atk_max { get { return this.atk_max; } set { } }
            public ushort Matk_min { get { return this.matk_min; } set { } }
            public ushort Matk_max { get { return this.matk_max; } set { } }

            public Dictionary<SagaLib.Elements, int> Elements { get { return this.elements; } set { } }
            public Dictionary<SagaLib.AbnormalStatus, short> Abnormalstatus { get { return this.abnormalstatus; } set { } }*/
        }
        public ActorMob()
        {
        }

        public ActorMob(uint mobID)
        {
            this.type = ActorType.MOB;
            this.baseData = Mob.MobFactory.Instance.GetMobData(mobID);
            this.Level = this.BaseData.level;
            this.MaxHP = this.baseData.hp;
            this.HP = this.MaxHP;
            this.MaxMP = this.baseData.mp;
            this.MP = this.MaxMP;
            this.MaxSP = this.baseData.sp;
            this.SP = this.MaxSP;
            this.Name = this.baseData.name;
            this.Speed = this.baseData.speed;
            this.Status.attackType = this.baseData.attackType;
            this.Status.aspd = this.baseData.aspd;
            this.Status.cspd = this.baseData.cspd;
            this.Status.def = this.baseData.def;
            this.Status.def_add = (short)this.baseData.def_add;
            this.Status.mdef = this.baseData.mdef;
            this.Status.mdef_add = (short)this.baseData.mdef_add;
            this.Status.min_atk1 = this.baseData.atk_min;
            this.Status.max_atk1 = this.baseData.atk_max;
            this.Status.min_atk2 = this.baseData.atk_min;
            this.Status.max_atk2 = this.baseData.atk_max;
            this.Status.min_atk3 = this.baseData.atk_min;
            this.Status.max_atk3 = this.baseData.atk_max;
            this.Status.min_matk = this.baseData.matk_min;
            this.Status.max_matk = this.baseData.matk_max;
            this.Status.hit_critical = this.baseData.cri;
            this.Status.avoid_critical = this.baseData.criavd;
            this.Status.hit_magic = this.baseData.hit_magic;
            this.Status.avoid_magic = this.baseData.avoid_magic;
            this.Race = this.baseData.race;
            foreach (SagaLib.Elements i in baseData.elements.Keys)
            {
                this.Elements[i] = this.baseData.elements[i];
                this.AttackElements[i] = 0;
            }
            foreach (SagaLib.AbnormalStatus i in baseData.abnormalStatus.Keys)
            {
                this.AbnormalStatus[i] = this.baseData.abnormalStatus[i];
            }
            this.Status.hit_melee = this.baseData.hit_melee;
            this.Status.hit_ranged = this.baseData.hit_ranged;
            this.Status.avoid_melee = this.baseData.avoid_melee;
            this.Status.avoid_ranged = this.baseData.avoid_ranged;

            this.Status.undead = this.baseData.undead;
        }
        public ActorMob(uint mobID, MobInfo info)
        {
            this.type = ActorType.MOB;
            this.baseData = Mob.MobFactory.Instance.GetMobData(mobID);
            if (info.level != 0)
                this.Level = info.level;
            this.MaxHP = info.maxhp;
            this.HP = info.maxhp;
            this.MaxMP = info.maxmp;
            this.MP = info.maxmp;
            this.MaxSP = info.maxsp;
            this.SP = info.maxsp;
            this.Name = info.name;
            this.Speed = info.speed;
            if (info.AttackType != null)
                this.Status.attackType = info.AttackType;
            else
                this.Status.attackType = this.baseData.attackType;
            this.Status.aspd = info.Aspd;
            this.Status.cspd = info.Cspd;
            this.Status.def = info.def;
            this.Status.def_add = (short)info.def_add;
            this.Status.mdef = info.mdef;
            this.Status.mdef_add = (short)info.mdef_add;
            this.Status.min_atk1 = info.atk_min;
            this.Status.max_atk1 = info.atk_max;
            this.Status.min_atk2 = info.atk_min;
            this.Status.max_atk2 = info.atk_max;
            this.Status.min_atk3 = info.atk_min;
            this.Status.max_atk3 = info.atk_max;
            this.Status.min_matk = info.matk_min;
            this.Status.max_matk = info.matk_max;
            this.Status.hit_critical = info.hit_critical;
            this.Status.avoid_critical = info.avoid_critical;
            this.Status.hit_magic = info.hit_magic;
            this.Status.avoid_magic = info.avoid_magic;
            this.Race = info.Race;

            foreach (SagaLib.Elements i in baseData.elements.Keys)
            {
                this.Elements[i] = info.elements[i];
                this.AttackElements[i] = 0;
            }
            foreach (SagaLib.AbnormalStatus i in baseData.abnormalStatus.Keys)
            {
                this.AbnormalStatus[i] = info.abnormalstatus[i];
            }
            this.Status.hit_melee = info.hit_melee;
            this.Status.hit_ranged = info.hit_ranged;
            this.Status.avoid_melee = info.avoid_melee;
            this.Status.avoid_ranged = info.avoid_ranged;

            this.Status.undead = info.undead;
            this.range = info.range;
            this.baseExp = info.baseExp;
            this.jobExp = info.jobExp;

            this.dropItems = new List<Mob.MobData.DropData>();
            this.dropItems = info.dropItems;

            this.dropItemsSpecial = new List<Mob.MobData.DropData>();
            this.dropItemsSpecial = info.dropItemsSpecial;
        }
    }
}
