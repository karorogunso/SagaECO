
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    /// <summary>
    /// 木材知識（木材知識）
    /// </summary>
    public class TreeInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Knowledge skill = new Knowledge(args.skill, sActor, "TreeInfo", SagaDB.Mob.MobType.TREE, SagaDB.Mob.MobType.TREE_MATERIAL);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

