
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// 庭師の手腕
    /// </summary>
    public class GardenerSkill : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 10f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type== ActorType.MOB)
                {
                    ActorMob mob = (ActorMob)act;
                    if (SkillHandler.Instance.CheckMobType(mob, "plant"))
                    {
                        realAffected.Add(act);
                    }
                }
            }
            SkillHandler.Instance.FixAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}