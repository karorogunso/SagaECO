using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Partner;


namespace SagaDB.Actor
{    
    public class ActorPartner : Actor
    {
        uint actorpartnerid;
        public uint partnerid;
        protected Partner.PartnerData basedata;
        byte lv;
        public byte reliability;
        public byte rank;
        public bool rebirth;
        MotionType motion;
        public bool motion_loop, online;
        public ushort perkpoint;
        public byte perk0, perk1, perk2, perk3, perk4, perk5;
        public Dictionary<EnumPartnerEquipSlot, Item.Item> equipments = new Dictionary<EnumPartnerEquipSlot, Item.Item>();
        public List<Item.Item> foods = new List<Item.Item>();
        public List<ushort> equipcubes_condition = new List<ushort>();
        public List<ushort> equipcubes_action = new List<ushort>();
        public List<ushort> equipcubes_activeskill = new List<ushort>();
        public List<ushort> equipcubes_passiveskill = new List<ushort>();
        public Dictionary<byte, ushort> ai_conditions = new Dictionary<byte, ushort>();
        public Dictionary<byte, ushort> ai_reactions = new Dictionary<byte, ushort>();
        public Dictionary<byte, ushort> ai_intervals = new Dictionary<byte, ushort>();
        public Dictionary<byte, bool> ai_states = new Dictionary<byte, bool>();
        public byte ai_mode, basic_ai_mode, basic_ai_mode_2;
        public ulong exp, rankexp, reliabilityexp;//to be added into sql
        public ushort reliabilityuprate; //to be added into sql
        public DateTime nextfeedtime;//to be added into sql

        public Dictionary<uint, Skill.Skill> Skills = new Dictionary<uint, SagaDB.Skill.Skill>();
        ActorPC owner;
        public byte battleStatus;
        public bool Fictitious;
        public uint LastAttackActorID;
        public bool AutoAttack =false;

        public ActorPartner(uint partnerid,Item.Item partneritem)
        {
            this.type = ActorType.PARTNER;
            this.basedata = Partner.PartnerFactory.Instance.GetPartnerData(partneritem.BaseData.petID);
            this.Name = this.basedata.name;
            this.Speed = 780;//this.basedata.speed;
            this.Status.attackType = this.basedata.attackType;
            this.sightRange = 1500;
            this.PictID = partneritem.PictID;
        }

        public Partner.PartnerData BaseData
        {
            get
            {
                return basedata;
            }
            set
            {
                this.basedata = value;
            }
        } 

        /// <summary>
        /// 存在于数据库的ActorPartnerID
        /// </summary>
        public uint ActorPartnerID
        {
            get
            {
                return actorpartnerid ;
            }
            set
            {
                actorpartnerid =value;
            }
        }
        public ActorPC Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
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
        /// 动作
        /// </summary>
        public MotionType Motion { get { return this.motion; } set { this.motion = value; } }
        public bool MotionLoop { get { return this.motion_loop; } set { this.motion_loop = value; } }   

        //public EmotionType Emotion { get { return this.emotion; } set { this.emotion = value; }}
        
    }
}
