
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 提升飽食度（フードファイター）
    /// </summary>
    public class FoodFighter : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "FoodFighter", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            float factor = 0.1f + 0.05f * skill.skill.Level;
            if (skill.Variable.ContainsKey("FoodFighter"))
                skill.Variable.Remove("FoodFighter");
            skill.Variable.Add("FoodFighter", (int)(factor * 100));
        }
        public void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (skill.Variable.ContainsKey("FoodFighter"))
                skill.Variable.Remove("FoodFighter");
        }
        #endregion
    }
}

