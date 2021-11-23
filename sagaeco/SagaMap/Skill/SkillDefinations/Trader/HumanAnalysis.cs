
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 人類分析
    /// </summary>
    public class HumanAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
                types.Add(SagaDB.Mob.MobType.HUMAN);
                types.Add(SagaDB.Mob.MobType.HUMAN_BOSS);
                types.Add(SagaDB.Mob.MobType.HUMAN_BOSS_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_BOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.HUMAN_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.HUMAN_RIDE);
                types.Add(SagaDB.Mob.MobType.HUMAN_SKILL);
                types.Add(SagaDB.Mob.MobType.HUMAN_SKILL_BOSS_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_SKILL_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_SMARK_BOSS_HETERODOXY);
                types.Add(SagaDB.Mob.MobType.HUMAN_SMARK_HETERODOXY);
                types.Add(SagaDB.Mob.MobType.HUMAN);
                types.Add(SagaDB.Mob.MobType.HUMAN_BOSS);
                types.Add(SagaDB.Mob.MobType.HUMAN_BOSS_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_BOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.HUMAN_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.HUMAN_RIDE);
                types.Add(SagaDB.Mob.MobType.HUMAN_SKILL);
                types.Add(SagaDB.Mob.MobType.HUMAN_SKILL_BOSS_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_SKILL_CHAMP);
                types.Add(SagaDB.Mob.MobType.HUMAN_SMARK_BOSS_HETERODOXY);
                types.Add(SagaDB.Mob.MobType.HUMAN_SMARK_HETERODOXY);

                ActorMob mob = (ActorMob)dActor;
                if (types.Contains(mob.BaseData.mobType))
                {
                    return 0;
                }
            }
            return -4;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Analysis skill = new Analysis(args.skill, dActor);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}
