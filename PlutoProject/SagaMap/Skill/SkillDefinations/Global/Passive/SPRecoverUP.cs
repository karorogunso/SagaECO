using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class SPRecoverUP:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "SPRecoverUP", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value;            
            value = skill.skill.Level;           
            if (skill.Variable.ContainsKey("SPRecover"))
                skill.Variable.Remove("SPRecover");
            skill.Variable.Add("SPRecover", value);
            actor.Status.sp_recover_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value = skill.Variable["SPRecover"];
            actor.Status.sp_recover_skill -= (short)value;
        }

        #endregion
    }
}
