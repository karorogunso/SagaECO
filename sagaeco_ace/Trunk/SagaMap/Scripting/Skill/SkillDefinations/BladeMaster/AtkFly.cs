
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 飛燕劍（燕返し）
    /// </summary>
    public class AtkFly : ISkill
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
            float factor = 1.4f + 0.1f * level;
            int times = 1;
            if (dActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)dActor;
                if (mob.BaseData.mobType == SagaDB.Mob.MobType.BIRD ||
                   mob.BaseData.mobType == SagaDB.Mob.MobType.BIRD_BOSS ||
                   mob.BaseData.mobType == SagaDB.Mob.MobType.BIRD_BOSS_SKILL ||
                   mob.BaseData.mobType == SagaDB.Mob.MobType.BIRD_NOTOUCH ||
                   mob.BaseData.mobType == SagaDB.Mob.MobType.BIRD_SPBOSS_SKILL ||
                   mob.BaseData.mobType == SagaDB.Mob.MobType.BIRD_UNITE)
                {
                    times = 2;
                }

            }
            List<Actor> Affected = new List<Actor>();
            for (int i = 0; i < times; i++)
            {
                Affected.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, Affected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}