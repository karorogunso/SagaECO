using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 解除霍汀克導彈!
    /// </summary>
    public class MobSalvoFire : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.58f;
            short range = 300;
            int times = 8;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, range, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    for (int i = 0; i < times; i++)
                    {
                        realAffected.Add(act);
                    }
                }
            }
            if (realAffected.Count > 0)
            {
                List<Actor> finalAffected = new List<Actor>();
                for (int i = 0; i < realAffected.Count; i++)
                {
                    finalAffected.Add(realAffected[SagaLib.Global.Random.Next(0, realAffected.Count - 1)]);
                }
                SkillHandler.Instance.PhysicalAttack(sActor, finalAffected, args, SagaLib.Elements.Neutral, factor);
            }
        }
        #endregion
    }
}