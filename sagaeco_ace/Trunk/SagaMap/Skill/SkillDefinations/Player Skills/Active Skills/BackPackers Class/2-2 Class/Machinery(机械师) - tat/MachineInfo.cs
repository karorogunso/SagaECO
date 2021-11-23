
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 機械知識（機械知識）
    /// </summary>
    public class MachineInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Knowledge skill = new Knowledge(args.skill, sActor, "MachineInfo", SagaDB.Mob.MobType.MACHINE, SagaDB.Mob.MobType.MACHINE_BOSS
               , SagaDB.Mob.MobType.MACHINE_BOSS_CHAMP, SagaDB.Mob.MobType.MACHINE_BOSS_SKILL
               , SagaDB.Mob.MobType.MACHINE_MATERIAL, SagaDB.Mob.MobType.MACHINE_NOTOUCH
               , SagaDB.Mob.MobType.MACHINE_NOTPTDROPRANGE, SagaDB.Mob.MobType.MACHINE_RIDE
               , SagaDB.Mob.MobType.MACHINE_RIDE_ROBOT, SagaDB.Mob.MobType.MACHINE_SKILL
               , SagaDB.Mob.MobType.MACHINE_SKILL_BOSS, SagaDB.Mob.MobType.MACHINE_SMARK_BOSS_SKILL_HETERODOXY_NONBLAST);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

