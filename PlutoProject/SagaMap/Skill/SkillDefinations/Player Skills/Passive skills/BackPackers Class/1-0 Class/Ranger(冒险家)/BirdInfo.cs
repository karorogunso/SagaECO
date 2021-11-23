
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 鳥類知識（鳥知識）
    /// </summary>
    public class BirdInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Knowledge skill = new Knowledge(args.skill, sActor, "BirdInfo", SagaDB.Mob.MobType.BIRD, SagaDB.Mob.MobType.BIRD_BOSS
               , SagaDB.Mob.MobType.BIRD_BOSS_SKILL, SagaDB.Mob.MobType.BIRD_NOTOUCH
               , SagaDB.Mob.MobType.BIRD_SPBOSS_SKILL, SagaDB.Mob.MobType.BIRD_UNITE);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

