using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30018 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ActorSpeak(sActor, "归来吧，脆弱的灵魂啊——");
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 2000, false);

            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    if (item.HP < (item.MaxHP / 2))
                    {
                        if (item.type == ActorType.PC)
                            Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("由于你的血量低于50%，被收割者收割了。");
                        SkillHandler.Instance.CauseDamage(sActor, item, 99999);
                        SkillHandler.Instance.ShowVessel(item, 99999);
                        SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(item.MapID), item, 5396);
                    }
                }
            }
        }
    }
}
