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
            if (pc.Status.Additions.ContainsKey("集中CD"))
                return -30;
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)

        {
            int cd = 60000;
            SkillCD skill2 = new SkillCD(args.skill, sActor, "集中CD", cd);
            SkillHandler.ApplyAddition(sActor, skill2);
            args.dActor = sActor.ActorID;
            int[] life = { 0, 20000, 18000, 18000, 15000, 12000 };
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HitMeleeUp", life[level]);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short atk1,atk2,atk3;
            float[] value = { 0, 1f, 1.3f, 1.6f, 1.9f, 2.2f };
            atk1 = (short)(actor.Status.max_atk1 * value[skill.skill.Level]);
            if (skill.Variable.ContainsKey("HitMeleeUp"))
                skill.Variable.Remove("HitMeleeUp");
            skill.Variable.Add("HitMeleeUp", atk1);
            actor.Status.max_atk1_skill += atk1;

            atk2 = (short)(actor.Status.max_atk1 * value[skill.skill.Level]);
            if (skill.Variable.ContainsKey("HitMeleeUp2"))
                skill.Variable.Remove("HitMeleeUp2");
            skill.Variable.Add("HitMeleeUp2", atk2);
            actor.Status.max_atk2_skill += atk2;

            atk3 = (short)(actor.Status.max_atk1 * value[skill.skill.Level]);
            if (skill.Variable.ContainsKey("HitMeleeUp3"))
                skill.Variable.Remove("HitMeleeUp3");
            skill.Variable.Add("HitMeleeUp3", atk3);
            actor.Status.max_atk3_skill += atk3;

            actor.Buff.最大攻撃力上昇 = true;
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

            actor.Buff.最大攻撃力上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
