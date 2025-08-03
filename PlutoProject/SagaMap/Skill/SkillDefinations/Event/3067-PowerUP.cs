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
    /// 力量提升
    /// </summary>
    public class PowerUP : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PowerUP", 60000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short atkadd = (short)skill.skill.Level;
            actor.Status.str_skill += atkadd;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short atkadd = (short)skill.skill.Level;
            actor.Status.str_skill -= atkadd;

        }
        #endregion
    }
}