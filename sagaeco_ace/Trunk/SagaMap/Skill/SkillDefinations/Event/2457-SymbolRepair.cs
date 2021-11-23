
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// シンボル修復
    /// </summary>
    public class SymbolRepair : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Mode.Symbol)
                    return 0;
                else
                    return -14;
            }
            else
                return -14;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                float amount = pc.DominionJobLevel * 10;
                if (pc.JobType == JobType.BACKPACKER)
                    amount = amount * 1.5f;
                if (amount > 320)
                    amount = 320;
                List<Actor> list = new List<Actor>();
                list.Add(dActor);
                SkillHandler.Instance.MagicAttack(sActor, list, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Neutral, -amount, 0, true);            
            }
        }
        #endregion
    }
}