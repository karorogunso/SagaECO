
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Explorer
{
    /// <summary>
    /// 刺棘陷阱（バーベッドトラップ）
    /// </summary>
    public class BarbedTrap : Trap
    {
        public BarbedTrap()
            :base(false,100, PosType.sActor)
        {
            
        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 22000 - 2000 * level;
        }
        public override void ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            factor *= 0.65f + 0.25f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, mActor, args, SagaLib.Elements.Neutral, factor);
            if (!mActor.Status.Additions.ContainsKey("鈍足"))
            {
                int rate = 30 + 10 * level;
                if (SkillHandler.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.鈍足, rate))
                {
                    int lt = 11000 - 1000 * level;
                    鈍足 sk = new 鈍足(args.skill, mActor, lt);
                    SkillHandler.ApplyAddition(mActor, sk);
                }
            }

        }            
    }
}