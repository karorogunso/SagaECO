using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 空中迴旋腿
    /// </summary>
    public class SummerSaltKick : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = 0.75f + 0.25f * level;
            ActorPC actorPC = (ActorPC)sActor;
            SkillHandler.Instance.PushBack(sActor, dActor, 2+level);
            Additions.Global.硬直 skill = new SagaMap.Skill.Additions.Global.硬直(args.skill, dActor, 500);
            SkillHandler.ApplyAddition(dActor, skill);
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            SkillHandler.Instance.JumpBack(sActor, 1, 500, SagaLib.MoveType.RUN);
        }
        #endregion
    }
}
