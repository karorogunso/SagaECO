using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 召喚活動木偶皇帝 [接續技能]
    /// </summary>
    public class SumMarioCont : ISkill
    {
        private Elements element;
        public SumMarioCont(Elements e)
        {
            element = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.7f + 3f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(dActor, 200, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args,element, factor);
            ActorMob mob = (ActorMob)sActor.Slave[0];
            map.DeleteActor(mob);
            sActor.Slave.RemoveAt(0);
        }
        #endregion
    }
}



                                                          