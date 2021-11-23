using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Partner
{
    public class PartnerData
    {
        public uint base_rank;
        public uint id, pictid;
        public string name;
        public float partnerSize;
        public ushort motionsetnumber;
        public PartnerType partnertype;
        public ushort partnertypeid;
        public ushort partnersystemid;
        public bool fly;
        public ushort speed;
        public ATTACK_TYPE attackType;
        public bool isrange;
        public float range;
        public byte level_in;
        public uint hp_in, mp_in, sp_in, hp_fn, mp_fn, sp_fn;
        public uint hp_in_re, mp_in_re, sp_in_re, hp_fn_re, mp_fn_re, sp_fn_re;
        public uint hp_rec_in, mp_rec_in, sp_rec_in, hp_rec_fn, mp_rec_fn, sp_rec_fn;
        public uint hp_rec_in_re, mp_rec_in_re, sp_rec_in_re, hp_rec_fn_re, mp_rec_fn_re, sp_rec_fn_re;
        public ushort atk_min_in, atk_max_in, atk_min_fn, atk_max_fn, atk_min_in_re, atk_max_in_re, atk_min_fn_re, atk_max_fn_re;
        public ushort matk_min_in, matk_max_in, matk_min_fn, matk_max_fn, matk_min_in_re, matk_max_in_re, matk_min_fn_re, matk_max_fn_re;
        public ushort def_in, def_add_in, def_fn, def_add_fn, def_in_re, def_add_in_re, def_fn_re, def_add_fn_re;
        public ushort mdef_in, mdef_add_in, mdef_fn, mdef_add_fn, mdef_in_re, mdef_add_in_re, mdef_fn_re, mdef_add_fn_re;
        public ushort hit_melee_in, hit_ranged_in, hit_magic_in, hit_critical_in, hit_melee_fn, hit_ranged_fn, hit_magic_fn, hit_critical_fn;
        public ushort hit_melee_in_re, hit_ranged_in_re, hit_magic_in_re, hit_critical_in_re, hit_melee_fn_re, hit_ranged_fn_re, hit_magic_fn_re, hit_critical_fn_re;
        public ushort avoid_melee_in, avoid_ranged_in, avoid_magic_in, avoid_critical_in, avoid_melee_fn, avoid_ranged_fn, avoid_magic_fn, avoid_critical_fn;
        public ushort avoid_melee_in_re, avoid_ranged_in_re, avoid_magic_in_re, avoid_critical_in_re, avoid_melee_fn_re, avoid_ranged_fn_re, avoid_magic_fn_re, avoid_critical_fn_re;
        public short aspd_in, cspd_in, aspd_fn, cspd_fn, aspd_in_re, cspd_in_re, aspd_fn_re, cspd_fn_re;
        public List<uint> born_skills = new List<uint>();
        public List<uint> born_cubes = new List<uint>();
        public Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
        public Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
        public int aiMode;
        public override string ToString()
        {
            return this.name;
        }
    }

    public class PartnerMotion
    {
        public byte ID;
        public uint MasterMotionID;
        public uint PartnerMotionID;
    }
    public class TalkInfo
    {
        /// <summary>
        /// 召唤宠物时的台词
        /// </summary>
        public List<string> Onsummoned = new List<string>();
        /// <summary>
        /// 处于战斗状态的台词
        /// </summary>
        public List<string> OnBattle = new List<string>();
        /// <summary>
        /// 主人死亡时的台词
        /// </summary>
        public List<string> OnMasterDead = new List<string>();
        /// <summary>
        /// 非战斗时的台词
        /// </summary>
        public List<string> OnNormal = new List<string>();
        /// <summary>
        /// 主人加入队伍时的台词
        /// </summary>
        public List<string> OnJoinParty = new List<string>();
        /// <summary>
        /// 主人的队伍解散时的台词
        /// </summary>
        public List<string> OnLeaveParty = new List<string>();
        /// <summary>
        /// 主人开始战斗时的台词
        /// </summary>
        public List<string> OnMasterFighting = new List<string>();
        /// <summary>
        /// 主人升级时的台词
        /// </summary>
        public List<string> OnMasterLevelUp = new List<string>();
        /// <summary>
        /// 主人在退出倒计时的台词
        /// </summary>
        public List<string> OnMasterQuit = new List<string>();
        /// <summary>
        /// 主人在/sit、/chair动作时的台词
        /// </summary>
        public List<string> OnMasterSit = new List<string>();
        /// <summary>
        /// 主人在/relax、/break动作时的台词
        /// </summary>
        public List<string> OnMasterRelax = new List<string>();
        /// <summary>
        /// 主人在/bow动作时的台词
        /// </summary>
        public List<string> OnMasterBow = new List<string>();
        /// <summary>
        /// 主人登录时的台词
        /// </summary>
        public List<string> OnMasterLogin = new List<string>();
        /// <summary>
        /// 搭档升级时的台词
        /// </summary>
        public List<string> OnLevelUp = new List<string>();
        /// <summary>
        /// 为搭档穿上装备时的台词
        /// </summary>
        public List<string> OnEquip = new List<string>();
        /// <summary>
        ///给予搭档食物时的台词
        /// </summary>
        public List<string> OnEat = new List<string>();
        /// <summary>
        ///搭档变成可以进食时的台词
        /// </summary>
        public List<string> OnEatReady = new List<string>();
    }
    public class PartnerFood
    {
        public uint itemID;
        public byte partnerrank_min, partnerrank_max;
        public byte systemID;
        public uint nextfeedtime;
        public uint rankexp;
        public ushort reliabilityuprate;
    }

    public class PartnerEquipment
    {
        public uint itemID;
        //public string itemname;
        public int hp_up, mp_up, sp_up, hp_rec, mp_rec, sp_rec;
        public short atk, matk, def, mdef;
        public short Shit, Lhit, Mhit, Chit, Savd, Lavd, Mavd, Cavd;
        public Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
        public Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
        public byte partnerrank;
        //public byte attacktype;
        public byte systemID;
    } 

    public class ActCubeData
    {
        public ushort uniqueID;
        public uint itemID;
        public PartnerCubeType cubetype;
        public string cubename;
        public byte partnerrank;
        public byte systemID;
        public byte reliability;
        public byte rebirth;
        public uint skillID;
        public ushort actionID;
        public uint parameter1, parameter2,parameter3;
    } 
}
