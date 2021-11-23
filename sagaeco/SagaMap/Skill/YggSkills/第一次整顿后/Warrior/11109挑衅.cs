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
    public class S11109 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("挑衅CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            OtherAddition 挑衅CD = new OtherAddition(null, sActor, "挑衅CD", 30000);
            SkillHandler.ApplyAddition(sActor, 挑衅CD);

            int lifetime = 3000 + 3000 * level;

            List<Actor> actors = map.GetActorsArea(sActor, 700, false);
            foreach (var item in actors)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item) && !item.Status.Additions.ContainsKey("被挑衅状态") && item.type == ActorType.MOB)
                {
                    ActorMob mob = (ActorMob)item;
                    ActorEventHandlers.MobEventHandler ef = (ActorEventHandlers.MobEventHandler)mob.e;
                    if (ef.AI.Hate.Count == 0 && !ef.AI.Mode.Active) continue;
                    SkillHandler.Instance.ShowEffectOnActor(mob, 4539);
                    OtherAddition 被挑衅状态 = new OtherAddition(null, mob, "被挑衅状态", lifetime);
                    被挑衅状态.OnAdditionStart += (s, e) =>
                    {
                        mob.PriorityTartget = sActor;
                    };
                    被挑衅状态.OnAdditionEnd += (s, e) =>
                    {
                        mob.PriorityTartget = null;
                    };
                    SkillHandler.ApplyAddition(mob, 被挑衅状态);
                }
            }
        }
    }
}
