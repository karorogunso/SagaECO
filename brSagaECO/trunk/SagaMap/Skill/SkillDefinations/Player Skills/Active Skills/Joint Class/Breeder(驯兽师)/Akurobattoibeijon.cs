
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    /// <summary>
    /// アクロバットイベイジョン（アクロバットイベイジョン）
    /// </summary>
    public class Akurobattoibeijon : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int damage = 2500;
            ActorPet p = SkillHandler.Instance.GetPet(sActor);
            if (p != null)
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> affected = map.GetActorsArea(p, 250, false);
                foreach (Actor act in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(p, act))
                    {
                        SkillHandler.Instance.AttractMob(p, act, damage);
                    }
                }
            }
        }
        #endregion
    }
}