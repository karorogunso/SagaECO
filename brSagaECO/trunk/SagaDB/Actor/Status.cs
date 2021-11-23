using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class Status
    {
        Actor owner = null;

        //final status, not allowed to directly change or set out of statusfactory, read only when calculating attack results and display
        public ushort min_atk1, min_atk2, min_atk3, max_atk1, max_atk2, max_atk3, min_matk, max_matk;
        public ushort def, mdef;
        public short def_add, mdef_add;
        public ushort hit_melee, hit_ranged, hit_magic, hit_critical;
        public ushort avoid_melee, avoid_ranged, avoid_magic, avoid_critical;

        //basic status, read-only when out of statusfactory
        public ushort min_atk_ori, max_atk_ori, min_matk_ori, max_matk_ori;
        



        //Possession bonus
        public short min_atk1_possession, min_atk2_possession, min_atk3_possession, max_atk1_possession, max_atk2_possession, max_atk3_possession, min_matk_possession, max_matk_possession;
        public short hit_melee_possession, hit_ranged_possession, avoid_melee_possession, avoid_ranged_possession;
        public short hp_possession, sp_possession, mp_possession;
        public short def_possession, def_add_possession, mdef_possession, mdef_add_possession;

        //Item bonus
        public short hit_melee_item, hit_ranged_item;
        public short hp_recover_item;
        public short atk1_item, atk2_item, atk3_item, matk_item;
        public short guard_item;
        public int speed_item;
        public short str_item, dex_item, int_item, vit_item, agi_item, mag_item, cri_item, criavd_item;
        public short hp_item, sp_item, mp_item, payl_item, volume_item, hp_rate_item = 100, sp_rate_item = 100, mp_rate_item = 100;

        //Skill bonus
        public short hit_melee_skill, hit_ranged_skill;
        public short avoid_melee_item, avoid_ranged_item, avoid_melee_skill, avoid_ranged_skill;
        public short def_skill, def_add_skill, mdef_skill, mdef_add_skill;
        public short hit_magic_item, hit_magic_skill, avoid_magic_item, avoid_magic_skill;
        public short min_atk1_skill, min_atk2_skill, min_atk3_skill, max_atk1_skill, max_atk2_skill, max_atk3_skill, min_matk_skill, max_matk_skill;
        public short cri_skill, criavd_skill, guard_skill;
        public short hp_skill, sp_skill, mp_skill;
        public short hp_recover_skill, sp_recover_skill, mp_recover_skill;
        public short str_skill, dex_skill, int_skill, vit_skill, agi_skill, mag_skill;
        public short combo_skill, combo_rate_skill;
        public short cri_dmg_skill;
        public int speed_skill;

        //Marionette bonus
        public short hp_mario, sp_mario, mp_mario;
        public short hp_recover_mario, mp_recover_mario;
        public short def_mario, def_add_mario, mdef_mario, mdef_add_mario;
        public short str_mario, dex_mario, int_mario, vit_mario, agi_mario, mag_mario;
        public short min_atk1_mario, min_atk2_mario, min_atk3_mario, max_atk1_mario, max_atk2_mario, max_atk3_mario, min_matk_mario, max_matk_mario;

        //Another bonus
        public short hit_melee_another, hit_ranged_another;
        public short avoid_melee_another, avoid_ranged_another;
        public short def_add_another, mdef_add_another;
        public short hp_another, sp_another, mp_another;
        public short str_another, dex_another, int_another, vit_another, agi_another, mag_another;
        public short min_atk1_another, min_atk2_another, min_atk3_another, max_atk1_another, max_atk2_another, max_atk3_another, min_matk_another, max_matk_another;

        //Title bonus
        public short hit_melee_title, hit_ranged_title;
        public short avoid_melee_title, avoid_ranged_title, cri_title;
        public short def_add_title, mdef_add_title;
        public short hp_title, sp_title, mp_title, hprecov_title, mprecov_title, sprecov_title;
        public short str_title, dex_title, int_title, vit_title, agi_title, mag_title;
        public short min_atk1_title, min_atk2_title, min_atk3_title, max_atk1_title, max_atk2_title, max_atk3_title, min_matk_title, max_matk_title;
        public short cri_avoid_title, aspd_title, cspd_title;

        //TamairePossession bonus
        public short hit_melee_tamaire, hit_ranged_tamaire;
        public short avoid_melee_tamaire, avoid_ranged_tamaire, cri_tamaire;
        public short def_add_tamaire, mdef_add_tamaire;
        public short hp_tamaire, sp_tamaire, mp_tamaire;
        public short min_atk1_tamaire, min_atk2_tamaire, min_atk3_tamaire, max_atk1_tamaire, max_atk2_tamaire, max_atk3_tamaire, min_matk_tamaire, max_matk_tamaire;
        public short aspd_tamaire, cspd_tamaire;

        /// <summary>
        /// 快精奖励的CSPD
        /// </summary>
        public short speedenchantcspdbonus;

        public float tenacity;
        public float weapon_add;

        /// <summary>
        /// 法术伤害减少比率
        /// </summary>
        public float MagicRuduceRate;

        /// <summary>
        /// 物理伤害减少比率
        /// </summary>
        public float PhysiceReduceRate;

        /// <summary>
        /// 坚韧
        /// </summary>
        public float Tenacity
        {
            get
            {
                return this.tenacity;
            }
            set
            {
                this.tenacity = value;
            }
        }
        /// <summary>
        /// 武器增加幅度
        /// </summary>
        public float Weapon_add
        {
            get
            {
                return this.weapon_add;
            }
            set
            {
                this.weapon_add = value;
            }
        }
        /// <summary>
        /// 对赌徒技能Double Up有效的技能ID
        /// </summary>
        public List<ushort> doubleUpList = new List<ushort>();

        /// <summary>
        /// 对延迟减少有效的技能ID和比率
        /// </summary>
        public Dictionary<ushort, int> delayCancelList = new Dictionary<ushort, int>();

        public short aspd, cspd, aspd_skill, cspd_skill, aspd_skill_limit, cspd_skill_limit;
        public float aspd_skill_perc = 1f;

        public ushort str_rev, dex_rev, int_rev, vit_rev, agi_rev, mag_rev;
        public short m_str_chip, m_dex_chip, m_int_chip, m_vit_chip, m_agi_chip, m_mag_chip;

        public short str_chip
        {
            get
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Race == PC_RACE.DEM)
                    {
                        if (pc.Form == DEM_FORM.NORMAL_FORM)
                            return 0;
                        else
                            return m_str_chip;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            set
            {
                this.m_str_chip = value;
            }

        }
        public short dex_chip
        {
            get
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Race == PC_RACE.DEM)
                    {
                        if (pc.Form == DEM_FORM.NORMAL_FORM)
                            return 0;
                        else
                            return m_dex_chip;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            set
            {
                this.m_dex_chip = value;
            }

        }
        public short int_chip
        {
            get
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Race == PC_RACE.DEM)
                    {
                        if (pc.Form == DEM_FORM.NORMAL_FORM)
                            return 0;
                        else
                            return m_int_chip;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            set
            {
                this.m_int_chip = value;
            }

        }
        public short vit_chip
        {
            get
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Race == PC_RACE.DEM)
                    {
                        if (pc.Form == DEM_FORM.NORMAL_FORM)
                            return 0;
                        else
                            return m_vit_chip;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            set
            {
                this.m_vit_chip = value;
            }

        }
        public short agi_chip
        {
            get
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Race == PC_RACE.DEM)
                    {
                        if (pc.Form == DEM_FORM.NORMAL_FORM)
                            return 0;
                        else
                            return m_agi_chip;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            set
            {
                this.m_agi_chip = value;
            }

        }
        public short mag_chip
        {
            get
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Race == PC_RACE.DEM)
                    {
                        if (pc.Form == DEM_FORM.NORMAL_FORM)
                            return 0;
                        else
                            return m_mag_chip;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            set
            {
                this.m_mag_chip = value;
            }

        }
        /// <summary>
        /// 伤害回复比
        /// </summary>
        public float absorb_hp;
        /// <summary>
        /// 被伤害标记等级
        /// </summary>
        public byte Damage_Up_Lv;
        /// <summary>
        /// 被暴击标记等级
        /// </summary>
        public byte Cri_Up_Lv;
        /// <summary>
        /// 避震波等级（3转剑35级技能）
        /// </summary>
        public int Pressure_lv;
        /// <summary>
        /// 魔法反射概率（3转骑士20级技能）
        /// </summary>
        public float reflex_odds;
        /// <summary>
        /// 格挡技能等级（3转骑士45级技能）
        /// </summary>
        public byte Blocking_LV;
        /// <summary>
        /// イレイザー等级（3转刺客10级技能）
        /// </summary>
        public byte Purger_Lv;
        /// <summary>
        /// 写轮眼概率（3转刺客25级技能）
        /// </summary>
        public byte Syaringan_rate;
        /// <summary>
        /// ノーヘイト减少比例（3转弓13级技能）
        /// </summary>
        public byte Nooheito_rate;
        /// <summary>
        /// 敵対心上昇比例（3转骑士13级技能）
        /// </summary>
        public byte HatredUp_rate;
        /// <summary>
        /// リベンジトリガー机率（3转弓23级技能）
        /// </summary>
        public byte MissRevenge_rate;
        public bool MissRevenge_hit;
        /// <summary>
        /// プラスエレメント伤害加成（WIZ3转3级技能）
        /// </summary>
        public float PlusElement_rate;
        /// <summary>
        /// 星灵术使伤害增加比例
        /// </summary>
        public float ElementDamegeUp_rate;
        /// <summary>
        /// 属性契约种类 1为火 2为水 3为土 4为风
        /// </summary>
        public byte Contract_Lv;
        /// <summary>
        /// 治疗效果提升率
        /// </summary>
        public float Cardinal_Rank;
        /// <summary>
        /// 技能属性加成
        /// </summary>
        public short hp_medicine, mp_medicine, sp_medicine;

        public short autoReviveRate = 0;

        public BATTLE_STATUS battleStatus;
        public ATTACK_TYPE attackType;

        /// <summary>
        /// 寄主承担凭依者凭依伤害的几率
        /// </summary>
        public short possessionTakeOver;

        /// <summary>
        /// 凭依贯穿伤害无效几率
        /// </summary>
        public short possessionCancel;

        /// <summary>
        /// 受攻击的时间戳
        /// </summary>
        public DateTime attackStamp = DateTime.Now;

        /// <summary>
        /// 正在攻击本Actor的Actors
        /// </summary>
        public List<Actor> attackingActors = new List<Actor>();

        /// <summary>
        /// 商人买卖相关
        /// </summary>
        public short buy_rate, sell_rate;

        public bool undead;

        //新加入的 Start
        public float damage_atk1_discount, damage_atk2_discount, damage_atk3_discount;
        //新加入的 End



        public Dictionary<SagaLib.Elements, int> elements_item = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> attackElements_item = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> elements_skill = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> attackElements_skill = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> elements_base = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> attackElements_base = new Dictionary<SagaLib.Elements, int>();

        public Dictionary<string, Addition> Additions = new Dictionary<string, Addition>();

        public Status(Actor owner)
        {
            this.owner = owner;
            this.elements_item.Add(SagaLib.Elements.Neutral, 0);
            this.elements_item.Add(SagaLib.Elements.Fire, 0);
            this.elements_item.Add(SagaLib.Elements.Water, 0);
            this.elements_item.Add(SagaLib.Elements.Wind, 0);
            this.elements_item.Add(SagaLib.Elements.Earth, 0);
            this.elements_item.Add(SagaLib.Elements.Holy, 0);
            this.elements_item.Add(SagaLib.Elements.Dark, 0);
            this.attackElements_item.Add(SagaLib.Elements.Neutral, 0);
            this.attackElements_item.Add(SagaLib.Elements.Fire, 0);
            this.attackElements_item.Add(SagaLib.Elements.Water, 0);
            this.attackElements_item.Add(SagaLib.Elements.Wind, 0);
            this.attackElements_item.Add(SagaLib.Elements.Earth, 0);
            this.attackElements_item.Add(SagaLib.Elements.Holy, 0);
            this.attackElements_item.Add(SagaLib.Elements.Dark, 0);
            this.elements_skill.Add(SagaLib.Elements.Neutral, 0);
            this.elements_skill.Add(SagaLib.Elements.Fire, 0);
            this.elements_skill.Add(SagaLib.Elements.Water, 0);
            this.elements_skill.Add(SagaLib.Elements.Wind, 0);
            this.elements_skill.Add(SagaLib.Elements.Earth, 0);
            this.elements_skill.Add(SagaLib.Elements.Holy, 0);
            this.elements_skill.Add(SagaLib.Elements.Dark, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Neutral, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Fire, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Water, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Wind, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Earth, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Holy, 0);
            this.attackElements_skill.Add(SagaLib.Elements.Dark, 0);
            this.elements_base.Add(SagaLib.Elements.Neutral, 0);
            this.elements_base.Add(SagaLib.Elements.Fire, 0);
            this.elements_base.Add(SagaLib.Elements.Water, 0);
            this.elements_base.Add(SagaLib.Elements.Wind, 0);
            this.elements_base.Add(SagaLib.Elements.Earth, 0);
            this.elements_base.Add(SagaLib.Elements.Holy, 0);
            this.elements_base.Add(SagaLib.Elements.Dark, 0);
            this.attackElements_base.Add(SagaLib.Elements.Neutral, 0);
            this.attackElements_base.Add(SagaLib.Elements.Fire, 0);
            this.attackElements_base.Add(SagaLib.Elements.Water, 0);
            this.attackElements_base.Add(SagaLib.Elements.Wind, 0);
            this.attackElements_base.Add(SagaLib.Elements.Earth, 0);
            this.attackElements_base.Add(SagaLib.Elements.Holy, 0);
            this.attackElements_base.Add(SagaLib.Elements.Dark, 0);
        }

        public void ClearItem()
        {
            this.atk1_item = 0;
            this.atk2_item = 0;
            this.atk3_item = 0;
            this.matk_item = 0;
            this.def_add = 0;
            this.mdef_add = 0;
            this.hit_melee_item = 0;
            this.hit_ranged_item = 0;
            this.avoid_melee_item = 0;
            this.avoid_ranged_item = 0;
            this.str_item = 0;
            this.dex_item = 0;
            this.vit_item = 0;
            this.int_item = 0;
            this.agi_item = 0;
            this.mag_item = 0;
            this.str_chip = 0;
            this.m_dex_chip = 0;
            this.m_vit_chip = 0;
            this.m_int_chip = 0;
            this.m_agi_chip = 0;
            this.m_mag_chip = 0;
            this.hp_item = 0;
            this.sp_item = 0;
            this.mp_item = 0;
            this.guard_item = 0;
            this.payl_item = 0;
            this.volume_item = 0;
            this.speed_item = 0;
            this.hp_rate_item = 100;
            this.sp_rate_item = 100;
            this.mp_rate_item = 100;
            this.hit_magic_item = 0;
            this.cri_item = 0;
            this.criavd_item = 0;
            this.avoid_magic_item = 0;

            this.elements_item[SagaLib.Elements.Neutral] = 0;
            this.elements_item[SagaLib.Elements.Fire] = 0;
            this.elements_item[SagaLib.Elements.Water] = 0;
            this.elements_item[SagaLib.Elements.Wind] = 0;
            this.elements_item[SagaLib.Elements.Earth] = 0;
            this.elements_item[SagaLib.Elements.Holy] = 0;
            this.elements_item[SagaLib.Elements.Dark] = 0;
            this.attackElements_item[SagaLib.Elements.Neutral] = 0;
            this.attackElements_item[SagaLib.Elements.Fire] = 0;
            this.attackElements_item[SagaLib.Elements.Water] = 0;
            this.attackElements_item[SagaLib.Elements.Wind] = 0;
            this.attackElements_item[SagaLib.Elements.Earth] = 0;
            this.attackElements_item[SagaLib.Elements.Holy] = 0;
            this.attackElements_item[SagaLib.Elements.Dark] = 0;
        }
    }
}
