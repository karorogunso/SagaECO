using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 魅影步法（フットワーク）
    /// </summary>
    public class AvoidUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "AvoidUp", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            //近戰迴避
            float avoid_melee_adds = new float[] { 0f, 0.05f, 0.07f, 0.11f }[level];
            int avoid_melee_add = (int)((float)actor.Status.avoid_melee * avoid_melee_adds);
            if (skill.Variable.ContainsKey("AvoidUp_avoid_melee"))
                skill.Variable.Remove("AvoidUp_avoid_melee");
            skill.Variable.Add("AvoidUp_avoid_melee", avoid_melee_add);
            actor.Status.avoid_melee_skill += (short)avoid_melee_add;

            //遠距迴避
            float avoid_ranged_adds = new float[] { 0f, 0.06f, 0.08f, 0.10f }[level];
            int avoid_ranged_add = (int)((float)actor.Status.avoid_ranged * avoid_ranged_adds);
            if (skill.Variable.ContainsKey("AvoidUp_avoid_ranged"))
                skill.Variable.Remove("AvoidUp_avoid_ranged");
            skill.Variable.Add("AvoidUp_avoid_ranged", avoid_ranged_add);
            actor.Status.avoid_ranged_skill += (short)avoid_ranged_add;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //近戰迴避
            actor.Status.avoid_melee_skill -= (short)skill.Variable["AvoidUp_avoid_melee"];
            //遠距迴避
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["AvoidUp_avoid_ranged"];
        }
        #endregion
    }
}
