
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
            float factor2 = 1.0f + 0.25f * level;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affacted = new List<Actor>();
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor2);
            }
            SkillArg arg2 = new SkillArg();
            arg2 = args.Clone();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affacted.Add(i);
                    SkillHandler.Instance.Seals(sActor, i);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affacted, arg2, SagaLib.Elements.Holy, factor);
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);
        }
        #endregion
    }
}