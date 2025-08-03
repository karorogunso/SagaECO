using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    [Serializable]
    public partial class Actor
    {
        public List<int> SkillCombo = new List<int>();
        public Race Race;
        short lastX = 0;
        short lastY = 0;
        uint id;
        public ActorType type;
        string name;
        public uint region;
        public bool invisble;
        byte x2, y2;
        short x, y;
        ushort dir;
        public uint sightRange;
        uint mapID;
        [NonSerialized]
        Buff buff = new Buff();
        uint hp, mp, sp, max_hp, max_mp, max_sp, ep, max_ep;
        uint shieldhp;
        byte isseals, ishomicidal, homicidal, seals, darks, hotblade, hotblademark, plies;//圣印、暗刻、烈刃
        byte _MuSoUCount;
        byte _SwordACount;
        public bool castaway;
        byte speedcut, attackRhythm;
        short speed;

        public ActorSkill skillsong;

        [NonSerialized]
        Dictionary<string, SagaLib.MultiRunTask> tasks = new Dictionary<string, SagaLib.MultiRunTask>();
        [NonSerialized]
        Dictionary<SagaLib.Elements, int> elements = new Dictionary<SagaLib.Elements, int>();
        [NonSerialized]
        Dictionary<SagaLib.Elements, int> attackElements = new Dictionary<SagaLib.Elements, int>();

        public Elements ShieldElement
        {
            get
            {
                Elements ele = SagaLib.Elements.Neutral;
                int atkvalue = 0;
                foreach (var item in this.Elements)
                {
                    if (atkvalue < (item.Value + this.Status.elements_item[item.Key] + this.Status.elements_skill[item.Key] + this.Status.elements_iris[item.Key]))
                    {
                        ele = item.Key;
                        atkvalue = (item.Value + this.Status.elements_item[item.Key] + this.Status.elements_skill[item.Key] + this.Status.elements_iris[item.Key]);
                    }
                }
                return ele;
            }
        }

        public Elements WeaponElement
        {
            get
            {
                Elements ele = SagaLib.Elements.Neutral;
                int atkvalue = 0;
                foreach (var item in this.AttackElements)
                {
                    if (atkvalue < (item.Value + this.Status.attackElements_item[item.Key] + this.Status.attackElements_skill[item.Key] + this.Status.attackelements_iris[item.Key]))
                    {
                        ele = item.Key;
                        atkvalue = (item.Value + this.Status.attackElements_item[item.Key] + this.Status.attackElements_skill[item.Key] + this.Status.attackelements_iris[item.Key]);
                    }
                }
                return ele;
            }
        }

        [NonSerialized]
        Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
        uint range;
        [NonSerialized]
        List<uint> visibleActors = new List<uint>();
        [NonSerialized]
        List<Actor> slaves = new List<Actor>();

        [NonSerialized]
        Status status = null;
        bool noNewStatus = false;

        /// <summary>
        /// 不受极大化放大的技能ID
        /// </summary>
        public List<uint> ZenOutLst = new List<uint>();

        VariableHolder<string, string> tStrVar = new VariableHolder<string, string>("");
        VariableHolder<string, int> tIntVar = new VariableHolder<string, int>(0);
        VariableHolderA<string, BitMask> tMask = new VariableHolderA<string, BitMask>();
        VariableHolderA<string, DateTime> tTimeVar = new VariableHolderA<string, DateTime>();

        /// <summary>
        /// 怪物的战斗开始时间，用于计算持续多久战斗，在Aggro时计时为DateTime.Now.
        /// </summary>
        public DateTime BattleStartTime;

        /// <summary>
        /// 临时字符串变量集
        /// </summary>
        public VariableHolder<string, string> TStr { get { return this.tStrVar; } }
        /// <summary>
        /// 临时整数变量集
        /// </summary>
        public VariableHolder<string, int> TInt { get { return this.tIntVar; } }

        public VariableHolderA<string, DateTime> TTime { get { return this.tTimeVar; } }

        /// <summary>
        /// 外观
        /// </summary>
        public uint PictID;
        public uint IllusionPictID;  //所有Actor都有的幻化pictID

        /// <summary>
        /// Actor事件处理器
        /// </summary>
        public ActorEventHandler e;

        /// <summary>
        /// Actor的名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public short LastX
        {
            get { return lastX; }
            set { lastX = value; }
        }
        public short LastY
        {
            get { return lastY; }
            set { lastY = value; }
        }
        /// <summary>
        /// 此Actor在服务器存在的唯一标识ID
        /// </summary>
        public uint ActorID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// Actor所在地图ID
        /// </summary>
        public uint MapID
        {
            get
            {
                return mapID;
            }
            set
            {
                mapID = value;
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public virtual byte Level { get { return 0; } set { } }
        /// <summary>
        /// X坐标
        /// </summary>
        public byte X2 { get { return this.x2; } set { this.x2 = value; } }
        /// <summary>
        /// Y坐标
        /// </summary>
        public byte Y2 { get { return this.y2; } set { this.y2 = value; } }
        /// <summary>
        /// X坐标
        /// </summary>
        public short X { get { return this.x; } set { this.x = value; } }
        /// <summary>
        /// Y坐标
        /// </summary>
        public short Y { get { return this.y; } set { this.y = value; } }
        /// <summary>
        /// 面向方向，0－360
        /// </summary>
        public ushort Dir { get { return this.dir; } set { this.dir = value; } }
        /// <summary>
        /// 最终移动速度，请避免在技能和其他效果中直接对pc或者mob对象赋值（伪actor没有问题），如需要请赋值_item _iris _skill
        /// </summary>
        public ushort Speed
        {
            //似乎是移动速度判定
            #region 试图的解锁，失败
            //get
            //{
            //    if (this.type == ActorType.MOB)
            //    {
            //        if (this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill >= 0)
            //        {
            //            return (ushort)(this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill);
            //        }
            //    }
            //    if (this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill >= 0)
            //    {
            //        return (ushort)(this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill);
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}
            //set
            //{
            //    if (this.type == ActorType.MOB)
            //    {
            //        if (this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill >= 0)
            //        {
            //            this.speed = (short)(this.speed + this.Status.speed_skill);
            //        }
            //    }
            //    if (this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill >= 0)
            //    {
            //        this.speed = (short)(this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill);
            //    }
            //    else
            //    {
            //        this.speed = 0;
            //    }
            //    //暂时锁值，因为容易卡速度
            //    this.speed = (short)value;
            //    if (e != null) e.PropertyUpdate(UpdateEvent.SPEED, 0);
            //}表现非常不正常的减速效果，待议先留着
            #endregion
            get
            {
                //if(this.type == ActorType.MOB)
                //{
                //    if (this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill >= 0)
                //    {
                //        return (ushort)(this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill);
                //    }
                //}
                return (ushort)this.speed;
                /*if (this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill >= 0)
                {
                    return (ushort)(this.speed + this.Status.speed_item + this.Status.speed_iris + this.Status.speed_skill);
                }
                else
                {
                    return 0;
                }*///暂时锁值，因为容易卡速度
            }
            set
            {
                this.speed = (short)value;
                if (e != null) e.PropertyUpdate(UpdateEvent.SPEED, 0);
            }
        }
        /// <summary>
        /// 圣印标记（0为不触发，1为触发）
        /// </summary>
        public byte IsSeals { get { return this.isseals; } set { this.isseals = value; } }
        public byte IsHomicidal { get { return this.ishomicidal; } set { this.ishomicidal = value; } }
        /// <summary>
        /// 冰棍层数
        /// </summary>
        public byte Plies { get { return this.plies; } set { this.plies = value; } }

        /// <summary>
        /// 圣印层数
        /// </summary>
        public byte Seals { get { return this.seals; } set { this.seals = value; } }

        /// <summary>
        /// 殺意层数
        /// </summary>
        public byte Homicidal { get { return this.homicidal; } set { this.homicidal = value; } }

        /// <summary>
        /// 减速层数
        /// </summary>
        public byte SpeedCut { get { return this.speedcut; } set { this.speedcut = value; } }

        public byte AttackRhythm { get { return this.attackRhythm; } set { this.attackRhythm = value; } }
        /// <summary>
        /// 暗刻状态
        /// </summary>
        public byte Darks
        {
            get { return this.darks; }
            set { this.darks = value; }
        }
        /// <summary>
        /// 烈刃标记
        /// </summary>
        public byte HotBladeMark
        {
            get { return this.hotblademark; }
            set { this.hotblademark = value; }
        }
        /// <summary>
        /// 烈刃层数
        /// </summary>
        public byte HotBlade
        {
            get { return this.hotblade; }
            set { this.hotblade = value; }
        }

        /// <summary>
        /// 无双斩击HIT数
        /// </summary>
        public byte MuSoUCount
        {
            get { return _MuSoUCount; }
            set { this._MuSoUCount = value; }
        }

        /// <summary>
        /// 利剑连袭HIT数
        /// </summary>
        public byte SwordACount
        {
            get { return _SwordACount; }
            set { this._SwordACount = value; }
        }


        private int killingmarkcounter = 0;
        /// <summary>
        /// 杀戮标记计数器
        /// </summary>
        public int KillingMarkCounter
        {
            get { return killingmarkcounter; }
            set { killingmarkcounter = value; }
        }

        private bool killingmarksouluse = false;

        /// <summary>
        /// 是否零件使用的杀戮标记
        /// </summary>
        public bool KillingMarkSoulUse
        {
            get { return killingmarksouluse; }
            set { killingmarksouluse = value; }
        }
        /// <summary>
        /// 护盾值（受伤优先扣除）
        /// </summary>
        public uint SHIELDHP { get { return this.shieldhp; } set { this.shieldhp = value; } }
        /// <summary>
        /// 生命
        /// </summary>
        public uint HP { get { return this.hp; } set { this.hp = value; } }
        /// <summary>
        /// 魔法
        /// </summary>
        public uint MP { get { return this.mp; } set { this.mp = value; } }
        /// <summary>
        /// 体力
        /// </summary>
        public uint SP { get { return this.sp; } set { this.sp = value; } }
        /// <summary>
        /// 最大生命
        /// </summary>
        public uint MaxHP { get { return this.max_hp; } set { this.max_hp = value; } }
        /// <summary>
        /// 最大魔法
        /// </summary>
        public uint MaxMP { get { return this.max_mp; } set { this.max_mp = value; } }
        /// <summary>
        /// 最大体力
        /// </summary>
        public uint MaxSP { get { return this.max_sp; } set { this.max_sp = value; } }

        public uint EP
        {
            get
            {
                return this.ep;
            }
            set
            {
                ep = value;
            }
        }

        public uint MaxEP { get { return this.max_ep; } set { this.max_ep = value; } }

        /// <summary>
        /// 所有属性相关
        /// </summary>
        public Status Status
        {
            get
            {
                if (status == null && !noNewStatus)
                {
                    status = new Status(this);
                    noNewStatus = true;
                }
                return this.status;
            }
            set { this.status = value; }
        }
        /// <summary>
        /// 射程
        /// </summary>
        public uint Range
        {
            get
            {
                return this.range;
            }
            set
            {
                this.range = value;
            }
        }


        /// <summary>
        /// 该Actor执行中的系统任务
        /// </summary>
        public Dictionary<string, SagaLib.MultiRunTask> Tasks { get { return this.tasks; } }

        /// <summary>
        /// 该Actor的附加状态
        /// </summary>
        public Buff Buff { get { return this.buff; } }

        /// <summary>
        /// 此Actor可见的Actor
        /// </summary>
        public List<uint> VisibleActors { get { return this.visibleActors; } }

        /// <summary>
        /// 防御元素属性值
        /// </summary>
        public Dictionary<SagaLib.Elements, int> Elements
        {
            get
            {
                if (this.elements.Count == 0)
                {
                    this.elements.Add(SagaLib.Elements.Neutral, 0);
                    this.elements.Add(SagaLib.Elements.Fire, 0);
                    this.elements.Add(SagaLib.Elements.Water, 0);
                    this.elements.Add(SagaLib.Elements.Wind, 0);
                    this.elements.Add(SagaLib.Elements.Earth, 0);
                    this.elements.Add(SagaLib.Elements.Holy, 0);
                    this.elements.Add(SagaLib.Elements.Dark, 0);
                }
                return this.elements;
            }
        }

        /// <summary>
        /// 攻击元素属性值
        /// </summary>
        public Dictionary<SagaLib.Elements, int> AttackElements
        {
            get
            {
                if (this.attackElements.Count == 0)
                {
                    this.attackElements.Add(SagaLib.Elements.Neutral, 0);
                    this.attackElements.Add(SagaLib.Elements.Fire, 0);
                    this.attackElements.Add(SagaLib.Elements.Water, 0);
                    this.attackElements.Add(SagaLib.Elements.Wind, 0);
                    this.attackElements.Add(SagaLib.Elements.Earth, 0);
                    this.attackElements.Add(SagaLib.Elements.Holy, 0);
                    this.attackElements.Add(SagaLib.Elements.Dark, 0);
                }
                return this.attackElements;
            }
        }

        public Dictionary<AbnormalStatus, short> AbnormalStatus
        {
            get
            {
                if (this.abnormalStatus.Count == 0)
                {
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Confused, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Frosen, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Paralyse, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Poisen, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Silence, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Sleep, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Stone, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Stun, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.MoveSpeedDown, 0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.SkillForbid, 0);
                }
                return this.abnormalStatus;
            }
        }

        /// <summary>
        /// 该Actor的附属Actor
        /// </summary>
        public List<Actor> Slave
        {
            get
            {
                return this.slaves;
            }
        }

        /// <summary>
        /// 该Actor的附属Actor，不会因为随从死亡而无法访问
        /// </summary>
        public List<Actor> SettledSlave = new List<Actor>();

        /// <summary>
        /// 清除该Actor所有任务以及状态
        /// </summary>
        public void ClearTaskAddition()
        {
            foreach (MultiRunTask i in this.Tasks.Values)
            {
                try
                {
                    i.Deactivate();
                }
                catch (Exception)
                {
                }
            }
            Addition[] additionlist = new Addition[this.Status.Additions.Count];
            this.Status.Additions.Values.CopyTo(additionlist, 0);
            foreach (Addition i in additionlist)
            {
                try
                {
                    if (i.Activated)
                    {
                        i.AdditionEnd();
                    }
                }
                catch
                {
                }
            }
            this.Status.Additions.Clear();
            this.Tasks.Clear();
        }
    }
}
