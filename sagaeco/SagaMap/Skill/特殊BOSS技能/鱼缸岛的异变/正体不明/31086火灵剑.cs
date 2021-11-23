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
    public class S31086 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    if(item != dActor)
                    {
                        SkillHandler.Instance.CauseDamage(dActor, item, (int)(item.MaxHP * 0.9f));
                        SkillHandler.Instance.ShowVessel(item, (int)(item.MaxHP * 0.9f));
                        SkillHandler.Instance.ShowEffectOnActor(item, 5423);
                        Burning burn = new Burning(args.skill, item, 6000, 90);
                        SkillHandler.ApplyAddition(item, burn);
                        Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("你被【火灵剑】的余炎击中了，请远离仇恨目标来躲避。");
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Fire, 3f);
        }

        #endregion
    }
}
