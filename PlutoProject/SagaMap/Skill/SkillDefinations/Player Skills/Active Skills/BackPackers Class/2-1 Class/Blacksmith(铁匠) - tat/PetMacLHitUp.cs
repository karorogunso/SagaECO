
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 平靜射擊（評定射撃）
    /// </summary>
    public class PetMacLHitUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 35000 - 5000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PetMacLHitUp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //遠命中
            int hit_ranged_add = (int)(actor.Status.hit_ranged * (0.1f + 0.1f * level));
            if (skill.Variable.ContainsKey("PetMacLHitUp_hit_ranged"))
                skill.Variable.Remove("PetMacLHitUp_hit_ranged");
            skill.Variable.Add("PetMacLHitUp_hit_ranged", hit_ranged_add);
            actor.Status.hit_ranged_skill += (short)hit_ranged_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //遠命中
            actor.Status.hit_ranged_skill -= (short)skill.Variable["PetMacLHitUp_hit_ranged"];
        }
        #endregion
    }
}
