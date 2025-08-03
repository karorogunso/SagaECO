
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    /// <summary>
    /// 激励（激励）
    /// </summary>
    public class Encouragement : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Encouragement", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            int max_atk1_add = 12;
            if (skill.Variable.ContainsKey("Encouragement_max_atk1"))
                skill.Variable.Remove("Encouragement_max_atk1");
            skill.Variable.Add("Encouragement_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = 12;
            if (skill.Variable.ContainsKey("Encouragement_max_atk2"))
                skill.Variable.Remove("Encouragement_max_atk2");
            skill.Variable.Add("Encouragement_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = 12;
            if (skill.Variable.ContainsKey("Encouragement_max_atk3"))
                skill.Variable.Remove("Encouragement_max_atk3");
            skill.Variable.Add("Encouragement_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = 6;
            if (skill.Variable.ContainsKey("Encouragement_min_atk1"))
                skill.Variable.Remove("Encouragement_min_atk1");
            skill.Variable.Add("Encouragement_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = 6;
            if (skill.Variable.ContainsKey("Encouragement_min_atk2"))
                skill.Variable.Remove("Encouragement_min_atk2");
            skill.Variable.Add("Encouragement_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = 6;
            if (skill.Variable.ContainsKey("Encouragement_min_atk3"))
                skill.Variable.Remove("Encouragement_min_atk3");
            skill.Variable.Add("Encouragement_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["Encouragement_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["Encouragement_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["Encouragement_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["Encouragement_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["Encouragement_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["Encouragement_min_atk3"];

        }
        #endregion
    }
}
