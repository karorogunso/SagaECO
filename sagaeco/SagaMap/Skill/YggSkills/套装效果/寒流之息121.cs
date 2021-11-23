using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class TwoMaceMastery : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "寒流之息", true);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int HPvalue = 0;
            int ATKvalue = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    ATKvalue = 2;
                    break;
                case 2:
                    ATKvalue = 3;
                    break;
                case 3:
                    ATKvalue = 4;
                    break;
                case 4:
                    ATKvalue = 5;
                    break;
                case 5:
                    ATKvalue = 5;
                    break;
            }

            if (skill.Variable.ContainsKey("寒流之息ATKUP"))
                skill.Variable.Remove("寒流之息ATKUP");
            skill.Variable.Add("寒流之息ATKUP", ATKvalue);
            actor.Status.atk1_rate_item += (short)ATKvalue;
            actor.Status.atk2_rate_item += (short)ATKvalue;
            actor.Status.atk3_rate_item += (short)ATKvalue;
            actor.Status.matk_rate_item += (short)ATKvalue;


            if (skill.skill.Level == 5)
                actor.TInt["寒流之息Lv5"] = 1;
            else
                actor.TInt["寒流之息Lv5"] = 0;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int ATKvalue = skill.Variable["寒流之息ATKUP"];
            actor.Status.atk1_rate_item -= (short)ATKvalue;
            actor.Status.atk2_rate_item -= (short)ATKvalue;
            actor.Status.atk3_rate_item -= (short)ATKvalue;
            actor.Status.matk_rate_item -= (short)ATKvalue;

            actor.TInt["寒流之息Lv5"] = 0;
        }

        #endregion
    }
}
