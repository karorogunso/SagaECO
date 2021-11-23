
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// 鬼人の構え
    /// </summary>
    public class DevilStance : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] totals = new int[] { 0, 45, 60, 75, 90, 120 };
            int lifetime = 1000 * (totals[level]);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DevilStance", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int Def = 0;
            if (actor.Status.def_add > 300)
            {
                Def = actor.Status.def_add - 300;
            }
            else
            {
                Def = actor.Status.def_add;
            }
            //最大攻擊
            int max_atk1_add = (int)(Def / 2);
            if (skill.Variable.ContainsKey("DevilStance_max_atk1"))
                skill.Variable.Remove("DevilStance_max_atk1");
            skill.Variable.Add("DevilStance_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(Def / 2);
            if (skill.Variable.ContainsKey("DevilStance_max_atk2"))
                skill.Variable.Remove("DevilStance_max_atk2");
            skill.Variable.Add("DevilStance_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(Def / 2);
            if (skill.Variable.ContainsKey("DevilStance_max_atk3"))
                skill.Variable.Remove("DevilStance_max_atk3");
            skill.Variable.Add("DevilStance_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(Def / 2);
            if (skill.Variable.ContainsKey("DevilStance_min_atk1"))
                skill.Variable.Remove("DevilStance_min_atk1");
            skill.Variable.Add("DevilStance_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(Def / 2);
            if (skill.Variable.ContainsKey("DevilStance_min_atk2"))
                skill.Variable.Remove("DevilStance_min_atk2");
            skill.Variable.Add("DevilStance_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(Def / 2);
            if (skill.Variable.ContainsKey("DevilStance_min_atk3"))
                skill.Variable.Remove("DevilStance_min_atk3");
            skill.Variable.Add("DevilStance_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //右防禦
            int def_add_add = (int)(-Def);
            if (skill.Variable.ContainsKey("DevilStance_def_add"))
                skill.Variable.Remove("DevilStance_def_add");
            skill.Variable.Add("DevilStance_def_add", def_add_add);
            actor.Status.def_add_skill += (short)def_add_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["DevilStance_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["DevilStance_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["DevilStance_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["DevilStance_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["DevilStance_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["DevilStance_min_atk3"];

            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["DevilStance_def_add"];

        }
        #endregion
    }
}
