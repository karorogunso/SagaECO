using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.PC
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        public StatusFactory()
        {

        }
        public void CalcStatus(ActorPC pc)
        {
            /*bool blocked = ClientManager.Blocked;
            if(!blocked)*/
            ClientManager.EnterCriticalArea();
            CalcEquipBonus(pc);
            CalcAnoPaperBouns(pc);
            CalcPlayerTitleBouns(pc);
            CalcRange(pc);
            //CalcStatsRev(pc);
            CalcPayV(pc);
            CalcHPMPSP(pc);
            CalcStats(pc);
            pc.Inventory.CalcPayloadVolume();
            /*if (blocked)*/
            ClientManager.LeaveCriticalArea();
        }

        public void ClearFoodStatus(ActorPC pc)
        {
            pc.Status.hit_magic_food = 0;
            pc.Status.cri_avoid_food = 0;
            pc.Status.avoid_magic_food = 0;
            pc.Status.aspd_food = 0;
            pc.Status.cspd_food = 0;
            pc.Status.str_food = 0;
            pc.Status.mag_food = 0;
            pc.Status.agi_food = 0;
            pc.Status.dex_food = 0;
            pc.Status.vit_food = 0;
            pc.Status.int_food = 0;
            pc.Status.hp_food = 0;
            pc.Status.mp_food = 0;
            pc.Status.sp_food = 0;
            pc.Status.hprecov_food = 0;
            pc.Status.mprecov_food = 0;
            pc.Status.sprecov_food = 0;
            pc.Status.min_atk1_food = 0;
            pc.Status.min_atk2_food = 0;
            pc.Status.min_atk3_food = 0;
            pc.Status.max_atk1_food = 0;
            pc.Status.max_atk2_food = 0;
            pc.Status.max_atk3_food = 0;
            pc.Status.min_matk_food = 0;
            pc.Status.max_matk_food = 0;
            pc.Status.def_add_food = 0;
            pc.Status.mdef_add_food = 0;
            pc.Status.cri_food = 0;
            pc.Status.hit_melee_food = 0;
            pc.Status.hit_ranged_food = 0;
            pc.Status.avoid_melee_food = 0;
            pc.Status.avoid_ranged_food = 0;
        }

        public void ClearSkillStatus(ActorPC pc)
        {
            pc.Status.agi_skill = 0;
            pc.Status.aspd_rate_skill = 0;
            pc.Status.aspd_skill = 0;
            pc.Status.avoid_critical_skill = 0;
            pc.Status.avoid_magic_skill = 0;
            pc.Status.avoid_melee_skill = 0;
            pc.Status.avoid_ranged_skill = 0;
            pc.Status.combo_rate_skill = 0;
            pc.Status.cspd_rate_skill = 0;
            pc.Status.cspd_skill = 0;
            pc.Status.def_add_skill = 0;
            pc.Status.def_skill = 0;
            pc.Status.dex_skill = 0;
            pc.Status.hp_skill = 0;
            pc.Status.int_skill = 0;
            pc.Status.mag_skill = 0;
            pc.Status.max_atk1_skill = 0;
            pc.Status.max_atk2_skill = 0;
            pc.Status.max_atk3_skill = 0;
            pc.Status.min_atk1_skill = 0;
            pc.Status.min_atk2_skill = 0;
            pc.Status.min_atk3_skill = 0;
            pc.Status.mdef_skill = 0;
            pc.Status.mdef_add_skill = 0;
            pc.Status.min_matk_skill = 0;
            pc.Status.max_matk_skill = 0;
            pc.Status.str_skill = 0;
        }
        public void CalcStatusOnSkillEffect(ActorPC pc)
        {
            CalcStats(pc);
            CalcHPMPSP(pc);
            
        }
        /// <summary>
        /// 计算普通攻击距离
        /// </summary>
        /// <param name="pc"></param>
        public void CalcRange(ActorPC pc)
        {
            IConcurrentDictionary<EnumEquipSlot, Item> equips;
            if (pc.Form == DEM_FORM.NORMAL_FORM)
                equips = pc.Inventory.Equipments;
            else
                equips = pc.Inventory.Parts;
            if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND) && pc.TInt["斥候远程模式"] == 1 && pc.Job == PC_JOB.HAWKEYE)
            {
                Item item = equips[EnumEquipSlot.LEFT_HAND];
                pc.Range = item.BaseData.range;
                if ((item.BaseData.itemType == ItemType.BOW || item.BaseData.itemType == ItemType.GUN || item.BaseData.itemType == ItemType.RIFLE || item.BaseData.itemType == ItemType.DUALGUN))
                    pc.Range = 9;

            }
            else if (pc.Job == PC_JOB.HAWKEYE)
            {
                if (equips.ContainsKey(EnumEquipSlot.LEFT_HAND) && pc.TInt["斥候远程模式"] == 1)
                {
                    Item item = equips[EnumEquipSlot.LEFT_HAND];
                    pc.Range = item.BaseData.range;
                }
                else
                {
                    pc.Range = 1;
                }
            }
            else if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND) && pc.Job != PC_JOB.HAWKEYE)
            {
                Item item = equips[EnumEquipSlot.RIGHT_HAND];
                if (item.BaseData.itemType == ItemType.BOOK || item.BaseData.itemType == ItemType.STAFF || (item.BaseData.itemType == ItemType.STRINGS && pc.Job == PC_JOB.CARDINAL))
                    pc.Range = 7;
                else if (pc.Status.Additions.ContainsKey("魔法少女") && (item.BaseData.itemType == ItemType.GUN || item.BaseData.itemType == ItemType.BOW || item.BaseData.itemType == ItemType.SPEAR || item.BaseData.itemType == ItemType.RIFLE || item.BaseData.itemType == ItemType.RAPIER))
                    pc.Range = 7;
                else
                    pc.Range = item.BaseData.range;
            }
            else
                pc.Range = 1;
           Network.Client.MapClient.FromActorPC(pc).SendRange();
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
        private void CalcStats(ActorPC pc)
        {
            ushort pet_min_atk1, pet_min_atk2, pet_min_atk3,
                pet_max_atk1, pet_max_atk2, pet_max_atk3,
                pet_min_matk, pet_max_matk,
                pet_def, pet_def_add, pet_mdef, pet_mdef_add,
                pet_aspd, pet_cspd,
                pet_hit_melee, pet_hit_ranged, pet_avoid_melee, pet_avoid_ranged;

            float rate = 1f;

            if (pc.Partner != null)
            {
                pet_min_atk1 = (ushort)checkPositive((float)(pc.Partner.Status.min_atk1 * rate));
                pet_min_atk2 = (ushort)checkPositive((float)(pc.Partner.Status.min_atk2 * rate));
                if (pet_min_atk2 > pet_min_atk1)
                    pet_min_atk1 = pet_min_atk2;
                pet_min_atk3 = (ushort)checkPositive((float)(pc.Partner.Status.min_atk3 * rate));
                if (pet_min_atk3 > pet_min_atk1)
                    pet_min_atk1 = pet_min_atk3;

                pet_max_atk1 = (ushort)checkPositive((float)(pc.Partner.Status.max_atk1 * rate));
                pet_max_atk2 = (ushort)checkPositive((float)(pc.Partner.Status.max_atk2 * rate));
                if (pet_max_atk2 > pet_max_atk1)
                    pet_max_atk1 = pet_max_atk2;
                pet_max_atk3 = (ushort)checkPositive((float)(pc.Partner.Status.max_atk3 * rate));
                if (pet_max_atk3 > pet_max_atk1)
                    pet_max_atk1 = pet_max_atk3;

                pet_min_matk = (ushort)checkPositive((float)(pc.Partner.Status.min_matk * rate));
                pet_max_matk = (ushort)checkPositive((float)(pc.Partner.Status.min_matk * rate));
                pet_def = (ushort)checkPositive((float)(pc.Partner.Status.def * rate));
                pet_def_add = (ushort)checkPositive((float)(pc.Partner.Status.def_add * rate));
                pet_mdef = (ushort)checkPositive((float)(pc.Partner.Status.mdef * rate));
                pet_mdef_add = (ushort)checkPositive((float)(pc.Partner.Status.mdef_add * rate));
                pet_aspd = (ushort)checkPositive((float)(pc.Partner.Status.aspd * rate));
                pet_cspd = (ushort)checkPositive((float)(pc.Partner.Status.cspd * rate));
                pet_hit_melee = (ushort)checkPositive((float)(pc.Partner.Status.hit_melee * rate));
                pet_hit_ranged = (ushort)checkPositive((float)(pc.Partner.Status.hit_ranged * rate));
                pet_avoid_melee = (ushort)checkPositive((float)(pc.Partner.Status.avoid_melee * rate));
                pet_avoid_ranged = (ushort)checkPositive((float)(pc.Partner.Status.avoid_ranged * rate));

                pc.TInt["Partner_HP"] = (ushort)checkPositive((float)(pc.Partner.MaxHP * rate * 0.1f));
            }
            else
            {
                pet_min_atk1 = 0;
                pet_min_atk2 = 0;
                pet_min_atk3 = 0;
                pet_max_atk1 = 0;
                pet_max_atk2 = 0;
                pet_max_atk3 = 0;
                pet_min_matk = 0;
                pet_max_matk = 0;
                pet_def = 0;
                pet_def_add = 0;
                pet_mdef = 0;
                pet_mdef_add = 0;
                pet_aspd = 0;
                pet_cspd = 0;
                pet_hit_melee = 0;
                pet_hit_ranged = 0;
                pet_avoid_melee = 0;
                pet_avoid_ranged = 0;
                pc.TInt["Partner_HP"] = 0;
            }

            //Item pet = pc.Inventory.Equipments[EnumEquipSlot.PET];

            //攻击力计算
            int TotalStr = pc.Str + pc.Status.str_item + pc.Status.str_iris + pc.Status.str_rev + pc.Status.str_skill + pc.Status.str_chip + pc.Status.str_ano + pc.Status.str_tit + pc.Status.str_food;
            ushort minatk = checkPositive(50 * 2 + TotalStr - 50);
            pc.Status.min_atk_bs = minatk;
            pc.Status.min_atk1 = (ushort)checkPositive((pc.Status.min_atk_bs + pc.Status.atk1_item + pc.Status.min_atk1_iris + pc.Status.min_atk1_skill + pc.Status.min_atk1_ano + pc.Status.min_atk1_tit + pc.Status.min_atk1_food + pet_min_atk1 + pc.Status.min_atk1_petpy) * (pc.Status.atk1_rate_item / 100f) * (pc.Status.min_atk1_rate_iris / 100f) * (pc.Status.min_atk1_rate_skill / 100f) + pc.TInt["临时ATK"]);
            pc.Status.min_atk2 = (ushort)checkPositive((pc.Status.min_atk_bs + pc.Status.atk2_item + pc.Status.min_atk2_iris + pc.Status.min_atk2_skill + pc.Status.min_atk2_ano + pc.Status.min_atk2_tit + pc.Status.min_atk2_food + pet_min_atk2 + pc.Status.min_atk1_petpy) * (pc.Status.atk2_rate_item / 100f) * (pc.Status.min_atk2_rate_iris / 100f) * (pc.Status.min_atk2_rate_skill / 100f) + pc.TInt["临时ATK"]);
            pc.Status.min_atk3 = (ushort)checkPositive((pc.Status.min_atk_bs + pc.Status.atk3_item + pc.Status.min_atk3_iris + pc.Status.min_atk3_skill + pc.Status.min_atk3_ano + pc.Status.min_atk3_tit + pc.Status.min_atk3_food + pet_min_atk3 + pc.Status.min_atk1_petpy) * (pc.Status.atk3_rate_item / 100f) * (pc.Status.min_atk3_rate_iris / 100f) * (pc.Status.min_atk3_rate_skill / 100f) + pc.TInt["临时ATK"]);

            TotalStr = pc.Str + pc.Status.str_item + pc.Status.str_iris + pc.Status.str_rev + pc.Status.str_skill + pc.Status.str_chip + pc.Status.str_ano + pc.Status.str_tit + pc.Status.str_food;
            ushort maxatk = checkPositive(50 * 4 + (TotalStr - 50) * 3);
            pc.Status.max_atk_bs = maxatk;
            pc.Status.max_atk1 = (ushort)checkPositive((pc.Status.max_atk_bs + pc.Status.atk1_item + pc.Status.max_atk1_iris + pc.Status.max_atk1_skill + pc.Status.max_atk1_ano + pc.Status.max_atk1_tit + pc.Status.max_atk1_food + pet_max_atk1 + pc.Status.max_atk1_petpy) * (pc.Status.atk1_rate_item / 100f) * (pc.Status.max_atk1_rate_iris / 100f) * (pc.Status.max_atk1_rate_skill / 100f) + pc.TInt["临时ATK"]);
            pc.Status.max_atk2 = (ushort)checkPositive((pc.Status.max_atk_bs + pc.Status.atk2_item + pc.Status.max_atk2_iris + pc.Status.max_atk2_skill + pc.Status.max_atk2_ano + pc.Status.max_atk2_tit + pc.Status.max_atk2_food + pet_max_atk2 + pc.Status.max_atk1_petpy) * (pc.Status.atk2_rate_item / 100f) * (pc.Status.max_atk2_rate_iris / 100f) * (pc.Status.max_atk2_rate_skill / 100f) + pc.TInt["临时ATK"]);
            pc.Status.max_atk3 = (ushort)checkPositive((pc.Status.max_atk_bs + pc.Status.atk3_item + pc.Status.max_atk3_iris + pc.Status.max_atk3_skill + pc.Status.max_atk3_ano + pc.Status.max_atk3_tit + pc.Status.max_atk3_food + pet_max_atk3 + pc.Status.max_atk1_petpy) * (pc.Status.atk3_rate_item / 100f) * (pc.Status.max_atk3_rate_iris / 100f) * (pc.Status.max_atk3_rate_skill / 100f) + pc.TInt["临时ATK"]);

            pc.Status.min_atk1 = (ushort)(pc.Status.min_atk1 + pc.Status.atk_min_to_max_per / 100f * (pc.Status.max_atk1 - pc.Status.min_atk1));
            pc.Status.min_atk2 = (ushort)(pc.Status.min_atk2 + pc.Status.atk_min_to_max_per / 100f * (pc.Status.max_atk2 - pc.Status.min_atk2));
            pc.Status.min_atk3 = (ushort)(pc.Status.min_atk3 + pc.Status.atk_min_to_max_per / 100f * (pc.Status.max_atk3 - pc.Status.min_atk3));
            pc.Status.min_atk1 = Math.Min(pc.Status.min_atk1, pc.Status.max_atk1);
            pc.Status.min_atk2 = Math.Min(pc.Status.min_atk2, pc.Status.max_atk2);
            pc.Status.min_atk3 = Math.Min(pc.Status.min_atk3, pc.Status.max_atk3);

            int TotalMag = pc.Mag + pc.Status.mag_item + pc.Status.mag_iris + pc.Status.mag_rev + pc.Status.mag_skill + pc.Status.mag_chip + pc.Status.mag_ano + pc.Status.mag_tit + pc.Status.mag_food;
            int TotalInt = pc.Int + pc.Status.int_item + pc.Status.int_iris + pc.Status.int_rev + pc.Status.int_skill + pc.Status.int_chip + pc.Status.int_ano + pc.Status.int_tit + pc.Status.int_food;
            ushort minmatk = checkPositive(50 * 2f + TotalMag * 1 + TotalInt * 1);
            pc.Status.min_matk_bs = minmatk;
            pc.Status.min_matk = (ushort)checkPositive((pc.Status.min_matk_bs + pc.Status.matk_item + pc.Status.min_matk_iris + pc.Status.min_matk_skill + pc.Status.min_matk_ano + pc.Status.min_matk_tit + pc.Status.min_matk_food + pet_min_matk + pc.Status.min_matk_petpy) * (pc.Status.matk_rate_item / 100f) * (pc.Status.min_matk_rate_iris / 100f) * (pc.Status.min_matk_rate_skill / 100f) + pc.TInt["临时MATK"]);

            TotalMag = pc.Mag + pc.Status.mag_item + pc.Status.mag_iris + pc.Status.mag_rev + pc.Status.mag_skill + pc.Status.mag_chip + pc.Status.mag_ano + pc.Status.mag_tit + pc.Status.mag_food;
            ushort maxmatk = (ushort)checkPositive(50 * 4f + (TotalMag - 50) * 3);
            pc.Status.max_matk_bs = maxmatk;
            pc.Status.max_matk = (ushort)checkPositive((pc.Status.max_matk_bs + pc.Status.matk_item + pc.Status.max_matk_iris + pc.Status.max_matk_skill + pc.Status.max_matk_ano + pc.Status.max_matk_tit + pc.Status.max_matk_food + pet_max_matk + pc.Status.max_matk_petpy) * (pc.Status.matk_rate_item / 100f) * (pc.Status.max_matk_rate_iris / 100f) * (pc.Status.max_matk_rate_skill / 100f) + pc.TInt["临时MATK"]);

            pc.Status.min_matk = (ushort)(pc.Status.min_matk + pc.Status.matk_min_to_max_per / 100f * (pc.Status.max_matk - pc.Status.min_matk));
            pc.Status.min_matk = Math.Min(pc.Status.min_matk, pc.Status.max_matk);

            if (pc.Status.Additions.ContainsKey("MAtkMinToMax"))
            {
                pc.Status.min_matk = pc.Status.max_matk;
            }
            //命中计算
            int TotalDex = pc.Dex + pc.Status.dex_item + pc.Status.dex_iris + pc.Status.dex_rev + pc.Status.dex_skill + pc.Status.dex_chip + pc.Status.dex_ano + pc.Status.dex_tit + pc.Status.dex_food;
            ushort hit_melee = (ushort)checkPositive((TotalDex * 2f));
            pc.Status.hit_melee_bs = hit_melee;
            pc.Status.hit_melee = (ushort)checkPositive((pc.Status.hit_melee_bs + pc.Status.hit_melee_item + pc.Status.hit_melee_iris + pc.Status.hit_melee_skill + pc.Status.hit_melee_ano + pc.Status.hit_melee_tit + pc.Status.hit_melee_food + pet_hit_melee + pc.Status.hit_melee_petpy));

            ushort hit_ranged = (ushort)checkPositive((TotalDex * 2f));
            pc.Status.hit_ranged_bs = hit_ranged;
            pc.Status.hit_ranged = (ushort)checkPositive((pc.Status.hit_ranged_bs + pc.Status.hit_ranged_item + pc.Status.hit_ranged_iris + pc.Status.hit_ranged_skill + pc.Status.hit_ranged_ano + pc.Status.hit_ranged_tit + pc.Status.hit_ranged_food + pet_hit_ranged + pc.Status.hit_ranged_petpy));

            ushort hit_magic = (ushort)checkPositive((pc.Int + pc.Status.int_item + pc.Status.int_iris + pc.Status.int_rev + pc.Status.int_skill + pc.Status.int_chip + pc.Status.int_ano + pc.Status.int_tit + pc.Status.int_food) * 2f);
            pc.Status.hit_magic_bs = hit_magic;
            pc.Status.hit_magic = (ushort)checkPositive((pc.Status.hit_magic_bs + pc.Status.hit_magic_item + pc.Status.hit_magic_iris + pc.Status.hit_magic_skill + pc.Status.hit_magic_tit + pc.Status.hit_magic_food));

            ushort hit_critical = 5; //no dependence on status points
            pc.Status.hit_critical_bs = hit_critical;
            pc.Status.hit_critical = (ushort)checkPositive(pc.Status.hit_critical_bs + pc.Status.hit_critical_item + pc.Status.hit_critical_iris + pc.Status.hit_critical_skill + pc.Status.cri_tit + pc.Status.cri_food);

            //闪避计算
            ushort avoid_melee = (ushort)checkPositive((pc.Agi + pc.Status.agi_item + pc.Status.agi_iris + pc.Status.agi_rev + pc.Status.agi_skill + pc.Status.agi_chip + pc.Status.agi_ano + pc.Status.agi_tit + pc.Status.agi_food) * 1f);
            pc.Status.avoid_melee_bs = avoid_melee;
            pc.Status.avoid_melee = (ushort)checkPositive((pc.Status.avoid_melee_bs + pc.Status.avoid_melee_item + pc.Status.avoid_melee_iris + pc.Status.avoid_melee_skill + pc.Status.avoid_melee_ano + pc.Status.avoid_melee_tit + pc.Status.avoid_melee_food + pet_avoid_melee + pc.Status.avoid_melee_petpy));

            ushort avoid_ranged = (ushort)checkPositive((pc.Agi + pc.Status.agi_item + pc.Status.agi_iris + pc.Status.agi_rev + pc.Status.agi_skill + pc.Status.agi_chip + pc.Status.agi_ano + pc.Status.agi_tit + pc.Status.agi_food) * 1f);
            pc.Status.avoid_ranged_bs = avoid_ranged;
            pc.Status.avoid_ranged = (ushort)checkPositive(pc.Status.avoid_ranged_bs + pc.Status.avoid_ranged_item + pc.Status.avoid_ranged_iris + pc.Status.avoid_ranged_skill + pc.Status.avoid_ranged_ano + pc.Status.avoid_ranged_tit + pc.Status.avoid_ranged_food + pet_avoid_ranged + pc.Status.avoid_ranged_petpy);

            ushort avoid_magic = (ushort)checkPositive((pc.Int + pc.Status.int_item + pc.Status.int_iris + pc.Status.int_rev + pc.Status.int_skill + pc.Status.int_chip + pc.Status.int_ano + pc.Status.int_tit + pc.Status.int_food) * 1f);
            pc.Status.avoid_magic_bs = avoid_magic;
            pc.Status.avoid_magic = (ushort)checkPositive(pc.Status.avoid_magic_bs + pc.Status.avoid_magic_item + pc.Status.avoid_magic_iris + pc.Status.avoid_magic_skill + pc.Status.avoid_magic_tit + pc.Status.avoid_magic_food);

            ushort avoid_critical = 5;
            pc.Status.avoid_critical_bs = avoid_critical;
            pc.Status.avoid_critical = (ushort)checkPositive(pc.Status.avoid_critical_bs + pc.Status.avoid_critical_item + pc.Status.avoid_critical_iris + pc.Status.avoid_critical_skill);

            if (pc.Status.avoid_melee > 400)
                pc.Status.avoid_melee = 400;
            if (pc.Status.avoid_ranged > 400)
                pc.Status.avoid_ranged = 400;
            if (pc.Status.avoid_magic > 400)
                pc.Status.avoid_magic = 400;

            //防御计算
            int TotalVit = pc.Vit + pc.Status.vit_item + pc.Status.vit_iris + pc.Status.vit_rev + pc.Status.vit_skill + pc.Status.vit_chip + pc.Status.vit_ano + pc.Status.vit_tit + pc.Status.vit_food;

            pc.Status.def_bs = 0;
            pc.Status.def = checkPositive(pc.Status.def_bs + pc.Status.def_item + pc.Status.def_iris + pc.Status.def_skill + pet_def);
            pc.Status.def_add_bs = (short)checkPositive((TotalVit - 50) * 5);
            pc.Status.def_add = (short)checkPositive(pc.Status.def_add_bs + pc.Status.def_add_item + pc.Status.def_add_iris + pc.Status.def_add_skill + pc.Status.def_add_ano + pc.Status.def_add_tit + pc.Status.def_add_food + pet_def_add + pc.Status.def_add_petpy);


            pc.Status.mdef_bs = 0;
            pc.Status.mdef = (ushort)checkPositive(pc.Status.mdef_bs + pc.Status.mdef_item + pc.Status.mdef_iris + pc.Status.mdef_skill + pet_mdef);
            pc.Status.mdef_add_bs = (short)checkPositive((TotalInt - 50) * 5);
            pc.Status.mdef_add = (short)checkPositive(pc.Status.mdef_add_bs + pc.Status.mdef_add_item + pc.Status.mdef_add_iris + pc.Status.mdef_add_skill + pc.Status.mdef_add_ano + pc.Status.mdef_add_tit + pc.Status.mdef_add_food + pet_mdef_add + pc.Status.mdef_add_petpy);

            //格挡计算
            pc.Status.guard_bs = 0;
            pc.Status.guard = (short)checkPositive(pc.Status.guard_bs + pc.Status.guard_item + pc.Status.guard_iris + pc.Status.guard_skill);

            //aspd&cspd计算
            ushort agi = (ushort)checkPositive(pc.Agi + pc.Status.agi_item + pc.Status.agi_iris + pc.Status.agi_rev + pc.Status.agi_skill + pc.Status.agi_chip + pc.Status.agi_ano + pc.Status.agi_tit + pc.Status.agi_food);
            ushort agi_effect = (ushort)checkPositive(agi * CalcAspdWeaponFactor(pc) * pc.Status.aspd_rate_item / 100f * pc.Status.aspd_rate_iris / 100f * pc.Status.aspd_rate_skill / 100f);
            pc.Status.aspd_bs = (short)checkPositive(1000 * 20 * agi / (20f * agi + 1000f));
            pc.Status.aspd = (short)checkPositive(1000 * 20 * agi_effect / (20f * agi_effect + 1000f) + pc.Status.aspd_item + pc.Status.aspd_iris + pc.Status.aspd_skill + pc.Status.aspd_petpy + pet_aspd + pc.Status.aspd_tit + pc.Status.aspd_food);
            if (pc.Status.aspd > 300)
                pc.Status.aspd -= 300;
            else
                pc.Status.aspd = 0;
            if (pc.Status.aspd > 900)
                pc.Status.aspd = 900;
           // pc.Status.aspd = 900;

            if (pc.Status.aspd < 1)
            {
                pc.Status.aspd = 1;
            }

            ushort dex = (ushort)checkPositive(pc.Dex + pc.Status.dex_item + pc.Status.dex_iris + pc.Status.dex_rev + pc.Status.dex_skill + pc.Status.dex_chip + pc.Status.dex_ano + pc.Status.dex_tit + pc.Status.dex_food);
            ushort dex_effect = (ushort)checkPositive(dex * CalcCspdWeaponFactor(pc) * pc.Status.cspd_rate_item / 100f * pc.Status.cspd_rate_iris / 100f * pc.Status.cspd_rate_skill / 100f);
            pc.Status.cspd_bs = (short)checkPositive(1000 * 20 * dex / (20f * dex + 1000f));
            pc.Status.cspd = (short)(1000 * 20 * dex_effect / (20f * dex_effect + 1000f) + pc.Status.cspd_item + pc.Status.cspd_iris + pc.Status.cspd_skill + pc.Status.cspd_petpy + pet_cspd + pc.Status.cspd_tit + +pc.Status.cspd_food);
            if (pc.Status.cspd > 1500)
                pc.Status.cspd = 1500;
            if (pc.Status.cspd < 1)
            {
                pc.Status.cspd = 1;
            }

            //恢复力计算
            pc.Status.hp_recover_bs = (short)checkPositive(pc.Vit + pc.Status.vit_item + pc.Status.vit_iris + pc.Status.vit_rev + pc.Status.vit_skill + pc.Status.vit_chip + pc.Status.vit_ano + pc.Status.vit_tit + pc.Status.vit_food);
            pc.Status.hp_recover = (short)checkPositive(pc.Status.hp_recover_bs + pc.Status.hp_recover_item + pc.Status.hp_recover_iris + pc.Status.hp_recover_skill + pc.Status.hprecov_tit + pc.Status.hprecov_food);
            pc.Status.mp_recover_bs = (short)checkPositive(pc.Mag + pc.Status.mag_item + pc.Status.mag_iris + pc.Status.mag_rev + pc.Status.mag_skill + pc.Status.mag_chip + pc.Status.mag_ano + pc.Status.mag_tit + pc.Status.mag_food);
            pc.Status.mp_recover = (short)checkPositive(pc.Status.mp_recover_bs + pc.Status.mp_recover_item + pc.Status.mp_recover_iris + pc.Status.mp_recover_skill + pc.Status.mprecov_tit + +pc.Status.mprecov_food);
            pc.Status.sp_recover_bs = (short)checkPositive(pc.Str + pc.Status.str_item + pc.Status.str_iris + pc.Status.str_rev + pc.Status.str_skill + pc.Status.str_chip + pc.Status.str_ano + pc.Status.str_tit + pc.Status.str_food);
            pc.Status.sp_recover = (short)checkPositive(pc.Status.sp_recover_bs + pc.Status.sp_recover_item + pc.Status.sp_recover_iris + pc.Status.sp_recover_skill + pc.Status.sprecov_tit + pc.Status.sprecov_food);


            if (pc.TInt["固定最小物理攻击力"] != 0)
            {
                pc.Status.min_atk1 = (ushort)pc.TInt["固定最小物理攻击力"];
                pc.Status.min_atk2 = (ushort)pc.TInt["固定最小物理攻击力"];
                pc.Status.min_atk3 = (ushort)pc.TInt["固定最小物理攻击力"];
            }
            if (pc.TInt["固定最大物理攻击力"] != 0)
            {
                pc.Status.max_atk1 = (ushort)pc.TInt["固定最大物理攻击力"];
                pc.Status.max_atk2 = (ushort)pc.TInt["固定最大物理攻击力"];
                pc.Status.max_atk3 = (ushort)pc.TInt["固定最大物理攻击力"];
            }
            if (pc.TInt["固定最小魔法攻击力"] != 0)
                pc.Status.min_matk = (ushort)pc.TInt["固定最小魔法攻击力"];
            if (pc.TInt["固定最大魔法攻击力"] != 0)
                pc.Status.max_matk = (ushort)pc.TInt["固定最大魔法攻击力"];

            if (pc.TInt["固定暴击率"] != 0)
            {
                pc.Status.hit_critical = (ushort)pc.TInt["固定暴击率"];
            }
            if (pc.TInt["固定暴击伤害"] != 0)
            {
                pc.Status.hit_magic = (ushort)pc.TInt["固定暴击伤害"];
                pc.Status.hit_melee = (ushort)pc.TInt["固定暴击伤害"];
                pc.Status.hit_ranged = (ushort)pc.TInt["固定暴击伤害"];
            }
            if (pc.TInt["固定防御力"] != 0)
            {
                pc.Status.def = (ushort)pc.TInt["固定防御力"];
                pc.Status.mdef = (ushort)pc.TInt["固定防御力"];
            }
            if(pc.TInt["固定加算防御力"] != 0)
            {
                pc.Status.def_add = (short)pc.TInt["固定加算防御力"];
                pc.Status.mdef_add = (short)pc.TInt["固定加算防御力"];
            }
            if (pc.TInt["固定速度"] != 0)
            {
                pc.Status.aspd = (short)pc.TInt["固定速度"];
                pc.Status.cspd = (short)pc.TInt["固定速度"];
            }

            if (pc.Status.max_matk >= 1000 && pc.Job == PC_JOB.CARDINAL)
                Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 123, 1);

        }

        public ushort RequiredBonusPoint(ushort current)
        {
            return (ushort)((current / 6) + 1);
        }

        public ushort GetTotalBonusPointForStats(ushort start, ushort stat)
        {
            int points = 0;
            for (ushort i = start; i < stat; i++)
            {
                points += RequiredBonusPoint(i);
            }
            return (ushort)points;
        }

        float CalcAspdWeaponFactor(ActorPC pc)
        {
            float r = 1.00f;
            /*if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                Item item = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                switch (item.BaseData.itemType)
                {
                    case ItemType.AXE:
                        r = 1.00f;
                        break;
                    case ItemType.BOOK:
                        r = 1.00f;
                        break;
                }
                if (item.BaseData.doubleHand && !pc.Status.Additions.ContainsKey("暗樱"))
                {
                    r = r * 0.7f;
                }
            }
            else
            { }*/
            return r;
        }

        float CalcCspdWeaponFactor(ActorPC pc)
        {
            float r = 1.00f;
            /*if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                Item item = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                switch (item.BaseData.itemType)
                {
                    case ItemType.AXE:
                        r = 1.00f;
                        break;
                    case ItemType.BOOK:
                        r = 1.00f;
                        break;
                }
                if (item.BaseData.doubleHand && !pc.Status.Additions.ContainsKey("暗樱"))
                {
                    r = r * 0.7f;
                }
            }
            else
            { }*/
            return r;
        }

        public void CalcPayV(ActorPC pc)
        {
            CalcPayl(pc);
            CalcVolume(pc);
        }

        void CalcVolume(ActorPC pc)
        {
            pc.Inventory.MaxVolume[SagaDB.Item.ContainerType.BODY] = (uint)(1000 + Configuration.Instance.VolumeRate * 0);
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                pc.Inventory.MaxVolume[ContainerType.BODY] = (uint)(pc.Inventory.MaxVolume[ContainerType.BODY] + pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.volumeUp + pc.Inventory.Equipments[EnumEquipSlot.PET].VolumeUp);
            if (pc.Account.GMLevel > 50)
                pc.Inventory.MaxVolume[ContainerType.BODY] += 2000000;
            else pc.Inventory.MaxVolume[ContainerType.BODY] += 8000;
        }

        void CalcPayl(ActorPC pc)
        {
            pc.Inventory.MaxPayload[SagaDB.Item.ContainerType.BODY] = (uint)(2000 + Configuration.Instance.PayloadRate * 0);
            if (pc.Status.Additions.ContainsKey("GoRiKi"))
            {
                Skill.Additions.Global.DefaultPassiveSkill skill = (Skill.Additions.Global.DefaultPassiveSkill)pc.Status.Additions["GoRiKi"];
                pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)(pc.Inventory.MaxPayload[ContainerType.BODY] * (1f + (float)(skill["GoRiKi"]) / 100));
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)(pc.Inventory.MaxPayload[ContainerType.BODY] + pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.weightUp + pc.Inventory.Equipments[EnumEquipSlot.PET].WeightUp);
            if (pc.Account.GMLevel > 50)
                pc.Inventory.MaxPayload[ContainerType.BODY] += 2000000;
            else pc.Inventory.MaxPayload[ContainerType.BODY] += 8000;
        }

        public void CalcHPMPSP(ActorPC pc)
        {
            pc.MaxHP = CalcMaxHP(pc);
            pc.MaxMP = CalcMaxMP(pc);
            pc.MaxSP = CalcMaxSP(pc);
            pc.MaxEP = CalcMaxEP(pc);
            //if (pc.HP > pc.MaxHP) pc.HP = pc.MaxHP;
            //if (pc.MP > pc.MaxMP) pc.MP = pc.MaxMP;
            //if (pc.SP > pc.MaxSP) pc.SP = pc.MaxSP;
            //if (pc.EP > pc.MaxEP) pc.EP = pc.MaxEP;
        }

        uint CalcMaxEP(ActorPC pc)
        {
            /*if (pc.Ring == null)
                return 30;
            else
                return (uint)(30 + pc.Ring.MemberCount * 2);*/
            //EP改革！
            uint maxep = 10000;
            if (pc.Job == PC_JOB.SOULTAKER)
                maxep = 15;
            return maxep;
        }

        uint CalcMaxHP(ActorPC pc)
        {
            short possession = 0;
            byte lv = 0;
            lv = pc.Level;
            lv = 0; //test
            /*if (pc.Pet != null)
            {
                if (pc.Pet.Ride)
                {
                    if (pc.Pet.MaxHP != 0)
                        return pc.Pet.MaxHP;
                }
            }*/
            foreach (ActorPC i in pc.PossesionedActors)
            {
                if (i == pc) continue;
                if (i.Status == null)
                    continue;
                possession += i.Status.hp_possession;
            }
            int TotalVit = pc.Vit + pc.Status.vit_item + pc.Status.vit_iris + pc.Status.vit_rev + pc.Status.vit_skill + pc.Status.vit_chip;
            int TotalHPBouns =  pc.Status.hp_item + pc.Status.hp_iris + pc.Status.hp_skill + pc.Status.hp_ano + pc.Status.hp_tit + pc.TInt["Partner_HP"] + pc.TInt["临时HP"] + pc.Status.hp_petpy + pc.Status.hp_food; //pc.TInt["幽怨之怒MaxHPUP"] +
            float baseHP = 1500f;
            if (pc.Job == PC_JOB.HAWKEYE)
                baseHP = 2800f;

            if (pc.TInt["临时HP固定HP"] != 0)
                return (uint)(pc.TInt["临时HP固定HP"] * HPJobFactor(pc));
            else
                return (uint)(((TotalVit * 20 + baseHP) * HPJobFactor(pc)) * (((pc.Status.hp_rate_item / 100f) * (pc.Status.hp_rate_iris / 100f) * (pc.Status.hp_rate_skill / 100f))) + TotalHPBouns);
        }
        uint CalcMaxMP(ActorPC pc)
        {
            /*short possession = 0;
            byte lv = 0;
            lv = pc.Level;
            lv = 0;
            if (pc.Pet != null)
            {
                if (pc.Pet.Ride)
                {
                    if (pc.Pet.MaxMP != 0)
                        return pc.Pet.MaxMP;
                }
            }
            foreach (ActorPC i in pc.PossesionedActors)
            {
                if (i == pc) continue;
                if (i.Status == null)
                    continue;
                possession += i.Status.mp_possession;
            }
            return (uint)(((pc.Mag + pc.Status.mag_item + pc.Status.mag_iris + pc.Status.mag_rev + pc.Status.mag_skill + pc.Status.mag_chip) * 10f + (1500f) * MPJobFactor(pc)) * ((float)((pc.Status.mp_rate_item / 100f) * (pc.Status.mp_rate_iris / 100f) * (pc.Status.mp_rate_skill / 100f))) + pc.Status.mp_item + pc.Status.mp_iris + pc.Status.mp_skill + pc.Status.mp_ano + pc.Status.mp_tit + pc.TInt["临时MP"]);*/
            uint maxmp = 1000;
            if (pc.Job == PC_JOB.ASTRALIST && pc.Int >= 50 && pc.Mag >= 50)
            {
                maxmp = 2000;
                int TotalInt = pc.Int + pc.Status.int_item + pc.Status.int_iris + pc.Status.int_rev + pc.Status.int_skill + pc.Status.int_chip + pc.Status.int_ano + pc.Status.int_tit + pc.Status.int_food;
                maxmp += (uint)((TotalInt - 50) * 50);
                int TotalMag = pc.Mag + pc.Status.mag_item + pc.Status.mag_iris + pc.Status.mag_rev + pc.Status.mag_skill + pc.Status.mag_chip + pc.Status.mag_ano + pc.Status.mag_tit + pc.Status.mag_food;
                maxmp += (uint)((TotalMag - 50) * 30);
            }
            /* if(pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) && pc.TInt["斥候远程模式"] == 1)
             {
                 Item item = pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                 ItemType it = item.BaseData.itemType;
                 if (it == ItemType.RIFLE)
                     maxmp = 9;//36;
                 if (it == ItemType.BOW)
                     maxmp = 9;//30;
                 if (it == ItemType.GUN)
                     maxmp = 9;//9;
                 if (it == ItemType.DUALGUN)
                     maxmp = 9;//18;
                 if (pc.TInt["弹仓扩容MP增加"] > 0)
                     maxmp += (uint)pc.TInt["弹仓扩容MP增加"];
             }
             if (pc.Job == PC_JOB.HAWKEYE && pc.TInt["斥候远程模式"] != 1)
                 maxmp = 0;*/
            if (pc.Job == PC_JOB.HAWKEYE)
                maxmp = 30;
            maxmp += (uint)pc.TInt["弹仓扩容MP增加"];
            return maxmp;
        }

        uint CalcMaxSP(ActorPC pc)
        {
            /*short possession = 0;
            byte lv = 0;
            lv = pc.Level;
            lv = 0;
            if (pc.Pet != null)
            {
                if (pc.Pet.Ride)
                {
                    if (pc.Pet.MaxSP != 0)
                        return pc.Pet.MaxSP;
                }
            }
            foreach (ActorPC i in pc.PossesionedActors)
            {
                if (i == pc) continue;
                possession += i.Status.sp_possession;
            }
            return (uint)(((pc.Str + pc.Status.str_item + pc.Status.str_iris + pc.Status.str_rev + pc.Status.str_skill + pc.Status.str_chip + pc.Status.str_ano) * 10f + (1500f) * SPJobFactor(pc)) * ((float)((pc.Status.sp_rate_item / 100f) * (pc.Status.sp_rate_iris / 100f) * (pc.Status.sp_rate_skill / 100f))) + pc.Status.sp_item + pc.Status.sp_iris + pc.Status.sp_skill + pc.Status.sp_ano + pc.Status.sp_tit + pc.TInt["临时SP"]);*/
            uint maxsp = 1000;
            if (pc.Job == PC_JOB.ASTRALIST)
                maxsp = (uint)(pc.TInt["续命"] * pc.Int);
            if (pc.Job == PC_JOB.HAWKEYE)
            {
                uint s = (uint)pc.TInt["灵巧身躯SP"];
                //maxsp += (uint)(s * 100 + s * (pc.Dex - 50) + s * 2 * (pc.Agi - 50));
                maxsp = 10 + s;
            }
            if(pc.Job == PC_JOB.CARDINAL)
            {
                int s = pc.TInt["韵律精通MAXSPUP"];
                maxsp += (uint)s;
            }
            if(pc.Job == PC_JOB.SOULTAKER)
            {
                maxsp = 10;
            }
            return maxsp;
       }

        float HPJobFactor(ActorPC pc)
        {
            PC_JOB job;
            if (pc.JobJoint == PC_JOB.NONE)
                job = pc.Job;
            else
                job = pc.JobJoint;

            switch (job)
            {
                //1次職
                case PC_JOB.NOVICE:
                    return 0.80f;
                case PC_JOB.SWORDMAN:
                    return 1.00f;
                case PC_JOB.FENCER:
                    return 1.00f;
                case PC_JOB.SCOUT:
                    return 1.00f;
                case PC_JOB.ARCHER:
                    return 1.00f;
                case PC_JOB.WIZARD:
                    return 1.00f;
                case PC_JOB.SHAMAN:
                    return 1.00f;
                case PC_JOB.VATES:
                    return 1.00f;
                case PC_JOB.WARLOCK:
                    return 1.00f;
                case PC_JOB.RANGER:
                    return 1.00f;
                case PC_JOB.MERCHANT:
                    return 1.00f;

                //2次職エキスパート
                case PC_JOB.BLADEMASTER:
                    return 3.05f;
                case PC_JOB.KNIGHT:
                    return 3.30f;
                case PC_JOB.ASSASSIN:
                    return 2.45f;//(2.20-2.45)
                case PC_JOB.STRIKER:
                    return 2.25f;//(2.07-2.25)
                case PC_JOB.SORCERER:
                    return 1.85f;
                case PC_JOB.ELEMENTER:
                    return 1.80f;
                case PC_JOB.DRUID:
                    return 1.95f;
                case PC_JOB.CABALIST:
                    return 2.60f;

                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                case PC_JOB.BLACKSMITH:
                    return 3;

                case PC_JOB.ALCHEMIST:
                    return 2.50f;

                case PC_JOB.EXPLORER:
                    return 2.80f;

                case PC_JOB.TRADER:
                    return 2.40f;

                //2次職テクニカル
                case PC_JOB.BOUNTYHUNTER:
                    return 2.90f; //?
                case PC_JOB.DARKSTALKER:
                    return 3;
                case PC_JOB.COMMAND:
                    return 2.50f;
                case PC_JOB.GUNNER:
                    return 2.15f;
                case PC_JOB.SAGE:
                    return 1.95f;
                case PC_JOB.ENCHANTER:
                    return 1.85f;//(1.62-1.85)
                case PC_JOB.BARD:
                    return 2.15f;//(2.00-2.15)
                case PC_JOB.NECROMANCER:
                    return 2.30f;
                case PC_JOB.MACHINERY:
                    return 2.60f;
                case PC_JOB.TREASUREHUNTER:
                    return 2.30f;
                case PC_JOB.GAMBLER:
                    return 2.40f;

                //3转职业补正
                case PC_JOB.GLADIATOR:
                    return 1.50f;
                case PC_JOB.GUARDIAN:
                    return 1.80f;
                case PC_JOB.ERASER:
                    return 1.50f;
                case PC_JOB.HAWKEYE:
                    return 0.85f;
                case PC_JOB.FORCEMASTER:
                    return 1.30f;
                case PC_JOB.ASTRALIST:
                    return 1.15f;
                case PC_JOB.CARDINAL:
                    return 1.20f;
                case PC_JOB.SOULTAKER:
                    return 1.25f;
                case PC_JOB.MAESTRO:
                    return 1.00f;
                case PC_JOB.HARVEST:
                    return 1.00f;
                case PC_JOB.STRIDER:
                    return 1.00f;
                case PC_JOB.ROYALDEALER:
                    return 1.00f;
                case PC_JOB.JOKER:
                    return 1.00f;

                default:
                    return 1;
            }
        }

        float MPJobFactor(ActorPC pc)
        {
            PC_JOB job;
            if (pc.JobJoint == PC_JOB.NONE)
                job = pc.Job;
            else
                job = pc.JobJoint;

            switch (job)
            {
                //1次職
                case PC_JOB.NOVICE:
                    return 1.00f;
                case PC_JOB.SWORDMAN:
                    return 1.00f;
                case PC_JOB.FENCER:
                    return 1.00f;
                case PC_JOB.SCOUT:
                    return 1.00f;
                case PC_JOB.ARCHER:
                    return 1.00f;
                case PC_JOB.WIZARD:
                    return 1.00f;
                case PC_JOB.SHAMAN:
                    return 1.00f;
                case PC_JOB.VATES:
                    return 1.00f;
                case PC_JOB.WARLOCK:
                    return 1.00f;
                case PC_JOB.RANGER:
                    return 1.00f;
                case PC_JOB.MERCHANT:
                    return 1.00f;
                //2次職エキスパート
                case PC_JOB.BLADEMASTER:
                    return 1.00f;
                case PC_JOB.KNIGHT:
                    return 1.00f;
                case PC_JOB.ASSASSIN:
                    return 1.00f;
                case PC_JOB.STRIKER:
                    return 1.00f;
                case PC_JOB.SORCERER:
                    return 1.00f;
                case PC_JOB.ELEMENTER:
                    return 1.00f;
                case PC_JOB.DRUID:
                    return 1.00f;
                case PC_JOB.CABALIST:
                    return 1.00f;

                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                case PC_JOB.BLACKSMITH:
                    return 1.20f;

                case PC_JOB.ALCHEMIST:
                    return 1.50f;

                case PC_JOB.EXPLORER:
                    return 1.30f;

                case PC_JOB.TRADER:
                    return 1.30f;

                //2次職テクニカル
                case PC_JOB.BOUNTYHUNTER:
                    return 1.00f;
                case PC_JOB.DARKSTALKER:
                    return 1.00f;
                case PC_JOB.COMMAND:
                    return 1.00f;//?
                case PC_JOB.GUNNER:
                    return 1.00f;
                case PC_JOB.SAGE:
                    return 1.00f;
                case PC_JOB.ENCHANTER:
                    return 1.00f;
                case PC_JOB.BARD:
                    return 1.00f;
                case PC_JOB.NECROMANCER:
                    return 1.00f;
                case PC_JOB.MACHINERY:
                    return 1.00f;
                case PC_JOB.TREASUREHUNTER:
                    return 1.00f;
                case PC_JOB.GAMBLER:
                    return 1.00f;

                //3转职业补正
                case PC_JOB.GLADIATOR:
                    return 1.00f;
                case PC_JOB.GUARDIAN:
                    return 1.00f;
                case PC_JOB.ERASER:
                    return 1.00f;
                case PC_JOB.HAWKEYE:
                    return 1.00f;
                case PC_JOB.FORCEMASTER:
                    return 1.70f;
                case PC_JOB.ASTRALIST:
                    return 1.70f;
                case PC_JOB.CARDINAL:
                    return 2.00f;
                case PC_JOB.SOULTAKER:
                    return 2.00f;
                case PC_JOB.MAESTRO:
                    return 1.00f;
                case PC_JOB.HARVEST:
                    return 1.00f;
                case PC_JOB.STRIDER:
                    return 1.00f;
                case PC_JOB.ROYALDEALER:
                    return 1.00f;
                case PC_JOB.JOKER:
                    return 1.00f;

                default:
                    return 1;
            }
        }

        float SPJobFactor(ActorPC pc)
        {
            PC_JOB job;
            if (pc.JobJoint == PC_JOB.NONE)
                job = pc.Job;
            else
                job = pc.JobJoint;
            switch (job)
            {
                //1次職
                case PC_JOB.NOVICE:
                    return 1.00f;
                case PC_JOB.SWORDMAN:
                    return 1.00f;
                case PC_JOB.FENCER:
                    return 1.00f;
                case PC_JOB.SCOUT:
                    return 1.00f;
                case PC_JOB.ARCHER:
                    return 1.00f;
                case PC_JOB.WIZARD:
                    return 1.00f;
                case PC_JOB.SHAMAN:
                    return 1.00f;
                case PC_JOB.VATES:
                    return 1.00f;
                case PC_JOB.WARLOCK:
                    return 1.00f;
                case PC_JOB.RANGER:
                    return 1.00f;
                case PC_JOB.MERCHANT:
                    return 1.00f;

                //2次職エキスパート
                case PC_JOB.BLADEMASTER:
                    return 1.00f;
                case PC_JOB.KNIGHT:
                    return 1.00f;
                case PC_JOB.ASSASSIN:
                    return 1.00f;
                case PC_JOB.STRIKER:
                    return 1.00f;
                case PC_JOB.SORCERER:
                    return 1.00f;
                case PC_JOB.ELEMENTER:
                    return 1.00f;
                case PC_JOB.DRUID:
                    return 1.00f;
                case PC_JOB.CABALIST:
                    return 1.00f;
                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                case PC_JOB.BLACKSMITH:
                    return 1.00f;
                case PC_JOB.ALCHEMIST:
                    return 1.00f;
                case PC_JOB.EXPLORER:
                    return 1.00f;
                case PC_JOB.TRADER:
                    return 1.00f;

                //2次職テクニカル
                case PC_JOB.BOUNTYHUNTER:
                    return 1.80f; //?
                case PC_JOB.DARKSTALKER:
                    return 1.70f;
                case PC_JOB.COMMAND:
                    return 1.80f;//?
                case PC_JOB.GUNNER:
                    return 2.30f;//?
                case PC_JOB.SAGE:
                    return 1.25f;//?
                case PC_JOB.ENCHANTER:
                    return 1.25f;
                case PC_JOB.BARD:
                    return 1.25f;
                case PC_JOB.NECROMANCER:
                    return 1.35f;//?
                case PC_JOB.MACHINERY:
                    return 1.90f;
                case PC_JOB.TREASUREHUNTER:
                    return 2.10f;
                case PC_JOB.GAMBLER:
                    return 1.70f;

                //3转职业补正
                case PC_JOB.GLADIATOR:
                    return 1.70f;
                case PC_JOB.GUARDIAN:
                    return 1.70f;
                case PC_JOB.ERASER:
                    return 2.00f;
                case PC_JOB.HAWKEYE:
                    return 2.00f;
                case PC_JOB.FORCEMASTER:
                    return 1.00f;
                case PC_JOB.ASTRALIST:
                    return 1.00f;
                case PC_JOB.CARDINAL:
                    return 1.00f;
                case PC_JOB.SOULTAKER:
                    return 1.00f;
                case PC_JOB.MAESTRO:
                    return 1.00f;
                case PC_JOB.HARVEST:
                    return 1.00f;
                case PC_JOB.STRIDER:
                    return 1.00f;
                case PC_JOB.ROYALDEALER:
                    return 1.00f;
                case PC_JOB.JOKER:
                    return 1.00f;

                default:
                    return 1;
            }
        }

        void CalcStatsRev(ActorPC pc)
        {
            ushort max1 = 20, max2 = 15, max3 = 12, max4 = 10, max5 = 8, max6 = 5;
            ushort joblvmax = 100;
            if (pc.JobJoint == PC_JOB.NONE)
            {
                byte joblv1 = 0;
                byte joblv2x = 0;
                byte joblv2t = 0;
                byte joblv3 = 0;

                switch (pc.Job)
                {
                    case PC_JOB.NOVICE:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.SWORDMAN:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.FENCER:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.SCOUT:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.ARCHER:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.WIZARD:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.SHAMAN:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.VATES:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.WARLOCK:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.RANGER:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    case PC_JOB.MERCHANT:
                        pc.Status.str_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv1 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv1 / joblvmax * max6);
                        break;
                    // 2次エキスパート職補正値 = FLOOR((JobLv + 30) * 補正率)
                    case PC_JOB.BLADEMASTER:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.KNIGHT:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.ASSASSIN:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.STRIKER:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.SORCERER:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.ELEMENTER:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.DRUID:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.CABALIST:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.BLACKSMITH:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.ALCHEMIST:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.EXPLORER:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    case PC_JOB.TRADER:
                        pc.Status.str_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2x / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2x / joblvmax * max6);
                        break;
                    //2次テクニカル職
                    case PC_JOB.BOUNTYHUNTER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.DARKSTALKER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.COMMAND:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.GUNNER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.SAGE:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.ENCHANTER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.BARD:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.NECROMANCER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.MACHINERY:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break; ;
                    case PC_JOB.TREASUREHUNTER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    case PC_JOB.GAMBLER:
                        pc.Status.str_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv2t / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv2t / joblvmax * max6);
                        break;
                    //3转职业属性补正
                    case PC_JOB.GLADIATOR:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.GUARDIAN:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.ERASER:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.HAWKEYE:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.FORCEMASTER:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.ASTRALIST:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.CARDINAL:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.SOULTAKER:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.MAESTRO:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.HARVEST:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.STRIDER:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.ROYALDEALER:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                    case PC_JOB.JOKER:
                        pc.Status.str_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.dex_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.int_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.vit_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.agi_rev = (ushort)(joblv3 / joblvmax * max6);
                        pc.Status.mag_rev = (ushort)(joblv3 / joblvmax * max6);
                        break;
                }
            }
            else
            {
                switch (pc.JobJoint)
                {
                    case PC_JOB.BREEDER:
                        pc.Status.str_rev = (ushort)(3 + (pc.JointJobLevel + 3) * 0.143);
                        pc.Status.dex_rev = (ushort)(6 + (pc.JointJobLevel + 1) * 0.25);
                        pc.Status.int_rev = (ushort)(1 + (pc.JointJobLevel) * 0.04);
                        pc.Status.vit_rev = (ushort)(6 + (pc.JointJobLevel + 1) * 0.25);
                        pc.Status.agi_rev = (ushort)(7 + (pc.JointJobLevel) * 0.28);
                        pc.Status.mag_rev = (ushort)(1 + (pc.JointJobLevel) * 0.04);
                        break;
                    case PC_JOB.GARDNER:
                        pc.Status.str_rev = (ushort)(3 + (pc.JointJobLevel + 2) * 0.125);
                        pc.Status.dex_rev = (ushort)(6 + (pc.JointJobLevel + 1) * 0.25);
                        pc.Status.int_rev = (ushort)(6 + (pc.JointJobLevel + 1) * 0.25);
                        pc.Status.vit_rev = (ushort)(5 + (pc.JointJobLevel + 2) * 0.25);
                        pc.Status.agi_rev = (ushort)(3 + (pc.JointJobLevel - 1) * 0.125);
                        pc.Status.mag_rev = (ushort)((pc.JointJobLevel) * 0.04);
                        break;
                }
            }
        }
    }
}
