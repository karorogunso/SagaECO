using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    /// <summary>
    /// エンジェルリング
    /// </summary>
    public class AngelRing : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = new float[] { 0.00f, 4.30f, 5.00f, 6.20f, 7.50f, 8.00f, 10.00f };
            float factor = factors[level];
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> affected = new List<Actor>();
            args.affectedActors.Clear();
            foreach (var i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, Elements.Holy, factor);
        }
        #endregion
    }
}
