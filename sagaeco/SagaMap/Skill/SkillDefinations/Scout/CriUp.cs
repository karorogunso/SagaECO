using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 會心一擊
    /// </summary>
    public class CriUp:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            //if (pc.Status.Additions.ContainsKey("会心CD"))
                //return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //killCD skill2 = new SkillCD(args.skill, sActor, "会心CD", 30000);
            //SkillHandler.ApplyAddition(sActor, skill2);
            args.dActor = 0;//不显示效果
            int life = 20000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "CriUp", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short atk1;
            atk1 = (short)(10 * 10);
            if (skill.Variable.ContainsKey("CriUp"))
                skill.Variable.Remove("CriUp");
            skill.Variable.Add("CriUp", atk1);
            actor.Status.hit_critical_skill += atk1;

            actor.Buff.HitCriUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["CriUp"];
            actor.Status.hit_critical_skill -= (short)value;

            actor.Buff.HitCriUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
