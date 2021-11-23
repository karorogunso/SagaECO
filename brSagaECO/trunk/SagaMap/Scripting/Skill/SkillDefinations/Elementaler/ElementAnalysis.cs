
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    /// <summary>
    /// 精靈分析（精霊分析）
    /// </summary>
    public class ElementAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
                types.Add(SagaDB.Mob.MobType.ELEMENT);
                types.Add(SagaDB.Mob.MobType.ELEMENT_BOSS_SKILL);
                types.Add(SagaDB.Mob.MobType.ELEMENT_MATERIAL_NOTOUCH_SKILL);
                types.Add(SagaDB.Mob.MobType.ELEMENT_NOTOUCH);
                types.Add(SagaDB.Mob.MobType.ELEMENT_NOTOUCH_SKILL);
                types.Add(SagaDB.Mob.MobType.ELEMENT_NOTPTDROPRANGE);
                types.Add(SagaDB.Mob.MobType.ELEMENT_SKILL);
                types.Add(SagaDB.Mob.MobType.ELEMENT_SKILL_BOSS);

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
