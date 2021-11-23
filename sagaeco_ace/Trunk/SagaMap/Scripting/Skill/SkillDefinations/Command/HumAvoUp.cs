using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 人族迴避
    /// </summary>
    public class HumAvoUp : ISkill
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

            SomeTypeAvoUp skill = new SomeTypeAvoUp(args.skill, dActor, "HumAvoUp");
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_BOSS, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_BOSS_CHAMP, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_CHAMP, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_RIDE, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_SKILL_BOSS_CHAMP, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_SKILL_CHAMP, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_SMARK_BOSS_HETERODOXY, value);
            skill.AddMobType(SagaDB.Mob.MobType.HUMAN_SMARK_HETERODOXY, value);
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