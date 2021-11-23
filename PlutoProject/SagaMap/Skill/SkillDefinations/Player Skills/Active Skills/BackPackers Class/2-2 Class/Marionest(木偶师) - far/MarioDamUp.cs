
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 變身活動木偶（マリオネットマスタリー）
    /// </summary>
    public class MarioDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC sAct = (ActorPC)sActor;
                if (sAct.Marionette != null)
                {
                    active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MarioDamUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;

            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_bs  * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_max_atk1"))
                skill.Variable.Remove("MarioDamUp_max_atk1");
            skill.Variable.Add("MarioDamUp_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_bs * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_max_atk2"))
                skill.Variable.Remove("MarioDamUp_max_atk2");
            skill.Variable.Add("MarioDamUp_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_bs * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_max_atk3"))
                skill.Variable.Remove("MarioDamUp_max_atk3");
            skill.Variable.Add("MarioDamUp_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_bs  * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_atk1"))
                skill.Variable.Remove("MarioDamUp_min_atk1");
            skill.Variable.Add("MarioDamUp_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_bs * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_atk2"))
                skill.Variable.Remove("MarioDamUp_min_atk2");
            skill.Variable.Add("MarioDamUp_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_bs * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_atk3"))
                skill.Variable.Remove("MarioDamUp_min_atk3");
            skill.Variable.Add("MarioDamUp_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //最小魔攻
            int min_matk_add = (int)(actor.Status.min_matk * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_matk"))
                skill.Variable.Remove("MarioDamUp_min_matk");
            skill.Variable.Add("MarioDamUp_min_matk", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

            //左防禦
            int def_add = (int)(actor.Status.def * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_def"))
                skill.Variable.Remove("MarioDamUp_def");
            skill.Variable.Add("MarioDamUp_def", def_add);
            actor.Status.def_skill += (short)def_add;

            //右防禦
            int def_add_add = (int)(actor.Status.def_add * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_def_add"))
                skill.Variable.Remove("MarioDamUp_def_add");
            skill.Variable.Add("MarioDamUp_def_add", def_add_add);
            actor.Status.def_add_skill += (short)def_add_add;

            //左魔防
            int mdef_add = (int)(actor.Status.mdef * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_mdef"))
                skill.Variable.Remove("MarioDamUp_mdef");
            skill.Variable.Add("MarioDamUp_mdef", mdef_add);
            actor.Status.mdef_skill += (short)mdef_add;

            //右魔防
            int mdef_add_add = (int)(actor.Status.mdef_add * (0.07f + 0.03f * level));
            if (skill.Variable.ContainsKey("MarioDamUp_mdef_add"))
                skill.Variable.Remove("MarioDamUp_mdef_add");
            skill.Variable.Add("MarioDamUp_mdef_add", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["MarioDamUp_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["MarioDamUp_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["MarioDamUp_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["MarioDamUp_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["MarioDamUp_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["MarioDamUp_min_atk3"];

            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["MarioDamUp_min_matk"];

            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["MarioDamUp_def"];

            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["MarioDamUp_def_add"];

            //左魔防
            actor.Status.mdef_skill -= (short)skill.Variable["MarioDamUp_mdef"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["MarioDamUp_mdef_add"];
                              
        }
        #endregion
    }
}

