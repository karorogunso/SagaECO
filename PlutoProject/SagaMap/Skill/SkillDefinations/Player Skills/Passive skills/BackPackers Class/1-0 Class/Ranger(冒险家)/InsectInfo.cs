
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 昆蟲知識（昆虫知識）
    /// </summary>
    public class InsectInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Knowledge skill = new Knowledge(args.skill, sActor, "InsectInfo", SagaDB.Mob.MobType.INSECT, SagaDB.Mob.MobType.INSECT_BOSS
               , SagaDB.Mob.MobType.INSECT_BOSS_NOTPTDROPRANGE, SagaDB.Mob.MobType.INSECT_BOSS_SKILL
               , SagaDB.Mob.MobType.INSECT_NOTOUCH, SagaDB.Mob.MobType.INSECT_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.INSECT_RIDE, SagaDB.Mob.MobType.INSECT_SKILL
               , SagaDB.Mob.MobType.INSECT_UNITE);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

