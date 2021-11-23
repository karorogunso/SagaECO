using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 沖天踢（タックル）
    /// </summary>
    public class Tackle:ISkill 
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
            float[] factors = { 0f, 1.1f ,1.4f, 1.7f, 2.0f ,2.4f};
            float factor = factors[level];
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            SkillHandler.Instance.PushBack(sActor, dActor, 1);

            Skill.Additions.Global.Stun stun = new Additions.Global.Stun(args.skill, dActor, 1000 * level);
            SkillHandler.ApplyAddition(dActor, stun);
        }       
        #endregion
    }
}
