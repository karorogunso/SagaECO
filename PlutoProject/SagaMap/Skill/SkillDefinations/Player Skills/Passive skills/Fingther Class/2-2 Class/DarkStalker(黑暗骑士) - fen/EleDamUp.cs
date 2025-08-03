using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 精靈傷害增加
    /// </summary>
    public class EleDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ruthless", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                pc.TInt["ruthless"] = skill.skill.Level;
            }
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                pc.TInt["ruthless"] = 0;
            }
        }
        #endregion
    }
}