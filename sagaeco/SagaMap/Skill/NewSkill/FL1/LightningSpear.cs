using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Fencer
{
    /// <summary>
    /// ライトニングスピア
    /// </summary>
    public class LightningSpear : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            args.type = ATTACK_TYPE.STAB;
            factor = 1.1f + 0.45f * level;
            List<Actor> actors = new List<Actor>();
            actors.Add(dActor);

            SkillHandler.Instance.PhysicalAttack(sActor, actors, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false, 0, true);
        }
        #endregion
    }
}
