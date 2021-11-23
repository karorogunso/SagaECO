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
    public class S31051 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000);

            foreach (var item in targets)
            {
                SkillHandler.Instance.CauseDamage(sActor, item, 233333333);
                SkillHandler.Instance.ShowVessel(item, 233333333);
                SkillHandler.Instance.ShowEffectOnActor(item, 5004, sActor);
            }
        }
    }
}
