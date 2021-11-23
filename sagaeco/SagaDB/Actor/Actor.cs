using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using SagaLib;

namespace SagaDB.Actor
{
    [Serializable]
    public partial class Actor
    {
        /// <summary>
        /// 用于定义BUFF额外伤害效果的委托
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="dActor"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        public delegate int BuffCallBack(Actor sActor, Actor dActor, int damage);
        /// <summary>
        /// 该Actor的BUFF委托列表
        /// </summary>
        public IConcurrentDictionary<string, BuffCallBack> OnBuffCallBackList = new IConcurrentDictionary<string, BuffCallBack>();

        public List<int> SkillCombo = new List<int>();

        short lastX = 0;
        short lastY = 0;
        uint id;
        public ActorType type;
        public string name;
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

        public bool castaway;
        byte speedcut, attackRhythm;
        short speed;

        [NonSerialized]
        IConcurrentDictionary<string, MultiRunTask> tasks = new IConcurrentDictionary<string, MultiRunTask>();
        [NonSerialized]
        Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
        [NonSerialized]
        Dictionary<Elements, int> attackElements = new Dictionary<Elements, int>();

        [NonSerialized]
        Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
        uint range;
        [NonSerialized]
        IConcurrentDictionary<uint, uint> visibleActors = new IConcurrentDictionary<uint, uint>();
        [NonSerialized]
        List<Actor> slaves = new List<Actor>();

        [NonSerialized]
        Status status = null;
        bool noNewStatus = false;

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

        public string nametemp;
        /// <summary>
        /// Actor的名称
        /// </summary>
        public string Name
        {
            get
            {
                if (TInt["randomPict"] == 1)
                    return nametemp;
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
            get
            {
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
                if (speed != (short)value) //只有在速度和原本发生改变时才发送速度封包
                {
                    speed = (short)value;
                    if (e != null) e.PropertyUpdate(UpdateEvent.SPEED, 0);
                }
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
        /// 护盾值（受伤优先扣除）
        /// </summary>
        public uint SHIELDHP { get { return this.shieldhp; } set { this.shieldhp = value; } }
        /// <summary>
        /// 生命
        /// </summary>
        public uint HP { get { if (Buff.Dead) return 0; else return this.hp; } set { if (Buff.Dead && value > 0) value = 0; hp = value; if (hp > uint.MaxValue / 2) hp = 0; else if (hp > max_hp) hp = max_hp; } }
        /// <summary>
        /// 魔法
        /// </summary>
        public uint MP { get { return this.mp; } set { this.mp = value; if (mp > uint.MaxValue / 2) mp = 0; else if (mp > max_mp) mp = max_mp; } }
        /// <summary>
        /// 体力
        /// </summary>
        public uint SP { get { return this.sp; } set { this.sp = value; if (sp > uint.MaxValue / 2) sp = 0; else if (sp > max_sp) sp = max_sp; } }
        /// <summary>
        /// 最大生命
        /// </summary>
        public uint MaxHP { get { return this.max_hp; } set { this.max_hp = value; if (hp > max_hp) hp = max_hp; } }
        /// <summary>
        /// 最大魔法
        /// </summary>
        public uint MaxMP { get { return this.max_mp; } set { this.max_mp = value; if (mp > max_mp) mp = max_mp; } }
        /// <summary>
        /// 最大体力
        /// </summary>
        public uint MaxSP { get { return this.max_sp; } set { this.max_sp = value; if (sp > max_sp) sp = max_sp; } }

        public uint EP
        {
            get
            {
                return this.ep;
            }
            set
            {
                ep = value;
                if (ep > uint.MaxValue / 2) ep = 0;
                else if (ep > max_ep) ep = max_ep;
            }
        }

        public uint MaxEP { get { return this.max_ep; } set { this.max_ep = value; if (ep > max_ep) ep = max_ep; } }

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
        /// 吟游诗人暗信仰值
        /// </summary>
        public ushort BeliefDark
        {
            get { return (ushort)Math.Max(0, (status.BeliefBalace - ep)); } //信仰光时，反馈为0而不是负值。
            set { EP = (uint)status.BeliefBalace - value; } //EP会截断溢出数值。
        }

        /// <summary>
        /// 吟游诗人光信仰值
        /// </summary>
        public ushort BeliefLight
        {
            get { return (ushort)Math.Max(0, (ep - status.BeliefBalace)); } //信仰暗时，反馈为0而不是负值。
            set { EP = (uint)status.BeliefBalace + value; } //EP会截断溢出数值。
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
        public IConcurrentDictionary<string, MultiRunTask> Tasks { get { return this.tasks; } }

        /// <summary>
        /// 该Actor的附加状态
        /// </summary>
        public Buff Buff { get { return this.buff; } }

        /// <summary>
        /// 此Actor可见的Actor
        /// </summary>
        public IConcurrentDictionary<uint, uint>  VisibleActors { get { return this.visibleActors; } }

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
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.鈍足, 0);
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
        public void ClearTaskAddition(bool includeStableAddition = false)
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
            string[] keys = new string[Status.Additions.Count];
            Status.Additions.Keys.CopyTo(keys, 0);
            foreach (string i in keys)
            {
                try
                {
                    if (Status.Additions[i].Activated && (Status.Additions[i].MyType != Addition.AdditionType.Stable || includeStableAddition))//includeStableAddition为false时，不移除StableAddition
                    {
                        Status.Additions[i].AdditionEnd();
                        Status.Additions.Remove(i);
                    }
                }
                catch
                {
                }
            }
            //this.Status.Additions.Clear();
            this.Tasks.Clear();
        }
        
    }
}
