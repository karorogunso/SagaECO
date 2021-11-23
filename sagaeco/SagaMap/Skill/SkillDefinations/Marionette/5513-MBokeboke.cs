
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionette
{
    /// <summary>
    /// 汪~~汪~~
    /// </summary>
    public class MBokeboke : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MBokeboke", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            //最大攻擊
            int max_atk1_add = -(int)(actor.Status.max_atk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_max_atk1"))
                skill.Variable.Remove("MBokeboke_max_atk1");
            skill.Variable.Add("MBokeboke_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = -(int)(actor.Status.max_atk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_max_atk2"))
                skill.Variable.Remove("MBokeboke_max_atk2");
            skill.Variable.Add("MBokeboke_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = -(int)(actor.Status.max_atk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_max_atk3"))
                skill.Variable.Remove("MBokeboke_max_atk3");
            skill.Variable.Add("MBokeboke_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = -(int)(actor.Status.min_atk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_min_atk1"))
                skill.Variable.Remove("MBokeboke_min_atk1");
            skill.Variable.Add("MBokeboke_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = -(int)(actor.Status.min_atk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_min_atk2"))
                skill.Variable.Remove("MBokeboke_min_atk2");
            skill.Variable.Add("MBokeboke_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = -(int)(actor.Status.min_atk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_min_atk3"))
                skill.Variable.Remove("MBokeboke_min_atk3");
            skill.Variable.Add("MBokeboke_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //最大魔攻
            int max_matk_add = -(int)(actor.Status.max_matk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_max_matk"))
                skill.Variable.Remove("MBokeboke_max_matk");
            skill.Variable.Add("MBokeboke_max_matk", max_matk_add);
            actor.Status.max_matk_skill += (short)max_matk_add;

            //最小魔攻
            int min_matk_add = -(int)(actor.Status.min_matk_bs * 0.1f);
            if (skill.Variable.ContainsKey("MBokeboke_min_matk"))
                skill.Variable.Remove("MBokeboke_min_matk");
            skill.Variable.Add("MBokeboke_min_matk", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["MBokeboke_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["MBokeboke_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["MBokeboke_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["MBokeboke_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["MBokeboke_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["MBokeboke_min_atk3"];

            //最大魔攻
            actor.Status.max_matk_skill -= (short)skill.Variable["MBokeboke_max_matk"];

            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["MBokeboke_min_matk"];

            //VIT
            //actor.Status.vit_skill -= (short)skill.Variable["MBokeboke_vit"];
       
        }
        #endregion
    }
}
