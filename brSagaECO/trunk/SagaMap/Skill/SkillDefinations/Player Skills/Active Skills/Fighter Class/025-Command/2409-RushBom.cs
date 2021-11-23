
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// ラッシュボム（ラッシュボム）
    /// </summary>
    public class RushBom : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint RushBomSeq = 2410, RushBomSeq2 = 2411;
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(RushBomSeq, level, 0));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(RushBomSeq, level, 300));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(RushBomSeq2, level, 3000));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(RushBomSeq2, level, 3300));
        }
        #endregion
    }
}