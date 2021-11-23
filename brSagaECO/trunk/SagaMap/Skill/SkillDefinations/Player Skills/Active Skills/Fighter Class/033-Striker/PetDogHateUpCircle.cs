
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 野獸咆哮（エンミティーロア）[接續技能]
    /// </summary>
    public class PetDogHateUpCircle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, false);
            
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    int hate = (int)(act.HP * 0.15f * level);
                    SkillHandler.Instance.AttractMob(sActor, act, hate);
                }
            }
           
        }
        #endregion
    }
}