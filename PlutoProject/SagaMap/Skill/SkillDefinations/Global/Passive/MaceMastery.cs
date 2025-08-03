using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class MaceMastery:ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.HAMMER ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.STAFF)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MaceMastery", active);
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

            if (skill.Variable.ContainsKey("MaceMasteryATK"))
                skill.Variable.Remove("MaceMasteryATK");
            skill.Variable.Add("MaceMasteryATK", value);
            actor.Status.min_atk1_skill += (short)value;

            value = skill.skill.Level * 2;

            if (skill.skill.Level == 5)
                value += 2;

            if (skill.Variable.ContainsKey("MaceMasteryHIT"))
                skill.Variable.Remove("MaceMasteryHIT");
            skill.Variable.Add("MaceMasteryHIT", value);
            actor.Status.hit_melee_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value = skill.Variable["MaceMasteryATK"];
            actor.Status.min_atk1_skill -= (short)value;
            value = skill.Variable["MaceMasteryHIT"];
            actor.Status.hit_melee_skill -= (short)value;
        }

        #endregion
    }
}
