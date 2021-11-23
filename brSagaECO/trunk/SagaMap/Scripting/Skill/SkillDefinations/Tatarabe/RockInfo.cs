
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    /// <summary>
    /// 鐵礦知識（鉱石知識）
    /// </summary>
    public class RockInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            Knowledge skill = new Knowledge(args.skill, sActor, "RockInfo", SagaDB.Mob.MobType.ROCK, SagaDB.Mob.MobType.ROCK_BOMB_SKILL
               , SagaDB.Mob.MobType.ROCK_BOSS_SKILL_NOTPTDROPRANGE, SagaDB.Mob.MobType.ROCK_BOSS_SKILL_WALL
               , SagaDB.Mob.MobType.ROCK_MATERIAL, SagaDB.Mob.MobType.ROCK_MATERIAL_BOSS_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.ROCK_MATERIAL_BOSS_SKILL_NOTPTDROPRANGE, SagaDB.Mob.MobType.ROCK_MATERIAL_EAST_NOTOUCH
               , SagaDB.Mob.MobType.ROCK_MATERIAL_NORTH_NOTOUCH, SagaDB.Mob.MobType.ROCK_MATERIAL_SKILL
               , SagaDB.Mob.MobType.ROCK_MATERIAL_SOUTH_NOTOUCH, SagaDB.Mob.MobType.ROCK_MATERIAL_WEST_NOTOUCH
               , SagaDB.Mob.MobType.ROCK_NOTPTDROPRANGE, SagaDB.Mob.MobType.ROCK_SKILL);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

