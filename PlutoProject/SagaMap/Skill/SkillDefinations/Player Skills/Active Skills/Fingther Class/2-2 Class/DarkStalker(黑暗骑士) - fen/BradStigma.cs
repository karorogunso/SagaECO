
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// </summary>
    /// 血的烙印（血の烙印）
    public class BradStigma : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            int lifetime = 25000;
            SkillHandler.Instance.AttractMob(sActor, dActor);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "BradStigma", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);


            //闇屬性傷害提升 16% 20% 24% 28% 32% 
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {


            int factor = new int[] { 0, 16, 20, 24, 28, 32 }[skill.skill.Level];
            if (skill.Variable.ContainsKey("BradStigma"))
                skill.Variable.Remove("BradStigma");
            skill.Variable.Add("BradStigma", factor);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("BradStigma"))
            {
                skill.Variable.Remove("BradStigma");
            }
        }
        #endregion
    }
}
