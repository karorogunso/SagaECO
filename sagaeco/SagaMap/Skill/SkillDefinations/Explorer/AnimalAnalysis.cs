
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Explorer
{
    /// <summary>
    /// 動物分析
    /// </summary>
    public class AnimalAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
                types.Add(SagaDB.Mob.MobType.ANIMAL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOMB_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOSS);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ANIMAL_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ANIMAL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ANIMAL_RIDE);
                types.Add(SagaDB.Mob.MobType.ANIMAL_RIDE_BREEDER);
                types.Add(SagaDB.Mob.MobType.ANIMAL_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ANIMAL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOMB_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOSS);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ANIMAL_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ANIMAL_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ANIMAL_RIDE);
                types.Add(SagaDB.Mob.MobType.ANIMAL_RIDE_BREEDER);
                types.Add(SagaDB.Mob.MobType.ANIMAL_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE);

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
