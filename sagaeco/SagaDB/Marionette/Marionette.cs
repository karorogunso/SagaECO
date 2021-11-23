using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Marionette
{
    public enum GatherType
    {
        Plant,
        Mineral,
        Food,
        Magic,
        Treasurebox,
        Excavation,
        Any,
        Strange
    }
    /// <summary>
    /// 活动木偶
    /// </summary>
    public class Marionette
    {
        string name;
        uint id, picID;
        Mob.MobType type;
        int duration, delay;
        public short str, dex, vit, intel, agi, mag;
        public short hp, mp, sp;
        public short move_speed;
        public short min_atk1, min_atk2, min_atk3, max_atk1, max_atk2, max_atk3, min_matk, max_matk;
        public short def, def_add, mdef, mdef_add;
        public short hit_melee, hit_ranged, hit_magic, hit_cri;
        public short avoid_melee, avoid_ranged, avoid_magic, avoid_cri;
        public short hp_recover, mp_recover;
        public short aspd, cspd;
        public List<ushort> skills = new List<ushort>();
        public Dictionary<GatherType, bool> gather = new Dictionary<GatherType, bool>();

        /// <summary>
        /// 活动木偶的名称
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }
        /// <summary>
        /// 活动木偶的ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }
        /// <summary>
        /// 活动木偶的显示ID
        /// </summary>
        public uint PictID { get { return this.picID; } set { this.picID = value; } }
        /// <summary>
        /// 活动木偶的怪物类型
        /// </summary>
        public Mob.MobType MobType { get { return this.type; } set { this.type = value; } }
        /// <summary>
        /// 变身时间
        /// </summary>
        public int Duration { get { return this.duration; } set { this.duration = value; } }
        /// <summary>
        /// 变身延迟
        /// </summary>
        public int Delay { get { return this.delay; } set { this.delay = value; } }

        public override string ToString()
        {
            return this.name;
        }
    }
}
