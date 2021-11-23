using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Wizard
{
   /// <summary>
    /// 魔法生物傷害增加
    /// </summary>
    public class ManDamUp : ISkill
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

            SomeTypeDamUp skill = new SomeTypeDamUp(args.skill, dActor, "ManDamUp");
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS_SKILL_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_LVDIFF, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_MATERIAL, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_RIDE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MAGIC_CREATURE_SKILL, value);
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
