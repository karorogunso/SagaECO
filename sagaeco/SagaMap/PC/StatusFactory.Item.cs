using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Iris;
using SagaDB.DEMIC;
using SagaDB.Title;
namespace SagaMap.PC
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        private void CalcPlayerTitleBouns(ActorPC pc)
        {
            uint TitleID = (uint)pc.AInt["称号_战斗"];
            if (Network.Client.MapClient.FromActorPC(pc).CheckTitle((int)TitleID))
            {
                if (TitleID != 0 && TitleFactory.Instance.TitleList.ContainsKey(TitleID))
                {
                    Title item = TitleFactory.Instance.TitleList[TitleID];
                    pc.Status.hp_tit = (short)item.hp;
                    pc.Status.mp_tit = (short)item.mp;
                    pc.Status.sp_tit = (short)item.sp;
                    pc.Status.min_atk1_tit = (short)item.atk_min;
                    pc.Status.min_atk2_tit = (short)item.atk_min;
                    pc.Status.min_atk3_tit = (short)item.atk_min;
                    pc.Status.max_atk1_tit = (short)item.atk_max;
                    pc.Status.max_atk2_tit = (short)item.atk_max;
                    pc.Status.max_atk3_tit = (short)item.atk_max;
                    pc.Status.min_matk_tit = (short)item.matk_min;
                    pc.Status.max_matk_tit = (short)item.matk_max;
                    pc.Status.def_add_tit = (short)item.def;
                    pc.Status.mdef_add_tit = (short)item.mdef;
                    pc.Status.cri_tit = (short)item.cri;
                    pc.Status.cri_avoid_tit = (short)item.cri_avoid;
                    pc.Status.hit_melee_tit = (short)item.hit_melee;
                    pc.Status.hit_ranged_tit = (short)item.hit_range;
                    pc.Status.hit_magic_tit = (short)item.hit_range;
                    pc.Status.avoid_melee_tit = (short)item.avoid_melee;
                    pc.Status.avoid_ranged_tit = (short)item.avoid_range;
                    pc.Status.avoid_magic_tit = (short)item.avoid_range;
                    pc.Status.aspd_tit = (short)item.aspd;
                    pc.Status.cspd_tit = (short)item.cspd;
                    Network.Client.MapClient.FromActorPC(pc).SendCharInfoUpdate();
                    return;
                }
            }
            pc.Status.hit_magic_tit = 0;
            pc.Status.cri_avoid_tit = 0;
            pc.Status.avoid_magic_tit = 0;
            pc.Status.aspd_tit = 0;
            pc.Status.cspd_tit = 0;


            pc.PlayerTitleID = 0;
            pc.PlayerTitle = "";
            pc.FirstName = "";
            pc.Status.str_tit = 0;
            pc.Status.mag_tit = 0;
            pc.Status.agi_tit = 0;
            pc.Status.dex_tit = 0;
            pc.Status.vit_tit = 0;
            pc.Status.int_tit = 0;
            pc.Status.hp_tit = 0;
            pc.Status.mp_tit = 0;
            pc.Status.sp_tit = 0;
            pc.Status.hprecov_tit = 0;
            pc.Status.mprecov_tit = 0;
            pc.Status.sprecov_tit = 0;
            pc.Status.min_atk1_tit = 0;
            pc.Status.min_atk2_tit = 0;
            pc.Status.min_atk3_tit = 0;
            pc.Status.max_atk1_tit = 0;
            pc.Status.max_atk2_tit = 0;
            pc.Status.max_atk3_tit = 0;
            pc.Status.min_matk_tit = 0;
            pc.Status.max_matk_tit = 0;
            pc.Status.def_add_tit = 0;
            pc.Status.mdef_add_tit = 0;
            pc.Status.cri_tit = 0;
            pc.Status.hit_melee_tit = 0;
            pc.Status.hit_ranged_tit = 0;
            pc.Status.avoid_melee_tit = 0;
            pc.Status.avoid_ranged_tit = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendCharInfoUpdate();
        }
        private void CalcAnoPaperBouns(ActorPC pc)
        {
            if (pc.UsingPaperID != 0 && pc.AnotherPapers.ContainsKey(pc.UsingPaperID))
            {
                AnotherDetail paper = pc.AnotherPapers[pc.UsingPaperID];
                int rate = 1;
                if (pc.Buff.不知道13)
                    rate = 2;
                if (AnotherFactory.Instance.AnotherPapers[pc.UsingPaperID].ContainsKey(paper.lv))
                {
                    Another paperstatus = AnotherFactory.Instance.AnotherPapers[pc.UsingPaperID][paper.lv];
                    pc.Status.str_ano = (short)(paperstatus.str * rate);
                    pc.Status.mag_ano = (short)(paperstatus.mag * rate);
                    pc.Status.agi_ano = (short)(paperstatus.agi * rate);
                    pc.Status.dex_ano = (short)(paperstatus.dex * rate);
                    pc.Status.vit_ano = (short)(paperstatus.vit * rate);
                    pc.Status.int_ano = (short)(paperstatus.ing * rate);
                    pc.Status.hp_ano = (short)(paperstatus.hp_add * rate);
                    pc.Status.mp_ano = (short)(paperstatus.mp_add * rate);
                    pc.Status.sp_ano = (short)(paperstatus.sp_add * rate);
                    pc.Status.min_atk1_ano = (short)(paperstatus.min_atk_add * rate);
                    pc.Status.min_atk2_ano = (short)(paperstatus.min_atk_add * rate);
                    pc.Status.min_atk3_ano = (short)(paperstatus.min_atk_add * rate);
                    pc.Status.max_atk1_ano = (short)(paperstatus.max_atk_add * rate);
                    pc.Status.max_atk2_ano = (short)(paperstatus.max_atk_add * rate);
                    pc.Status.max_atk3_ano = (short)(paperstatus.max_atk_add * rate);
                    pc.Status.min_matk_ano = (short)(paperstatus.min_matk_add * rate);
                    pc.Status.max_matk_ano = (short)(paperstatus.max_matk_add * rate);
                    pc.Status.def_add_ano = (short)(paperstatus.def_add * rate);
                    pc.Status.mdef_add_ano = (short)(paperstatus.mdef_add * rate);
                    pc.Status.hit_melee_ano = (short)(paperstatus.hit_melee_add * rate);
                    pc.Status.hit_ranged_ano = (short)(paperstatus.hit_magic_add * rate);
                    pc.Status.avoid_melee_ano = (short)(paperstatus.avoid_melee_add * rate);
                    pc.Status.avoid_ranged_ano = (short)(paperstatus.avoid_magic_add * rate);
                    return;
                }
            }
            pc.Status.str_ano = 0;
            pc.Status.mag_ano = 0;
            pc.Status.agi_ano = 0;
            pc.Status.dex_ano = 0;
            pc.Status.vit_ano = 0;
            pc.Status.int_ano = 0;
            pc.Status.hp_ano = 0;
            pc.Status.mp_ano = 0;
            pc.Status.sp_ano = 0;
            pc.Status.min_atk1_ano = 0;
            pc.Status.min_atk2_ano = 0;
            pc.Status.min_atk3_ano = 0;
            pc.Status.max_atk1_ano = 0;
            pc.Status.max_atk2_ano = 0;
            pc.Status.max_atk3_ano = 0;
            pc.Status.min_matk_ano = 0;
            pc.Status.max_matk_ano = 0;
            pc.Status.def_add_ano = 0;
            pc.Status.mdef_add_ano = 0;
            pc.Status.hit_melee_ano = 0;
            pc.Status.hit_ranged_ano = 0;
            pc.Status.avoid_melee_ano = 0;
            pc.Status.avoid_ranged_ano = 0;
        }
        private void CalcEquipBonus(ActorPC pc)
        {
            pc.Status.ClearItem();
            pc.ClearIrisAbilities();

            pc.Inventory.MaxPayload[ContainerType.BODY] = 0;
            pc.Inventory.MaxPayload[ContainerType.BACK_BAG] = 0;
            pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] = 0;
            pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] = 0;
            pc.Inventory.MaxVolume[ContainerType.BODY] = 0;
            pc.Inventory.MaxVolume[ContainerType.BACK_BAG] = 0;
            pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] = 0;
            pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] = 0;

            IConcurrentDictionary<EnumEquipSlot, Item> equips;
            if (pc.Form == DEM_FORM.NORMAL_FORM)
                equips = pc.Inventory.Equipments;
            else
                equips = pc.Inventory.Parts;
            foreach (EnumEquipSlot j in equips.Keys)
            {
                Item i = equips[j];
                if (i.Stack == 0)
                    continue;
                if(pc.Job == PC_JOB.HAWKEYE)
                {
                    if (pc.TInt["斥候远程模式"] == 0 && j == EnumEquipSlot.LEFT_HAND)
                        continue;
                    if (pc.TInt["斥候远程模式"] == 1 && j == EnumEquipSlot.RIGHT_HAND)
                        continue;
                }
                else
                {
                    if(j == EnumEquipSlot.LEFT_HAND)
                    {
                        if (i.BaseData.itemType == ItemType.GUN || i.BaseData.itemType == ItemType.DUALGUN || i.BaseData.itemType == ItemType.RIFLE || i.BaseData.itemType == ItemType.BOW)
                            continue;
                    }
                }

                //去掉左手武器判定
                if ((j != EnumEquipSlot.PET || i.BaseData.itemType == ItemType.BACK_DEMON)
                    && !(j == EnumEquipSlot.LEFT_HAND && i.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && i.EquipSlot.Count == 1))
                {
                    int weapon_atk1_add = 0, weapon_atk2_add = 0, weapon_atk3_add = 0, weapon_matk_add = 0;
                    float rate = pc.Status.weapon_add;

                    ItemFactory.Instance.CalcRefineBouns(i);

                    weapon_atk1_add = (int)(i.BaseData.atk1 * rate);
                    weapon_atk2_add = (int)(i.BaseData.atk2 * rate);
                    weapon_atk3_add = (int)(i.BaseData.atk3 * rate);
                    weapon_matk_add = (int)(i.BaseData.matk * rate);

                    pc.Status.atk1_rate_item = (short)(pc.Status.atk1_rate_item + i.BaseData.atk1 + i.Atk1 + weapon_atk1_add + i.atkrate_refine + i.Potential_ATK_rate);
                    pc.Status.atk2_rate_item = (short)(pc.Status.atk2_rate_item + i.BaseData.atk2 + i.Atk2 + weapon_atk2_add + i.atkrate_refine + i.Potential_ATK_rate);
                    pc.Status.atk3_rate_item = (short)(pc.Status.atk3_rate_item + i.BaseData.atk3 + i.Atk3 + weapon_atk3_add + i.atkrate_refine + i.Potential_ATK_rate);

                    if (pc.Status.Additions.ContainsKey("魔法少女") &&
                        (i.BaseData.itemType == ItemType.GUN || i.BaseData.itemType == ItemType.BOW || i.BaseData.itemType == ItemType.SPEAR || i.BaseData.itemType == ItemType.RIFLE || i.BaseData.itemType == ItemType.RAPIER))
                        pc.Status.matk_rate_item = (short)(pc.Status.matk_rate_item + 35 + i.MAtk + weapon_matk_add + i.matkrate_refine + i.Potential_ATK_rate); //魔攻百分比加成固定35
                    else
                        pc.Status.matk_rate_item = (short)(pc.Status.matk_rate_item + i.BaseData.matk + i.MAtk + weapon_matk_add + i.matkrate_refine + i.Potential_ATK_rate);

                    pc.Status.atk1_item = (short)(pc.Status.atk1_item + i.WeightUp / 10 + i.BaseData.weightUp / 10 + i.atk_refine);//蓝字临时改动
                    pc.Status.atk2_item = (short)(pc.Status.atk2_item + i.WeightUp / 10 + i.BaseData.weightUp / 10 + i.atk_refine);//蓝字临时改动
                    pc.Status.atk3_item = (short)(pc.Status.atk3_item + i.WeightUp / 10 + i.BaseData.weightUp / 10 + i.atk_refine);//蓝字临时改动
                    pc.Status.matk_item = (short)(pc.Status.matk_item + i.VolumeUp / 10 + i.BaseData.volumeUp / 10 + i.matk_refine);//蓝字临时改动

                    pc.Status.def_item = (short)(pc.Status.def_item + i.BaseData.def + i.Def + i.defrate_refine); //装备提供左防（%）
                    pc.Status.mdef_item = (short)(pc.Status.mdef_item + i.BaseData.mdef + i.MDef + i.mdefrate_refine);  //装备提供左防（%）
                    pc.Status.hit_melee_item = (short)(pc.Status.hit_melee_item + i.BaseData.hitMelee + i.HitMelee + i.hit_refine);
                    pc.Status.hit_ranged_item = (short)(pc.Status.hit_ranged_item + i.BaseData.hitRanged + i.HitRanged + i.hit_refine);
                    pc.Status.avoid_melee_item = (short)(pc.Status.avoid_melee_item + i.BaseData.avoidMelee + i.AvoidMelee);
                    pc.Status.avoid_ranged_item = (short)(pc.Status.avoid_ranged_item + i.BaseData.avoidRanged + i.AvoidRanged);
                    pc.Status.hit_critical_item = (short)(pc.Status.hit_critical_item + i.BaseData.hitCritical + i.HitCritical + i.cri_refine);
                    pc.Status.avoid_critical_item = (short)(pc.Status.avoid_critical_item + i.BaseData.avoidCritical + i.AvoidCritical);
                    pc.Status.hit_magic_item = (short)(pc.Status.hit_magic_item + i.BaseData.hitMagic + i.HitMagic + i.mhit_refine);
                    pc.Status.avoid_magic_item = (short)(pc.Status.avoid_magic_item + i.BaseData.avoidMagic + i.AvoidMagic);
                    pc.Status.str_item = (short)(pc.Status.str_item + i.BaseData.str + i.Str + i.Potential_Str);
                    pc.Status.agi_item = (short)(pc.Status.agi_item + i.BaseData.agi + i.Agi + i.Potential_Agi);
                    pc.Status.dex_item = (short)(pc.Status.dex_item + i.BaseData.dex + i.Dex + i.Potential_Dex);
                    pc.Status.vit_item = (short)(pc.Status.vit_item + i.BaseData.vit + i.Vit + i.Potential_Vit);
                    pc.Status.int_item = (short)(pc.Status.int_item + i.BaseData.intel + i.Int + i.Potential_Int);
                    pc.Status.mag_item = (short)(pc.Status.mag_item + i.BaseData.mag + i.Mag + i.Potential_Mag);
                    pc.Status.hp_item = (short)(pc.Status.hp_item + i.BaseData.hp + i.HP + i.hp_refine + i.Potential_HP);
                    pc.Status.sp_item = (short)(pc.Status.sp_item + i.BaseData.sp + i.SP);
                    pc.Status.mp_item = (short)(pc.Status.mp_item + i.BaseData.mp + i.MP);
                    pc.Status.speed_item = (int)(pc.Status.speed_item + i.BaseData.speedUp + i.SpeedUp);
                    pc.Status.hp_recover_item = (short)(pc.Status.hp_recover_item + i.BaseData.hpRecover + i.HPRecover + i.recover_refine);
                    //pc.Status.mp_recover_item = (short)(pc.Status.mp_recover_item + i.BaseData.mpRecover + i.MPRecover);
                    //pc.Status.sp_recover_item = (short)(pc.Status.sp_recover_item + i.BaseData.spRecover + i.SPRecover); sb的gongho这时候在item里只有魔恢复力不分mp和sp了
                    pc.Status.aspd_item = (short)(pc.Status.aspd_item + i.ASPD + i.spd_refine + i.Potential_Aspd);
                    pc.Status.cspd_item = (short)(pc.Status.cspd_item + i.CSPD + i.spd_refine + i.Potential_Cspd);


                    if (i.BaseData.speedUp != 0 || i.SpeedUp != 0)
                    {
                        if (pc.Online)
                        {
                            pc.e.PropertyUpdate(UpdateEvent.SPEED, 0);
                        }
                    }

                    if (i.IsArmor)
                    {
                        foreach (Elements k in pc.Elements.Keys)
                        {
                            pc.Status.elements_item[k] += i.BaseData.element[k];
                        }
                    }
                    if (i.IsWeapon || i.BaseData.itemType == ItemType.ARROW || i.BaseData.itemType == ItemType.BULLET || i.BaseData.itemType == ItemType.THROW)
                    {
                        foreach (Elements k in pc.Elements.Keys)
                        {
                            pc.Status.attackelements_item[k] += i.BaseData.element[k];
                        }
                    }
                }
                if (i.BaseData.weightUp != 0)
                {
                    switch (j)
                    {
                        case EnumEquipSlot.PET:
                            pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)(pc.Inventory.MaxPayload[ContainerType.BODY] + i.BaseData.weightUp + i.WeightUp);
                            break;
                        case EnumEquipSlot.BACK:
                            pc.Inventory.MaxPayload[ContainerType.BACK_BAG] = (uint)(pc.Inventory.MaxPayload[ContainerType.BACK_BAG] + i.BaseData.weightUp + i.WeightUp);
                            break;
                        case EnumEquipSlot.LEFT_HAND:
                            pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] = (uint)(pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] + i.BaseData.weightUp + i.WeightUp);
                            break;
                        case EnumEquipSlot.RIGHT_HAND:
                            pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] = (uint)(pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] + i.BaseData.weightUp + i.WeightUp);
                            break;
                    }
                }
                if (i.BaseData.volumeUp != 0)
                {
                    float rate = 0f;
                    if (pc.Status.Additions.ContainsKey("Packing"))
                    {
                        Skill.Additions.Global.DefaultPassiveSkill skill = (Skill.Additions.Global.DefaultPassiveSkill)pc.Status.Additions["Packing"];
                        rate = (float)skill["Packing"] / 100;
                    }
                    switch (j)
                    {
                        case EnumEquipSlot.PET:
                            pc.Inventory.MaxVolume[ContainerType.BODY] = (uint)(pc.Inventory.MaxVolume[ContainerType.BODY] + (i.BaseData.volumeUp + i.VolumeUp) * (1f + rate));
                            break;
                        case EnumEquipSlot.BACK:
                            pc.Inventory.MaxVolume[ContainerType.BACK_BAG] = (uint)(pc.Inventory.MaxVolume[ContainerType.BACK_BAG] + (i.BaseData.volumeUp + i.VolumeUp) * (1f + rate));
                            break;
                        case EnumEquipSlot.LEFT_HAND:
                            pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] = (uint)(pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] + (i.BaseData.volumeUp + i.VolumeUp) * (1f + rate));
                            break;
                        case EnumEquipSlot.RIGHT_HAND:
                            pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] = (uint)(pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] + (i.BaseData.volumeUp + i.VolumeUp) * (1f + rate));
                            break;
                    }
                }
                ApplyIrisCardAbilities(pc, i);
            }
            ApplyIrisRes(pc);
            Manager.MapClientManager.Instance.FindClient(pc).OnPlayerElements();
            CalcDemicChips(pc);
        }
        /// <summary>
        /// Calculate item's card ability values on PC
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="item"></param>
        void ApplyIrisCardAbilities(ActorPC pc, Item item)
        {
            if (!item.Locked)
                return;

            #region Iris Card Ability Calculation
            Dictionary<AbilityVector, int> IrisCardAbilityValues = item.VectorValues(false, false);
            foreach (AbilityVector i in IrisCardAbilityValues.Keys)
            {
                if (!pc.IrisAbilityValues.ContainsKey(i))
                {
                    pc.IrisAbilityValues.Add(i, IrisCardAbilityValues[i]);
                }
                else
                {
                    pc.IrisAbilityValues[i] += IrisCardAbilityValues[i];
                }
            }


            #endregion

            Dictionary<Elements, int> elements = item.IrisElements(false);
            if (item.IsArmor || item.IsWeapon)
            {
                if (item.IsWeapon)
                {
                    foreach (Elements i in elements.Keys)
                    {
                        pc.Status.attackelements_iris[i] += elements[i];
                    }
                }
                if (item.IsArmor)
                {
                    foreach (Elements i in elements.Keys)
                    {
                        pc.Status.elements_iris[i] += elements[i];
                    }
                }
            }
        }

        /// <summary>
        /// Calculate item's card ability levels on PC and basic(original) RAs
        /// </summary>
        /// <param name="pc"></param>
        void ApplyIrisRes(ActorPC pc)
        {
            #region Iris Card Ability Level Calculation      
            //int[] lvs = new int[10] { 1, 30, 80, 150, 250, 370, 510, 660, 820, 999 }; old/original settings
            //int[] lvs = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; new settings
            foreach (AbilityVector i in pc.IrisAbilityValues.Keys)
            {
                pc.IrisAbilityLevels.Add(i, Math.Min(pc.IrisAbilityValues[i], 10));
            }
            #endregion

            #region ReleaseAbility for UI

            Dictionary<ReleaseAbility, int> releaseabilities = UIStatusModRAs(pc, pc.IrisAbilityLevels);

            foreach (ReleaseAbility i in releaseabilities.Keys)
            {
                int value = releaseabilities[i];
                Status Status = pc.Status;
                switch (i)
                {
                    //Ygg Card bonus 2
                    case ReleaseAbility.EXP_HUMAN://戏命师
                        Status.Playman += (short)value;
                        break;
                    case ReleaseAbility.EXP_BIRD://蓬莱之烟
                        Status.Penglai += (short)value;
                        break;
                    case ReleaseAbility.EXP_ANIMAL://庭师
                        Status.Instructor += (short)value;
                        break;
                    case ReleaseAbility.EXP_INSECT://黑暗低语
                        Status.BeliefBalace += (short)value;
                        break;
                    case ReleaseAbility.EXP_ELEMENT://攻击速度提升
                        Status.aspd_iris += (short)value;
                        break;
                    case ReleaseAbility.EXP_UNDEAD://吟唱速度提升
                        Status.cspd_iris += (short)value;
                        break;
                    case ReleaseAbility.STR_UP:
                        pc.Status.str_iris += (short)value;
                        break;
                    case ReleaseAbility.DEX_UP:
                        pc.Status.dex_iris += (short)value;
                        break;
                    case ReleaseAbility.INT_UP:
                        pc.Status.int_iris += (short)value;
                        break;
                    case ReleaseAbility.VIT_UP:
                        pc.Status.vit_iris += (short)value;
                        break;
                    case ReleaseAbility.AGI_UP:
                        pc.Status.agi_iris += (short)value;
                        break;
                    case ReleaseAbility.MAG_UP:
                        pc.Status.mag_iris += (short)value;
                        break;
                    case ReleaseAbility.HP_MAX_UP:
                        pc.Status.hp_iris += (short)value;
                        break;
                    case ReleaseAbility.ATK_FIX_UP:
                        pc.Status.min_atk1_iris += (short)value;
                        pc.Status.min_atk2_iris += (short)value;
                        pc.Status.min_atk3_iris += (short)value;
                        pc.Status.max_atk1_iris += (short)value;
                        pc.Status.max_atk2_iris += (short)value;
                        pc.Status.max_atk3_iris += (short)value;
                        break;
                    case ReleaseAbility.ATK_PER_UP:
                        pc.Status.min_atk1_rate_iris += (short)value;
                        pc.Status.min_atk2_rate_iris += (short)value;
                        pc.Status.min_atk3_rate_iris += (short)value;
                        pc.Status.max_atk1_rate_iris += (short)value;
                        pc.Status.max_atk2_rate_iris += (short)value;
                        pc.Status.max_atk3_rate_iris += (short)value;
                        break;
                    case ReleaseAbility.MATK_FIX_UP:
                        pc.Status.min_matk_iris += (short)value;
                        pc.Status.max_matk_iris += (short)value;
                        break;
                    case ReleaseAbility.MATK_PER_UP:
                        pc.Status.min_matk_rate_iris += (short)value;
                        pc.Status.max_matk_rate_iris += (short)value;
                        break;
                    case ReleaseAbility.SHIT_FIX_UP:
                        pc.Status.hit_melee_iris += (short)value;
                        break;
                    case ReleaseAbility.SHIT_PER_UP:
                        pc.TInt["番茄护盾"] += value;
                        break;
                    case ReleaseAbility.LHIT_FIX_UP:
                        pc.Status.hit_ranged_iris += (short)value;
                        break;
                    case ReleaseAbility.LHIT_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.SAVOID_FIX_UP:
                        pc.Status.avoid_melee_iris += (short)value;
                        break;
                    case ReleaseAbility.SAVOID_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.LAVOID_FIX_UP:
                        pc.Status.avoid_ranged_iris += (short)value;
                        break;
                    case ReleaseAbility.LAVOID_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.DEF_UP:
                        pc.Status.def_add_iris += (short)value;
                        break;
                    case ReleaseAbility.DEF_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.MDEF_FIX_UP:
                        pc.Status.mdef_add_iris += (short)value;
                        break;
                    case ReleaseAbility.MDEF_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.WEAPON_FIX_UP:
                        pc.Status.min_atk1_rate_iris += (short)value;
                        pc.Status.min_atk2_rate_iris += (short)value;
                        pc.Status.min_atk3_rate_iris += (short)value;
                        pc.Status.max_atk1_rate_iris += (short)value;
                        pc.Status.max_atk2_rate_iris += (short)value;
                        pc.Status.max_atk3_rate_iris += (short)value;
                        break;
                    case ReleaseAbility.WEAPON_PER_UP:
                        pc.Status.weapon_add += (float)value;
                        break;
                    case ReleaseAbility.EQUIP_DEF_FIX_UP:
                        pc.Status.def_iris += (short)value;
                        break;
                    case ReleaseAbility.EQUIP_DEF_UP:
                        //tbc
                        break;
                    case ReleaseAbility.HIT_UP:
                        //tbc
                        break;
                    case ReleaseAbility.AVOID_UP:
                        //tbc
                        break;
                    case ReleaseAbility.BDAMAGE_DOWN:
                        //tbc
                        break;
                    case ReleaseAbility.MDAMAGE_DOWN:
                        //tbc
                        break;
                    case ReleaseAbility.ELMDMG_PER_DWON:
                        //tbc
                        break;
                    case ReleaseAbility.PT_DAMUP:
                        //tbc
                        break;
                    case ReleaseAbility.PT_DAMDOWN:
                        //tbc
                        break;
                    case ReleaseAbility.HELL_DAMUP:
                        //tbc
                        break;
                    case ReleaseAbility.HELL_DAMDOWN:
                        //tbc
                        break;
                    case ReleaseAbility.CRITICAL_UP:
                        pc.Status.hit_critical_iris += (short)value;
                        break;
                    case ReleaseAbility.CRIAVOID_UP:
                        pc.Status.avoid_critical_iris += (short)value;
                        break;
                    case ReleaseAbility.GUARD_UP:
                        pc.Status.guard_iris += (short)value;
                        break;
                    case ReleaseAbility.PAYLOAD_UP:
                        pc.Status.payl_iris += (short)value;
                        break;
                    case ReleaseAbility.CAPACITY_UP:
                        pc.Status.volume_iris += (short)value;
                        break;
                    case ReleaseAbility.LV_DIFF_DOWN:
                        //tbc
                        break;
                    case ReleaseAbility.REGI_POISON_UP:
                        pc.AbnormalStatus[AbnormalStatus.Poisen] += (short)value;
                        break;
                    case ReleaseAbility.REGI_STONE_UP:
                        pc.AbnormalStatus[AbnormalStatus.Stone] += (short)value;
                        break;
                    case ReleaseAbility.REGI_SLEEP_UP:
                        pc.AbnormalStatus[AbnormalStatus.Sleep] += (short)value;
                        break;
                    case ReleaseAbility.REGI_SILENCE_UP:
                        pc.AbnormalStatus[AbnormalStatus.Silence] += (short)value;
                        break;
                    case ReleaseAbility.REGI_SLOW_UP:
                        pc.AbnormalStatus[AbnormalStatus.鈍足] += (short)value;
                        break;
                    case ReleaseAbility.REGI_CONFUSION_UP:
                        pc.AbnormalStatus[AbnormalStatus.Confused] += (short)value;
                        break;
                    case ReleaseAbility.REGI_ICE_UP:
                        pc.AbnormalStatus[AbnormalStatus.Frosen] += (short)value;
                        break;
                    case ReleaseAbility.REGI_STUN_UP:
                        pc.AbnormalStatus[AbnormalStatus.Stun] += (short)value;
                        break;
                    case ReleaseAbility.CAN_BTPDOWN_PER:
                        //tbc
                        break;
                    case ReleaseAbility.P_CSPD_PER_DOWN:
                        //tbc
                        break;
                    case ReleaseAbility.P_CSPD_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.M_CSPD_PER_DOWN:
                        //tbc
                        break;
                    case ReleaseAbility.M_CSPD_PER_UP:
                        //tbc
                        break;
                    case ReleaseAbility.LV_DIFF_UP:
                        //tbc
                        break;

                    //part related
                    case ReleaseAbility.PART_R_HPMAX_FIX_UP:
                        //tbc
                        pc.Pet.MaxHP += (uint)value;
                        break;
                    case ReleaseAbility.PART_R_HPHEAL_UP:
                        //tbc
                        break;
                    case ReleaseAbility.PART_R_HPMAX_UP:
                        //tbc
                        pc.Pet.MaxHP += (uint)(pc.Pet.MaxHP * ((float)value / 100));
                        break;

                    case ReleaseAbility.DUR_DAMAGE_DOWN:
                        //tbc
                        break;
                    case ReleaseAbility.CHGSTATE_RATE_UP:
                        //tbc
                        break;
                    case ReleaseAbility.FOOD_UP:
                        //tbc
                        break;
                    case ReleaseAbility.POTION_UP:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_HUMAN:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_BIRD:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_ANIMAL:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_INSECT:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_MAGIC_CREATURE:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_PLANT:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_WATER_ANIMAL:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_MACHINE:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_ROCK:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_ELEMENT:
                        //tbc
                        break;
                    case ReleaseAbility.DAMUP_UNDEAD:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_HUMAN:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_BIRD:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_ANIMAL:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_INSECT:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_MAGIC_CREATURE:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_PLANT:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_WATER_ANIMAL:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_MACHINE:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_ROCK:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_ELEMENT:
                        //tbc
                        break;
                    case ReleaseAbility.DAMDOWN_UNDEAD:
                        //tbc
                        break;
                    case ReleaseAbility.EXP_MAGIC_CREATURE:
                        //tbc
                        break;
                    case ReleaseAbility.EXP_PLANT:
                        //tbc
                        break;
                    case ReleaseAbility.EXP_WATER_ANIMAL:
                        //tbc
                        break;
                    case ReleaseAbility.EXP_MACHINE:
                        //tbc
                        break;
                    case ReleaseAbility.EXP_ROCK:
                        //tbc
                        break;

                    case ReleaseAbility.NONE:
                        break;

                        /*case ReleaseAbility.HP_MAX_UP:
    pc.Status.heal_70up_iris += (short)value;
    break;
case ReleaseAbility.HP_PER_UP:
    pc.Status.atk_70up_iris += (short)value;
    break;
case ReleaseAbility.MP_MAX_UP:
    pc.Status.mpcost_70down_iris += (short)value;
    break;
case ReleaseAbility.MP_PER_UP:
    pc.Status.spcost_70down_iris += (short)value;
    break;
case ReleaseAbility.SP_MAX_UP:
    pc.Status.atkup_job40_iris += (short)value;
    break;
case ReleaseAbility.SP_PER_UP:
    pc.Status.expup_job40_iris += (short)value;
    break;
case ReleaseAbility.HP_REC_UP:
    pc.Status.spnocost_iris += (short)value;
    break;
case ReleaseAbility.MP_REC_UP:
    pc.Status.mpnocost_iris += (short)value;
    break;
case ReleaseAbility.SP_REC_UP:
    pc.Status.spweap_atkup_iris += (short)value;
    break;
case ReleaseAbility.SKILL_MP_PER_DOWN:
    pc.Status.heal_attacked_iris += (short)value;
    break;
case ReleaseAbility.SKILL_SP_PER_DOWN:
    pc.Status.holymortar_combo_iris += (short)value;
    break;
case ReleaseAbility.STR_UP:
    pc.Status.Mammon_iris += (short)value;
    break;*/

                }
            }

            #endregion

        }

        /// <summary>
        /// Get all RAs for ui display
        /// </summary>
        /// <param name="irislevels"></param>
        /// <returns></returns>
        Dictionary<ReleaseAbility, int> UIStatusModRAs(ActorPC pc, Dictionary<AbilityVector, int> irislevels)
        {
            Dictionary<ReleaseAbility, int> list = new Dictionary<ReleaseAbility, int>();
            Dictionary<PC_GENDER, ushort> genderlist = new Dictionary<PC_GENDER, ushort>();
            genderlist.Add(PC_GENDER.FEMALE, 0);
            genderlist.Add(PC_GENDER.MALE, 0);
            genderlist.Add(PC_GENDER.NONE, 0);
            Dictionary<PC_JOB, ushort> joblist = new Dictionary<PC_JOB, ushort>();
            joblist.Add(PC_JOB.GLADIATOR, 0);
            joblist.Add(PC_JOB.HAWKEYE, 0);
            joblist.Add(PC_JOB.FORCEMASTER, 0);
            joblist.Add(PC_JOB.CARDINAL, 0);
            joblist.Add(PC_JOB.NOVICE, 0);
            Dictionary<PC_RACE, ushort> racelist = new Dictionary<PC_RACE, ushort>();
            racelist.Add(PC_RACE.EMIL, 0);
            racelist.Add(PC_RACE.TITANIA, 0);
            racelist.Add(PC_RACE.DOMINION, 0);
            racelist.Add(PC_RACE.DEM, 0);
            racelist.Add(PC_RACE.NONE, 0);

            if (pc.Party != null)
            {
                foreach (ActorPC j in pc.Party.Members.Values)
                {
                    if (genderlist.ContainsKey(j.Gender))
                    {
                        genderlist[j.Gender]++;

                    }
                    else
                    {
                        genderlist.Add(j.Gender, 1);
                    }
                    if (joblist.ContainsKey(j.Job))
                    {
                        joblist[j.Job]++;

                    }
                    else
                    {
                        joblist.Add(j.Job, 1);
                    }
                    if (racelist.ContainsKey(j.Race))
                    {
                        racelist[j.Race]++;

                    }
                    else
                    {
                        racelist.Add(j.Race, 1);
                    }
                }
            }

            foreach (AbilityVector i in irislevels.Keys)
            {
                bool RAstate = false;
                if (i.ID < 1000) //原版能力
                {
                    RAstate = true;
                }
                else if (i.ID >= 1000 && i.ID < 2000) //组队条件触发的面板显示的能力
                {
                    if (i.ID < 1100) //单人or无队伍条件
                    {
                        if (pc.Party == null)
                            RAstate = true;
                    }
                    else //多人队伍条件
                    {
                        if (pc.Party != null)
                        {
                            switch (i.ID)
                            {
                                case 1101:
                                    if (pc.Party.Members.Count == 2 && genderlist[PC_GENDER.FEMALE] == 1)
                                    {
                                        RAstate = true;
                                    }
                                    break;
                                case 1102:
                                    if (pc.Party.Members.Count == 2 && genderlist[PC_GENDER.MALE] == 2)
                                    {
                                        RAstate = true;
                                    }
                                    break;
                                case 1103:
                                    if (pc.Party.Members.Count == 2 && genderlist[PC_GENDER.FEMALE] == 2)
                                    {
                                        RAstate = true;
                                    }
                                    break;
                                case 1801:
                                case 1802:
                                    if (joblist[PC_JOB.GLADIATOR] > 0 && joblist[PC_JOB.HAWKEYE] > 0 && joblist[PC_JOB.FORCEMASTER] > 0 && joblist[PC_JOB.CARDINAL] > 0)
                                    {
                                        RAstate = true;
                                    }
                                    break;
                                case 1804:
                                    if (joblist[PC_JOB.GLADIATOR] > 0 && joblist[PC_JOB.HAWKEYE] > 0 && joblist[PC_JOB.FORCEMASTER] > 0 && joblist[PC_JOB.CARDINAL] > 0)
                                    {
                                        if (joblist[PC_JOB.GLADIATOR] < 3 && joblist[PC_JOB.HAWKEYE] <3 && joblist[PC_JOB.FORCEMASTER] <3 && joblist[PC_JOB.CARDINAL] <3)
                                            RAstate = true;
                                    }
                                    break;
                                case 1901:
                                case 1902:
                                    if (racelist[PC_RACE.EMIL] > 0 && racelist[PC_RACE.TITANIA] > 0 && racelist[PC_RACE.DOMINION] > 0)
                                    {
                                        RAstate = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
                if (RAstate == true)
                {
                    Dictionary<ReleaseAbility, int> ability = i.ReleaseAbilities[(byte)irislevels[i]];
                    foreach (ReleaseAbility j in ability.Keys)
                    {
                        if (list.ContainsKey(j))
                            list[j] += ability[j];
                        else
                            list.Add(j, ability[j]);
                    }
                }
            }
            return list;
        }

        void CalcDemicChips(ActorPC pc)
        {
            Dictionary<byte, DEMICPanel> chips;
            if (pc.InDominionWorld)
                chips = pc.Inventory.DominionDemicChips;
            else
                chips = pc.Inventory.DemicChips;
            Dictionary<uint, int> skills = new Dictionary<uint, int>();

            #region CalcChips
            foreach (byte i in chips.Keys)
            {
                foreach (Chip j in chips[i].Chips)
                {
                    byte x1 = 255, y1 = 255, x2 = 255, y2 = 255;
                    if (chips[i].EngageTask1 != 255)
                    {
                        x1 = (byte)(chips[i].EngageTask1 % 9);
                        y1 = (byte)(chips[i].EngageTask1 / 9);
                    }
                    if (chips[i].EngageTask2 != 255)
                    {
                        x2 = (byte)(chips[i].EngageTask2 % 9);
                        y2 = (byte)(chips[i].EngageTask2 / 9);
                    }
                    bool nearEngage = j.IsNear(x1, y1) || j.IsNear(x2, y2);

                    if (j.Data.type < 20)
                    {
                        int rate = 1;
                        if (nearEngage)
                            rate = 2;
                        pc.Status.m_str_chip += (short)(rate * j.Data.str);
                        pc.Status.m_agi_chip += (short)(rate * j.Data.agi);
                        pc.Status.m_vit_chip += (short)(rate * j.Data.vit);
                        pc.Status.m_dex_chip += (short)(rate * j.Data.dex);
                        pc.Status.m_int_chip += (short)(rate * j.Data.intel);
                        pc.Status.m_mag_chip += (short)(rate * j.Data.mag);
                    }
                    else if (j.Data.type < 30)
                    {
                        int level = 1;
                        if (nearEngage)
                            level = 2;
                        if (skills.ContainsKey(j.Data.skill1))
                            skills[j.Data.skill1] += level;
                        else if (j.Data.skill1 != 0)
                            skills.Add(j.Data.skill1, level);

                        if (skills.ContainsKey(j.Data.skill2))
                            skills[j.Data.skill2] += level;
                        else if (j.Data.skill2 != 0)
                            skills.Add(j.Data.skill2, level);

                        if (skills.ContainsKey(j.Data.skill3))
                            skills[j.Data.skill3] += level;
                        else if (j.Data.skill3 != 0)
                            skills.Add(j.Data.skill3, level);
                    }
                    else
                    {
                        Chip next = null;
                        if (ChipFactory.Instance.ByChipID.ContainsKey(j.Data.engageTaskChip) && nearEngage)
                            next = new Chip(ChipFactory.Instance.ByChipID[j.Data.engageTaskChip]);
                        else
                            next = j;
                        pc.Status.m_str_chip += (short)(next.Data.str);
                        pc.Status.m_agi_chip += (short)(next.Data.agi);
                        pc.Status.m_vit_chip += (short)(next.Data.vit);
                        pc.Status.m_dex_chip += (short)(next.Data.dex);
                        pc.Status.m_int_chip += (short)(next.Data.intel);
                        pc.Status.m_mag_chip += (short)(next.Data.mag);
                        pc.Status.hp_rate_item += (short)(next.Data.hp);
                        pc.Status.sp_rate_item += (short)(next.Data.sp);
                        pc.Status.mp_rate_item += (short)(next.Data.mp);                         
                    }
                }
            }
            #endregion

            foreach (uint i in skills.Keys)
            {
                int level = 0;
                if (pc.Form != DEM_FORM.MACHINA_FORM)
                    level = 0;
                else
                    level = skills[i];
                if (pc.Skills.ContainsKey(i))
                {
                    if (pc.Skills[i].Level != level)
                    {
                        pc.Skills[i].Level = (byte)level;
                        if (pc.Skills[i].Level > pc.Skills[i].MaxLevel)
                            pc.Skills[i].Level = pc.Skills[i].MaxLevel;
                    }
                }
                else
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(i, 1);
                    skill.Level = (byte)level;
                    if (skill.Level > skill.MaxLevel)
                        skill.Level = skill.MaxLevel;
                    skill.NoSave = true;
                    pc.Skills.Add(i, skill);
                }
            }
        }
    }

 
}
