
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 艾卡卡（アルカナカード）
    /// </summary>
    public class SumArcanaCard : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint[] NextSkillIDs = { 2432, 2431, 2434, 2435 };
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillIDs[SagaLib.Global.Random.Next (0, NextSkillIDs.Length - 1)], level, 0));
        }
        #endregion
    }
}