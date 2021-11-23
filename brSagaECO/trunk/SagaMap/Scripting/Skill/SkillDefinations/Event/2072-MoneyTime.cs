using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.ActorEventHandlers;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 流金岁月
    /// </summary>
    public class MoneyTime : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Gold >= 2000)
            {
                return 0;
            }
            else
            {
                return -2;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                pc.Gold -= 2000;
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MoneyTime", 60000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Speed += 120;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.Speed > 120)
            {
                actor.Speed -= 120;
            }
        }
        #endregion
    }
}