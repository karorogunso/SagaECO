using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 機械迴避
    /// </summary>
    public class MciAvoUp : ISkill
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

            SomeTypeAvoUp skill = new SomeTypeAvoUp(args.skill, dActor, "MciAvoUp");
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_BOSS_CHAMP, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_MATERIAL, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_RIDE, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_RIDE_ROBOT, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_SKILL_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.MACHINE_SMARK_BOSS_SKILL_HETERODOXY_NONBLAST, value);
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
