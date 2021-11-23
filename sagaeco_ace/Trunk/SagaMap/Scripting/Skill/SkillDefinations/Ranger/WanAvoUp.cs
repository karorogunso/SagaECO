using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 水中迴避
    /// </summary>
    public class WanAvoUp : ISkill
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

            SomeTypeAvoUp skill = new SomeTypeAvoUp(args.skill, dActor, "WanAvoUp");
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL, value);
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL_LVDIFF, value);
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL_RIDE, value);
            skill.AddMobType(SagaDB.Mob.MobType.WATER_ANIMAL_SKILL, value);
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