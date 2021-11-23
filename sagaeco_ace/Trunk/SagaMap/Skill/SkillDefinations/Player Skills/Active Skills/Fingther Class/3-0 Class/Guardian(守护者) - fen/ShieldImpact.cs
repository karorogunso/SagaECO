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
                    return -17;
            }
            else
                return -17;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 6.20f, 7.00f, 8.10f, 8.60f, 9.00f }[level];
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 300, false);
            List<Actor> affected = new List<Actor>();
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
                }
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
            }

            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
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
