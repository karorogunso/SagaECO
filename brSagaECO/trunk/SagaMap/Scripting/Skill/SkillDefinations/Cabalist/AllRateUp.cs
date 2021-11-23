
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 提升異常狀態成功率（状態異常成功率上昇）
    /// </summary>
    public class AllRateUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            float[] rate = { 0f, 1.4f, 1.6f, 1.8f, 1.9f, 1.95f };
            AllRateUpBuff skill = new AllRateUpBuff(args.skill, sActor, rate[level]);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class AllRateUpBuff : DefaultPassiveSkill 
        {
            public float Rate = 0f;
            public AllRateUpBuff(SagaDB.Skill.Skill skill, Actor actor,float rate)
                : base(skill, actor, "AllRateUp", true)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.Rate = rate;
            }

            void StartEvent(Actor actor, DefaultPassiveSkill skill)
            {
            }

            void EndEvent(Actor actor, DefaultPassiveSkill skill)
            {
            }
        }
        #endregion
    }
}

