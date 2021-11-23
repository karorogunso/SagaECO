using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21112 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //获取锁定目标
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            //造成混合伤害
            float factor = 3f + 1.2f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor> { dActor }, args, SkillHandler.DefType.Def, Elements.Dark, 0, factor, true);

            //附加中毒DEBUFF
            float PoisonFactor = 0.5f + 0.5f * level;
            int PoisonTime = level > 2 ? 10000 : 15000;
            int PoisonDamage = SkillHandler.Instance.CalcDamage(false, sActor, dActor, null, SkillHandler.DefType.MDef, Elements.Dark, 50, PoisonFactor);
            Poison1 skill = new Poison1(null, dActor, PoisonTime, PoisonDamage);
            SkillHandler.ApplyBuffAutoRenew(dActor, skill);
        }
    }
}
