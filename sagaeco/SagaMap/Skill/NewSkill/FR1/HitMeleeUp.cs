using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 集中
    /// </summary>
    public class HitMeleeUp:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)

        {
            args.dActor = sActor.ActorID;
            int[] life = { 0, 20000};
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HitMeleeUp", life[level]);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short atk1,atk2,atk3;
            float[] value = { 0, 0.5f };
            atk1 = (short)(actor.Status.max_atk_bs * value[skill.skill.Level]);
            if (skill.Variable.ContainsKey("HitMeleeUp"))
                skill.Variable.Remove("HitMeleeUp");
            skill.Variable.Add("HitMeleeUp", atk1);
            actor.Status.max_atk1_skill += atk1;

            atk2 = (short)(actor.Status.max_atk_bs * value[skill.skill.Level]);
            if (skill.Variable.ContainsKey("HitMeleeUp2"))
                skill.Variable.Remove("HitMeleeUp2");
            skill.Variable.Add("HitMeleeUp2", atk2);
            actor.Status.max_atk2_skill += atk2;

            atk3 = (short)(actor.Status.max_atk_bs * value[skill.skill.Level]);
            if (skill.Variable.ContainsKey("HitMeleeUp3"))
                skill.Variable.Remove("HitMeleeUp3");
            skill.Variable.Add("HitMeleeUp3", atk3);
            actor.Status.max_atk3_skill += atk3;

            actor.Buff.AtkMaxUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["HitMeleeUp"];
            actor.Status.max_atk1_skill -= (short)value;

            int value2 = skill.Variable["HitMeleeUp2"];
            actor.Status.max_atk2_skill -= (short)value;

            int value3 = skill.Variable["HitMeleeUp3"];
            actor.Status.max_atk3_skill -= (short)value;

            actor.Buff.AtkMaxUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
