using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class ThrowMastery : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ThrowMastery", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value;
            value = skill.skill.Level * 5;
            if (skill.skill.Level == 5)
                value += 5;

            if (skill.Variable.ContainsKey("MasteryATK"))
                skill.Variable.Remove("MasteryATK");
            skill.Variable.Add("MasteryATK", value);
            actor.Status.min_atk3_skill += (short)value;

            value = skill.skill.Level * 2;

            if (skill.skill.Level == 5)
                value += 2;

            if (skill.Variable.ContainsKey("MasteryHIT"))
                skill.Variable.Remove("MasteryHIT");
            skill.Variable.Add("MasteryHIT", value);
            actor.Status.hit_ranged_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type == ActorType.PC)
            {
                int value = skill.Variable["MasteryATK"];
                actor.Status.min_atk3_skill -= (short)value;
                value = skill.Variable["MasteryHIT"];
                actor.Status.hit_ranged_skill -= (short)value;
            }
        }

        #endregion
    }
}
