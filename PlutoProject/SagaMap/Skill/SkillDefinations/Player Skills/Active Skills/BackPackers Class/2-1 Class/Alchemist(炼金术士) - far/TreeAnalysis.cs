
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 木材分析
    /// </summary>
    public class TreeAnalysis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> types = new List<MobType>();
                types.Add(SagaDB.Mob.MobType.TREE);
                types.Add(SagaDB.Mob.MobType.TREE_MATERIAL);

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
