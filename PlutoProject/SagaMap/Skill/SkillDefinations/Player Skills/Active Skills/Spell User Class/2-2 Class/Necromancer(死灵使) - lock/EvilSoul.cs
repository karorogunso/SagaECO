
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 邪惡靈魂（イビルソウル）
    /// </summary>
    public class EvilSoul : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 + 10000 * level;

            ActorPC realDActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            DefaultBuff skill = new DefaultBuff(args.skill, realDActor, "EvilSoul", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realDActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int factor = new int[] { 0, 20, 40, 60, 80, 95 }[skill.skill.Level];
            if (skill.Variable.ContainsKey("EvilSoul"))
                skill.Variable.Remove("EvilSoul");
            skill.Variable.Add("EvilSoul", factor);
            actor.Buff.イビルソウル = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("EvilSoul"))
                skill.Variable.Remove("EvilSoul");
            actor.Buff.イビルソウル = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
