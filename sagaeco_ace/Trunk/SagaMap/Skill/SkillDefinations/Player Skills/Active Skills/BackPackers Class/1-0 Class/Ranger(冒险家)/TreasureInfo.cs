
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 寶物箱知識（宝箱知識）
    /// </summary>
    public class TreasureInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Knowledge skill = new Knowledge(args.skill, sActor, "TreasureInfo", SagaDB.Mob.MobType.TREASURE_BOX, SagaDB.Mob.MobType.TREASURE_BOX_MATERIAL);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

