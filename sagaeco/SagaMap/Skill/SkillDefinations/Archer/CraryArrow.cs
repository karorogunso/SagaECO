using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    public class CrazyArrow : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc,args))
                    return -5;
                else
                    return 0;
            
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.7f+0.3f*level;
            args.argType = SkillArg.ArgType.Attack;
            args.delayRate = 5f;

            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> target = map.GetActorsArea(dActor, 200, true);
            
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
