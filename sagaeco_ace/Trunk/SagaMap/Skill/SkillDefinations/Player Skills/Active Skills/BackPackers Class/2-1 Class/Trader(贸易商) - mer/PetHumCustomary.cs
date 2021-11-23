
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 賞金（チップ）[接續技能]
    /// </summary>
    public class PetHumCustomary : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint[] SkillIDs = { 6404, 6405, 6406 };
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo( SkillIDs[SagaLib.Global.Random.Next(0,SkillIDs.Length-1)],level,0));
        }        
        #endregion
    }
}
