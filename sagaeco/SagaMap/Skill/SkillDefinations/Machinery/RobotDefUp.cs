
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 提升機器人的防禦力（ロボット防御力上昇）
    /// </summary>
    public class RobotDefUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet != null)
            {
                if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
                {
                    active = true;
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "RobotDefUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            //右防禦
            int def_add_add = (int)(actor.Status.def_add_bs * (0.08f + 0.02f * level));
            if (skill.Variable.ContainsKey("RobotDefUp_def_add"))
                skill.Variable.Remove("RobotDefUp_def_add");
            skill.Variable.Add("RobotDefUp_def_add", def_add_add);
            actor.Status.def_add_skill += (short)def_add_add;

            //右魔防
            int mdef_add_add = (int)(actor.Status.mdef_add_bs * (0.08f + 0.02f * level));
            if (skill.Variable.ContainsKey("RobotDefUp_mdef_add"))
                skill.Variable.Remove("RobotDefUp_mdef_add");
            skill.Variable.Add("RobotDefUp_mdef_add", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
 
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["RobotDefUp_def_add"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["RobotDefUp_mdef_add"];
  
        }
        #endregion
    }
}

