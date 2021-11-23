using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21116 : ISkill
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

            //设定伤害，并判断目标是否满足加伤要求
            float factor = 3f + 1f * level;
            if(dActor.HP < dActor.MaxHP * (0.1f + 0.1f * level))
                factor += factor * (1f + 0.5f * level);

            //造成混合伤害
            SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor> { dActor }, args, SkillHandler.DefType.Def, Elements.Water, 0, factor, true);
        }
    }
}
