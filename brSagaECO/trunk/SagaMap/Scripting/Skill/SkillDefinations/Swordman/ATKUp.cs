using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 攻擊．煥發
    /// </summary>
    public class ATKUp:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;//不显示效果
            int life = 0;
            life = (100 - 10 * level) * 1000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ATKUp", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short value;
            value = (short)(1 + skill.skill.Level * 4);            

            if (skill.Variable.ContainsKey("ATK"))
                skill.Variable.Remove("ATK");
            skill.Variable.Add("ATK", value);
            actor.Status.max_atk1_skill += value;
            actor.Status.max_atk2_skill += value;
            actor.Status.max_atk3_skill += value;

            actor.Buff.最大攻撃力上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value  = skill.Variable["ATK"];
            actor.Status.max_atk1_skill -= (short)value;
            actor.Status.max_atk2_skill -= (short)value;
            actor.Status.max_atk3_skill -= (short)value;

            actor.Buff.最大攻撃力上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
