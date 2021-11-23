
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 結束旋律（オーバーチューン）
    /// </summary>
    public class RobotOverTune : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet == null)
            {
                return -53;//需回傳"需裝備寵物"
            }
            if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            {
                return 0;
            }
            return -53;//需回傳"需裝備寵物"
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000 + 20000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "RobotOverTune", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_ori  * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_atk1"))
                skill.Variable.Remove("RobotOverTune_max_atk1");
            skill.Variable.Add("RobotOverTune_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_ori * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_atk2"))
                skill.Variable.Remove("RobotOverTune_max_atk2");
            skill.Variable.Add("RobotOverTune_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_ori * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_atk3"))
                skill.Variable.Remove("RobotOverTune_max_atk3");
            skill.Variable.Add("RobotOverTune_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_ori  * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_atk1"))
                skill.Variable.Remove("RobotOverTune_min_atk1");
            skill.Variable.Add("RobotOverTune_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_ori * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_atk2"))
                skill.Variable.Remove("RobotOverTune_min_atk2");
            skill.Variable.Add("RobotOverTune_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_ori * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_atk3"))
                skill.Variable.Remove("RobotOverTune_min_atk3");
            skill.Variable.Add("RobotOverTune_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //最大魔攻
            int max_matk_add = (int)(actor.Status.max_matk * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_matk"))
                skill.Variable.Remove("RobotOverTune_max_matk");
            skill.Variable.Add("RobotOverTune_max_matk", max_matk_add);
            actor.Status.max_matk_skill += (short)max_matk_add;

            //最小魔攻
            int min_matk_add = (int)(actor.Status.min_matk * (0.05f + 0.1f * level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_matk"))
                skill.Variable.Remove("RobotOverTune_min_matk");
            skill.Variable.Add("RobotOverTune_min_matk", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["RobotOverTune_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["RobotOverTune_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["RobotOverTune_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["RobotOverTune_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["RobotOverTune_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["RobotOverTune_min_atk3"];

            //最大魔攻
            actor.Status.max_matk_skill -= (short)skill.Variable["RobotOverTune_max_matk"];

            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["RobotOverTune_min_matk"];
          
        }
        #endregion
    }
}
