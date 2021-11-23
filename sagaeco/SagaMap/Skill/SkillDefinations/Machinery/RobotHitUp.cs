
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 提升機器人的命中率（ロボット命中力上昇）
    /// </summary>
    public class RobotHitUp : ISkill
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
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "RobotHitUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;

            //近命中
            int hit_melee_add = (int)(actor.Status.hit_melee_bs * (0.08f + 0.02f * level));
            if (skill.Variable.ContainsKey("RobotHitUp_hit_melee"))
                skill.Variable.Remove("RobotHitUp_hit_melee");
            skill.Variable.Add("RobotHitUp_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;

            //遠命中
            int hit_ranged_add = (int)(actor.Status.hit_ranged_bs * (0.08f + 0.02f * level));
            if (skill.Variable.ContainsKey("RobotHitUp_hit_ranged"))
                skill.Variable.Remove("RobotHitUp_hit_ranged");
            skill.Variable.Add("RobotHitUp_hit_ranged", hit_ranged_add);
            actor.Status.hit_ranged_skill += (short)hit_ranged_add;
      
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["RobotHitUp_hit_melee"];

            //遠命中
            actor.Status.hit_ranged_skill -= (short)skill.Variable["RobotHitUp_hit_ranged"];
              
        }
        #endregion
    }
}

