using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 士氣叫喊
    /// </summary>
    public class MobCircleAtkup : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobCircleAtkup", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_bs  * 0.02f);
            if (skill.Variable.ContainsKey("MobCircleAtkup_max_atk1"))
                skill.Variable.Remove("MobCircleAtkup_max_atk1");
            skill.Variable.Add("MobCircleAtkup_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_bs * 0.02f);
            if (skill.Variable.ContainsKey("MobCircleAtkup_max_atk2"))
                skill.Variable.Remove("MobCircleAtkup_max_atk2");
            skill.Variable.Add("MobCircleAtkup_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_bs * 0.02f);
            if (skill.Variable.ContainsKey("MobCircleAtkup_max_atk3"))
                skill.Variable.Remove("MobCircleAtkup_max_atk3");
            skill.Variable.Add("MobCircleAtkup_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["MobCircleAtkup_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["MobCircleAtkup_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["MobCircleAtkup_max_atk3"];

                
        }
        #endregion
    }
}