using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    public class GrimReaper : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            args.type = ATTACK_TYPE.BLOW;

            factor = 1.0f + 0.3f * level;
            List<Actor> actors = new List<Actor>();
            if (level == 6)
            {
                EffectArg arg = new EffectArg();
                arg.effectID = 5236;
                arg.actorID = dActor.ActorID;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);
                factor = 2f;
                for (int i = 0; i < 3; i++)
                {
                    actors.Add(dActor);
                }
                args.delay = 500;
                SkillHandler.Instance.PhysicalAttack(sActor, actors, args, SkillHandler.DefType.Def, SagaLib.Elements.Dark, 0, factor, false, 0.6f, false);
            }
            else
            {
                actors.Add(dActor);
                SkillHandler.Instance.PhysicalAttack(sActor, actors, args, SkillHandler.DefType.Def, SagaLib.Elements.Dark, 0, factor, false, 0.1f, false);
            }

        }

        #endregion
    }
}
