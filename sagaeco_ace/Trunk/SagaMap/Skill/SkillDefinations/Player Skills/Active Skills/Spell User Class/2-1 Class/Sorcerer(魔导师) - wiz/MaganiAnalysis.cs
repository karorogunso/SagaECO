
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 魔法生物分析（魔法生物分析）
    /// </summary>
    public class MaganiAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS_SKILL);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_BOSS_SKILL_NOTPTDROPRANGE);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_LVDIFF);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_MATERIAL);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_NOTOUCH);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_NOTPTDROPRANGE);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_RIDE);
               types.Add(SagaDB.Mob.MobType.MAGIC_CREATURE_SKILL);

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
