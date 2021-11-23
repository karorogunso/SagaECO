using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 敏捷的動作
    /// </summary>
    public class AvoidMeleeUp:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;//不显示效果
            int life = 10000 * level;            
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AvoidMeleeUp", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short atk1;
            atk1 = (short)(skill.skill.Level * 6);
            if (skill.Variable.ContainsKey("AvoidMeleeUp"))
                skill.Variable.Remove("AvoidMeleeUp");
            skill.Variable.Add("AvoidMeleeUp", atk1);
            actor.Status.avoid_melee_skill += atk1;

            actor.Buff.近距離回避率上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["AvoidMeleeUp"];
            actor.Status.avoid_melee_skill -= (short)value;

            actor.Buff.近距離回避率上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
