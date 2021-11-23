using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 特攻武術修練（体術マスタリー）
    /// </summary>
    public class MartialArtDamUp : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MartialArtDamUp", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_bs * 0.1f * level);
            if (skill.Variable.ContainsKey("MartialArtDamUp_min_atk1"))
                skill.Variable.Remove("MartialArtDamUp_min_atk1");
            skill.Variable.Add("MartialArtDamUp_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill = (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_bs * 0.1f * level);
            if (skill.Variable.ContainsKey("MartialArtDamUp_min_atk2"))
                skill.Variable.Remove("MartialArtDamUp_min_atk2");
            skill.Variable.Add("MartialArtDamUp_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill = (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_bs * 0.1f * level);
            if (skill.Variable.ContainsKey("MartialArtDamUp_min_atk3"))
                skill.Variable.Remove("MartialArtDamUp_min_atk3");
            skill.Variable.Add("MartialArtDamUp_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill = (short)min_atk3_add;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["MartialArtDamUp_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["MartialArtDamUp_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["MartialArtDamUp_min_atk3"];
        }
        #endregion
    }
}
