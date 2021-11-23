using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 解開憑依
    /// </summary>
    public class MobTrDrop : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 30;
            if (dActor.type != ActorType.PC)
            {
                return;
            }
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                SkillHandler.Instance.PossessionCancel((ActorPC)dActor, PossessionPosition.NONE);
            }
        }
        #endregion
    }
}