using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Partner;

namespace SagaMap.Partner
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        public StatusFactory()
        {
        }

        public void CalcPartnerStatus(ActorPartner partner)
        {
            ClearPartnerEquipBouns(partner);
            CalcPartnerEquipBonus(partner);
            CalcPartnerRange(partner);
            CalcPartnerHPMPSP(partner);
            CalcPartnerStats(partner);
        }

        /// <summary>
        /// 计算普通攻击距离
        /// </summary>
        /// <param name="pc"></param>
        public void CalcPartnerRange(ActorPartner partner)
        {
            Dictionary<EnumPartnerEquipSlot, Item> equips=partner.equipments;
            if (equips.ContainsKey(EnumPartnerEquipSlot.WEAPON))
            {
                Item item = equips[EnumPartnerEquipSlot.WEAPON];
                partner.Range = item.BaseData.range;
            }
            else
            {
                partner.Range = (uint)(partner.BaseData.range);
            }
        }

        ushort checkPositive(double num)
        {
            if (num > 0)
                return (ushort)num;
            return 0;
        }

        /// <summary>
        /// 计算素质属性能力
        /// </summary>
        /// <param name="pc"></param>
        private void CalcPartnerStats(ActorPartner partner)
        {      
            float lv_rate = 1f;
            /*if (!partner.rebirth)
            {
                partner.Status.min_atk_bs = (ushort)checkPositive((partner.BaseData.atk_min_fn - partner.BaseData.atk_min_in) * lv_rate + partner.BaseData.atk_min_in);
                partner.Status.max_atk_bs = (ushort)checkPositive((partner.BaseData.atk_max_fn - partner.BaseData.atk_max_in) * lv_rate + partner.BaseData.atk_max_in);
                partner.Status.min_matk_bs = (ushort)checkPositive((partner.BaseData.matk_min_fn - partner.BaseData.matk_min_in) * lv_rate + partner.BaseData.matk_min_in);
                partner.Status.max_matk_bs = (ushort)checkPositive((partner.BaseData.matk_max_fn - partner.BaseData.matk_max_in) * lv_rate + partner.BaseData.matk_max_in);
                partner.Status.def_bs = (ushort)checkPositive((partner.BaseData.def_fn - partner.BaseData.def_in) * lv_rate + partner.BaseData.def_in);
                partner.Status.def_add_bs = (short)checkPositive((partner.BaseData.def_add_fn - partner.BaseData.def_add_in) * lv_rate + partner.BaseData.def_add_in);
                partner.Status.mdef_bs = (ushort)checkPositive((partner.BaseData.mdef_fn - partner.BaseData.mdef_in) * lv_rate + partner.BaseData.mdef_in);
                partner.Status.mdef_add_bs = (short)checkPositive((partner.BaseData.mdef_add_fn - partner.BaseData.mdef_add_in) * lv_rate + partner.BaseData.mdef_add_in);
                partner.Status.hit_melee_bs = (ushort)checkPositive((partner.BaseData.hit_melee_fn - partner.BaseData.hit_melee_in) * lv_rate + partner.BaseData.hit_melee_in);
                partner.Status.hit_ranged_bs = (ushort)checkPositive((partner.BaseData.hit_ranged_fn - partner.BaseData.hit_ranged_in) * lv_rate + partner.BaseData.hit_ranged_in);
                partner.Status.hit_magic_bs = (ushort)checkPositive((partner.BaseData.hit_magic_fn - partner.BaseData.hit_magic_in) * lv_rate + partner.BaseData.hit_magic_in);
                partner.Status.hit_critical_bs = (ushort)checkPositive((partner.BaseData.hit_critical_fn - partner.BaseData.hit_critical_in) * lv_rate + partner.BaseData.hit_critical_in);
                partner.Status.avoid_melee_bs = (ushort)checkPositive((partner.BaseData.avoid_melee_fn - partner.BaseData.avoid_melee_in) * lv_rate + partner.BaseData.avoid_melee_in);
                partner.Status.avoid_ranged_bs = (ushort)checkPositive((partner.BaseData.avoid_ranged_fn - partner.BaseData.avoid_ranged_in) * lv_rate + partner.BaseData.avoid_ranged_in);
                partner.Status.avoid_magic_bs = (ushort)checkPositive((partner.BaseData.avoid_magic_fn - partner.BaseData.avoid_magic_in) * lv_rate + partner.BaseData.avoid_magic_in);
                partner.Status.avoid_critical_bs = (ushort)checkPositive((partner.BaseData.avoid_critical_fn - partner.BaseData.avoid_critical_in) * lv_rate + partner.BaseData.avoid_critical_in);
                partner.Status.aspd_bs = (short)checkPositive((partner.BaseData.aspd_fn - partner.BaseData.aspd_in) * lv_rate + partner.BaseData.aspd_in);
                partner.Status.cspd_bs = (short)checkPositive((partner.BaseData.cspd_fn - partner.BaseData.cspd_in) * lv_rate + partner.BaseData.cspd_in);
                partner.Status.hp_recover_bs = (short)checkPositive((partner.BaseData.hp_rec_fn - partner.BaseData.hp_rec_in) * lv_rate + partner.BaseData.hp_rec_in);
                partner.Status.mp_recover_bs = (short)checkPositive((partner.BaseData.mp_rec_fn - partner.BaseData.mp_rec_in) * lv_rate + partner.BaseData.mp_rec_in);
                partner.Status.sp_recover_bs = (short)checkPositive((partner.BaseData.sp_rec_fn - partner.BaseData.sp_rec_in) * lv_rate + partner.BaseData.sp_rec_in);
            }
            else
            {*/
                partner.Status.min_atk_bs = (ushort)checkPositive((partner.BaseData.atk_min_fn_re - partner.BaseData.atk_min_in_re) * lv_rate + partner.BaseData.atk_min_in_re + partner.perk0 * 1);
                partner.Status.max_atk_bs = (ushort)checkPositive((partner.BaseData.atk_max_fn_re - partner.BaseData.atk_max_in_re) * lv_rate + partner.BaseData.atk_max_in_re + partner.perk0 * 1);
                partner.Status.min_matk_bs = (ushort)checkPositive((partner.BaseData.matk_min_fn_re - partner.BaseData.matk_min_in_re) * lv_rate + partner.BaseData.matk_min_in_re + partner.perk1 * 1);
                partner.Status.max_matk_bs = (ushort)checkPositive((partner.BaseData.matk_max_fn_re - partner.BaseData.matk_max_in_re) * lv_rate + partner.BaseData.matk_max_in_re + partner.perk1 * 1);
                partner.Status.def_bs = (ushort)checkPositive((partner.BaseData.def_fn_re - partner.BaseData.def_in_re) * lv_rate + partner.BaseData.def_in_re + partner.perk2 / 10);
                partner.Status.def_add_bs = (short)checkPositive((partner.BaseData.def_add_fn_re - partner.BaseData.def_add_in_re) * lv_rate + partner.BaseData.def_add_in_re + partner.perk2 * 1);
                partner.Status.mdef_bs = (ushort)checkPositive((partner.BaseData.mdef_fn_re - partner.BaseData.mdef_in_re) * lv_rate + partner.BaseData.mdef_in_re + partner.perk3 / 10);
                partner.Status.mdef_add_bs = (short)checkPositive((partner.BaseData.mdef_add_fn_re - partner.BaseData.mdef_add_in_re) * lv_rate + partner.BaseData.mdef_add_in_re + partner.perk3*1); 
                partner.Status.hit_melee_bs = (ushort)checkPositive((partner.BaseData.hit_melee_fn_re - partner.BaseData.hit_melee_in_re) * lv_rate + partner.BaseData.hit_melee_in_re + partner.perk4 * 3);
                partner.Status.hit_ranged_bs = (ushort)checkPositive((partner.BaseData.hit_ranged_fn_re - partner.BaseData.hit_ranged_in_re) * lv_rate + partner.BaseData.hit_ranged_in_re + partner.perk4 *3);
                partner.Status.hit_magic_bs = (ushort)checkPositive((partner.BaseData.hit_magic_fn_re - partner.BaseData.hit_magic_in_re) * lv_rate + partner.BaseData.hit_magic_in_re + partner.perk4 * 3);
                partner.Status.hit_critical_bs = (ushort)checkPositive((partner.BaseData.hit_critical_fn_re - partner.BaseData.hit_critical_in_re) * lv_rate + partner.BaseData.hit_critical_in_re + partner.perk4 * 3);
                partner.Status.avoid_melee_bs = (ushort)checkPositive((partner.BaseData.avoid_melee_fn_re - partner.BaseData.avoid_melee_in_re) * lv_rate + partner.BaseData.avoid_melee_in_re + partner.perk5 * 3);
                partner.Status.avoid_ranged_bs = (ushort)checkPositive((partner.BaseData.avoid_ranged_fn_re - partner.BaseData.avoid_ranged_in_re) * lv_rate + partner.BaseData.avoid_ranged_in_re + partner.perk5 * 3);
                partner.Status.avoid_magic_bs = (ushort)checkPositive((partner.BaseData.avoid_magic_fn_re - partner.BaseData.avoid_magic_in_re) * lv_rate + partner.BaseData.avoid_magic_in_re + partner.perk5 * 3);
                partner.Status.avoid_critical_bs = (ushort)checkPositive((partner.BaseData.avoid_critical_fn_re - partner.BaseData.avoid_critical_in_re) * lv_rate + partner.BaseData.avoid_critical_in_re + partner.perk5 * 3);
                partner.Status.aspd_bs = (short)checkPositive(partner.perk5 * 2);
                partner.Status.cspd_bs = (short)checkPositive(partner.perk3 * 2);
                partner.Status.hp_recover_bs = (short)checkPositive((partner.BaseData.hp_rec_fn_re - partner.BaseData.hp_rec_in_re) * lv_rate + partner.BaseData.hp_rec_in_re);
                partner.Status.mp_recover_bs = (short)checkPositive((partner.BaseData.mp_rec_fn_re - partner.BaseData.mp_rec_in_re) * lv_rate + partner.BaseData.mp_rec_in_re);
                partner.Status.sp_recover_bs = (short)checkPositive((partner.BaseData.sp_rec_fn_re - partner.BaseData.sp_rec_in_re) * lv_rate + partner.BaseData.sp_rec_in_re);
            //}
                partner.Status.min_atk1 = (ushort)checkPositive((partner.Status.min_atk_bs + partner.Status.atk1_item + partner.Status.min_atk1_skill) * (partner.Status.atk1_rate_item / 100f) * (partner.Status.min_atk1_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.min_atk2 = (ushort)checkPositive((partner.Status.min_atk_bs + partner.Status.atk2_item + partner.Status.min_atk2_skill) * (partner.Status.atk2_rate_item / 100f) * (partner.Status.min_atk2_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.min_atk3 = (ushort)checkPositive((partner.Status.min_atk_bs + partner.Status.atk3_item + partner.Status.min_atk3_skill) * (partner.Status.atk3_rate_item / 100f) * (partner.Status.min_atk3_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.max_atk1 = (ushort)checkPositive((partner.Status.max_atk_bs + partner.Status.atk1_item + partner.Status.max_atk1_skill) * (partner.Status.atk1_rate_item / 100f) * (partner.Status.max_atk1_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.max_atk2 = (ushort)checkPositive((partner.Status.max_atk_bs + partner.Status.atk2_item + partner.Status.max_atk2_skill) * (partner.Status.atk2_rate_item / 100f) * (partner.Status.max_atk2_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.max_atk3 = (ushort)checkPositive((partner.Status.max_atk_bs + partner.Status.atk3_item + partner.Status.max_atk3_skill) * (partner.Status.atk3_rate_item / 100f) * (partner.Status.max_atk3_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.min_matk = (ushort)checkPositive((partner.Status.min_matk_bs + partner.Status.matk_item + partner.Status.min_matk_skill) * (partner.Status.matk_rate_item / 100f) * (partner.Status.min_matk_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.Status.max_matk = (ushort)checkPositive((partner.Status.max_matk_bs + partner.Status.matk_item + partner.Status.max_matk_skill) * (partner.Status.matk_rate_item / 100f) * (partner.Status.max_matk_rate_skill / 100f) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.def = (ushort)checkPositive((partner.Status.def_bs  + partner.Status.def_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.def_add = (short)checkPositive((partner.Status.def_add_bs + partner.Status.def_item + partner.Status.def_add_item + partner.Status.def_add_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.mdef = (ushort)checkPositive((partner.Status.mdef_bs  + partner.Status.mdef_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.mdef_add = (short)checkPositive((partner.Status.mdef_add_bs + partner.Status.mdef_item + partner.Status.mdef_add_item + partner.Status.mdef_add_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.hit_melee = (ushort)checkPositive(((partner.Status.hit_melee_bs + partner.Status.hit_melee_item + partner.Status.hit_melee_skill)) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.hit_ranged = (ushort)checkPositive(((partner.Status.hit_ranged_bs + partner.Status.hit_ranged_item + partner.Status.hit_ranged_skill)) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.hit_magic = (ushort)checkPositive(((partner.Status.hit_magic_bs + partner.Status.hit_magic_item + partner.Status.hit_magic_skill)) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.hit_critical = (ushort)checkPositive((partner.Status.hit_critical_bs + partner.Status.hit_critical_item + partner.Status.hit_critical_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.avoid_melee = (ushort)checkPositive(((partner.Status.avoid_melee_bs + partner.Status.avoid_melee_item + partner.Status.avoid_melee_skill)) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.avoid_ranged = (ushort)checkPositive((partner.Status.avoid_ranged_bs + partner.Status.avoid_ranged_item + partner.Status.avoid_ranged_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.avoid_magic = (ushort)checkPositive((partner.Status.avoid_magic_bs + partner.Status.avoid_magic_item + partner.Status.avoid_magic_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            partner.Status.avoid_critical = (ushort)checkPositive((partner.Status.avoid_critical_bs + partner.Status.avoid_critical_item + partner.Status.avoid_critical_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            if (partner.Status.avoid_melee > 400)
                partner.Status.avoid_melee = 400;
            if (partner.Status.avoid_ranged > 400)
                partner.Status.avoid_ranged = 400;
            if (partner.Status.avoid_magic > 400)
                partner.Status.avoid_magic = 400;
            partner.Status.aspd = (short)checkPositive((partner.Status.aspd_bs + partner.Status.aspd_item + partner.Status.aspd_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            if (partner.Status.aspd > 1500)
                partner.Status.aspd = 1500;
            if (partner.Status.aspd < 1)
            {
                partner.Status.aspd = 1;
            }
            partner.Status.cspd = (short)checkPositive((partner.Status.cspd_bs + partner.Status.cspd_item + partner.Status.cspd_skill) * FoodBouns(partner) * ReliabilityBouns(partner));
            if (partner.Status.cspd > 1500)
                partner.Status.cspd = 1500;
            if (partner.Status.cspd < 1)
            {
                partner.Status.cspd = 1;
            }
            //恢复力计算
            partner.Status.hp_recover = (short)checkPositive(partner.Status.hp_recover_bs + partner.Status.hp_recover_item + partner.Status.hp_recover_skill);
            partner.Status.mp_recover = (short)checkPositive(partner.Status.mp_recover_bs + partner.Status.mp_recover_item + partner.Status.mp_recover_skill);
            partner.Status.sp_recover = (short)checkPositive(partner.Status.sp_recover_bs + partner.Status.sp_recover_item + partner.Status.sp_recover_skill);
        }
        float FoodBouns(ActorPartner partner)
        {
            if (partner.reliabilityuprate <= 100)
                return 1f;
            return partner.reliabilityuprate / 100f;
        }
        float ReliabilityBouns(ActorPartner partner)
        {
            switch(partner.reliability)
            {
                case 0:
                    return 1f;
                case 1:
                    return 1.1f;
                case 2:
                    return 1.2f;
                case 3:
                    return 1.3f;
                case 4:
                    return 1.4f;
                case 5:
                    return 1.5f;
                case 6:
                    return 1.6f;
                case 7:
                    return 1.7f;
                case 8:
                    return 1.8f;
                case 9:
                    return 1.9f;
            }
            return 1f;
        }
        public void CalcPartnerHPMPSP(ActorPartner partner)
        {
            float lv_rate = 1f;
            //if (!partner.rebirth)
            {
                partner.MaxHP = (uint)(((partner.BaseData.hp_fn - partner.BaseData.hp_in) * lv_rate + partner.BaseData.hp_in+partner.perk2 * 10) * FoodBouns(partner) * ReliabilityBouns(partner));
                partner.MaxMP = 0;// (uint)((partner.BaseData.mp_fn - partner.BaseData.mp_in) * lv_rate + partner.BaseData.mp_in);
                partner.MaxSP = 0;// (uint)((partner.BaseData.sp_fn - partner.BaseData.sp_in) * lv_rate + partner.BaseData.sp_in);
            }
            /*else
            {
                partner.MaxHP = (uint)((partner.BaseData.hp_fn_re - partner.BaseData.hp_in_re) * lv_rate + partner.BaseData.hp_in_re);
                partner.MaxMP = (uint)((partner.BaseData.mp_fn_re - partner.BaseData.mp_in_re) * lv_rate + partner.BaseData.mp_in_re);
                partner.MaxSP = (uint)((partner.BaseData.sp_fn_re - partner.BaseData.sp_in_re) * lv_rate + partner.BaseData.sp_in_re);
            }*/
            if (partner.HP > partner.MaxHP) partner.HP = partner.MaxHP;
            if (partner.MP > partner.MaxMP) partner.MP = partner.MaxMP;
            if (partner.SP > partner.MaxSP) partner.SP = partner.MaxSP;
        }

        float HPSystemTypeFactor(ActorPartner partner)
        {
            switch (partner.BaseData.partnersystemid)
            {
                case 0:
                    return 0.80f;
                case 1:
                    return 1.00f;
                case 2:
                    return 1.00f;
                case 3:
                    return 1.00f;
                case 4:
                    return 1.00f;
                case 5:
                    return 1.00f;
                case 6:
                    return 1.00f;
                case 7:
                    return 1.00f;
                case 8:
                    return 1.00f;
                case 9:
                    return 1.00f;
                case 10:
                    return 1.00f;
                case 11:
                    return 1.80f;
                case 12:
                    return 1.80f;
                
                default:
                    return 1;
            }
        }

        float MPSystemTypeFactor(ActorPartner partner)
        {
            switch (partner.BaseData.partnersystemid)
            {
                case 0:
                    return 0.80f;
                case 1:
                    return 1.00f;
                case 2:
                    return 1.00f;
                case 3:
                    return 1.00f;
                case 4:
                    return 1.00f;
                case 5:
                    return 1.00f;
                case 6:
                    return 1.00f;
                case 7:
                    return 1.00f;
                case 8:
                    return 1.00f;
                case 9:
                    return 1.00f;
                case 10:
                    return 1.00f;
                case 11:
                    return 1.80f;
                case 12:
                    return 1.80f;

                default:
                    return 1;
            }
        }

        float SPSystemTypeFactor(ActorPartner partner)
        {
            switch (partner.BaseData.partnersystemid)
            {
                case 0:
                    return 0.80f;
                case 1:
                    return 1.00f;
                case 2:
                    return 1.00f;
                case 3:
                    return 1.00f;
                case 4:
                    return 1.00f;
                case 5:
                    return 1.00f;
                case 6:
                    return 1.00f;
                case 7:
                    return 1.00f;
                case 8:
                    return 1.00f;
                case 9:
                    return 1.00f;
                case 10:
                    return 1.00f;
                case 11:
                    return 1.80f;
                case 12:
                    return 1.80f;

                default:
                    return 1;
            }
        }
    }
}
