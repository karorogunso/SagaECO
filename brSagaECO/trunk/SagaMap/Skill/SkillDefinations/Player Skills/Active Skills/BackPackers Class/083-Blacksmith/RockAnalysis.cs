
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 礦石分析（鉱石分析）
    /// </summary>
    public class RockAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
                types.Add(SagaDB.Mob.MobType.ROCK);
                types.Add(SagaDB.Mob.MobType.ROCK_BOMB_SKILL);
                types.Add(SagaDB.Mob.MobType.ROCK_BOSS_SKILL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ROCK_BOSS_SKILL_WALL);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_BOSS_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_BOSS_SKILL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_EAST_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_NORTH_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_SKILL);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_SOUTH_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ROCK_MATERIAL_WEST_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ROCK_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ROCK_SKILL);

                ActorMob mob = (ActorMob)dActor;
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
