
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    /// <summary>
    /// 植物知識（植物知識）
    /// </summary>
    public class PlantInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Knowledge skill = new Knowledge(args.skill, sActor, "PlantInfo", SagaDB.Mob.MobType.PLANT, SagaDB.Mob.MobType.PLANT_BOSS
               , SagaDB.Mob.MobType.PLANT_BOSS_SKILL, SagaDB.Mob.MobType.PLANT_BOSS_SKILL_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.PLANT_MARK, SagaDB.Mob.MobType.PLANT_MATERIAL
               , SagaDB.Mob.MobType.PLANT_MATERIAL_BOSS_MARK, SagaDB.Mob.MobType.PLANT_MATERIAL_EAST
               , SagaDB.Mob.MobType.PLANT_MATERIAL_EAST_BOSS_SKILL_WALL, SagaDB.Mob.MobType.PLANT_MATERIAL_HETERODOXY
               , SagaDB.Mob.MobType.PLANT_MATERIAL_NORTH, SagaDB.Mob.MobType.PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL
               , SagaDB.Mob.MobType.PLANT_MATERIAL_NOTPTDROPRANGE, SagaDB.Mob.MobType.PLANT_MATERIAL_SKILL
               , SagaDB.Mob.MobType.PLANT_MATERIAL_SOUTH, SagaDB.Mob.MobType.PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL
               , SagaDB.Mob.MobType.PLANT_MATERIAL_WEST, SagaDB.Mob.MobType.PLANT_MATERIAL_WEST_BOSS_SKILL_WALL
               , SagaDB.Mob.MobType.PLANT_NOTOUCH, SagaDB.Mob.MobType.PLANT_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.PLANT_SKILL, SagaDB.Mob.MobType.PLANT_UNITE );
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

