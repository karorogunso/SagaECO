using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 動物傷害增加
    /// </summary>
    public class AniDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ushort[] Values = { 0, 3, 6, 9, 12, 15 };//%

            ushort value = Values[level];

            SomeTypeDamUp skill = new SomeTypeDamUp(args.skill, dActor, "AniDamUp");
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_BOMB_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_RIDE, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_RIDE_BREEDER, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE, value);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}