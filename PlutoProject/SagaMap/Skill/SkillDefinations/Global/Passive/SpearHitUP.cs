using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class SpearHitUP:ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SPEAR)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "SpearHitUP", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    value = 2;
                    break;
                case 2:
                    value = 5;
                    break;
                case 3:
                    value = 8;
                    break;
                case 4:
                    value = 11;
                    break;
                case 5:
                    value = 15;
                    break;
            }
            if (skill.Variable.ContainsKey("MasteryHIT"))
                skill.Variable.Remove("MasteryHIT");
            skill.Variable.Add("MasteryHIT", value);
            actor.Status.hit_melee_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type == ActorType.PC)
            {
                int value = skill.Variable["MasteryHIT"];
                actor.Status.hit_melee_skill -= (short)value;
            }
        }

        #endregion
    }
}
