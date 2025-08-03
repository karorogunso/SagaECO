﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Marionette
{
    /// <summary>
    /// 木偶時的MP自然恢復
    /// </summary>
    public class MPRecovery : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //int lifetime = 1800000;
            SagaMap.Skill.Additions.Global.MPRecovery skill = new SagaMap.Skill.Additions.Global.MPRecovery(args.skill, dActor, int.MaxValue, 5000, true);
            SkillHandler.ApplyAddition(dActor, skill);
            //DefaultBuff skill2 = new DefaultBuff(args.skill, dActor, "MarionetteMPRecovery", lifetime);
            //skill.OnAdditionStart += this.StartEventHandler;
            //skill.OnAdditionEnd += this.EndEventHandler;
            //SkillHandler.ApplyAddition(dActor, skill2);
        }
        //void StartEventHandler(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.mp_recover_skill += 15;
        //}
        //void EndEventHandler(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.mp_recover_skill -= 15;
        //}
        #endregion
    }
}
