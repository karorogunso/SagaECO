using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  攻擊援手（アタックアシスト）
    /// </summary>
    public class A_T_PJoint : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.PossessionTarget != 0)
            {
                if (!dActor.Status.Additions.ContainsKey("A_T_PJoint"))
                {
                    return 0;
                }
                else
                {
                    return -24;
                }
            }
            else
            {
                return -23;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 15000 + level * 5000;
            Actor dActorReal = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            PJointBuff skill = new PJointBuff(args.skill, sActor, dActorReal, life);
            SkillHandler.ApplyAddition(dActorReal, skill);
        }
        public class PJointBuff : DefaultBuff
        {
            Actor sActor;
            public PJointBuff(SagaDB.Skill.Skill skill,Actor sActor, Actor actor, int lifetime)
                : base(skill, actor, "A_T_PJoint", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.sActor = sActor;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                float factor = 0.15f + skill.skill.Level * 0.05f;

                //最大攻擊
                int max_atk1_add = (int)(sActor.Status.max_atk_bs  * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_atk1"))
                    skill.Variable.Remove("A_T_PJoint_max_atk1");
                skill.Variable.Add("A_T_PJoint_max_atk1", max_atk1_add);
                actor.Status.max_atk1_skill += (short)max_atk1_add;

                //最大攻擊
                int max_atk2_add = (int)(sActor.Status.max_atk_bs * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_atk2"))
                    skill.Variable.Remove("A_T_PJoint_max_atk2");
                skill.Variable.Add("A_T_PJoint_max_atk2", max_atk2_add);
                actor.Status.max_atk2_skill += (short)max_atk2_add;

                //最大攻擊
                int max_atk3_add = (int)(sActor.Status.max_atk_bs * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_atk3"))
                    skill.Variable.Remove("A_T_PJoint_max_atk3");
                skill.Variable.Add("A_T_PJoint_max_atk3", max_atk3_add);
                actor.Status.max_atk3_skill += (short)max_atk3_add;

                //最小攻擊
                int min_atk1_add = (int)(sActor.Status.min_atk_bs  * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_atk1"))
                    skill.Variable.Remove("A_T_PJoint_min_atk1");
                skill.Variable.Add("A_T_PJoint_min_atk1", min_atk1_add);
                actor.Status.min_atk1_skill += (short)min_atk1_add;

                //最小攻擊
                int min_atk2_add = (int)(sActor.Status.min_atk_bs * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_atk2"))
                    skill.Variable.Remove("A_T_PJoint_min_atk2");
                skill.Variable.Add("A_T_PJoint_min_atk2", min_atk2_add);
                actor.Status.min_atk2_skill += (short)min_atk2_add;

                //最小攻擊
                int min_atk3_add = (int)(sActor.Status.min_atk_bs * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_atk3"))
                    skill.Variable.Remove("A_T_PJoint_min_atk3");
                skill.Variable.Add("A_T_PJoint_min_atk3", min_atk3_add);
                actor.Status.min_atk3_skill += (short)min_atk3_add;

                //最小魔攻
                int min_matk_add = (int)(sActor.Status.min_matk_bs * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_matk"))
                    skill.Variable.Remove("A_T_PJoint_min_matk");
                skill.Variable.Add("A_T_PJoint_min_matk", min_matk_add);
                actor.Status.min_matk_skill += (short)min_matk_add;

                //最大魔攻
                int max_matk_add = (int)(sActor.Status.max_matk_bs * factor);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_matk"))
                    skill.Variable.Remove("A_T_PJoint_max_matk");
                skill.Variable.Add("A_T_PJoint_max_matk", max_matk_add);
                actor.Status.max_matk_skill += (short)max_matk_add;
                 
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                //最大攻擊
                actor.Status.max_atk1_skill -= (short)skill.Variable["A_T_PJoint_max_atk1"];

                //最大攻擊
                actor.Status.max_atk2_skill -= (short)skill.Variable["A_T_PJoint_max_atk2"];

                //最大攻擊
                actor.Status.max_atk3_skill -= (short)skill.Variable["A_T_PJoint_max_atk3"];

                //最小攻擊
                actor.Status.min_atk1_skill -= (short)skill.Variable["A_T_PJoint_min_atk1"];

                //最小攻擊
                actor.Status.min_atk2_skill -= (short)skill.Variable["A_T_PJoint_min_atk2"];

                //最小攻擊
                actor.Status.min_atk3_skill -= (short)skill.Variable["A_T_PJoint_min_atk3"];

                //最小魔攻
                actor.Status.min_matk_skill -= (short)skill.Variable["A_T_PJoint_min_matk"];

                //最小魔攻
                actor.Status.max_matk_skill -= (short)skill.Variable["A_T_PJoint_max_matk"];
            }
        }
       
        #endregion
    }
}
