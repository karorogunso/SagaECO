using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Warlock
{
    public class ChaosWidow: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            float SuckBlood = 0;
            factor = 1f + 0.24f * level;
            if (dActor.Darks == 1)
            {
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 5202);
                dActor.Darks = 0;
                SuckBlood = 0.05f;
                SkillArg add = new SkillArg();
                add.argType = SkillArg.ArgType.Actor_Active;
                add.skill = args.skill;
                SkillHandler.Instance.MagicAttack(sActor, dActor, add, SagaLib.Elements.Dark, factor);
            }
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            SkillHandler.Instance.MagicAttack(sActor, list, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, factor, 0, false, false, SuckBlood);
        }

        #endregion
    }
}
