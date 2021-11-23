using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31005 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5101);
            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 500);
            actors.Add(sActor);
            foreach (var item in actors)
            {
                int damage = (int)(item.MaxHP * 2f);
                SkillHandler.Instance.CauseDamage(sActor, item, damage);
                SkillHandler.Instance.ShowVessel(item, damage);
                SkillHandler.Instance.ShowEffectOnActor(item, 5075);
            }
        }
    }
}
