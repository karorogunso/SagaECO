
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    /// <summary>
    /// 聖光審判術（ターンアンデッド）
    /// </summary>
    public class TurnUndead : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.5f + 0.4f * level;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 200, true);
            List<Actor> affacted = new List<Actor>();

            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affacted.Add(i);
                    SkillHandler.Instance.Seals(sActor, i);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affacted, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}