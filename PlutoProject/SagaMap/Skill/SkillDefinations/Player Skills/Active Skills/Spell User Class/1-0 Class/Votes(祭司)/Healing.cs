using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    /// <summary>
    /// ヒーリング
    /// </summary>
    public class Healing : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("Spell"))
            {
                return -7;
            }
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Mode.Symbol)
                    return -14;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = new float[] { 0, -1.0f, -1.7f, -2.3f, -2.7f, -3.0f, -100f };
            float factor = factors[level];
            factor += sActor.Status.Cardinal_Rank;
            if (dActor.type == ActorType.MOB)
            {
                ActorMob m = (ActorMob)dActor;
                if (m.Status.undead)
                    factor = -factors[level];
            }
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}
