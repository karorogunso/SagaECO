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
            float SuckBlood = 0.3f;
            factor = 1f + 0.24f * level;
            if (dActor.Darks == 1)
            {
                float factor2 = 0.8f + 0.2f * level;
                if (level == 6)
                {
                    factor2 = 3f;
                    SuckBlood = 0.3f;
                }
                else
                    SuckBlood = 0.05f;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 5202);
                dActor.Darks = 0;
                SkillArg add = new SkillArg();
                add = args.Clone();
                add.skill.BaseData.id = 100;
                SkillHandler.Instance.MagicAttack(sActor, dActor, add, SagaLib.Elements.Dark, factor2);
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, add, sActor, true);
                add.skill.BaseData.id = 3134;
            }
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            if (level == 6)
                factor = 5f;
            SkillHandler.Instance.MagicAttack(sActor, list, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, factor, 0, false, false, SuckBlood);
        }

        #endregion
    }
}
