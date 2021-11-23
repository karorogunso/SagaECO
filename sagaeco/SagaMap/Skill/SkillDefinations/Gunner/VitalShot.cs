using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 重點射擊（バイタルショット）
    /// </summary>
    public class VitalShot:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            SkillHandler.Instance.Homicidal(sActor, 1);
            HPDown hp = new HPDown(args.skill, sActor, dActor, 10000, SkillHandler.Instance.CalcDamage(false, sActor, dActor, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, 0.2f));
            SkillHandler.ApplyAddition(sActor, hp);
        }
        #endregion
    }
}
