using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 近身戰鬥修練（CQB）
    /// </summary>
    public class CQB : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 6000 + 2000 * level;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, dActor, "CQB", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {

            int level = skill.skill.Level;

            //近命中
            int hit_melee_add = (int)(actor.Status.hit_melee + (2 + 2 * level));
            if (skill.Variable.ContainsKey("CQB_hit_melee"))
                skill.Variable.Remove("CQB_hit_melee");
            skill.Variable.Add("CQB_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill = (short)hit_melee_add;

            //近戰迴避
            int avoid_melee_add = (int)(actor.Status.avoid_melee + (2 + 3 * level));
            if (skill.Variable.ContainsKey("CQB_avoid_melee"))
                skill.Variable.Remove("CQB_avoid_melee");
            skill.Variable.Add("CQB_avoid_melee", avoid_melee_add);
            actor.Status.avoid_melee_skill = (short)avoid_melee_add;

        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["CQB_hit_melee"];

            //近戰迴避
            actor.Status.avoid_melee_skill -= (short)skill.Variable["CQB_avoid_melee"];

        }
        #endregion
    }
}
