
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 植物分析（植物分析）
    /// </summary>
    public class PlantAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
                types.Add(SagaDB.Mob.MobType.PLANT);
                types.Add(SagaDB.Mob.MobType.PLANT_BOSS);
                types.Add(SagaDB.Mob.MobType.PLANT_BOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.PLANT_BOSS_SKILL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.PLANT_MARK);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_BOSS_MARK);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_EAST);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_EAST_BOSS_SKILL_WALL);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_HETERODOXY);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_NORTH);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_SKILL);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_SOUTH);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_WEST);
                types.Add(SagaDB.Mob.MobType.PLANT_MATERIAL_WEST_BOSS_SKILL_WALL);
                types.Add(SagaDB.Mob.MobType.PLANT_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.PLANT_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.PLANT_SKILL);
                types.Add(SagaDB.Mob.MobType.PLANT_UNITE);

                ActorMob mob=(ActorMob)dActor;
                if (types.Contains(mob.BaseData.mobType))
                {
                    return 0;
                }
            }
            return -4;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Analysis skill = new Analysis(args.skill, dActor);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}
