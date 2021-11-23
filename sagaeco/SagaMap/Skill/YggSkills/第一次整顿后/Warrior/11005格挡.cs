using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11005 : ISkill
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
                        active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "技能格挡", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            byte level = skill.skill.Level;

            int ParryRateUP = 5 + 5 * level;
            if (skill.Variable.ContainsKey("ParryRateUP"))
                skill.Variable.Remove("ParryRateUP");
            skill.Variable.Add("ParryRateUP", ParryRateUP);
            actor.Status.ParryRate += ParryRateUP;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int ParryRateUP = skill.Variable["ParryRateUP"];
            actor.Status.ParryRate -= ParryRateUP;
        }

        #endregion
    }
}
