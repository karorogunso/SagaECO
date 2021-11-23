
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 堅固肉體（ソリッドボディ）
    /// </summary>
    public class SolidBody : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 5000 + 2000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "SolidBody", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            //加上暈眩抗性
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(3057, 5, 0));
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //左防禦
            int def_add = (int)(3 * level);
            if (skill.Variable.ContainsKey("SolidBody_def"))
                skill.Variable.Remove("SolidBody_def");
            skill.Variable.Add("SolidBody_def", def_add);
            actor.Status.def_skill += (short)def_add;
  
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["SolidBody_def"];
            }
        #endregion
    }
}
