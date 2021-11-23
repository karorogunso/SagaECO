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
    /// 蜂針
    /// </summary>
    public class MobPerfectcritical : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f;
            List<Actor> actors=new List<Actor>();
            actors.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, actors, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Neutral, 0,factor,false );
        }
        #endregion
    }
}