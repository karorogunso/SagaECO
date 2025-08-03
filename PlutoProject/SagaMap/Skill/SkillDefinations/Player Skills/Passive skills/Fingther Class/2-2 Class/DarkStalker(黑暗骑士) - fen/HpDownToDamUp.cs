
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 價格轉移（プライスオブペイン）
    /// </summary>
    public class HpDownToDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "HpDownToDamUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            uint HPPercent = (uint)(actor.HP / actor.MaxHP * 100);
            float damUP = 0f;
            if (HPPercent <= 100 && HPPercent > 80)
            {
                damUP = 0f;
            }
            else if (HPPercent <= 80 && HPPercent > 60)
            {
                damUP = 0.04f;
            }
            else if (HPPercent <= 60 && HPPercent > 40)
            {
                damUP = 0.06f;
            }
            else if (HPPercent <= 40 && HPPercent > 20)
            {
                damUP = 0.08f;
            }
            else if (HPPercent <= 20 && HPPercent >= 0)
            {
                damUP = 0.1f;
            }

            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_bs * damUP);
            if (skill.Variable.ContainsKey("HpDownToDamUp_max_atk1"))
                skill.Variable.Remove("HpDownToDamUp_max_atk1");
            skill.Variable.Add("HpDownToDamUp_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_bs * damUP);
            if (skill.Variable.ContainsKey("HpDownToDamUp_max_atk2"))
                skill.Variable.Remove("HpDownToDamUp_max_atk2");
            skill.Variable.Add("HpDownToDamUp_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_bs * damUP);
            if (skill.Variable.ContainsKey("HpDownToDamUp_max_atk3"))
                skill.Variable.Remove("HpDownToDamUp_max_atk3");
            skill.Variable.Add("HpDownToDamUp_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_bs * damUP);
            if (skill.Variable.ContainsKey("HpDownToDamUp_min_atk1"))
                skill.Variable.Remove("HpDownToDamUp_min_atk1");
            skill.Variable.Add("HpDownToDamUp_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_bs * damUP);
            if (skill.Variable.ContainsKey("HpDownToDamUp_min_atk2"))
                skill.Variable.Remove("HpDownToDamUp_min_atk2");
            skill.Variable.Add("HpDownToDamUp_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_bs * damUP);
            if (skill.Variable.ContainsKey("HpDownToDamUp_min_atk3"))
                skill.Variable.Remove("HpDownToDamUp_min_atk3");
            skill.Variable.Add("HpDownToDamUp_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["HpDownToDamUp_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["HpDownToDamUp_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["HpDownToDamUp_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["HpDownToDamUp_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["HpDownToDamUp_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["HpDownToDamUp_min_atk3"];

        }
        #endregion
    }
}

