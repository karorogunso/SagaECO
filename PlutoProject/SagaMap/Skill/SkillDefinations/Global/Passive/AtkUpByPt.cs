
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 團結力量（コンバインフォース）
    /// </summary>
    public class AtkUpByPt : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            ActorPC sActorPC = (ActorPC)sActor;
            if (sActorPC.Party != null)
            {
                active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "AtkUpByPt", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            float atk_add = new float[] { 0, 0.01f, 0.03f, 0.06f }[level];
            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_bs * atk_add);
            if (skill.Variable.ContainsKey("AtkUpByPt_max_atk1"))
                skill.Variable.Remove("AtkUpByPt_max_atk1");
            skill.Variable.Add("AtkUpByPt_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_bs * atk_add);
            if (skill.Variable.ContainsKey("AtkUpByPt_max_atk2"))
                skill.Variable.Remove("AtkUpByPt_max_atk2");
            skill.Variable.Add("AtkUpByPt_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_bs * atk_add);
            if (skill.Variable.ContainsKey("AtkUpByPt_max_atk3"))
                skill.Variable.Remove("AtkUpByPt_max_atk3");
            skill.Variable.Add("AtkUpByPt_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_bs * atk_add);
            if (skill.Variable.ContainsKey("AtkUpByPt_min_atk1"))
                skill.Variable.Remove("AtkUpByPt_min_atk1");
            skill.Variable.Add("AtkUpByPt_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_bs * atk_add);
            if (skill.Variable.ContainsKey("AtkUpByPt_min_atk2"))
                skill.Variable.Remove("AtkUpByPt_min_atk2");
            skill.Variable.Add("AtkUpByPt_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_bs * atk_add);
            if (skill.Variable.ContainsKey("AtkUpByPt_min_atk3"))
                skill.Variable.Remove("AtkUpByPt_min_atk3");
            skill.Variable.Add("AtkUpByPt_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

        }
        public void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["AtkUpByPt_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["AtkUpByPt_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["AtkUpByPt_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkUpByPt_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkUpByPt_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkUpByPt_min_atk3"];

        }
        #endregion
    }
}

