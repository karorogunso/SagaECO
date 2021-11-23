
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 狂亂時間（オーバーワーク）
    /// </summary>
    public class OverWork : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "OverWork", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            if (dActor.ActorID == sActor.ActorID)
            {
                SkillHandler.ApplyAddition(dActor, skill);
                EffectArg arg2 = new EffectArg();
                arg2.effectID = 5170;
                arg2.actorID = dActor.ActorID;
                Manager.MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, dActor, true);
            }

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int overwork = new int[] { 0, 15, 20, 25, 30, 35 }[skill.skill.Level];
            if (skill.Variable.ContainsKey("OverWork"))
                skill.Variable.Remove("OverWork");
            skill.Variable.Add("OverWork", overwork);
            actor.Buff.OverWork = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("OverWork"))
                skill.Variable.Remove("OverWork");
            actor.Buff.OverWork = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
