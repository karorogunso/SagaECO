using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Guardian
{
    public class ShieldImpact : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) &&
                sActor.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                if (sActor.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
                    sActor.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                {
                    return 0;
                }
                else
                    return -5;
            }
            else
                return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 6.20f, 7.00f, 8.10f, 8.60f, 9.00f }[level];
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 100, false);
            List<Actor> affected = new List<Actor>();
            affected.Add(dActor);
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                    if (SkillHandler.Instance.CanAdditionApply(sActor, i, SkillHandler.DefaultAdditions.Stun, 70))
                    {
                        Stun stun = new Stun(args.skill, i, 2000);
                        SkillHandler.ApplyAddition(i, stun);
                    }
                    if (SagaLib.Global.Random.Next(0, 99) <= 40)//ステータス減少は4割ほど-wiki
                    {
                        if (i.type == ActorType.PC)
                        {
                            if (level >= 3)
                            {
                                DefaultBuff skill = new DefaultBuff(args.skill, i, "AGI_DOWN", 20000);
                                skill.OnAdditionStart += this.StartAgiDown;
                                skill.OnAdditionEnd += this.EndAgiDown;
                                SkillHandler.ApplyAddition(i, skill);
                            }
                            if (level >= 4)
                            {
                                DefaultBuff skill = new DefaultBuff(args.skill, i, "STR_DOWN", 20000);
                                skill.OnAdditionStart += this.StartStrDown;
                                skill.OnAdditionEnd += this.EndStrDown;
                                SkillHandler.ApplyAddition(i, skill);
                            }
                            if (level >= 5)
                            {
                                DefaultBuff skill = new DefaultBuff(args.skill, i, "DEX_DOWN", 20000);
                                skill.OnAdditionStart += this.StartDexDown;
                                skill.OnAdditionEnd += this.EndDexDown;
                                SkillHandler.ApplyAddition(i, skill);
                            }
                        }
                        else
                        {
                            if (level >= 3)
                            {
                                DefaultBuff skill = new DefaultBuff(args.skill, i, "SAVOID_DOWN", 20000);
                                skill.OnAdditionStart += this.StartSavoidDown;
                                skill.OnAdditionEnd += this.EndSavoidDown;
                                SkillHandler.ApplyAddition(i, skill);
                            }
                            if (level >= 4)
                            {
                                DefaultBuff skill = new DefaultBuff(args.skill, i, "ATK_DOWN", 20000);
                                skill.OnAdditionStart += this.StartAtkDown;
                                skill.OnAdditionEnd += this.EndAtkDown;
                                SkillHandler.ApplyAddition(i, skill);
                            }
                            if (level >= 5)
                            {
                                DefaultBuff skill = new DefaultBuff(args.skill, i, "SHIT_DOWN", 20000);
                                skill.OnAdditionStart += this.StartShitDown;
                                skill.OnAdditionEnd += this.EndShitDown;
                                SkillHandler.ApplyAddition(i, skill);
                            }
                        }
                    }
                }


            }

            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
        }
        short[] value = { 0, 0, 0, 13, 15, 18 };
        void StartAgiDown(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("SHIELD_AGI_DOWN"))
                skill.Variable.Remove("SHIELD_AGI_DOWN");
            skill.Variable.Add("SHIELD_AGI_DOWN", value[skill.skill.Level]);
            actor.Status.agi_skill -= value[skill.skill.Level];
            actor.Buff.AGIDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndAgiDown(Actor actor, DefaultBuff skill)
        {
            actor.Status.agi_skill += (short)skill.Variable["SHIELD_AGI_DOWN"];
            actor.Buff.AGIDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void StartSavoidDown(Actor actor, DefaultBuff skill)
        {
            ushort avoid_value = (ushort)(actor.Status.avoid_melee * ((100 - value[skill.skill.Level]) / 100.0f));
            if (skill.Variable.ContainsKey("SHIELD_Savoid_DOWN"))
                skill.Variable.Remove("SHIELD_Savoid_DOWN");
            skill.Variable.Add("SHIELD_Savoid_DOWN", avoid_value);
            actor.Status.avoid_melee -= avoid_value;
            actor.Buff.AGIDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndSavoidDown(Actor actor, DefaultBuff skill)
        {
            actor.Status.avoid_melee += (ushort)skill.Variable["SHIELD_Savoid_DOWN"];
            actor.Buff.AGIDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void StartStrDown(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("SHIELD_STR_DOWN"))
                skill.Variable.Remove("SHIELD_STR_DOWN");
            skill.Variable.Add("SHIELD_STR_DOWN", value[skill.skill.Level]);
            actor.Status.str_skill -= value[skill.skill.Level];
            actor.Buff.STRDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndStrDown(Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill += (short)skill.Variable["SHIELD_STR_DOWN"];
            actor.Buff.STRDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void StartAtkDown(Actor actor, DefaultBuff skill)
        {
            short min_atk1_value = (short)(actor.Status.min_atk1 * ((100 - value[skill.skill.Level]) / 100.0f));
            short min_atk2_value = (short)(actor.Status.min_atk2 * ((100 - value[skill.skill.Level]) / 100.0f));
            short min_atk3_value = (short)(actor.Status.min_atk3 * ((100 - value[skill.skill.Level]) / 100.0f));
            short max_atk1_value = (short)(actor.Status.max_atk1 * ((100 - value[skill.skill.Level]) / 100.0f));
            short max_atk2_value = (short)(actor.Status.max_atk2 * ((100 - value[skill.skill.Level]) / 100.0f));
            short max_atk3_value = (short)(actor.Status.max_atk3 * ((100 - value[skill.skill.Level]) / 100.0f));

            if (skill.Variable.ContainsKey("SHIELD_MINATK1_DOWN"))
                skill.Variable.Remove("SHIELD_MINATK1_DOWN");
            skill.Variable.Add("SHIELD_MINATK1_DOWN", min_atk1_value);
            actor.Status.min_atk1_skill -= min_atk1_value;

            if (skill.Variable.ContainsKey("SHIELD_MINATK2_DOWN"))
                skill.Variable.Remove("SHIELD_MINATK2_DOWN");
            skill.Variable.Add("SHIELD_MINATK2_DOWN", min_atk2_value);
            actor.Status.min_atk2_skill -= min_atk2_value;

            if (skill.Variable.ContainsKey("SHIELD_MINATK3_DOWN"))
                skill.Variable.Remove("SHIELD_MINATK3_DOWN");
            skill.Variable.Add("SHIELD_MINATK3_DOWN", min_atk3_value);
            actor.Status.min_atk3_skill -= min_atk3_value;

            if (skill.Variable.ContainsKey("SHIELD_MAXATK1_DOWN"))
                skill.Variable.Remove("SHIELD_MAXATK1_DOWN");
            skill.Variable.Add("SHIELD_MAXATK1_DOWN", max_atk1_value);
            actor.Status.max_atk1_skill -= max_atk1_value;

            if (skill.Variable.ContainsKey("SHIELD_MAXATK2_DOWN"))
                skill.Variable.Remove("SHIELD_MAXATK2_DOWN");
            skill.Variable.Add("SHIELD_MAXATK2_DOWN", max_atk2_value);
            actor.Status.max_atk2_skill -= max_atk2_value;

            if (skill.Variable.ContainsKey("SHIELD_MAXATK3_DOWN"))
                skill.Variable.Remove("SHIELD_MAXATK3_DOWN");
            skill.Variable.Add("SHIELD_MAXATK3_DOWN", max_atk3_value);
            actor.Status.max_atk3_skill -= max_atk3_value;

            actor.Buff.MinAtkDown = true;
            actor.Buff.MaxAtkDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndAtkDown(Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill += (short)skill.Variable["SHIELD_MINATK1_DOWN"];
            actor.Status.min_atk2_skill += (short)skill.Variable["SHIELD_MINATK2_DOWN"];
            actor.Status.min_atk3_skill += (short)skill.Variable["SHIELD_MINATK3_DOWN"];
            actor.Status.max_atk1_skill += (short)skill.Variable["SHIELD_MAXATK1_DOWN"];
            actor.Status.max_atk2_skill += (short)skill.Variable["SHIELD_MAXATK2_DOWN"];
            actor.Status.max_atk3_skill += (short)skill.Variable["SHIELD_MAXATK3_DOWN"];
            actor.Status.max_atk3_skill += (short)skill.Variable["SHIELD_MAXATK3_DOWN"];
            actor.Buff.MinAtkDown = false;
            actor.Buff.MaxAtkDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void StartShitDown(Actor actor, DefaultBuff skill)
        {
            short hit_melee_value = (short)(actor.Status.hit_melee * ((100 - value[skill.skill.Level]) / 100.0f));
            if (skill.Variable.ContainsKey("SHIELD_SHIT_DOWN"))
                skill.Variable.Remove("SHIELD_SHIT_DOWN");
            skill.Variable.Add("SHIELD_SHIT_DOWN", hit_melee_value);
            actor.Status.hit_melee_skill -= hit_melee_value;
            actor.Buff.ShortHitDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndShitDown(Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill += (short)skill.Variable["SHIELD_SHIT_DOWN"];
            actor.Buff.ShortHitDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void StartDexDown(Actor actor, DefaultBuff skill)
        {

            if (skill.Variable.ContainsKey("SHIELD_DEX_DOWN"))
                skill.Variable.Remove("SHIELD_DEX_DOWN");
            skill.Variable.Add("SHIELD_DEX_DOWN", value[skill.skill.Level]);
            actor.Status.dex_skill -= value[skill.skill.Level];
            actor.Buff.DEXDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndDexDown(Actor actor, DefaultBuff skill)
        {
            actor.Status.dex_skill += (short)skill.Variable["SHIELD_DEX_DOWN"];
            actor.Buff.DEXDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
