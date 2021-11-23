using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 集中 コンセントレート
    /// </summary>
    public class HitMeleeUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = sActor.ActorID;
            int life = 180000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HitMeleeUp", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = 5 * skill.skill.Level;
            if (skill.Variable.ContainsKey("HitMeleeUp"))
                skill.Variable.Remove("HitMeleeUp");
            skill.Variable.Add("HitMeleeUp", value);
            actor.Status.hit_melee_skill += (short)value;

            actor.Buff.ShortHitUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["HitMeleeUp"];
            actor.Status.hit_melee_skill -= (short)value;

            if (skill.Variable.ContainsKey("HitMeleeUp"))
                skill.Variable.Remove("HitMeleeUp");

            actor.Buff.ShortHitUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
