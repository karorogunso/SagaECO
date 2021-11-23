using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S13000 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "纯白信仰", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            ((ActorPC)actor).TInt["纯白信仰增益"] = 5 + 15 * skill.skill.Level;
            ((ActorPC)actor).TInt["纯白信仰增益2"] = 5 + 15 * skill.skill.Level;
            if(skill.skill.Level == 4)
            {
                ((ActorPC)actor).TInt["纯白信仰增益"] = 50;
                ((ActorPC)actor).TInt["纯白信仰增益2"] = 80;
            }
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            ((ActorPC)actor).TInt["纯白信仰增益"] = 0;
            ((ActorPC)actor).TInt["纯白信仰增益2"] = 0;
        }

        #endregion
    }
}
