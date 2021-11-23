using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    /// <summary>
    /// ホークアイ
    /// </summary>
    public class EagleEye : ISkill 
    {
        #region ISkill 成員

        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)sActor;
            int[] lifetimes = new int[] { 0, 60000, 90000, 120000, 150000, 180000 };
            int lifetime = lifetimes[args.skill.Level];
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ホークアイ", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float rate = 0.25f + 0.25f * skill.skill.Level;

            int atk1_add_min = (int)(actor.Status.min_atk1 * rate);
            if (skill.Variable.ContainsKey("EagleEye_min_atk1"))
                skill.Variable.Remove("EagleEye_min_atk1");
            skill.Variable.Add("EagleEye_min_atk1", atk1_add_min);
            actor.Status.min_atk1_skill += (short)atk1_add_min;

            int atk2_add_min = (int)(actor.Status.min_atk2 * rate);
            if (skill.Variable.ContainsKey("EagleEye_min_atk2"))
                skill.Variable.Remove("EagleEye_min_atk2");
            skill.Variable.Add("EagleEye_min_atk2", atk2_add_min);
            actor.Status.min_atk2_skill += (short)atk2_add_min;

            int atk3_add_min = (int)(actor.Status.min_atk3 * rate);
            if (skill.Variable.ContainsKey("EagleEye_min_atk3"))
                skill.Variable.Remove("EagleEye_min_atk3");
            skill.Variable.Add("EagleEye_min_atk3", atk3_add_min);
            actor.Status.min_atk3_skill += (short)atk3_add_min;

            int atk1_add_max = (int)(actor.Status.max_atk1 * rate);
            if (skill.Variable.ContainsKey("EagleEye_max_atk1"))
                skill.Variable.Remove("EagleEye_max_atk1");
            skill.Variable.Add("EagleEye_max_atk1", atk1_add_max);
            actor.Status.max_atk1_skill += (short)atk1_add_max;

            int atk2_add_max = (int)(actor.Status.max_atk2 * rate);
            if (skill.Variable.ContainsKey("EagleEye_max_atk2"))
                skill.Variable.Remove("EagleEye_max_atk2");
            skill.Variable.Add("EagleEye_max_atk2", atk2_add_max);
            actor.Status.max_atk2_skill += (short)atk2_add_max;

            int atk3_add_max = (int)(actor.Status.max_atk3 * rate);
            if (skill.Variable.ContainsKey("EagleEye_max_atk3"))
                skill.Variable.Remove("EagleEye_max_atk3");
            skill.Variable.Add("EagleEye_max_atk3", atk3_add_max);
            actor.Status.max_atk3_skill += (short)atk3_add_max;

            actor.Buff.三转未知强力增强 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["EagleEye_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["EagleEye_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["EagleEye_min_atk3"];
            actor.Status.max_atk1_skill -= (short)skill.Variable["EagleEye_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["EagleEye_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["EagleEye_max_atk3"];
            actor.Buff.三转未知强力增强 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
