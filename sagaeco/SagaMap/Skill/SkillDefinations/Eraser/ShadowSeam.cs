using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// 影縫い
    /// </summary>
    public class ShadowSeam : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("ShadowSeam"))
                return -30;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = { 0, 5000, 8000, 10000, 13000, 15000 };
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "ShadowSeam", lifetime[level]);
            SkillHandler.ApplyAddition(sActor, skill);

        }
        #endregion
    }
}
