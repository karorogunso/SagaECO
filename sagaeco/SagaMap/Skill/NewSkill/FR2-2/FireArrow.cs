using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.FR2_2
{
    /// <summary>
    /// 火燄箭
    /// </summary>
    public class FireArrow: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            else
                return -5;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillArg args2 = args.Clone();
            float factor = 0;
            factor = 1.0f + 0.4f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Fire, factor);
            //SkillHandler.Instance.MagicAttack(sActor, dActor, args2, SagaLib.Elements.Fire, factor);
            args.AddSameActor(args2);
            Firing1 fire = new Firing1(args.skill, sActor, dActor, 10000, SkillHandler.Instance.CalcDamage(true, sActor, dActor, args, SkillHandler.DefType.MDef, SagaLib.Elements.Fire, 50, 0.2f));
            SkillHandler.ApplyAddition(dActor, fire);
        }
        #endregion
    }
}
