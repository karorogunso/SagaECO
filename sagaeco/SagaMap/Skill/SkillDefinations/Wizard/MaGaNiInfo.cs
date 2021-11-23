
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Wizard
{
    /// <summary>
    /// 魔法生物知識（魔法生物知識）
    /// </summary>
    public class MaGaNiInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            Knowledge skill = new Knowledge(args.skill, sActor, "MaGaNiInfo", SagaDB.Mob.MobType.MAGIC_CREATURE, SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS
               , SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS_SKILL, SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS_SKILL_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.MAGIC_CREATURE_LVDIFF, SagaDB.Mob.MobType.MAGIC_CREATURE_MATERIAL
               , SagaDB.Mob.MobType.MAGIC_CREATURE_NOTOUCH, SagaDB.Mob.MobType.MAGIC_CREATURE_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.MAGIC_CREATURE_RIDE, SagaDB.Mob.MobType.MAGIC_CREATURE_SKILL);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

