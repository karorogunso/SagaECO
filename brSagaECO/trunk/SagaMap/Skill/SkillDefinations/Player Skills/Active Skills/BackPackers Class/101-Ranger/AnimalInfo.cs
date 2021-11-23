
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 動物知識（動物知識）
    /// </summary>
    public class AnimalInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Knowledge skill = new Knowledge(args.skill, sActor, "AnimalInfo", SagaDB.Mob.MobType.ANIMAL, SagaDB.Mob.MobType.ANIMAL_BOMB_SKILL
               , SagaDB.Mob.MobType.ANIMAL_BOSS, SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL
               , SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE, SagaDB.Mob.MobType.ANIMAL_NOTOUCH
               , SagaDB.Mob.MobType.ANIMAL_NOTPTDROPRANGE, SagaDB.Mob.MobType.ANIMAL_RIDE
               , SagaDB.Mob.MobType.ANIMAL_RIDE_BREEDER, SagaDB.Mob.MobType.ANIMAL_SKILL
               , SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL, SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

