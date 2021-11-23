using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class LandKlug:ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 600, true);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Earth, 2f);
        }

        #endregion
    }
}
