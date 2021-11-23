using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Traveler
{
    public class ThunderFall_Effect : ISkill
    {
       public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

       public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
       {
           /*
           float factor = 1;
           Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
           List<Actor> list = map.GetRoundAreaActors(args.x, args.y, 150);
           List<Actor> affected = new List<Actor>();
           foreach (Actor i in list)
           {
               if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                   affected.Add(i);
           }
           SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Wind, factor);
 */
       }
    }
}
