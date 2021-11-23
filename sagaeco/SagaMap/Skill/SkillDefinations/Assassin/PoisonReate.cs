using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 狂毒氣（狂気毒）
    /// </summary>
    public class PoisonReate :  ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                    return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
                int life = 20000;
                DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "PoisonReate", life);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                skill.OnCheckValid += this.ValidCheck;
                SkillHandler.ApplyAddition(realdActor, skill);
        }
        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.speed_skill += 300;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.speed_skill -= 300;
        }
        #endregion
    }
}
