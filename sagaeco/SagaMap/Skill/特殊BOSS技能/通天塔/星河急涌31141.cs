using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31141 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 3000,false);
            foreach (var item in actors)
            {
                if(item.type == ActorType.SKILL)
                {
                    if (item.Name == "星原之握")
                        ((ActorSkill)item).TInt["星河急涌"] = 1;
                    SkillHandler.Instance.ShowEffectByActor(item, 5293);
                }
            }
        }
    }
}
