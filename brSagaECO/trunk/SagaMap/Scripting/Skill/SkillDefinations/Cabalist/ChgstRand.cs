
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 森羅萬象（アブレイション）
    /// </summary>
    public class ChgstRand : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.1f + 0.1f * level;
            int rate = 2 * level;
            int lifetime = 4500 + 1000 * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Dark, factor);
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill1);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rate))
            {
                Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill2);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Silence, rate))
            {
                Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill3);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.CannotMove, rate))
            {
                Additions.Global.CannotMove skill4 = new SagaMap.Skill.Additions.Global.CannotMove(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill4);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Confuse, rate))
            {
                Additions.Global.Confuse skill5 = new SagaMap.Skill.Additions.Global.Confuse(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill5);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Frosen, rate))
            {
                Additions.Global.Freeze skill6 = new SagaMap.Skill.Additions.Global.Freeze(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill6);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Additions.Global.Poison skill7 = new SagaMap.Skill.Additions.Global.Poison(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill7);
            }
        }
        #endregion
    }
}