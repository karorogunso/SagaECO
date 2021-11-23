
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
    /// 尖刺陷阱（バンジステーク）
    /// </summary>
    public class Bungestac: Trap
    {
        public Bungestac()
            :base(false,100, PosType.sActor)
        {
            
        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 22000 - 2000 * level;
        }
        public override void ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            factor = new float[] { 0, 0.9f, 1.15f, 1.2f, 1.35f, 1.55f }[level];
            SkillHandler.Instance.PhysicalAttack(sActor, mActor, args, sActor.WeaponElement, factor);
            if (!mActor.Status.Additions.ContainsKey("CannotMove"))
            {
                int rate = 30 + 10 * level;
                if (SkillHandler.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.CannotMove, rate))
                {
                    int lt = 11000 - 1000 * level;
                    CannotMove sk = new CannotMove(args.skill, mActor, lt);
                    SkillHandler.ApplyAddition(mActor, sk);
                }
            }

        }            
    }
}
