
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    /// <summary>
    /// 投擲泥土（スローダート）
    /// </summary>
    public class ThrowDirt : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 20 + 5 * level;
            if(SkillHandler.Instance.CanAdditionApply(sActor,dActor,"ThrowDirt",rate))
            {
                int lifetime = 35000 - 5000 * level;
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ThrowDirt", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //近命中
            int hit_melee_add = (int)(15 + 5 * level);
            if (skill.Variable.ContainsKey("ThrowDirt_hit_melee"))
                skill.Variable.Remove("ThrowDirt_hit_melee");
            skill.Variable.Add("ThrowDirt_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;
           
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["ThrowDirt_hit_melee"];
        }
        #endregion
    }
}
