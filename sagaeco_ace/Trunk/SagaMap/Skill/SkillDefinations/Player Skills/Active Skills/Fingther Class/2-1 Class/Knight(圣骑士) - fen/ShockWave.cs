using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 聖騎士的憤怒（衝撃波）
    /// </summary>
    public class ShockWave : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.25f + 0.25f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
