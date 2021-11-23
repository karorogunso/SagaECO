using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31061 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int damage = (int)(dActor.MaxHP * 0.7f);
            //dActor.EP = 0;
            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
            SkillHandler.Instance.ShowVessel(dActor, damage);
            if (sActor.type == ActorType.MOB)
                SkillHandler.Instance.ActorSpeak(sActor, "前期投入是必要的。因为想要零件！");
            零件回收 skill = new 零件回收(null, dActor, 10000);
            SkillHandler.ApplyAddition(dActor, skill);
            if (sActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)sActor;
                mob.TInt["零件数"] += 6;
                SkillHandler.Instance.ShowVessel(mob, 0, -mob.TInt["零件数"], 0);
            }
        }
        #endregion
    }
}
