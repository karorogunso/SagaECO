
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 斷腕（アームブレイク）
    /// </summary>
    public class ArmBreak : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcBowAndArrow(sActor);
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowDown(sActor);
            int lifetime = 2000 + 2000 * level;
            int rate = 50 + 6 * level;
            float factor = 0.75f + 0.25f * level;
            //對首腦級魔物無效
            if (dActor.type == ActorType.MOB)
            {
                ActorMob dActorMob = (ActorMob)dActor;
                if (SkillHandler.Instance.isBossMob(dActorMob))
                {
                    return;
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "ArmBreak", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ArmBreak", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //物理技能和攻擊無法使用
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}
