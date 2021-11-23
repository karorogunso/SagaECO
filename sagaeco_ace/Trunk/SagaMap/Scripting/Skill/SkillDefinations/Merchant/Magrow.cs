
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Merchant
{
    /// <summary>
    /// 高聲放歌（ファシーボイス）
    /// </summary>
    public class Magrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 70000 - 10000 * level;
            int rate = 10 + 10 * level;
            if(SkillHandler.Instance.CanAdditionApply(sActor,dActor,"Magrow",rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Magrow", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //最小魔攻
            int min_matk_add = (int)(actor.Status.min_matk * (0.1f + 0.1f * level));
            if (skill.Variable.ContainsKey("AtkUpOne_min_matk"))
                skill.Variable.Remove("AtkUpOne_min_matk");
            skill.Variable.Add("AtkUpOne_min_matk", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["AtkUpOne_min_matk"];      
        }
        #endregion
    }
}
