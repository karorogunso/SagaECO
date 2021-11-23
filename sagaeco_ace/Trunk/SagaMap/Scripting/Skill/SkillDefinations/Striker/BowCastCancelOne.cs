
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    public class BowCastCancelOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 15000;
            BowCastCancelOneBuff skill = new BowCastCancelOneBuff(args.skill, dActor, lifetime);
            skill.CancelRate = level / 10f;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion

        #region BowCastCancelOneBuff
        public class BowCastCancelOneBuff : DefaultBuff
        {
            /// <summary>
            /// 短縮時間(%)
            /// </summary>
            public float CancelRate;

            public BowCastCancelOneBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
                : base(skill, actor, "BowCastCancelOne", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
            }
        }
        #endregion
    }
}
