using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 重點射擊（バイタルショット）
    /// </summary>
    public class VitalShot : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor) && CheckPossible(sActor))
            {
                return 0;
            }
            else
            {
                return -5;
            }
        }
        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW ||
                        pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.1f + 0.1f * level;
            int rateDown = 25 + 5 * level, rateSlow = 20 + 5 * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (SagaLib.Global.Random.Next(0, 99) < rateDown)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "VitalShot", 15000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rateSlow))
            {
                int lifetime = 4000 + 500 * level;
                Additions.Global.MoveSpeedDown skill2 = new SagaMap.Skill.Additions.Global.MoveSpeedDown(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill2);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //近命中
            int hit_melee_add = -(int)(actor.Status.hit_melee * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("VitalShot_hit_melee"))
                skill.Variable.Remove("VitalShot_hit_melee");
            skill.Variable.Add("VitalShot_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill = (short)hit_melee_add;

            //遠命中
            int hit_ranged_add = -(int)(actor.Status.hit_ranged * (0.13f + 0.09f * level));
            if (skill.Variable.ContainsKey("VitalShot_hit_ranged"))
                skill.Variable.Remove("VitalShot_hit_ranged");
            skill.Variable.Add("VitalShot_hit_ranged", hit_ranged_add);
            actor.Status.hit_ranged_skill = (short)hit_ranged_add;

            //近戰迴避
            int avoid_melee_add = -(int)(actor.Status.avoid_melee * (0.1f + 0.02f * level));
            if (skill.Variable.ContainsKey("VitalShot_avoid_melee"))
                skill.Variable.Remove("VitalShot_avoid_melee");
            skill.Variable.Add("VitalShot_avoid_melee", avoid_melee_add);
            actor.Status.avoid_melee_skill = (short)avoid_melee_add;

            //遠距迴避
            int avoid_ranged_add = -(int)(actor.Status.avoid_ranged * (0.09f + 0.03f * level));
            if (skill.Variable.ContainsKey("VitalShot_avoid_ranged"))
                skill.Variable.Remove("VitalShot_avoid_ranged");
            skill.Variable.Add("VitalShot_avoid_ranged", avoid_ranged_add);
            actor.Status.avoid_ranged_skill = (short)avoid_ranged_add;
            actor.Buff.ShortHitDown = true;
            actor.Buff.LongHitDown = true;
            actor.Buff.ShortDodgeDown = true;
            actor.Buff.LongDodgeDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["VitalShot_hit_melee"];

            //遠命中
            actor.Status.hit_ranged_skill -= (short)skill.Variable["VitalShot_hit_ranged"];

            //近戰迴避
            actor.Status.avoid_melee_skill -= (short)skill.Variable["VitalShot_avoid_melee"];

            //遠距迴避
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["VitalShot_avoid_ranged"];
            actor.Buff.ShortHitDown = false;
            actor.Buff.LongHitDown = false;
            actor.Buff.ShortDodgeDown = false;
            actor.Buff.LongDodgeDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
