using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Iris;
using SagaDB.DEMIC;
using SagaDB.Skill;
using SagaMap.Skill;
using SagaMap.Network.Client;

namespace SagaMap.PC
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        /// <summary>
        /// 获取强化奖励数值
        /// </summary>
        /// <param name="item">道具本身</param>
        /// <param name="Type">0: 生命 1: ATK DEF MATK 2: MDEF 3: 爆击相关</param>
        /// <returns></returns>
        public short GetEnhanceBonus(Item item, int Type)
        {
            short value = 0;
            short[] hps = new short[26] { 0,
                                          100, 20, 70, 30, 80, 40, 90, 50, 100, 150,
                                          150, 60, 110, 70, 200, 200, 120, 80, 130, 250,
                                          250, 90, 140, 100, 250 };
            short[] atk_def_matk = new short[26] { 0,
                                           10, 3, 5, 3, 6, 3, 7, 3, 8, 13,
                                           13, 3, 9, 3, 15, 15, 10, 3, 11, 20,
                                           20, 3, 12, 3, 22 };
            short[] mdef = new short[26] { 0,
                                           10, 2, 5, 2, 6, 3, 6, 3, 6, 15,
                                           15, 4, 7, 4, 10, 10, 7, 4, 7, 15,
                                           15, 5, 8, 5, 15 };
            short[] cris = new short[26] { 0,
                                           5, 1, 3, 2, 4, 3, 4, 3, 5, 9,
                                           5, 1, 2, 3, 4, 5, 1, 2, 3, 4,
                                           5, 1, 2, 3, 4 };
            switch (Type)
            {
                case 0:
                    for (int i = 0; i <= item.LifeEnhance; i++)
                        value += hps[i];
                    break;
                case 1:
                    for (int i = 0; i <= item.PowerEnhance; i++)
                        value += atk_def_matk[i];
                    break;
                case 2:
                    for (int i = 0; i <= item.CritEnhance; i++)
                        value += cris[i];
                    break;
                case 3:
                    for (int i = 0; i <= item.MagEnhance; i++)
                        value += mdef[i];
                    break;
                case 4:
                    for (int i = 0; i <= item.MagEnhance; i++)
                        value += atk_def_matk[i];
                    break;
                default:
                    Logger.ShowError("未知的附魔类型");
                    value = 0;
                    break;
            }
            return value;
        }
        private void CalcEquipBonus(ActorPC pc)
        {
            pc.Status.ClearItem();

            pc.Inventory.MaxPayload[ContainerType.BODY] = 0;
            pc.Inventory.MaxPayload[ContainerType.BACK_BAG] = 0;
            pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] = 0;
            pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] = 0;
            pc.Inventory.MaxVolume[ContainerType.BODY] = 0;
            pc.Inventory.MaxVolume[ContainerType.BACK_BAG] = 0;
            pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] = 0;
            pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] = 0;

            Dictionary<EnumEquipSlot, Item> equips;
            if (pc.Form == DEM_FORM.NORMAL_FORM)
                equips = pc.Inventory.Equipments;
            else
                equips = pc.Inventory.Parts;
            foreach (SagaDB.Item.EnumEquipSlot j in equips.Keys)
            {
                Item i = equips[j];
                if (i.Stack == 0)
                    continue;
                //去掉左手武器判定
                if ((j != EnumEquipSlot.PET || i.BaseData.itemType == ItemType.BACK_DEMON)
                    && !(j == EnumEquipSlot.LEFT_HAND && i.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && i.EquipSlot.Count == 1))
                {
                    int skill_Atk_add1 = 0, skill_Atk_add2 = 0, skill_Atk_add3 = 0, skill_Matk_add = 0;
                    float rate = pc.Status.Weapon_add;
                    skill_Atk_add1 = (int)(i.BaseData.atk1 * rate);
                    skill_Atk_add2 = (int)(i.BaseData.atk2 * rate);
                    skill_Atk_add3 = (int)(i.BaseData.atk3 * rate);
                    skill_Matk_add = (int)(i.BaseData.matk * rate);

                    if (Configuration.Instance.AtkMastery)//熟练度影响
                    {
                        /*熟练度不要了！
                        pc.Status.atk1_item = (short)(pc.Status.atk1_item + i.BaseData.atk1 + i.Atk1 + (float)(i.BaseData.atk1 * pc.DefLv / 20) + skill_Atk_add1);
                        pc.Status.atk2_item = (short)(pc.Status.atk2_item + i.BaseData.atk2 + i.Atk2 + (float)(i.BaseData.atk2 * pc.DefLv / 20) + skill_Atk_add2);
                        pc.Status.atk3_item = (short)(pc.Status.atk3_item + i.BaseData.atk3 + i.Atk3 + (float)(i.BaseData.atk3 * pc.DefLv / 20) + skill_Atk_add3);*/
                        pc.Status.atk1_item = (short)(pc.Status.atk1_item + i.BaseData.atk1 + i.Atk1 + skill_Atk_add1);
                        pc.Status.atk2_item = (short)(pc.Status.atk2_item + i.BaseData.atk2 + i.Atk2 + skill_Atk_add2);
                        pc.Status.atk3_item = (short)(pc.Status.atk3_item + i.BaseData.atk3 + i.Atk3 + skill_Atk_add3);

                        //pc.Status.avoid_critical = (ushort)(pc.Status.avoid_critical + i.BaseData.avoidCritical + i.AvoidCritical + (float)(pc.DefLv / 2));
                        //pc.Status.hit_critical = (ushort)(pc.Status.hit_critical + i.BaseData.hitCritical + i.HitCritical + (float)(pc.DefLv / 2));

                        //pc.Status.matk_item = (short)(pc.Status.matk_item + i.BaseData.matk + i.MAtk + (float)(i.BaseData.matk * pc.MDefLv / 20) + skill_Matk_add);
                        pc.Status.matk_item = (short)(pc.Status.matk_item + i.BaseData.matk + i.MAtk + skill_Matk_add);
                        //pc.Status.avoid_magic = (ushort)(pc.Status.avoid_magic + i.BaseData.avoidMagic + i.AvoidMagic + (float)(pc.MDefLv / 2));
                        //pc.Status.hit_magic = (ushort)(pc.Status.hit_magic + i.BaseData.hitMagic + i.HitMagic + (float)(pc.MDefLv / 2));
                    }
                    else
                    {
                        pc.Status.atk1_item = (short)(pc.Status.atk1_item + i.BaseData.atk1 + i.Atk1 + skill_Atk_add1);
                        pc.Status.atk2_item = (short)(pc.Status.atk2_item + i.BaseData.atk2 + i.Atk2 + skill_Atk_add2);
                        pc.Status.atk3_item = (short)(pc.Status.atk3_item + i.BaseData.atk3 + i.Atk3 + skill_Atk_add3);
                        pc.Status.matk_item = (short)(pc.Status.matk_item + i.BaseData.matk + i.MAtk + skill_Matk_add);
                    }

                    pc.Status.hp_item = (short)(pc.Status.hp_item + i.BaseData.hp + i.HP);
                    pc.Status.def_add = (short)(pc.Status.def_add + i.BaseData.def + i.Def);
                    pc.Status.mdef_add = (short)(pc.Status.mdef_add + i.BaseData.mdef + i.MDef);
                    pc.Status.hit_melee_item = (short)(pc.Status.hit_melee_item + i.BaseData.hitMelee + i.HitMelee);
                    pc.Status.hit_ranged_item = (short)(pc.Status.hit_ranged_item + i.BaseData.hitRanged + i.HitRanged);
                    pc.Status.avoid_melee_item = (short)(pc.Status.avoid_melee_item + i.BaseData.avoidMelee + i.AvoidMelee);
                    pc.Status.avoid_ranged_item = (short)(pc.Status.avoid_ranged_item + i.BaseData.avoidRanged + i.AvoidRanged);
                    pc.Status.str_item = (short)(pc.Status.str_item + i.BaseData.str + i.Str);
                    pc.Status.agi_item = (short)(pc.Status.agi_item + i.BaseData.agi + i.Agi);
                    pc.Status.dex_item = (short)(pc.Status.dex_item + i.BaseData.dex + i.Dex);
                    pc.Status.vit_item = (short)(pc.Status.vit_item + i.BaseData.vit + i.Vit);
                    pc.Status.int_item = (short)(pc.Status.int_item + i.BaseData.intel + i.Int);
                    pc.Status.mag_item = (short)(pc.Status.mag_item + i.BaseData.mag + i.Mag);
                    pc.Status.hp_item = (short)(pc.Status.hp_item + i.BaseData.hp + i.HP);
                    pc.Status.sp_item = (short)(pc.Status.sp_item + i.BaseData.sp + i.SP);
                    pc.Status.mp_item = (short)(pc.Status.mp_item + i.BaseData.mp + i.MP);
                    pc.Status.cri_item = (short)(pc.Status.cri_item + i.BaseData.hitCritical + i.HitCritical);
                    pc.Status.criavd_item = (short)(pc.Status.criavd_item + i.BaseData.avoidCritical + i.AvoidCritical);
                    pc.Status.hit_magic_item = (short)(pc.Status.hit_magic_item + i.BaseData.hitMagic + i.HitMagic);
                    pc.Status.avoid_magic_item = (short)(pc.Status.avoid_magic_item + i.BaseData.avoidMagic + i.AvoidMagic);
                    pc.Status.speed_item = (int)(i.BaseData.speedUp + i.SpeedUp);

                    if (i.BaseData.speedUp != 0 || i.SpeedUp != 0)
                    {
                        if (pc.Online)
                        {
                            pc.e.PropertyUpdate(UpdateEvent.SPEED, 0);
                        }
                    }

                    if (i.IsWeapon)
                    {
                        foreach (Elements k in i.BaseData.element.Keys)
                        {
                            pc.Status.attackElements_item[k] += i.BaseData.element[k];
                        }
                    }
                    else if ((i.IsArmor || i.IsEquipt) && !i.IsAmmo)
                    {
                        foreach (Elements k in i.BaseData.element.Keys)
                        {
                            pc.Status.elements_item[k] += i.BaseData.element[k];
                        }
                    }
                    if (i.BaseData.itemType == ItemType.ARROW || i.BaseData.itemType == ItemType.BULLET)
                    {
                        foreach (Elements k in i.BaseData.element.Keys)
                        {
                            pc.Status.attackElements_item[k] += i.BaseData.element[k];
                        }
                    }
                }
                if (i.BaseData.weightUp != 0)
                {
                    switch (j)
                    {
                        case EnumEquipSlot.PET:
                            pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)(pc.Inventory.MaxPayload[ContainerType.BODY] + i.BaseData.weightUp);
                            break;
                        case EnumEquipSlot.BACK:
                            pc.Inventory.MaxPayload[ContainerType.BACK_BAG] = (uint)(pc.Inventory.MaxPayload[ContainerType.BACK_BAG] + i.BaseData.weightUp);
                            break;
                        case EnumEquipSlot.LEFT_HAND:
                            pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] = (uint)(pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] + i.BaseData.weightUp);
                            break;
                        case EnumEquipSlot.RIGHT_HAND:
                            pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] = (uint)(pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] + i.BaseData.weightUp);
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
                            pc.Inventory.MaxVolume[ContainerType.BODY] = (uint)(pc.Inventory.MaxVolume[ContainerType.BODY] + i.BaseData.volumeUp * (1f + rate));
                            break;
                        case EnumEquipSlot.BACK:
                            pc.Inventory.MaxVolume[ContainerType.BACK_BAG] = (uint)(pc.Inventory.MaxVolume[ContainerType.BACK_BAG] + i.BaseData.volumeUp * (1f + rate));
                            break;
                        case EnumEquipSlot.LEFT_HAND:
                            pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] = (uint)(pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] + i.BaseData.volumeUp * (1f + rate));
                            break;
                        case EnumEquipSlot.RIGHT_HAND:
                            pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] = (uint)(pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] + i.BaseData.volumeUp * (1f + rate));
                            break;
                    }
                }
                if (i.BaseData.possibleSkill != 0)//装备附带主动技能
                {
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(i.BaseData.possibleSkill, 1);
                    if (skill != null)
                    {
                        if (!pc.Skills.ContainsKey(i.BaseData.possibleSkill))
                        {
                            pc.Skills.Add(i.BaseData.possibleSkill, skill);
                        }
                    }
                }

                if (i.BaseData.passiveSkill != 0)//装备附带被动技能
                {
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(i.BaseData.passiveSkill, 1);
                    if (skill != null)
                    {
                        if (!pc.Skills.ContainsKey(i.BaseData.passiveSkill))
                        {
                            pc.Skills.Add(i.BaseData.passiveSkill, skill);
                            if (!skill.BaseData.active)
                            {
                                SkillArg arg = new SkillArg();
                                arg.skill = skill;
                                SkillHandler.Instance.SkillCast(pc, pc, arg);
                            }
                        }
                    }
                }
                ApplyIrisCard(pc, i);
            }

            MapClient mc = SagaMap.Manager.MapClientManager.Instance.FindClient(pc);
            if (mc != null)
                mc.OnPlayerElements();
            CalcDemicChips(pc);
        }

        private void CalcEquipmentSetBonus(ActorPC pc)
        {
            Dictionary<EnumEquipSlot, Item> equips;
            if (pc.Form == DEM_FORM.NORMAL_FORM)
                equips = pc.Inventory.Equipments;
            else
                equips = pc.Inventory.Parts;

            var setequips = equips.ToDictionary(x => x.Key, y => y.Value.ItemID);

            EquipmentSet set = new EquipmentSet();
            bool fActive = false;

            foreach (var item in EquipmentSetFactory.Instance.Items)
            {
                var a = item.Value.SetSlots.Where(f => f.Value != 0);
                var x = setequips.Intersect(a);
                if (x.Count() == a.Count())
                {
                    set = item.Value;
                    fActive = true;
                    pc.EquipSetID = item.Key;
                    break;
                }
            }
            if (fActive)
            {
                pc.Status.def += (ushort)set.Bonus.def;
                pc.Status.mdef += (ushort)set.Bonus.mdef;
                pc.Status.def_add += (short)set.Bonus.def_add;
                pc.Status.mdef_add += (short)set.Bonus.mdef_add;
                pc.Status.hit_melee_item += (short)set.Bonus.shit;
                pc.Status.hit_ranged_item += (short)set.Bonus.lhit;
                pc.Status.avoid_melee_item += (short)set.Bonus.savoid;
                pc.Status.avoid_ranged_item += (short)set.Bonus.lavoid;
                pc.Status.str_item += (short)set.Bonus.str;
                pc.Status.agi_item += (short)set.Bonus.agi;
                pc.Status.dex_item += (short)set.Bonus.dex;
                pc.Status.vit_item += (short)set.Bonus.vit;
                pc.Status.int_item += (short)set.Bonus._int;
                pc.Status.mag_item += (short)set.Bonus.mag;
                pc.Status.hp_item += (short)set.Bonus.mhp;
                pc.Status.sp_item += (short)set.Bonus.msp;
                pc.Status.mp_item += (short)set.Bonus.mmp;
                pc.Status.cri_item += (short)set.Bonus.cri;
                pc.Status.criavd_item += (short)set.Bonus.criavoid;
                pc.Status.speed_item += (short)set.Bonus.speed;
                pc.Status.aspd_skill += (short)set.Bonus.aspd;
                pc.Status.cspd_skill += (short)set.Bonus.cspd;

                if (set.Bonus.speed != 0)
                {
                    if (pc.Online)
                    {
                        pc.e.PropertyUpdate(UpdateEvent.SPEED, 0);
                    }
                }
                if (set.Bonus.askill1 != 0)//装备附带主动技能
                {
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(set.Bonus.askill1, 1);
                    if (skill != null)
                    {
                        if (!pc.Skills.ContainsKey(set.Bonus.askill1))
                        {
                            pc.Skills.Add(set.Bonus.askill1, skill);
                        }
                    }
                }

                if (set.Bonus.pskill1 != 0)//装备附带被动技能
                {
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(set.Bonus.pskill1, 1);
                    if (skill != null)
                    {
                        if (!pc.Skills.ContainsKey(set.Bonus.pskill1))
                        {
                            pc.Skills.Add(set.Bonus.pskill1, skill);
                            if (!skill.BaseData.active)
                            {
                                SkillArg arg = new SkillArg();
                                arg.skill = skill;
                                SkillHandler.Instance.SkillCast(pc, pc, arg);
                            }
                        }
                    }
                }
            }
            else
            {
                if (pc.EquipSetID != 0)
                {
                    //移除套装技能
                    SetBonus bonus = EquipmentSetFactory.Instance.GetBonus(pc.EquipSetID);
                    if (bonus.askill1 != 0)//装备附带主动技能
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(bonus.askill1, 1);
                        if (skill != null)
                        {
                            if (pc.Skills.ContainsKey(bonus.askill1))
                            {
                                pc.Skills.Remove(bonus.askill1);
                            }
                        }
                    }

                    if (bonus.pskill1 != 0)//装备附带被动技能
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(bonus.pskill1, 1);
                        if (skill != null)
                        {
                            if (pc.Skills.ContainsKey(bonus.pskill1))
                            {
                                pc.Skills.Remove(bonus.pskill1);
                                SkillHandler.Instance.CastPassiveSkills(pc);
                            }
                        }
                    }
                }
            }
        }

        void ApplyIrisCard(ActorPC pc, Item item)
        {
            if (!item.Locked)
                return;

            #region ReleaseAbility

            Dictionary<ReleaseAbility, int> abilities = item.ReleaseAbilities(false);
            foreach (ReleaseAbility i in abilities.Keys)
            {
                int value = abilities[i];
                switch (i)
                {
                    case ReleaseAbility.HP最大値上昇Plus:
                        pc.Status.hp_item += (short)value;
                        break;
                    case ReleaseAbility.HP回復力上昇Perc:
                        pc.Status.hp_recover_item += (short)value;
                        break;
                    case ReleaseAbility.VIT上昇Plus:
                        pc.Status.vit_item += (short)value;
                        break;
                    //case ReleaseAbility.AGIPlus上升:
                    //    pc.Status.agi_item += (short)value;
                    //    break;
                    //case ReleaseAbility.物理攻击值Plus上升:
                    //    pc.Status.atk1_item += (short)value;
                    //    pc.Status.atk2_item += (short)value;
                    //    pc.Status.atk3_item += (short)value;
                    //    break;
                    //case ReleaseAbility.DEFPlus上升:
                    //    pc.Status.def_add += (short)value;
                    //    break;
                    //case ReleaseAbility.DEXPlus上升:
                    //    pc.Status.dex_item += (short)value;
                    //    break;
                    //case ReleaseAbility.HP最大值Plus上升:
                    //    pc.Status.hp_item += (short)value;
                    //    break;
                    //case ReleaseAbility.INTPlus上升:
                    //    pc.Status.int_item += (short)value;
                    //    break;
                    //case ReleaseAbility.远距命中值Perc上升:
                    //    pc.Status.hit_ranged_item += (short)(pc.Status.hit_ranged * ((float)value / 100 + 1));
                    //    break;
                    //case ReleaseAbility.远距命中值Plus上升:
                    //    pc.Status.hit_ranged_item += (short)value;
                    //    break;
                    //case ReleaseAbility.MAGPlus上升:
                    //    pc.Status.mag_item += (short)value;
                    //    break;
                    //case ReleaseAbility.魔法攻击值Plus上升:
                    //    pc.Status.matk_item += (short)value;
                    //    break;
                    //case ReleaseAbility.MP最大值Plus上升:
                    //    pc.Status.mp_item += (short)value;
                    //    break;
                    //case ReleaseAbility.近距命中值Perc上升:
                    //    pc.Status.hit_melee_item += (short)(pc.Status.hit_melee * ((float)value / 100 + 1f));
                    //    break;
                    //case ReleaseAbility.近距命中值Plus上升:
                    //    pc.Status.hit_melee_item += (short)value;
                    //    break;
                    //case ReleaseAbility.SP最大值Plus上升:
                    //    pc.Status.sp_item += (short)value;
                    //    break;
                    //case ReleaseAbility.STRPlus上升:
                    //    pc.Status.str_item += (short)value;
                    //    break;
                    //case ReleaseAbility.VITPlus上升:
                    //    pc.Status.vit_item += (short)value;
                    //    break;
                    //case ReleaseAbility.防御率Perc上升:
                    //    pc.Status.guard_item += (short)value;
                    //    break;
                    //case ReleaseAbility.体积Perc上升:
                    //    pc.Status.volume_item += (short)value;
                    //    break;
                    //case ReleaseAbility.重量Perc上升:
                    //    pc.Status.payl_item += (short)value;
                    //    break;
                    //case ReleaseAbility.防具的防御力Perc上升:
                    //    pc.Status.def_add += (short)(item.BaseData.def * ((float)value / 100));
                    //    break;
                    //case ReleaseAbility.防具防御力Plus上升:
                    //    pc.Status.def_add += (short)value;
                    //    break;
                    //case ReleaseAbility.武器攻击力Perc上升:
                    //    pc.Status.atk1_item += (short)(item.BaseData.atk1 * ((float)value / 100));
                    //    pc.Status.atk2_item += (short)(item.BaseData.atk2 * ((float)value / 100));
                    //    pc.Status.atk3_item += (short)(item.BaseData.atk3 * ((float)value / 100));
                    //    break;
                    //case ReleaseAbility.武器攻击力Plus上升:
                    //    pc.Status.atk1_item += (short)value;
                    //    pc.Status.atk2_item += (short)value;
                    //    pc.Status.atk3_item += (short)value;
                    //    break;
                }
            }

            #endregion

            Dictionary<Elements, int> elements = item.Elements(false);
            if (item.IsArmor || item.IsWeapon)
            {
                if (item.IsWeapon)
                {
                    foreach (Elements i in elements.Keys)
                    {
                        pc.Status.attackElements_item[i] += elements[i];
                    }
                }
                if (item.IsArmor)
                {
                    foreach (Elements i in elements.Keys)
                    {
                        pc.Status.elements_item[i] += elements[i];
                    }
                }
            }
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
