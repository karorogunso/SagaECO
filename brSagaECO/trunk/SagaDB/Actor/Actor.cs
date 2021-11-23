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
        public ActorType type;
        public uint region;
        public bool invisble;

        public uint sightRange;
        
        /// <summary>
        /// 不受极大化放大的技能ID
        /// </summary>
        public List<uint> ZenOutLst = new List<uint>();

        /// <summary>
        /// 外观
        /// </summary>
        public uint PictID;

        /// <summary>
        /// Actor事件处理器
        /// </summary>
        public ActorEventHandler e;
        

        string name;
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
        short lastX = 0;
        short lastY = 0;
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

        uint id;
        /// <summary>
        /// 此Actor在服务器存在的唯一标识ID
        /// </summary>
        public uint ActorID { get { return this.id; } set { this.id = value; } }

        uint mapID;
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

        byte ac_add_rate;
        /// <summary>
        /// 异常命中率提升
        /// </summary>
        public byte Ac_add_rate { get { return this.ac_add_rate; } set { this.ac_add_rate = value; } }


        /// <summary>
        /// 等级
        /// </summary>
        public virtual byte Level { get { return 0; } set { } }

        byte x2, y2;
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
        short x, y;
        public short X { get { return this.x; } set { this.x = value; } }
        /// <summary>
        /// Y坐标
        /// </summary>
        public short Y { get { return this.y; } set { this.y = value; } }
        ushort dir;
        /// <summary>
        /// 面向方向，0－360
        /// </summary>
        public ushort Dir { get { return this.dir; } set { this.dir = value; } }
        ushort speed = 420;
        /// <summary>
        /// 移动速度，玩家默认为420
        /// </summary>
        public ushort Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                //int sp = value - this.Status.speed_up;
                //if (sp >= 0)
                this.speed = value;
                //else
                //    this.Status.speed_up = 0;
                if (e != null) e.PropertyUpdate(UpdateEvent.SPEED, 0);
            }
        }

        byte isseals, seals, darks, hotblade, hotblademark;
        /// <summary>
        /// 圣印标记（0为不触发，1为触发）
        /// </summary>
        public byte IsSeals
        {
            get { return this.isseals; }
            set { this.isseals = value; }
        }
        /// <summary>
        /// 圣印层数
        /// </summary>
        public byte Seals
        {
            get { return this.seals; }
            set { this.seals = value; }
        }
        /// <summary>
        /// 暗刻状态
        /// </summary>
        public byte Darks
        {
            get { return this.darks; }
            set { this.darks = value; }
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
        /// 烈刃标记
        /// </summary>
        public byte HotBladeMark
        {
            get { return this.hotblademark; }
            set { this.hotblademark = value; }
        }
        byte poison, speedcut, attackRhythm;
        /// <summary>
        /// 减速层数
        /// </summary>
        public byte SpeedCut
        {
            get { return this.speedcut; }
            set { this.speedcut = value; }
        }
        public byte AttackRhythm
        {
            get { return this.attackRhythm; }
            set { this.attackRhythm = value; }
        }

        byte _MuSoUCount;
        /// <summary>
        /// 无双斩击HIT数
        /// </summary>
        public byte MuSoUCount
        {
            get { return _MuSoUCount; }
            set { this._MuSoUCount = value; }
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

        float criDamageUP;
        /// <summary>
        /// 暴击伤害提升率
        /// </summary>
        public float CriDamageUP
        {
            get { return this.criDamageUP; }
            set { this.criDamageUP = value; }
        }

        uint hp, mp, sp, max_hp, max_mp, max_sp, ep, max_ep;
        /// <summary>
        /// 生命值
        /// </summary>
        public uint HP { get { return this.hp; } set { this.hp = value; } }
        /// <summary>
        /// 魔力值
        /// </summary>
        public uint MP { get { return this.mp; } set { this.mp = value; } }
        /// <summary>
        /// 精力值
        /// </summary>
        public uint SP { get { return this.sp; } set { this.sp = value; } }
        /// <summary>
        /// 最大生命值
        /// </summary>
        public uint MaxHP { get { return this.max_hp; } set { this.max_hp = value; } }
        /// <summary>
        /// 最大魔力值
        /// </summary>
        public uint MaxMP { get { return this.max_mp; } set { this.max_mp = value; } }
        /// <summary>
        /// 最大精力值
        /// </summary>
        public uint MaxSP { get { return this.max_sp; } set { this.max_sp = value; } }
        /// <summary>
        /// 能量值
        /// </summary>
        public uint EP {get { return this.ep; } set {this.ep = value;}}
        /// <summary>
        /// 最大能量值
        /// </summary>
        public uint MaxEP { get { return this.max_ep; } set { this.max_ep = value; } }

        [NonSerialized]
        Status status = null;
        bool noNewStatus = false;
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

        uint range;
        /// <summary>
        /// 射程
        /// </summary>
        public uint Range { get { return this.range; } set { this.range = value; } }

        [NonSerialized]
        Dictionary<string, SagaLib.MultiRunTask> tasks = new Dictionary<string, SagaLib.MultiRunTask>();
        /// <summary>
        /// 该Actor执行中的系统任务
        /// </summary>
        public Dictionary<string, SagaLib.MultiRunTask> Tasks { get { return this.tasks; } }

        [NonSerialized]
        Buff buff = new Buff();
        /// <summary>
        /// 该Actor的附加状态
        /// </summary>
        public Buff Buff { get { return this.buff; } }

        [NonSerialized]
        List<uint> visibleActors = new List<uint>();
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
                Dictionary<SagaLib.Elements, int> eles = new Dictionary<Elements, int>();
                eles.Add(SagaLib.Elements.Neutral, this.status.elements_item[SagaLib.Elements.Neutral] + this.status.elements_skill[SagaLib.Elements.Neutral] + this.Status.elements_base[SagaLib.Elements.Neutral]);
                eles.Add(SagaLib.Elements.Fire, this.status.elements_item[SagaLib.Elements.Fire] + this.status.elements_skill[SagaLib.Elements.Fire] + this.status.elements_base[SagaLib.Elements.Fire]);
                eles.Add(SagaLib.Elements.Water, this.status.elements_item[SagaLib.Elements.Water] + this.status.elements_skill[SagaLib.Elements.Water] + this.status.elements_base[SagaLib.Elements.Water]);
                eles.Add(SagaLib.Elements.Wind, this.status.elements_item[SagaLib.Elements.Wind] + this.status.elements_skill[SagaLib.Elements.Wind] + this.status.elements_base[SagaLib.Elements.Wind]);
                eles.Add(SagaLib.Elements.Earth, this.status.elements_item[SagaLib.Elements.Earth] + this.status.elements_skill[SagaLib.Elements.Earth] + this.status.elements_base[SagaLib.Elements.Earth]);
                eles.Add(SagaLib.Elements.Holy, this.status.elements_item[SagaLib.Elements.Holy] + this.status.elements_skill[SagaLib.Elements.Holy] + this.status.elements_base[SagaLib.Elements.Holy]);
                eles.Add(SagaLib.Elements.Dark, this.status.elements_item[SagaLib.Elements.Dark] + this.status.elements_skill[SagaLib.Elements.Dark] + this.status.elements_base[SagaLib.Elements.Dark]);
                return eles;
            }
        }

        /// <summary>
        /// 攻击元素属性值
        /// </summary>
        public Dictionary<SagaLib.Elements, int> AttackElements
        {
            get
            {
                Dictionary<SagaLib.Elements, int> eles = new Dictionary<Elements, int>();
                eles.Add(SagaLib.Elements.Neutral, this.status.attackElements_item[SagaLib.Elements.Neutral] + this.status.attackElements_skill[SagaLib.Elements.Neutral] + this.status.attackElements_base[SagaLib.Elements.Neutral]);
                eles.Add(SagaLib.Elements.Fire, this.status.attackElements_item[SagaLib.Elements.Fire] + this.status.attackElements_skill[SagaLib.Elements.Fire] + this.status.attackElements_base[SagaLib.Elements.Fire]);
                eles.Add(SagaLib.Elements.Water, this.status.attackElements_item[SagaLib.Elements.Water] + this.status.attackElements_skill[SagaLib.Elements.Water] + this.status.attackElements_base[SagaLib.Elements.Water]);
                eles.Add(SagaLib.Elements.Wind, this.status.attackElements_item[SagaLib.Elements.Wind] + this.status.attackElements_skill[SagaLib.Elements.Wind] + this.status.attackElements_base[SagaLib.Elements.Wind]);
                eles.Add(SagaLib.Elements.Earth, this.status.attackElements_item[SagaLib.Elements.Earth] + this.status.attackElements_skill[SagaLib.Elements.Earth] + this.status.attackElements_base[SagaLib.Elements.Earth]);
                eles.Add(SagaLib.Elements.Holy, this.status.attackElements_item[SagaLib.Elements.Holy] + this.status.attackElements_skill[SagaLib.Elements.Holy] + this.status.attackElements_base[SagaLib.Elements.Dark]);
                eles.Add(SagaLib.Elements.Dark, this.status.attackElements_item[SagaLib.Elements.Dark] + this.status.attackElements_skill[SagaLib.Elements.Dark] + this.status.attackElements_base[SagaLib.Elements.Holy]);
                return eles;
            }
        }
        
        [NonSerialized]
        Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
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
                }
                return this.abnormalStatus;
            }
        }

        [NonSerialized]
        List<Actor> slaves = new List<Actor>();
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
                        i.AdditionEnd();
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
