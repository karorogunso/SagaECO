
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 水中生物知識（水中生物知識）
    /// </summary>
    public class WataniInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            Knowledge skill = new Knowledge(args.skill, sActor, "WataniInfo", SagaDB.Mob.MobType.WATER_ANIMAL, SagaDB.Mob.MobType.WATER_ANIMAL_BOSS
               , SagaDB.Mob.MobType.WATER_ANIMAL_BOSS_SKILL, SagaDB.Mob.MobType.WATER_ANIMAL_LVDIFF
               , SagaDB.Mob.MobType.WATER_ANIMAL_NOTOUCH, SagaDB.Mob.MobType.WATER_ANIMAL_RIDE
               , SagaDB.Mob.MobType.WATER_ANIMAL_SKILL);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

