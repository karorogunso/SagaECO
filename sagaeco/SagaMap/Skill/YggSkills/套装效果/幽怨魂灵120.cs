using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class TwoSpearMastery : ISkill
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
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "幽怨魂灵", true);
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
                    HPvalue = 3;
                    break;
                case 2:
                    ATKvalue = 2;
                    HPvalue = 3;
                    break;
                case 3:
                    ATKvalue = 2;
                    HPvalue = 8;
                    break;
                case 4:
                case 5:
                    ATKvalue = 4;
                    HPvalue = 8;
                    break;
            }

            if (skill.Variable.ContainsKey("幽怨魂灵MaxHPUP"))
                skill.Variable.Remove("幽怨魂灵MaxHPUP");
            skill.Variable.Add("幽怨魂灵MaxHPUP", HPvalue);
            actor.Status.hp_rate_skill += (short)HPvalue;

            if (skill.Variable.ContainsKey("幽怨魂灵ATKUP"))
                skill.Variable.Remove("幽怨魂灵ATKUP");
            skill.Variable.Add("幽怨魂灵ATKUP", ATKvalue);
            actor.Status.atk1_rate_item += (short)ATKvalue;
            actor.Status.atk2_rate_item += (short)ATKvalue;
            actor.Status.atk3_rate_item += (short)ATKvalue;
            actor.Status.matk_rate_item += (short)ATKvalue;


            if (skill.skill.Level == 5)
                actor.TInt["幽怨魂灵Lv5"] = 1;
            else
                actor.TInt["幽怨魂灵Lv5"] = 0;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int HPvalue = skill.Variable["幽怨魂灵MaxHPUP"];
            actor.Status.hp_rate_skill -= (short)HPvalue;

            int ATKvalue = skill.Variable["幽怨魂灵ATKUP"];
            actor.Status.atk1_rate_item -= (short)ATKvalue;
            actor.Status.atk2_rate_item -= (short)ATKvalue;
            actor.Status.atk3_rate_item -= (short)ATKvalue;
            actor.Status.matk_rate_item -= (short)ATKvalue;

            actor.TInt["幽怨魂灵Lv5"] = 0;
        }

        #endregion
    }
}
