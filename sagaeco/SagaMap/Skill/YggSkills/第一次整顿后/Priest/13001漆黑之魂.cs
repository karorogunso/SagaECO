using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S13001 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "漆黑之魂", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            ((ActorPC)actor).TInt["漆黑之魂增益"] = 25 + 25 * skill.skill.Level;
            if(skill.skill.Level == 4)
                ((ActorPC)actor).TInt["漆黑之魂增益"] = 125;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            ((ActorPC)actor).TInt["漆黑之魂增益"] = 0;
        }

        #endregion
    }
}
