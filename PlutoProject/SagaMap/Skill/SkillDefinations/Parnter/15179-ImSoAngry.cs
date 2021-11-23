using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// まみーみたいに怒るよ！
    /// </summary>
    public class ImSoAngry : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 6000;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "ImSoAngry", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk1);
            if (skill.Variable.ContainsKey("ImSoAngry_min_atk1"))
                skill.Variable.Remove("ImSoAngry_min_atk1");
            skill.Variable.Add("ImSoAngry_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk2);
            if (skill.Variable.ContainsKey("ImSoAngry_min_atk2"))
                skill.Variable.Remove("ImSoAngry_min_atk2");
            skill.Variable.Add("ImSoAngry_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk3);
            if (skill.Variable.ContainsKey("ImSoAngry_min_atk3"))
                skill.Variable.Remove("ImSoAngry_min_atk3");
            skill.Variable.Add("ImSoAngry_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk1);
            if (skill.Variable.ContainsKey("ImSoAngry_max_atk1"))
                skill.Variable.Remove("ImSoAngry_max_atk1");
            skill.Variable.Add("ImSoAngry_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk2);
            if (skill.Variable.ContainsKey("ImSoAngry_max_atk2"))
                skill.Variable.Remove("ImSoAngry_max_atk2");
            skill.Variable.Add("ImSoAngry_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk3);
            if (skill.Variable.ContainsKey("ImSoAngry_max_atk3"))
                skill.Variable.Remove("ImSoAngry_max_atk3");
            skill.Variable.Add("ImSoAngry_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["ImSoAngry_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["ImSoAngry_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["ImSoAngry_min_atk3"];

            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["ImSoAngry_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["ImSoAngry_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["ImSoAngry_max_atk3"];
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }

        #endregion
    }
}
