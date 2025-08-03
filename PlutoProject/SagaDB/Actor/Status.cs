using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class Status
    {
        Actor owner = null;

        /// <summary>
        /// 伤害减少百分比
        /// </summary>
        public float ReduceDamage = 0f;
        /// <summary>
        /// 造成的仇恨比
        /// </summary>
        public float HateRate = 1f;
        /// <summary>
        /// 造成的普通伤害百分比
        /// </summary>
        public float HitDamageRate = 1f;
        /// <summary>
        /// 造成的暴击伤害百分比
        /// </summary>
        public float CriDamageRate = 1f;
        /// <summary>
        /// 造成的伤害百分比
        /// </summary>
        public float DamageRate = 1f;
        /// <summary>
        /// 格挡率,100为100%
        /// </summary>
        public int ParryRate = 0;

        /// <summary>
        /// 植物吸收盾
        /// </summary>
        public uint PlantShield = 0;

        /// <summary>
        /// 攻击渗透
        /// </summary>
        public bool DefRatioAtk = false;

        //basic status, read-only when out of statusfactory
        public ushort min_atk_bs, max_atk_bs, min_matk_bs, max_matk_bs;
        public ushort def_bs, mdef_bs; //%def
        public short def_add_bs, mdef_add_bs; //-def
        public short guard_bs;
        public ushort hit_melee_bs, hit_ranged_bs, hit_magic_bs, hit_critical_bs;
        public ushort avoid_melee_bs, avoid_ranged_bs, avoid_magic_bs, avoid_critical_bs;
        public short aspd_bs, cspd_bs;
        public short hp_recover_bs, mp_recover_bs, sp_recover_bs;
        public ushort str_rev, dex_rev, int_rev, vit_rev, agi_rev, mag_rev;

        //final status, not allowed to directly change or set out of statusfactory, read only when calculating attack results and display
        public ushort min_atk1, min_atk2, min_atk3, max_atk1, max_atk2, max_atk3, min_matk, max_matk;
        public ushort def, mdef; //%def
        public ushort atk_min_to_max_per, matk_min_to_max_per;//min to max up percentage of atks
        public short def_add, mdef_add; //-def
        public short guard;
        public ushort hit_melee, hit_ranged, hit_magic, hit_critical;
        public ushort avoid_melee, avoid_ranged, avoid_magic, avoid_critical;
        public short aspd, cspd;
        public short hp_recover, mp_recover, sp_recover;
        public short hp_medicine, mp_medicine, sp_medicine;


        //item bonus (except Iris Card)
        public short atk1_item, atk2_item, atk3_item, matk_item;
        public short atk1_rate_item = 100, atk2_rate_item = 100, atk3_rate_item = 100, matk_rate_item = 100;
        public short def_item, mdef_item;
        public short def_add_item, mdef_add_item;
        public short guard_item;
        public short hit_melee_item, hit_ranged_item, hit_magic_item, hit_critical_item;
        public short avoid_melee_item, avoid_ranged_item, avoid_magic_item, avoid_critical_item;
        public short aspd_item, cspd_item, aspd_item_limit, cspd_item_limit;
        public short aspd_rate_item = 100, cspd_rate_item = 100;
        public short str_item, dex_item, int_item, vit_item, agi_item, mag_item;
        public int hp_item, sp_item, mp_item, payl_item, volume_item, hp_rate_item = 100, sp_rate_item = 100, mp_rate_item = 100;
        public short hp_recover_item, sp_recover_item, mp_recover_item, cri_item, criavd_item;
        public int speed_item;

        //Iris Card bonus
        public short weapon_add_iris = 0, weapon_add_rate_iris = 0;//武器伤害直接增加用参数
        public short level_hit_iris = 0, level_avoid_iris = 0;//等级差命中与回避
        public short avoid_end_rate = 100;//最终回避计算
        public short physice_rate_iris = 100, magic_rate_iris = 100;//物理与魔法直接减少
        public short Element_down_iris = 100;//被属性攻击减少幅度
        public short foot_iris = 100, potion_iris = 100;//食物和药水
        public short human_dmg_up_iris = 100, bird_dmg_up_iris = 100, animal_dmg_up_iris = 100, insect_dmg_up_iris = 100, magic_c_dmg_up_iris = 100, plant_dmg_up_iris = 100, water_a_dmg_up_iris = 100, machine_dmg_up_iris = 100, rock_dmg_up_iris = 100, element_dmg_up_iris = 100, undead_dmg_up_iris = 100;//对种族增伤参数
        public short human_dmg_down_iris = 100, bird_dmg_down_iris = 100, animal_dmg_down_iris = 100, insect_dmg_down_iris = 100, magic_c_dmg_down_iris = 100, plant_dmg_down_iris = 100, water_a_dmg_down_iris = 100, machine_dmg_down_iris = 100, rock_dmg_down_iris = 100, element_dmg_down_iris = 100, undead_dmg_down_iris = 100;//种族减伤参数
        public short human_hit_up_iris = 100, bird_hit_up_iris = 100, animal_hit_up_iris = 100, insect_hit_up_iris = 100, magic_c_hit_up_iris = 100, plant_hit_up_iris = 100, water_a_hit_up_iris = 100, machine_hit_up_iris = 100, rock_hit_up_iris = 100, element_hit_up_iris = 100, undead_hit_up_iris = 100;//对种族命中提升参数
        public short human_avoid_up_iris = 100, bird_avoid_up_iris = 100, animal_avoid_up_iris = 100, insect_avoid_up_iris = 100, magic_c_avoid_up_iris = 100, plant_avoid_up_iris = 100, water_a_avoid_up_iris = 100, machine_avoid_up_iris = 100, rock_avoid_up_iris = 100, element_avoid_up_iris = 100, undead_avoid_up_iris = 100;//种族回避提升参数
        public short human_cri_up_iris = 0, bird_cri_up_iris = 0, animal_cri_up_iris = 0, insect_cri_up_iris = 0, magic_c_cri_up_iris = 0, plant_cri_up_iris = 0, water_a_cri_up_iris = 0, machine_cri_up_iris = 0, rock_cri_up_iris = 0, element_cri_up_iris = 0, undead_cri_up_iris = 0;//对种族暴击提升参数
        public short pt_dmg_up_iris = 100, pt_dmg_down_iris = 100;
        public short min_atk1_iris, min_atk2_iris, min_atk3_iris, max_atk1_iris, max_atk2_iris, max_atk3_iris, min_matk_iris, max_matk_iris;
        public short min_atk1_rate_iris = 100, min_atk2_rate_iris = 100, min_atk3_rate_iris = 100, max_atk1_rate_iris = 100, max_atk2_rate_iris = 100, max_atk3_rate_iris = 100, min_matk_rate_iris = 100, max_matk_rate_iris = 100;
        public short def_iris, mdef_iris;
        public short def_add_iris, mdef_add_iris;
        public short guard_iris = 0, volume_iris = 0, payl_add_iris = 0, volume_add_iris = 0;//负重体积相关
        public short hit_melee_iris, hit_ranged_iris, hit_magic_iris, hit_critical_iris, hit_melee_rate_iris = 100, hit_ranged_rate_iris = 100, hit_magic_rate_iris = 100, hit_critical_rate_iris = 100;
        public short avoid_melee_iris, avoid_ranged_iris, avoid_magic_iris, avoid_critical_iris, avoid_melee_rate_iris = 100, avoid_ranged_rate_iris = 100, avoid_magic_rate_iris = 100, avoid_critical_rate_iris = 100;
        public short aspd_iris, cspd_iris, aspd_iris_limit, cspd_iris_limit;
        public short aspd_rate_iris = 100, cspd_rate_iris = 100;
        public short str_iris = 0, dex_iris = 0, int_iris = 0, vit_iris = 0, agi_iris = 0, mag_iris = 0;
        public short hp_iris, sp_iris, mp_iris, payl_iris, hp_rate_iris = 100, sp_rate_iris = 100, mp_rate_iris = 100;
        public short sp_rate_down_iris = 100, mp_rate_down_iris = 100;
        public short hp_recover_iris, sp_recover_iris, mp_recover_iris;
        public int speed_iris;

        //skill bonus
        public ushort min_atk_ori, max_atk_ori, min_matk_ori, max_matk_ori;
        public short min_atk1_skill, min_atk2_skill, min_atk3_skill, max_atk1_skill, max_atk2_skill, max_atk3_skill, min_matk_skill, max_matk_skill;
        public short min_atk1_rate_skill = 100, min_atk2_rate_skill = 100, min_atk3_rate_skill = 100, max_atk1_rate_skill = 100, max_atk2_rate_skill = 100, max_atk3_rate_skill = 100, min_matk_rate_skill = 100, max_matk_rate_skill = 100;
        public short def_skill, def_add_skill, mdef_skill, mdef_add_skill;
        public short guard_skill;
        public short hit_melee_skill, hit_ranged_skill, hit_magic_skill, hit_critical_skill;
        public short avoid_melee_skill, avoid_ranged_skill, avoid_magic_skill, avoid_critical_skill;
        public short aspd_skill, cspd_skill, aspd_skill_limit, cspd_skill_limit;
        public short aspd_rate_skill = 100, cspd_rate_skill = 100;
        public short str_skill, dex_skill, int_skill, vit_skill, agi_skill, mag_skill;
        public short hp_skill, sp_skill, mp_skill, hp_rate_skill = 100, sp_rate_skill = 100, mp_rate_skill = 100;
        public short hp_recover_skill, sp_recover_skill, mp_recover_skill;
        public short cri_skill, cri_skill_rate, criavd_skill;
        public int speed_skill;
        public float aspd_skill_perc;

        //Another bonus
        public short hit_melee_anthor, hit_ranged_anthor;
        public short avoid_melee_anthor, avoid_ranged_anthor;
        public short def_add_anthor, mdef_add_anthor;
        public short hp_anthor, sp_anthor, mp_anthor;
        public short str_anthor, dex_anthor, int_anthor, vit_anthor, agi_anthor, mag_anthor;
        public short min_atk1_anthor, min_atk2_anthor, min_atk3_anthor, max_atk1_anthor, max_atk2_anthor, max_atk3_anthor, min_matk_anthor, max_matk_anthor;

        //tamaire bonus
        public short hit_melee_tamaire, hit_ranged_tamaire;
        public short avoid_melee_tamaire, avoid_ranged_tamaire, cri_tamaire;
        public short def_add_tamaire, mdef_add_tamaire;
        public short hp_tamaire, sp_tamaire, mp_tamaire;
        public short min_atk1_tamaire, min_atk2_tamaire, min_atk3_tamaire, max_atk1_tamaire, max_atk2_tamaire, max_atk3_tamaire, min_matk_tamaire, max_matk_tamaire;
        public short aspd_tamaire, cspd_tamaire;

        //title bonus
        public short hit_melee_tit, hit_ranged_tit;
        public short avoid_melee_tit, avoid_ranged_tit, cri_tit;
        public short def_add_tit, mdef_add_tit;
        public short hp_tit, sp_tit, mp_tit, hprecov_tit, mprecov_tit, sprecov_tit;
        public short str_tit, dex_tit, int_tit, vit_tit, agi_tit, mag_tit;
        public short min_atk1_tit, min_atk2_tit, min_atk3_tit, max_atk1_tit, max_atk2_tit, max_atk3_tit, min_matk_tit, max_matk_tit;
        public short cri_avoid_tit, hit_magic_tit, avoid_magic_tit, aspd_tit, cspd_tit;

        //宠物凭依 bonus
        public short hit_melee_petpy, hit_ranged_petpy;
        public short avoid_melee_petpy, avoid_ranged_petpy, cri_petpy, cspd_petpy, aspd_petpy;
        public short def_add_petpy, mdef_add_petpy;
        public short hp_petpy, sp_petpy, mp_petpy, hprecov_petpy, mprecov_petpy, sprecov_petpy;
        public short str_petpy, dex_petpy, int_petpy, vit_petpy, agi_petpy, mag_petpy;
        public short min_atk1_petpy, min_atk2_petpy, min_atk3_petpy, max_atk1_petpy, max_atk2_petpy, max_atk3_petpy, min_matk_petpy, max_matk_petpy;

        //DEM chip bonus
        public short m_str_chip, m_dex_chip, m_int_chip, m_vit_chip, m_agi_chip, m_mag_chip;

        //木偶变身bonus
        public short min_atk1_mario, min_atk2_mario, min_atk3_mario, max_atk1_mario, max_atk2_mario, max_atk3_mario, min_matk_mario, max_matk_mario;
        public short str_mario, dex_mario, int_mario, vit_mario, agi_mario, mag_mario;
        public short def_mario, def_add_mario, mdef_mario, mdef_add_mario;
        public short hp_mario, sp_mario, mp_mario;
        public short hp_recover_mario, mp_recover_mario;

        //Ygg Card bonus 1
        public short heal_70up_iris, atk_70up_iris, mpcost_70down_iris, spcost_70down_iris, atkup_job40_iris, expup_job40_iris, spnocost_iris, mpnocost_iris,
            spweap_atkup_iris, heal_attacked_iris, holymortar_combo_iris, atkup_alljob40_iris, Mammon_iris;

        //Ygg Card bonus 2
        public short Playman, Penglai, Instructor, DarkWhisper;

        /// <summary>
        /// 法术伤害减少比率
        /// </summary>
        public float MagicRuduceRate;

        /// <summary>
        /// 物理伤害减少比率
        /// </summary>
        public float PhysiceReduceRate = 0;

        /// <summary>
        /// 快精奖励的CSPD
        /// </summary>
        public short speedenchantcspdbonus;
        /// <summary>
        /// CSPD共享奖励的CSPD
        /// </summary>
        public short communioncspdbonus;

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

        //other general status
        /// <summary>
        /// 状态持续时间补正百分数（正值为增加）
        /// </summary>
        public short buffer_bonus, buffee_bonus, debuffer_bonus, debuffee_bonus;
        /// <summary>
        /// 控制类效果判定率修正
        /// </summary>
        public short control_rate_bonus;
        /// <summary>
        /// 武器装备效果补正百分比
        /// </summary>
        public float weapon_rate = 100;
        /// <summary>
        /// 战斗状态判定 未使用
        /// </summary>
        public BATTLE_STATUS battleStatus;
        /// <summary>
        /// 物理攻击类型：打 斩 刺
        /// </summary>
        public ATTACK_TYPE attackType;
        /// <summary>
        /// 受攻击的时间戳 效果待测试
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
        /// <summary>
        /// 不死族状态
        /// </summary>
        public bool undead;
        /// <summary>
        /// 连击补正
        /// </summary>
        public short combo_skill, combo_rate_skill;
        /// <summary>
        /// 打 斩 刺 分类专属%防御值
        /// </summary>
        public float damage_atk1_discount, damage_atk2_discount, damage_atk3_discount;


        /// <summary>
        /// 打 斩 刺 分类专属%防御值
        /// </summary>
        public bool noAttact;

        #region Skill Related Status
        /// <summary>
        /// 对Zen有效的技能ID
        /// </summary>
        public List<ushort> zenList = new List<ushort>();
        /// <summary>
        /// 对暗Zen有效的技能ID
        /// </summary>
        public List<ushort> darkZenList = new List<ushort>();
        /// <summary>
        /// 对赌徒技能Double Up有效的技能ID
        /// </summary>
        public List<ushort> doubleUpList = new List<ushort>();
        /// <summary>
        /// 对延迟减少有效的技能ID和比率
        /// </summary>
        public Dictionary<ushort, int> delayCancelList = new Dictionary<ushort, int>();
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
        /// 杀界等级（2-1剑38级技能）
        /// </summary>
        public int EaseCt_lv;
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
        /// 收获大师等级（3转收获者JOB45技能）
        /// </summary>
        public byte HarvestMaster_Lv;
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
        /// フォースマスター等级（3转WIZ10级技能）
        /// </summary>
        public byte ForceMaster_Lv;
        /// <summary>
        /// 星灵术使伤害增加比例
        /// </summary>
        public float ElementDamegeUp_rate;
        /// <summary>
        /// 属性契约种类 1为火 2为水 3为土 4为风
        /// </summary>
        public byte Contract_Lv;
        /// <summary>
        /// 受治疗提升率
        /// </summary>
        public float Cardinal_Rank;

        public short autoReviveRate = 0;
        /// <summary>
        /// 无属性抗性
        /// </summary>
        public byte NeutralDamegeDown_rate;

        /// <summary>
        /// 不包括无属性的全属性抗性
        /// </summary>
        public byte ElementDamegeDown_rate;
        #endregion



        #region Possession Related Status
        /// <summary>
        /// 寄主承担凭依者凭依伤害的几率
        /// </summary>
        public short possessionTakeOver;
        /// <summary>
        /// 凭依贯穿伤害无效几率
        /// </summary>
        public short possessionCancel;
        //for 魂
        public short min_atk1_possession, min_atk2_possession, min_atk3_possession, max_atk1_possession, max_atk2_possession, max_atk3_possession, min_matk_possession, max_matk_possession;
        public short hit_melee_possession, hit_ranged_possession, avoid_melee_possession, avoid_ranged_possession;
        public short hp_possession, sp_possession, mp_possession;
        public short def_possession, def_add_possession, mdef_possession, mdef_add_possession;
        #endregion

        //Element 
        public Dictionary<SagaLib.Elements, int> elements_item = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> attackElements_item = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> elements_iris = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> attackelements_iris = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> elements_skill = new Dictionary<SagaLib.Elements, int>();
        public Dictionary<SagaLib.Elements, int> attackElements_skill = new Dictionary<SagaLib.Elements, int>();

        public Dictionary<string, Addition> Additions = new Dictionary<string, Addition>();
        public Dictionary<uint, int> SkillRate = new Dictionary<uint, int>();
        public Dictionary<uint, long> SkillDamage = new Dictionary<uint, long>();
        public Dictionary<byte, int> AddRace = new Dictionary<byte, int>();
        public Dictionary<byte, int> SubRace = new Dictionary<byte, int>();
        public Dictionary<byte, int> AddElement = new Dictionary<byte, int>();
        public Dictionary<byte, int> SubElement = new Dictionary<byte, int>();

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
            this.elements_iris.Add(SagaLib.Elements.Neutral, 0);
            this.elements_iris.Add(SagaLib.Elements.Fire, 0);
            this.elements_iris.Add(SagaLib.Elements.Water, 0);
            this.elements_iris.Add(SagaLib.Elements.Wind, 0);
            this.elements_iris.Add(SagaLib.Elements.Earth, 0);
            this.elements_iris.Add(SagaLib.Elements.Holy, 0);
            this.elements_iris.Add(SagaLib.Elements.Dark, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Neutral, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Fire, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Water, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Wind, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Earth, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Holy, 0);
            this.attackelements_iris.Add(SagaLib.Elements.Dark, 0);
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
        }
        /// <summary>
        /// Clear Item and Iris Card Bonus
        /// </summary>
        public void ClearItem()
        {
            //item bonus
            this.atk1_item = 0;
            this.atk2_item = 0;
            this.atk3_item = 0;
            this.matk_item = 0;
            this.atk1_rate_item = 100;
            this.atk2_rate_item = 100;
            this.atk3_rate_item = 100;
            this.matk_rate_item = 100;
            this.def_item = 0;
            this.mdef_item = 0;
            this.def_add_item = 0;
            this.mdef_add_item = 0;
            this.guard_item = 0;
            this.hit_melee_item = 0;
            this.hit_ranged_item = 0;
            this.hit_magic_item = 0;
            this.hit_critical_item = 0;
            this.avoid_melee_item = 0;
            this.avoid_ranged_item = 0;
            this.avoid_magic_item = 0;
            this.avoid_critical_item = 0;
            this.aspd_item = 0;
            this.cspd_item = 0;
            this.aspd_item_limit = 0;
            this.cspd_item_limit = 0;
            this.aspd_rate_item = 100;
            this.cspd_rate_item = 100;
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
            this.payl_item = 0;
            this.volume_item = 0;
            this.speed_item = 0;
            this.hp_rate_item = 100;
            this.sp_rate_item = 100;
            this.mp_rate_item = 100;
            this.hp_recover_item = 0;
            this.mp_recover_item = 0;
            this.sp_recover_item = 0;
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
            this.cri_item = 0;
            this.avoid_critical_item = 0;
            this.criavd_item = 0;
            this.SkillDamage.Clear();
            this.SkillRate.Clear();
            this.AddRace.Clear();
            this.SubRace.Clear();
            this.AddElement.Clear();
            this.SubElement.Clear();
            //Iris bonus

            //Ygg Card bonus
            //public short heal_70up_iris, atk_70up_iris, mpcost_70down_iris, spcost_70down_iris, atkup_job40_iris, expup_job40_iris, spnocost_iris, mpnocost_iris,
            //  spweap_atkup_iris, heal_attacked_iris, holymortar_combo_iris;

            //Ygg Card bonus 1
            heal_70up_iris = 0;
            atk_70up_iris = 0;
            mpcost_70down_iris = 0;
            spcost_70down_iris = 0;
            atkup_job40_iris = 0;
            expup_job40_iris = 0;
            spnocost_iris = 0;
            mpnocost_iris = 0;
            spweap_atkup_iris = 0;
            heal_attacked_iris = 0;
            holymortar_combo_iris = 0;
            atkup_alljob40_iris = 0;
            Mammon_iris = 0;

            //Ygg Card bonus 2
            Playman = 0;
            Penglai = 0;
            Instructor = 0;
            DarkWhisper = 0;


            this.min_atk1_iris = 0;
            this.min_atk2_iris = 0;
            this.min_atk3_iris = 0;
            this.max_atk1_iris = 0;
            this.max_atk2_iris = 0;
            this.max_atk3_iris = 0;
            this.min_matk_iris = 0;
            this.max_matk_iris = 0;
            this.min_atk1_rate_iris = 100;
            this.min_atk2_rate_iris = 100;
            this.min_atk3_rate_iris = 100;
            this.max_atk1_rate_iris = 100;
            this.max_atk2_rate_iris = 100;
            this.max_atk3_rate_iris = 100;
            this.min_matk_rate_iris = 100;
            this.max_matk_rate_iris = 100;
            this.def_iris = 0;
            this.mdef_iris = 0;
            this.def_add_iris = 0;
            this.mdef_add_iris = 0;
            this.guard_iris = 0;
            this.volume_iris = 0;
            this.payl_add_iris = 0;
            this.volume_add_iris = 0;
            this.hit_melee_iris = 0;
            this.hit_ranged_iris = 0;
            this.hit_magic_iris = 0;
            this.hit_critical_iris = 0;
            this.avoid_melee_iris = 0;
            this.avoid_ranged_iris = 0;
            this.avoid_magic_iris = 0;
            this.avoid_critical_iris = 0;
            this.aspd_iris = 0;
            this.cspd_iris = 0;
            this.aspd_iris_limit = 0;
            this.cspd_iris_limit = 0;
            this.aspd_rate_iris = 100;
            this.cspd_rate_iris = 100;
            this.str_iris = 0;
            this.dex_iris = 0;
            this.vit_iris = 0;
            this.int_iris = 0;
            this.agi_iris = 0;
            this.mag_iris = 0;
            this.hp_iris = 0;
            this.sp_iris = 0;
            this.mp_iris = 0;
            this.payl_iris = 0;
            this.speed_iris = 0;
            this.hp_rate_iris = 100;
            this.sp_rate_iris = 100;
            this.mp_rate_iris = 100;
            this.sp_rate_down_iris = 100;
            this.mp_rate_down_iris = 100;
            this.hp_recover_iris = 0;
            this.mp_recover_iris = 0;
            this.sp_recover_iris = 0;
            this.hit_melee_rate_iris = 100;
            this.hit_ranged_rate_iris = 100;
            this.avoid_melee_rate_iris = 100;
            this.avoid_ranged_rate_iris = 100;
            this.weapon_add_iris = 0;
            this.weapon_add_rate_iris = 0;
            this.level_avoid_iris = 0;
            this.level_hit_iris = 0;
            this.avoid_end_rate = 100;
            this.physice_rate_iris = 100;
            this.magic_rate_iris = 100;
            this.pt_dmg_down_iris = 100;
            this.pt_dmg_up_iris = 100;
            this.Element_down_iris = 100;
            this.human_dmg_up_iris = 100;
            this.bird_dmg_up_iris = 100;
            this.animal_dmg_up_iris = 100;
            this.insect_dmg_up_iris = 100;
            this.magic_c_dmg_up_iris = 100;
            this.plant_dmg_up_iris = 100;
            this.water_a_dmg_up_iris = 100;
            this.machine_dmg_up_iris = 100;
            this.rock_dmg_up_iris = 100;
            this.element_dmg_up_iris = 100;
            this.undead_dmg_up_iris = 100;
            this.human_dmg_down_iris = 100;
            this.bird_dmg_down_iris = 100;
            this.animal_dmg_down_iris = 100;
            this.insect_dmg_down_iris = 100;
            this.magic_c_dmg_down_iris = 100;
            this.plant_dmg_down_iris = 100;
            this.water_a_dmg_down_iris = 100;
            this.machine_dmg_down_iris = 100;
            this.rock_dmg_down_iris = 100;
            this.element_dmg_down_iris = 100;
            this.undead_dmg_down_iris = 100;
            this.human_hit_up_iris = 100;
            this.bird_hit_up_iris = 100;
            this.animal_hit_up_iris = 100;
            this.insect_hit_up_iris = 100;
            this.magic_c_hit_up_iris = 100;
            this.plant_hit_up_iris = 100;
            this.water_a_hit_up_iris = 100;
            this.machine_hit_up_iris = 100;
            this.rock_hit_up_iris = 100;
            this.element_hit_up_iris = 100;
            this.undead_hit_up_iris = 100;
            this.human_avoid_up_iris = 100;
            this.bird_avoid_up_iris = 100;
            this.animal_avoid_up_iris = 100;
            this.insect_avoid_up_iris = 100;
            this.magic_c_avoid_up_iris = 100;
            this.plant_avoid_up_iris = 100;
            this.water_a_avoid_up_iris = 100;
            this.machine_avoid_up_iris = 100;
            this.rock_avoid_up_iris = 100;
            this.element_avoid_up_iris = 100;
            this.undead_avoid_up_iris = 100;
            this.human_avoid_up_iris = 0;
            this.bird_avoid_up_iris = 0;
            this.animal_avoid_up_iris = 0;
            this.insect_avoid_up_iris = 0;
            this.magic_c_avoid_up_iris = 0;
            this.plant_avoid_up_iris = 0;
            this.water_a_avoid_up_iris = 0;
            this.machine_avoid_up_iris = 0;
            this.rock_avoid_up_iris = 0;
            this.element_avoid_up_iris = 0;
            this.undead_avoid_up_iris = 0;
            //short foot_iris = 0, potion_iris
            this.foot_iris = 100;
            this.potion_iris = 100;
            this.elements_iris[SagaLib.Elements.Neutral] = 0;
            this.elements_iris[SagaLib.Elements.Fire] = 0;
            this.elements_iris[SagaLib.Elements.Water] = 0;
            this.elements_iris[SagaLib.Elements.Wind] = 0;
            this.elements_iris[SagaLib.Elements.Earth] = 0;
            this.elements_iris[SagaLib.Elements.Holy] = 0;
            this.elements_iris[SagaLib.Elements.Dark] = 0;
            this.attackelements_iris[SagaLib.Elements.Neutral] = 0;
            this.attackelements_iris[SagaLib.Elements.Fire] = 0;
            this.attackelements_iris[SagaLib.Elements.Water] = 0;
            this.attackelements_iris[SagaLib.Elements.Wind] = 0;
            this.attackelements_iris[SagaLib.Elements.Earth] = 0;
            this.attackelements_iris[SagaLib.Elements.Holy] = 0;
            this.attackelements_iris[SagaLib.Elements.Dark] = 0;
            this.PhysiceReduceRate = 0;
        }
    }
}
