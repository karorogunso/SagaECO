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
    public class S11122 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3f + level * 1f;
            
            if (dActor.Tasks.ContainsKey("SkillCast") )
            {
                SkillHandler.Instance.CancelSkillCast(dActor,sActor);
                factor *= 3;
                if (!dActor.Status.Additions.ContainsKey("迅龙击晕眩CD"))
                {
                    OtherAddition skill = new OtherAddition(null, dActor, "迅龙击晕眩CD", 60000);
                    SkillHandler.ApplyAddition(dActor, skill);

                    if (!dActor.Status.Additions.ContainsKey("Stun"))
                    {
                        Stun stun = new Stun(null, dActor, 4000 + level * 1000);
                        SkillHandler.ApplyAddition(dActor, stun);
                    }
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
        }
    }
}
