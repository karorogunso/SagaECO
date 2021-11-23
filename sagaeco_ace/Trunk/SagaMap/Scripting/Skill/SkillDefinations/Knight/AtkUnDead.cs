using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Knight
{
    public class AtkUnDead : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
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
            float factor = 2.0f;
            if (dActor is ActorMob)
            {
                ActorMob dActorMob = (ActorMob)dActor;
                if (dActorMob.BaseData.mobType == SagaDB.Mob.MobType.UNDEAD)
                {
                    //加成
                    factor = 4.0f;
                }
            }
            factor = factor + 0.2f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
