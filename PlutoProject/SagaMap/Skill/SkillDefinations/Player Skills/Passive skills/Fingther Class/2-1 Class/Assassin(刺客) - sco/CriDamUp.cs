using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 增強致命攻擊（クリティカルダメージ上昇）
    /// </summary>
    public class CriDamUp : ISkill 
    {
         #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "CriDamUp", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value = (10 + 2 * skill.skill.Level);
            if (skill.Variable.ContainsKey("CriDamUp"))
                skill.Variable.Remove("CriDamUp");
            skill.Variable.Add("CriDamUp", value);
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (skill.Variable.ContainsKey("CriDamUp"))
                skill.Variable.Remove("CriDamUp");
        }
         #endregion
    }
}
