using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 岩石迴避
    /// </summary>
    public class StoAvoUp : ISkill
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

            SomeTypeAvoUp skill = new SomeTypeAvoUp(args.skill, dActor, "StoAvoUp");
            skill.AddMobType(SagaDB.Mob.MobType.ROCK, value);
            skill.AddMobType(SagaDB.Mob.MobType.ROCK_BOSS_SKILL_NOTPTDROPRANGE, value);
            skill.AddMobType(SagaDB.Mob.MobType.ROCK_MATERIAL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ROCK_MATERIAL_NORTH_NOTOUCH, value);
            skill.AddMobType(SagaDB.Mob.MobType.ROCK_MATERIAL_SKILL, value);
            skill.AddMobType(SagaDB.Mob.MobType.ROCK_MATERIAL_SOUTH_NOTOUCH, value);
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