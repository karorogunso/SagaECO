using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Explorer
{
    /// <summary>
    /// 蟲族命中
    /// </summary>
    public class InsHitUp : ISkill
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

            SomeTypeHitUp skill = new SomeTypeHitUp(args.skill, dActor, "InsHitUp");
            skill.AddMobType(SagaDB.Mob.MobType.INSECT, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_BOSS_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_RIDE, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.INSECT_UNITE, value);
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