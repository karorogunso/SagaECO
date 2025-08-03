using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 火焰之心（フレイムハート）
    /// </summary>
    public class FrameHart : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 + 10000 * level;
            ActorPC realDActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            DefaultBuff skill = new DefaultBuff(args.skill, realDActor, "FrameHart", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realDActor, skill);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("FrameHart"))
                skill.Variable.Remove("FrameHart");

            skill.Variable.Add("FrameHart", 105 + skill.skill.Level * 10);

            actor.Buff.FlameHart = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("FrameHart"))
                skill.Variable.Remove("FrameHart");

            actor.Buff.FlameHart = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }

        #endregion
    }
}
