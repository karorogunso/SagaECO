using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class ThunderBall : ISkill
    {
        bool MobUse;
        public ThunderBall()
        {
            this.MobUse = false;
        }
        public ThunderBall(bool MobUse)
        {
            this.MobUse = MobUse;
        }
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
            if (MobUse)
            {
                level = 5;
            }
            float factor = 1.3f + 0.2f * level;
            if (SagaLib.Global.Random.Next(0, 100) < 60)
            {
                factor += 0.8f + 0.2f * level;

                EffectArg arg = new EffectArg();
                arg.effectID = 5073;
                arg.actorID = dActor.ActorID;
                if (sActor.type == ActorType.PC)
                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);

            }
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Wind, factor);
        }

        #endregion
    }
}
