using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 爪武器修練（クローマスタリー）
    /// </summary>
    public class ClawMastery : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ClawMastery", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            //最小攻擊
            int min_atk1_add = (int)(10 + 10 * level);
            if (skill.Variable.ContainsKey("ClawMastery_min_atk1"))
                skill.Variable.Remove("ClawMastery_min_atk1");
            skill.Variable.Add("ClawMastery_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(10 + 10 * level);
            if (skill.Variable.ContainsKey("ClawMastery_min_atk2"))
                skill.Variable.Remove("ClawMastery_min_atk2");
            skill.Variable.Add("ClawMastery_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(10 + 10 * level);
            if (skill.Variable.ContainsKey("ClawMastery_min_atk3"))
                skill.Variable.Remove("ClawMastery_min_atk3");
            skill.Variable.Add("ClawMastery_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //近命中
            int hit_melee_add = (int)(4 * level);
            if (skill.Variable.ContainsKey("ClawMastery_hit_melee"))
                skill.Variable.Remove("ClawMastery_hit_melee");
            skill.Variable.Add("ClawMastery_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;

        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["ClawMastery_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["ClawMastery_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["ClawMastery_min_atk3"];

            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["ClawMastery_hit_melee"];

        }

        #endregion
    }
}
