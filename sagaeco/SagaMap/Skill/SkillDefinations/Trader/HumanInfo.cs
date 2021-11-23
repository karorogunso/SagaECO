
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 人類知識（人間知識）
    /// </summary>
    public class HumanInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            Knowledge skill = new Knowledge(args.skill, sActor, "HumanInfo", SagaDB.Mob.MobType.HUMAN, SagaDB.Mob.MobType.HUMAN_BOSS
               , SagaDB.Mob.MobType.HUMAN_BOSS_CHAMP, SagaDB.Mob.MobType.HUMAN_BOSS_SKILL
               , SagaDB.Mob.MobType.HUMAN_CHAMP, SagaDB.Mob.MobType.HUMAN_NOTOUCH
               , SagaDB.Mob.MobType.HUMAN_RIDE, SagaDB.Mob.MobType.HUMAN_SKILL
               , SagaDB.Mob.MobType.HUMAN_SKILL_BOSS_CHAMP, SagaDB.Mob.MobType.HUMAN_SKILL_CHAMP
               , SagaDB.Mob.MobType.HUMAN_SMARK_BOSS_HETERODOXY, SagaDB.Mob.MobType.HUMAN_SMARK_HETERODOXY);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

