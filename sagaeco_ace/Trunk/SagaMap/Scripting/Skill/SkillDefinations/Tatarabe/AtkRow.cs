using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    /// <summary>
    /// 煙氣（ＤＯＧＥＺＡ）
    /// </summary>
    public class AtkRow :ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.PC)
            {
                return -12;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 70000 - 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AtkRow", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            float factor = 0.1f * level;
            int max_atk1_add = -(int)(actor.Status.max_atk_ori  * (factor));
            int min_atk1_add = -(int)(actor.Status.min_atk_ori  * (factor));
            int max_atk2_add = -(int)(actor.Status.max_atk_ori * (factor));
            int min_atk2_add = -(int)(actor.Status.min_atk_ori * (factor));
            int max_atk3_add = -(int)(actor.Status.max_atk_ori * (factor));
            int min_atk3_add = -(int)(actor.Status.min_atk_ori * (factor));
            //大傷
            if (skill.Variable.ContainsKey("AtkRow_max_atk1_add"))
                skill.Variable.Remove("AtkRow_max_atk1_add");
            skill.Variable.Add("AtkRow_max_atk1_add", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            if (skill.Variable.ContainsKey("AtkRow_max_atk2_add"))
                skill.Variable.Remove("AtkRow_max_atk2_add");
            skill.Variable.Add("AtkRow_max_atk2_add", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            if (skill.Variable.ContainsKey("AtkRow_max_atk3_add"))
                skill.Variable.Remove("AtkRow_max_atk3_add");
            skill.Variable.Add("AtkRow_max_atk3_add", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;
            //小傷
            if (skill.Variable.ContainsKey("AtkRow_min_atk1_add"))
                skill.Variable.Remove("AtkRow_min_atk1_add");
            skill.Variable.Add("AtkRow_min_atk1_add", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            if (skill.Variable.ContainsKey("AtkRow_min_atk2_add"))
                skill.Variable.Remove("AtkRow_min_atk2_add");
            skill.Variable.Add("AtkRow_min_atk2_add", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            if (skill.Variable.ContainsKey("AtkRow_min_atk3_add"))
                skill.Variable.Remove("AtkRow_min_atk3_add");
            skill.Variable.Add("AtkRow_min_atk3_add", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

           
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkRow_max_atk1_add"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkRow_max_atk2_add"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkRow_max_atk3_add"];
            //大傷
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkRow_min_atk1_add"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkRow_min_atk2_add"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkRow_min_atk3_add"];
        }
        #endregion
    }
}
