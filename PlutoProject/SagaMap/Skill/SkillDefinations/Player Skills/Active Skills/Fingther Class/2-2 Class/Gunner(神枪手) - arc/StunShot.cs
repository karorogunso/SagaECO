using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 昏厥射擊（スタンショット）
    /// </summary>
    public class StunShot : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcGunAndBullet(sActor);
        }
        
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcBulletDown(sActor);
            float factor = 1.5f;
            int rate = 15 + 5 * level;
            int lifetime = 1000 + 1000 * level;
            args.argType = SkillArg.ArgType.Attack;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill1);
            }
        }
        #endregion
    }
}
