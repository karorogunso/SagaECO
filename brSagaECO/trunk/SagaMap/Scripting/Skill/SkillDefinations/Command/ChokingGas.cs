
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 窒息毒氣（チョーキングガス）
    /// </summary>
    public class ChokingGas : Trap
    {
         public ChokingGas()
            :base(true ,200, PosType.sActor)
        {
            
        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 30000;
        }
        public override void ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            int rate = 35 + 10 * level;
            if (!mActor.Status.Additions.ContainsKey("Silence"))
            {
                if (SkillHandler.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.Silence, rate))
                {
                    Silence sk = new Silence(args.skill, mActor, 10000);
                    SkillHandler.ApplyAddition(mActor, sk);
                }
            }

        }
    }
}