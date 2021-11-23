using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40102 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            List<Actor> actors = map.GetActorsArea(sActor, 300, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);
                    硬直 yz = new 硬直(args.skill,item, 1000);
                    SkillHandler.ApplyAddition(item, yz);
                }
            }
            SkillHandler.Instance.ShowEffectByActor(sActor, 5259);
            SkillHandler.Instance.PhysicalAttack(sActor, targets, args, Elements.Earth, 4f);

            #endregion
        }
    }
}