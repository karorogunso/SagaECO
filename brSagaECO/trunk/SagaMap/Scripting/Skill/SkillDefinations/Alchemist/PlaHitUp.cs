using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 植物命中
    /// </summary>
    public class PlaHitUp : ISkill
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

            SomeTypeHitUp skill = new SomeTypeHitUp(args.skill, dActor, "PlaHitUp");
            skill.AddMobType(SagaDB.Mob.MobType.PLANT, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_BOSS_SKILL_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MARK, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_BOSS_MARK, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_EAST, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_EAST_BOSS_SKILL_WALL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_HETERODOXY, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_NORTH, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_SOUTH, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_WEST, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_MATERIAL_WEST_BOSS_SKILL_WALL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.PLANT_UNITE, value);
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
