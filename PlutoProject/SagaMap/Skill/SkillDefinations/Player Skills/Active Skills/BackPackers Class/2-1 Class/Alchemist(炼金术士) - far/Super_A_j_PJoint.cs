using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    ///  强力援手（クリティカルアシスト）
    /// </summary>
    public class Super_A_T_PJoint : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.PossessionTarget != 0)
            {
                if (!dActor.Status.Additions.ContainsKey("Super_A_T_PJoint"))
                {
                    return 0;
                }
                else
                {
                    return -24;
                }
            }
            else
            {
                return -23;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 9999999;//应该是无限长时间
            Actor dActorReal = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            SPJointBuff skill = new SPJointBuff(args.skill, sActor, dActorReal, life);
            SkillHandler.ApplyAddition(dActorReal, skill);
        }
        public class SPJointBuff : DefaultBuff
        {
            Actor sActor;
            public SPJointBuff(SagaDB.Skill.Skill skill, Actor sActor, Actor actor, int lifetime)
                : base(skill, actor, "Super_A_T_PJoint", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.sActor = sActor;
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
