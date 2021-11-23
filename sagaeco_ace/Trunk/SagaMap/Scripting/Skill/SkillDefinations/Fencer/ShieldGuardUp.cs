
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Fencer
{
    /// <summary>
    /// 盾術修練（シールドマスタリー）
    /// </summary>
    public class ShieldGuardUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ShieldGuardUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            int rate = 1 * level;
            if (skill.Variable.ContainsKey("ShieldGuardUp"))
                skill.Variable.Remove("ShieldGuardUp");
            skill.Variable.Add("ShieldGuardUp", rate);
            actor.Status.def_skill += (short)rate;
        }
        public void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value2 = skill.Variable["ShieldGuardUp"];
            actor.Status.def_skill -= (short)value2;

        }
        #endregion
    }
}

                                                          