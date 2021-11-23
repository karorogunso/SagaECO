using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// もっと褒めてもいいのよ！
    /// </summary>
    public class YouCanPraiseMore : ISkill
    {
        #region ISkill 成員
        int KillingMarkCounter = 0;
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                return -1;
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 420000;
            if (sActor.KillingMarkCounter != 0)
                KillingMarkCounter = sActor.KillingMarkCounter;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Efuikasu", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.KillingMarkCounter = Math.Min(KillingMarkCounter, 20);
            actor.Buff.KillingMark = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.KillingMarkCounter = 0;
            actor.Buff.KillingMark = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
