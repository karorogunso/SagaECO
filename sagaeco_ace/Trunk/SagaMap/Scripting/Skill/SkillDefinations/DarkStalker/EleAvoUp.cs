using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 精靈迴避
    /// </summary>
    public class EleAvoUp : ISkill
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

            SomeTypeDamUp skill = new SomeTypeDamUp(args.skill, dActor, "EleAvoUp");
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_BOSS_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_MATERIAL_NOTOUCH_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_NOTOUCH_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ELEMENT_SKILL_BOSS, value);
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