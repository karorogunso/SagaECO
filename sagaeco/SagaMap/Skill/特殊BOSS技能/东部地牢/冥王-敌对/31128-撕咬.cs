using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31128 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int damage = (int)(dActor.MaxHP * 0.4f);
            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
            SkillHandler.Instance.ShowVessel(dActor, damage);

            sActor.HP += (uint)damage;
            SkillHandler.Instance.ShowVessel(sActor, -damage);
        }
    }
}
