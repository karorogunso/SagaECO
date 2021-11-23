using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// エフィカス
    /// </summary>
    public class Efuikasu : ISkill
    {
        #region ISkill 成員
        int KillingMarkCounter = 0;
        Actor skilluser = null;
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            int[] lifetimes = new int[] { 0, 180000, 240000, 300000, 360000, 420000 };
            int lifetime = lifetimes[level];
            Actor realactor = SkillHandler.Instance.GetPossesionedActor(sActor as ActorPC);
            if (realactor.KillingMarkCounter != 0)
                KillingMarkCounter = realactor.KillingMarkCounter;
            if (realactor.ActorID != sActor.ActorID)
                skilluser = sActor;
            else
                skilluser = realactor;
            DefaultBuff skill = new DefaultBuff(args.skill, realactor, "Efuikasu", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realactor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.ActorID == skilluser.ActorID)
            {
                actor.KillingMarkSoulUse = false;
                actor.KillingMarkCounter = Math.Min(KillingMarkCounter, 20);
            }
            else
            {
                actor.KillingMarkSoulUse = true;
                actor.KillingMarkCounter = Math.Min(KillingMarkCounter, 10);
            }
            actor.Buff.KillingMark = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.KillingMarkSoulUse = false;
            actor.KillingMarkCounter = 0;
            actor.Buff.KillingMark = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
