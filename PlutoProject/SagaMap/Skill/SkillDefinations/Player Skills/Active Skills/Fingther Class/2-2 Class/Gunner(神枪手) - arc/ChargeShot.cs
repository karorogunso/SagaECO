using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 烈炎射擊（チャージショット）
    /// </summary>
    public class ChargeShot : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcGunAndBullet(sActor);
        }
        
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcBulletDown(sActor);
            float factor = 1.1f + 0.1f * level;
            args.argType = SkillArg.ArgType.Attack;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            SkillHandler.Instance.PushBack(sActor, dActor, 3);
            Additions.Global.Stiff skill1 = new SagaMap.Skill.Additions.Global.Stiff(args.skill, dActor, 3000);
            SkillHandler.ApplyAddition(dActor, skill1);
        }
        #endregion
    }
}
