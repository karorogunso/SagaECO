using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 捕縛
    /// </summary>
    public class Caputrue : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if(dActor.type== ActorType.PC)
            {
                ActorPC pc=(ActorPC)dActor;
                SkillHandler.Instance.Warp(pc, pc.SaveMap, pc.SaveX, pc.SaveY);
            }
        }
        #endregion
    }
}