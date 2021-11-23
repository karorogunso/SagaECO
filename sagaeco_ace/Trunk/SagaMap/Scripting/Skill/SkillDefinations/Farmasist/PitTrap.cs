
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.ActorEventHandlers;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Skill.SkillDefinations.Global;
namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    /// <summary>
    /// 地坑陷阱（ピットトラップ）
    /// </summary>
    public class PitTrap : Trap
    {
       public PitTrap()
            :base(true ,100, PosType.sActor)
        {
            
        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 22000 - 2000 * level;
        }
        public override void ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            int rate = 30 + 10 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.鈍足, rate))
            {
                int[] lf = { 0, 6000, 5500, 5000, 4500, 4000 };
                鈍足 sk = new 鈍足(args.skill, mActor, lf[level]);
                SkillHandler.ApplyAddition(mActor, sk);
            }
        }            
    }
}